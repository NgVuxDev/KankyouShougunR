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
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Const;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.DTO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using Shougun.Function.ShougunCSCommon.Utility;
using AuthManager = r_framework.Authority.Manager;
using DBAccessor = Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Accessor.DBAccessor;
using Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.APP;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.DTO;

namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Logic
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
        public UIHeaderForm headerForm;

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

        private GcCustomMoveToNextContorol_Cust cust_Next;
        private GcCustomMoveToPreviousContorol_Cust cust_Previous;

        public Dictionary<string, TabGoDto> map = new Dictionary<string, TabGoDto>();

        internal string csvPath = string.Empty;

        public string[] columnUkeire = {ConstCls.COLUMN_CHECK,
                                        ConstCls.COLUMN_HINMEI_CD,
                                        ConstCls.COLUMN_HINMEI_NAME,
                                        ConstCls.COLUMN_SUURYOU,
                                        ConstCls.COLUMN_UNIT_CD,
                                        ConstCls.COLUMN_NEW_TANKA,
                                        ConstCls.COLUMN_NEW_KINGAKU,
                                        ConstCls.COLUMN_MEISAI_BIKOU};

        public string[] columnShukka = {ConstCls.COLUMN_CHECK,
                                        ConstCls.COLUMN_HINMEI_CD,
                                        ConstCls.COLUMN_HINMEI_NAME,
                                        ConstCls.COLUMN_SUURYOU,
                                        ConstCls.COLUMN_UNIT_CD,
                                        ConstCls.COLUMN_NEW_TANKA,
                                        ConstCls.COLUMN_NEW_KINGAKU,
                                        ConstCls.COLUMN_MEISAI_BIKOU};

        public string[] columnUrsh = {ConstCls.COLUMN_CHECK,
                                      ConstCls.COLUMN_HINMEI_CD,
                                      ConstCls.COLUMN_HINMEI_NAME,
                                      ConstCls.COLUMN_SUURYOU,
                                      ConstCls.COLUMN_UNIT_CD,
                                      ConstCls.COLUMN_NEW_TANKA,
                                      ConstCls.COLUMN_NEW_KINGAKU,
                                      ConstCls.COLUMN_MEISAI_BIKOU};
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
            this.form.txtDenshuKbn.TextChanged -= this.form.txtDenshuKbn_TextChanged;

            this.headerForm.cbx_Hinmei.CheckedChanged -= this.cbx_Hinmei_CheckedChanged;
            this.headerForm.cbx_MeisaiBikou.CheckedChanged -= this.cbx_MeisaiBikou_CheckedChanged;
            this.headerForm.cbx_Suuryou.CheckedChanged -= this.cbx_Suuryou_CheckedChanged;
            this.headerForm.cbx_Tanka.CheckedChanged -= this.cbx_Tanka_CheckedChanged;
            this.headerForm.cbx_Unit.CheckedChanged -= this.cbx_Unit_CheckedChanged;

            #region 明細グリッド

            this.form.mrDetail.CellEnter -= this.form.mrDetail_CellEnter;
            this.form.mrDetail.CellValidated -= this.form.mrDetail_CellValidated;
            this.form.mrDetail.CellValidating -= this.form.mrDetail_CellValidating;
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
            this.form.txtDenshuKbn.TextChanged += this.form.txtDenshuKbn_TextChanged;
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Shift | Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Unregister(Keys.Shift | Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Next, Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Next, Keys.Tab);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Previous, Keys.Shift | Keys.Enter);
            this.form.mrDetail.ShortcutKeyManager.Register(cust_Previous, Keys.Shift | Keys.Tab);

            this.headerForm.cbx_Hinmei.CheckedChanged += this.cbx_Hinmei_CheckedChanged;
            this.headerForm.cbx_MeisaiBikou.CheckedChanged += this.cbx_MeisaiBikou_CheckedChanged;
            this.headerForm.cbx_Suuryou.CheckedChanged += this.cbx_Suuryou_CheckedChanged;
            this.headerForm.cbx_Tanka.CheckedChanged += this.cbx_Tanka_CheckedChanged;
            this.headerForm.cbx_Unit.CheckedChanged += this.cbx_Unit_CheckedChanged;

            #region 明細グリッド

            this.form.mrDetail.CellEnter += this.form.mrDetail_CellEnter;
            this.form.mrDetail.CellValidated += this.form.mrDetail_CellValidated;
            this.form.mrDetail.CellValidating += this.form.mrDetail_CellValidating;
            this.form.mrDetail.CellClick += this.form.mrDetail_CellClick;

            #endregion

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region へーダーイベント
        public void cbx_Hinmei_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.mrDetail.Rows.Count == 0)
            {
                return;
            }
            foreach (Row row in this.form.mrDetail.Rows)
            {
                row.Cells[ConstCls.COLUMN_HINMEI_CD].ReadOnly = !this.headerForm.cbx_Hinmei.Checked;
                row.Cells[ConstCls.COLUMN_HINMEI_CD].UpdateBackColor(row.Cells[ConstCls.COLUMN_HINMEI_CD].Selected);
                row.Cells[ConstCls.COLUMN_HINMEI_NAME].ReadOnly = !this.headerForm.cbx_Hinmei.Checked;
                row.Cells[ConstCls.COLUMN_HINMEI_NAME].UpdateBackColor(row.Cells[ConstCls.COLUMN_HINMEI_NAME].Selected);
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        int rowIndex = this.GetListIndex(1, row.Index);
                        int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = "";
                        T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = tud.HINMEI_CD;
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tud.HINMEI_NAME;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tud.DENPYOU_KBN_CD.Value;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tud.DENPYOU_KBN_CD.Value.ToString());
                        break;
                    case "2":
                        rowIndex = this.GetListIndex(2, row.Index);
                        detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = "";
                        T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = tsd.HINMEI_CD;
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tsd.HINMEI_NAME;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tsd.DENPYOU_KBN_CD.Value;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tsd.DENPYOU_KBN_CD.Value.ToString());
                        break;
                    case "3":
                        rowIndex = this.GetListIndex(3, row.Index);
                        detailIndex = row.Index - this.tusResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = "";
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = "";
                        T_UR_SH_DETAIL tusd = this.tusResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_HINMEI_CD].Value = tusd.HINMEI_CD;
                        row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tusd.HINMEI_NAME;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tusd.DENPYOU_KBN_CD.Value;
                        row.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tusd.DENPYOU_KBN_CD.Value.ToString());
                        break;
                }
            }
            this.MapInit(this.dto.denshuKbnCd);
            LogUtility.DebugMethodEnd();
        }

        public void cbx_MeisaiBikou_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.mrDetail.Rows.Count == 0)
            {
                return;
            }
            foreach (Row row in this.form.mrDetail.Rows)
            {
                row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].ReadOnly = !this.headerForm.cbx_MeisaiBikou.Checked;
                row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].UpdateBackColor(row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Selected);
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        int rowIndex = this.GetListIndex(1, row.Index);
                        int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = "";
                        T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tud.MEISAI_BIKOU;
                        break;
                    case "2":
                        rowIndex = this.GetListIndex(2, row.Index);
                        detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = "";
                        T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tsd.MEISAI_BIKOU;
                        break;
                    case "3":
                        rowIndex = this.GetListIndex(3, row.Index);
                        detailIndex = row.Index - this.tusResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = "";
                        T_UR_SH_DETAIL tusd = this.tusResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tusd.MEISAI_BIKOU;
                        break;
                }
            }
            this.MapInit(this.dto.denshuKbnCd);
            LogUtility.DebugMethodEnd();
        }

        public void cbx_Suuryou_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.mrDetail.Rows.Count == 0)
            {
                return;
            }
            foreach (Row row in this.form.mrDetail.Rows)
            {
                row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = !this.headerForm.cbx_Suuryou.Checked;
                if (this.headerForm.cbx_Suuryou.Checked)
                {
                    switch (this.dto.denshuKbnCd)
                    {
                        case "1":
                            int rowIndex = this.GetListIndex(1, row.Index);
                            int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                            T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                            if (!tud.STACK_JYUURYOU.IsNull && !tud.EMPTY_JYUURYOU.IsNull && (tud.UNIT_CD.Value == 3 || tud.UNIT_CD.Value == 1))
                            {
                                row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                            }
                            break;
                        case "2":
                            rowIndex = this.GetListIndex(2, row.Index);
                            detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                            T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                            if (!tsd.STACK_JYUURYOU.IsNull && !tsd.EMPTY_JYUURYOU.IsNull && (tsd.UNIT_CD.Value == 3 || tsd.UNIT_CD.Value == 1))
                            {
                                row.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                            }
                            break;
                        case "3":
                            break;
                    }
                }
                row.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(row.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        int rowIndex = this.GetListIndex(1, row.Index);
                        int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = "";
                        T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = tud.SUURYOU.IsNull ? "" : tud.SUURYOU.Value.ToString();
                        break;
                    case "2":
                        rowIndex = this.GetListIndex(2, row.Index);
                        detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = "";
                        T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = tsd.SUURYOU.IsNull ? "" : tsd.SUURYOU.Value.ToString();
                        break;
                    case "3":
                        rowIndex = this.GetListIndex(3, row.Index);
                        detailIndex = row.Index - this.tusResult[rowIndex].rowIndex;
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = "";
                        T_UR_SH_DETAIL tusd = this.tusResult[rowIndex].detailList[detailIndex];
                        row.Cells[ConstCls.COLUMN_SUURYOU].Value = tusd.SUURYOU.IsNull ? "" : tusd.SUURYOU.Value.ToString();
                        break;
                }
            }
            this.MapInit(this.dto.denshuKbnCd);
            LogUtility.DebugMethodEnd();
        }

        public void cbx_Tanka_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.mrDetail.Rows.Count == 0)
            {
                return;
            }
            foreach (Row row in this.form.mrDetail.Rows)
            {
                if (this.headerForm.cbx_Tanka.Checked)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                    {
                        row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                    {
                        row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                    }
                    else
                    {
                        row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                        row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                    }
                    row.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                    row.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                }
                else
                {
                    row.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                    row.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                    row.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                    row.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);

                    switch (this.dto.denshuKbnCd)
                    {
                        case "1":
                            int rowIndex = this.GetListIndex(1, row.Index);
                            int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = "";
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = "";
                            T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                            if (!tud.TANKA.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = tud.TANKA.IsNull ? "" : tud.TANKA.Value.ToString();
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tud.KINGAKU.IsNull ? 0 : tud.KINGAKU.Value) + (tud.HINMEI_KINGAKU.IsNull ? 0 : tud.HINMEI_KINGAKU.Value);
                            break;
                        case "2":
                            rowIndex = this.GetListIndex(2, row.Index);
                            detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = "";
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = "";
                            T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                            if (!tsd.TANKA.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = tsd.TANKA.IsNull ? "" : tsd.TANKA.Value.ToString();
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tsd.KINGAKU.IsNull ? 0 : tsd.KINGAKU.Value) + (tsd.HINMEI_KINGAKU.IsNull ? 0 : tsd.HINMEI_KINGAKU.Value);
                            break;
                        case "3":
                            rowIndex = this.GetListIndex(3, row.Index);
                            detailIndex = row.Index - this.tusResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = "";
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = "";
                            T_UR_SH_DETAIL tusd = this.tusResult[rowIndex].detailList[detailIndex];
                            if (!tusd.TANKA.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_NEW_TANKA].Value = tusd.TANKA.IsNull ? "" : tusd.TANKA.Value.ToString();
                            }
                            row.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tusd.KINGAKU.IsNull ? 0 : tusd.KINGAKU.Value) + (tusd.HINMEI_KINGAKU.IsNull ? 0 : tusd.HINMEI_KINGAKU.Value);
                            break;
                    }
                }
            }
            this.MapInit(this.dto.denshuKbnCd);
            LogUtility.DebugMethodEnd();
        }

        public void cbx_Unit_CheckedChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.form.mrDetail.Rows.Count == 0)
            {
                return;
            }
            foreach (Row row in this.form.mrDetail.Rows)
            {
                row.Cells[ConstCls.COLUMN_UNIT_CD].ReadOnly = !this.headerForm.cbx_Unit.Checked;
                row.Cells[ConstCls.COLUMN_UNIT_CD].UpdateBackColor(row.Cells[ConstCls.COLUMN_UNIT_CD].Selected);
                if (!this.headerForm.cbx_Unit.Checked)
                {
                    switch (this.dto.denshuKbnCd)
                    {
                        case "1":
                            int rowIndex = this.GetListIndex(1, row.Index);
                            int detailIndex = row.Index - this.tuResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_UNIT_CD].Value = "";
                            row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = "";
                            T_UKEIRE_DETAIL tud = this.tuResult[rowIndex].detailList[detailIndex];
                            if (!tud.UNIT_CD.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tud.UNIT_CD.Value.ToString());
                                row.Cells[ConstCls.COLUMN_UNIT_CD].Value = tud.UNIT_CD.Value;
                            }
                            break;
                        case "2":
                            rowIndex = this.GetListIndex(2, row.Index);
                            detailIndex = row.Index - this.tsResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_UNIT_CD].Value = "";
                            row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = "";
                            T_SHUKKA_DETAIL tsd = this.tsResult[rowIndex].detailList[detailIndex];
                            if (!tsd.UNIT_CD.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tsd.UNIT_CD.Value.ToString());
                                row.Cells[ConstCls.COLUMN_UNIT_CD].Value = tsd.UNIT_CD.Value;
                            }
                            break;
                        case "3":
                            rowIndex = this.GetListIndex(3, row.Index);
                            detailIndex = row.Index - this.tusResult[rowIndex].rowIndex;
                            row.Cells[ConstCls.COLUMN_UNIT_CD].Value = "";
                            row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = "";
                            T_UR_SH_DETAIL tusd = this.tusResult[rowIndex].detailList[detailIndex];
                            if (!tusd.UNIT_CD.IsNull)
                            {
                                row.Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tusd.UNIT_CD.Value.ToString());
                                row.Cells[ConstCls.COLUMN_UNIT_CD].Value = tusd.UNIT_CD.Value;
                            }
                            break;
                    }
                }
            }
            this.MapInit(this.dto.denshuKbnCd);
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
                this.headerForm.ReadDataNumber.Text = "0";

                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                // ヘッダー部
                const string CSV_OUTPUT_KBN = "更新CSV出力";
                const string CSV_PATH = "CSV保管先";
                this.headerForm.txtCsvOutputKbn.Text = this.GetUserProfileValue(userProfile, CSV_OUTPUT_KBN);
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
                this.form.HINMEI_CD.Text = string.Empty;
                this.form.HINMEI_NAME_RYAKU.Text = string.Empty;
                this.form.SHURUI_CD.Text = string.Empty;
                this.form.SHURUI_NAME_RYAKU.Text = string.Empty;
                this.form.BUNRUI_CD.Text = string.Empty;
                this.form.BUNRUI_NAME_RYAKU.Text = string.Empty;
                this.form.UNIT_CD.Text = string.Empty;
                this.form.UNIT_NAME_RYAKU.Text = string.Empty;
                // 伝票区分
                this.form.DENPYOU_KBN_CD.Text = "1";
                this.form.DENPYOU_KBN_NAME.Text = "売上";
                // 取引区分
                this.form.TORIHIKI_KBN_CD.Text = "3";
                this.form.TORIHIKI_KBN_NAME.Text = "全て";

                // 入力エラークリア
                this.form.TORIHIKISAKI_CD.IsInputErrorOccured = false;
                this.form.GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.GENBA_CD.IsInputErrorOccured = false;
                this.form.UPN_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIZUMI_GENBA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
                this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
                this.form.HINMEI_CD.IsInputErrorOccured = false;
                this.form.SHURUI_CD.IsInputErrorOccured = false;
                this.form.BUNRUI_CD.IsInputErrorOccured = false;
                this.form.UNIT_CD.IsInputErrorOccured = false;

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
                int rowIndex = 0;
                int unDisPlayRows = 0;
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        #region 受入
                        for (int i = 0; i < this.tuResult.Count; i++)
                        {
                            T_UKEIRE_ENTRY tue = this.tuResult[i].entry;
                            this.tuResult[i].seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(tue.TORIHIKISAKI_CD);
                            this.tuResult[i].shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(tue.TORIHIKISAKI_CD);
                            foreach (T_UKEIRE_DETAIL tud in this.tuResult[i].detailList)
                            {
                                this.form.mrDetail.Rows.Add();
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_CHECK].Value = false;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = this.AddYobi(tue.DENPYOU_DATE.Value);
                                if (!tue.URIAGE_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tue.URIAGE_DATE.Value;
                                }
                                if (!tue.SHIHARAI_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tue.SHIHARAI_DATE.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tue.TORIHIKISAKI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tue.TORIHIKISAKI_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tue.GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tue.GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_CD].Value = tue.GENBA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tue.GENBA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tue.UNPAN_GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tue.UNPAN_GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = tue.NIOROSHI_GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = tue.NIOROSHI_GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = tue.NIOROSHI_GENBA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = tue.NIOROSHI_GENBA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tue.UKEIRE_NUMBER.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value = tud.HINMEI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tud.HINMEI_NAME;
                                if (!tud.SUURYOU.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value = tud.SUURYOU.Value;
                                }
                                if (!tud.UNIT_CD.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tud.UNIT_CD.Value.ToString());
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value = tud.UNIT_CD.Value;
                                }
                                if (!tud.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TANKA].Value = tud.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KINGAKU].Value = (tud.KINGAKU.IsNull ? 0 : tud.KINGAKU.Value) + (tud.HINMEI_KINGAKU.IsNull ? 0 : tud.HINMEI_KINGAKU.Value);
                                if (!tud.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value = tud.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tud.KINGAKU.IsNull ? 0 : tud.KINGAKU.Value) + (tud.HINMEI_KINGAKU.IsNull ? 0 : tud.HINMEI_KINGAKU.Value);
                                short torihikiKbnCd = tud.DENPYOU_KBN_CD.Value == 1 ? this.tuResult[i].entry.URIAGE_TORIHIKI_KBN_CD.Value : this.tuResult[i].entry.SHIHARAI_TORIHIKI_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKI_KBN].Value = this.accessor.GetTorihikiKbnNameFast(torihikiKbnCd);
                                short zeikeisanKbnCd = tud.DENPYOU_KBN_CD.Value == 1 ? (tue.URIAGE_ZEI_KEISAN_KBN_CD.IsNull ? (short)-1 : tue.URIAGE_ZEI_KEISAN_KBN_CD.Value) : (tue.SHIHARAI_ZEI_KEISAN_KBN_CD.IsNull ? (short)-1 : tue.SHIHARAI_ZEI_KEISAN_KBN_CD.Value);
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_ZEIKEISAN_KBN].Value = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tue.KYOTEN_CD.IsNull ? "" : tue.KYOTEN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENSHU_KBN].Value = "受入";
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tud.DENPYOU_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tud.DENPYOU_KBN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tud.MEISAI_BIKOU;

                                string hinmeiCd = tud.HINMEI_CD;
                                string shuruiCd = "";
                                string bunruiCd = "";
                                short unitCd = -1;
                                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(tud.HINMEI_CD);
                                if (hinmei != null)
                                {
                                    shuruiCd = hinmei.SHURUI_CD;
                                }
                                if (hinmei != null)
                                {
                                    bunruiCd = hinmei.BUNRUI_CD;
                                }
                                if (!tud.UNIT_CD.IsNull)
                                {
                                    unitCd = tud.UNIT_CD.Value;
                                }
                                if (this.headerForm.cbx_Suuryou.Checked)
                                {
                                    if ((Convert.ToString(this.form.mrDetail.Rows[rowIndex][ConstCls.COLUMN_UNIT_CD].Value) == "1"
                                        || Convert.ToString(this.form.mrDetail.Rows[rowIndex][ConstCls.COLUMN_UNIT_CD].Value) == "3")
                                        && (!tud.STACK_JYUURYOU.IsNull && !tud.EMPTY_JYUURYOU.IsNull))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                    }
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Selected);
                                }
                                if (this.headerForm.cbx_Tanka.Checked)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                                    }
                                    else if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    else
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                                }
                                if ((!string.IsNullOrEmpty(this.form.HINMEI_CD.Text) && hinmeiCd != this.form.HINMEI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.SHURUI_CD.Text) && shuruiCd != this.form.SHURUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.BUNRUI_CD.Text) && bunruiCd != this.form.BUNRUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.UNIT_CD.Text) && unitCd != Convert.ToInt16(this.form.UNIT_CD.Text)))
                                {
                                    this.form.mrDetail.Rows[rowIndex].Visible = false;
                                    unDisPlayRows += 1;
                                }
                                rowIndex++;
                            }
                        }
                        #endregion
                        break;

                    case "2":
                        #region 出荷
                        for (int i = 0; i < this.tsResult.Count; i++)
                        {
                            T_SHUKKA_ENTRY tse = this.tsResult[i].entry;
                            this.tsResult[i].seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(tse.TORIHIKISAKI_CD);
                            this.tsResult[i].shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(tse.TORIHIKISAKI_CD);
                            foreach (T_SHUKKA_DETAIL tsd in this.tsResult[i].detailList)
                            {
                                this.form.mrDetail.Rows.Add();
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_CHECK].Value = false;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = this.AddYobi(tse.DENPYOU_DATE.Value);
                                if (!tse.URIAGE_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tse.URIAGE_DATE.Value;
                                }
                                if (!tse.SHIHARAI_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tse.SHIHARAI_DATE.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tse.TORIHIKISAKI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tse.TORIHIKISAKI_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tse.GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tse.GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_CD].Value = tse.GENBA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tse.GENBA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tse.UNPAN_GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tse.UNPAN_GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = string.Empty;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = string.Empty;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = string.Empty;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = string.Empty;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tse.SHUKKA_NUMBER.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value = tsd.HINMEI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tsd.HINMEI_NAME;
                                if (!tsd.SUURYOU.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value = tsd.SUURYOU.Value;
                                }
                                if (!tsd.UNIT_CD.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tsd.UNIT_CD.Value.ToString());
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value = tsd.UNIT_CD.Value;
                                }
                                if (!tsd.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TANKA].Value = tsd.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KINGAKU].Value = (tsd.KINGAKU.IsNull ? 0 : tsd.KINGAKU.Value) + (tsd.HINMEI_KINGAKU.IsNull ? 0 : tsd.HINMEI_KINGAKU.Value);
                                if (!tsd.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value = tsd.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tsd.KINGAKU.IsNull ? 0 : tsd.KINGAKU.Value) + (tsd.HINMEI_KINGAKU.IsNull ? 0 : tsd.HINMEI_KINGAKU.Value);
                                short torihikiKbnCd = tsd.DENPYOU_KBN_CD.Value == 1 ? this.tsResult[i].entry.URIAGE_TORIHIKI_KBN_CD.Value : this.tsResult[i].entry.SHIHARAI_TORIHIKI_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKI_KBN].Value = this.accessor.GetTorihikiKbnNameFast(torihikiKbnCd);
                                short zeikeisanKbnCd = tsd.DENPYOU_KBN_CD.Value == 1 ? tse.URIAGE_ZEI_KEISAN_KBN_CD.Value : tse.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_ZEIKEISAN_KBN].Value = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tse.KYOTEN_CD.IsNull ? "" : tse.KYOTEN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENSHU_KBN].Value = "出荷";
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tsd.DENPYOU_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tsd.DENPYOU_KBN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tsd.MEISAI_BIKOU;

                                string hinmeiCd = tsd.HINMEI_CD;
                                string shuruiCd = "";
                                string bunruiCd = "";
                                short unitCd = -1;
                                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(tsd.HINMEI_CD);
                                if (hinmei != null)
                                {
                                    shuruiCd = hinmei.SHURUI_CD;
                                }
                                if (hinmei != null)
                                {
                                    bunruiCd = hinmei.BUNRUI_CD;
                                }
                                if (!tsd.UNIT_CD.IsNull)
                                {
                                    unitCd = tsd.UNIT_CD.Value;
                                }
                                if (this.headerForm.cbx_Suuryou.Checked)
                                {
                                    if ((Convert.ToString(this.form.mrDetail.Rows[rowIndex][ConstCls.COLUMN_UNIT_CD].Value) == "1"
                                        || Convert.ToString(this.form.mrDetail.Rows[rowIndex][ConstCls.COLUMN_UNIT_CD].Value) == "3")
                                        && (!tsd.STACK_JYUURYOU.IsNull && !tsd.EMPTY_JYUURYOU.IsNull))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                    }
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Selected);
                                }
                                if (this.headerForm.cbx_Tanka.Checked)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                                    }
                                    else if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    else
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                                }
                                if ((!string.IsNullOrEmpty(this.form.HINMEI_CD.Text) && hinmeiCd != this.form.HINMEI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.SHURUI_CD.Text) && shuruiCd != this.form.SHURUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.BUNRUI_CD.Text) && bunruiCd != this.form.BUNRUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.UNIT_CD.Text) && unitCd != Convert.ToInt16(this.form.UNIT_CD.Text)))
                                {
                                    this.form.mrDetail.Rows[rowIndex].Visible = false;
                                    unDisPlayRows += 1;
                                }
                                rowIndex++;
                            }
                        }
                        #endregion
                        break;

                    case "3":
                        #region 売上支払
                        for (int i = 0; i < this.tusResult.Count; i++)
                        {
                            T_UR_SH_ENTRY tuse = this.tusResult[i].entry;
                            this.tusResult[i].seikyuu = this.accessor.GetTorihikisakiSeikyuuDataByCode(tuse.TORIHIKISAKI_CD);
                            this.tusResult[i].shiharai = this.accessor.GetTorihikisakiShiharaiDataByCode(tuse.TORIHIKISAKI_CD);
                            foreach (T_UR_SH_DETAIL tusd in this.tusResult[i].detailList)
                            {
                                this.form.mrDetail.Rows.Add();
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_CHECK].Value = false;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_DATE].Value = this.AddYobi(tuse.DENPYOU_DATE.Value);
                                if (!tuse.URIAGE_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_URIAGE_DATE].Value = tuse.URIAGE_DATE.Value;
                                }
                                if (!tuse.SHIHARAI_DATE.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SHIHARAI_DATE].Value = tuse.SHIHARAI_DATE.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value = tuse.TORIHIKISAKI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKISAKI_NAME].Value = tuse.TORIHIKISAKI_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_CD].Value = tuse.GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GYOUSHA_NAME].Value = tuse.GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_CD].Value = tuse.GENBA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_GENBA_NAME].Value = tuse.GENBA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value = tuse.UNPAN_GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_NAME].Value = tuse.UNPAN_GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value = tuse.NIOROSHI_GYOUSHA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_NAME].Value = tuse.NIOROSHI_GYOUSHA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value = tuse.NIOROSHI_GENBA_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NIOROSHI_GENBA_NAME].Value = tuse.NIOROSHI_GENBA_NAME;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_NO].Value = tuse.UR_SH_NUMBER.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value = tusd.HINMEI_CD;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value = tusd.HINMEI_NAME;
                                if (!tusd.SUURYOU.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value = tusd.SUURYOU.Value;
                                }
                                if (!tusd.UNIT_CD.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.accessor.GetUnitNameFast(tusd.UNIT_CD.Value.ToString());
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value = tusd.UNIT_CD.Value;
                                }
                                if (!tusd.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TANKA].Value = tusd.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KINGAKU].Value = (tusd.KINGAKU.IsNull ? 0 : tusd.KINGAKU.Value) + (tusd.HINMEI_KINGAKU.IsNull ? 0 : tusd.HINMEI_KINGAKU.Value);
                                if (!tusd.TANKA.IsNull)
                                {
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value = tusd.TANKA.Value;
                                }
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = (tusd.KINGAKU.IsNull ? 0 : tusd.KINGAKU.Value) + (tusd.HINMEI_KINGAKU.IsNull ? 0 : tusd.HINMEI_KINGAKU.Value);
                                short torihikiKbnCd = tusd.DENPYOU_KBN_CD.Value == 1 ? this.tusResult[i].entry.URIAGE_TORIHIKI_KBN_CD.Value : this.tusResult[i].entry.SHIHARAI_TORIHIKI_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_TORIHIKI_KBN].Value = this.accessor.GetTorihikiKbnNameFast(torihikiKbnCd);
                                short zeikeisanKbnCd = tusd.DENPYOU_KBN_CD.Value == 1 ? tuse.URIAGE_ZEI_KEISAN_KBN_CD.Value : tuse.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_ZEIKEISAN_KBN].Value = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_KYOTEN_NAME].Value = this.accessor.GetKyotenNameFast(tuse.KYOTEN_CD.IsNull ? "" : tuse.KYOTEN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENSHU_KBN].Value = "売上支払";
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = tusd.DENPYOU_KBN_CD.Value;
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(tusd.DENPYOU_KBN_CD.Value.ToString());
                                this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = tusd.MEISAI_BIKOU;

                                string hinmeiCd = tusd.HINMEI_CD;
                                string shuruiCd = "";
                                string bunruiCd = "";
                                short unitCd = -1;
                                M_HINMEI hinmei = this.accessor.GetHinmeiDataByCode(tusd.HINMEI_CD);
                                if (hinmei != null)
                                {
                                    shuruiCd = hinmei.SHURUI_CD;
                                }
                                if (hinmei != null)
                                {
                                    bunruiCd = hinmei.BUNRUI_CD;
                                }
                                if (!tusd.UNIT_CD.IsNull)
                                {
                                    unitCd = tusd.UNIT_CD.Value;
                                }
                                if (this.headerForm.cbx_Tanka.Checked)
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                                    }
                                    else if (!string.IsNullOrEmpty(Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value)))
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    else
                                    {
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                        this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                    }
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                    this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                                }
                                if ((!string.IsNullOrEmpty(this.form.HINMEI_CD.Text) && hinmeiCd != this.form.HINMEI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.SHURUI_CD.Text) && shuruiCd != this.form.SHURUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.BUNRUI_CD.Text) && bunruiCd != this.form.BUNRUI_CD.Text)
                                    || (!string.IsNullOrEmpty(this.form.UNIT_CD.Text) && unitCd != Convert.ToInt16(this.form.UNIT_CD.Text)))
                                {
                                    this.form.mrDetail.Rows[rowIndex].Visible = false;
                                    unDisPlayRows += 1;
                                }
                                rowIndex++;
                            }
                        }
                        #endregion
                        break;
                }
                this.headerForm.ReadDataNumber.Text = (this.form.mrDetail.RowCount - unDisPlayRows).ToString();
                this.MapInit(this.dto.denshuKbnCd);
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
            try
            {
                // 画面モード別処理
                switch (this.dto.denshuKbnCd)
                {
                    case "1":
                        #region 受入
                        this.tuRegist = new List<ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL>>();
                        for (int i = 0; i < this.tuResult.Count; i++)
                        {
                            if (this.tuResult[i].check)
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
                                newDto.seikyuu = dto.seikyuu;
                                newDto.shiharai = dto.shiharai;
                                newDto.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;
                                newDto.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_UKEIRE_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);
                                newDto.entry.CREATE_DATE = dto.entry.CREATE_DATE;
                                newDto.entry.CREATE_PC = dto.entry.CREATE_PC;
                                newDto.entry.CREATE_USER = dto.entry.CREATE_USER;

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

                                // 画面入力値設定
                                for (int j = 0; j < newDto.detailList.Count; j++)
                                {
                                    T_UKEIRE_DETAIL detail = newDto.detailList[j];
                                    detail.SEQ = newDto.entry.SEQ;

                                    int rowIndex = newDto.rowIndex + j;
                                    if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                    if (this.ToNBoolean(cell.Value) != true)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    if (this.headerForm.cbx_Hinmei.Checked)
                                    {
                                        detail.HINMEI_CD = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value);
                                        detail.HINMEI_NAME = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value);
                                        detail.DENPYOU_KBN_CD = Convert.ToInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value);
                                    }
                                    if (this.headerForm.cbx_Suuryou.Checked)
                                    {
                                        decimal? suuryou = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value);
                                        if (suuryou == null)
                                        {
                                            detail.SUURYOU = SqlDecimal.Null;
                                        }
                                        else
                                        {
                                            detail.SUURYOU = suuryou.Value;
                                        }
                                    }
                                    if (this.headerForm.cbx_Unit.Checked)
                                    {
                                        short? tani = this.ToNInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value);
                                        if (tani == null)
                                        {
                                            detail.UNIT_CD = SqlInt16.Null;
                                        }
                                        else
                                        {
                                            detail.UNIT_CD = tani.Value;
                                        }
                                    }
                                    decimal? tanka = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value);
                                    if (tanka == null)
                                    {
                                        detail.TANKA = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.TANKA = tanka.Value;
                                    }
                                    decimal? kingaku = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value);
                                    if (kingaku == null)
                                    {
                                        detail.KINGAKU = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.KINGAKU = kingaku.Value;
                                    }
                                    if (this.headerForm.cbx_MeisaiBikou.Checked)
                                    {
                                        detail.MEISAI_BIKOU = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value);
                                    }
                                }
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    detailHidden.SEQ = newDto.entry.SEQ;
                                    // 明細金額計算用に、画面表示時と同じロジックで金額を設定
                                    detailHidden.KINGAKU = (detailHidden.KINGAKU.IsNull ? 0 : detailHidden.KINGAKU.Value) + (detailHidden.HINMEI_KINGAKU.IsNull ? 0 : detailHidden.HINMEI_KINGAKU.Value);

                                    newDto.detailListHidden.Add(CopyEntity<T_UKEIRE_DETAIL, T_UKEIRE_DETAIL>.Copy(detailHidden));
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
                        for (int i = 0; i < this.tsResult.Count; i++)
                        {
                            if (this.tsResult[i].check)
                            {
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> dto = this.tsResult[i];
                                ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL> newDto = new ResultDto<T_SHUKKA_ENTRY, T_SHUKKA_DETAIL>();
                                newDto.entry = CopyEntity<T_SHUKKA_ENTRY, T_SHUKKA_ENTRY>.Copy(dto.entry);
                                for (int j = 0; j < dto.detailList.Count; j++)
                                {
                                    newDto.detailList.Add(CopyEntity<T_SHUKKA_DETAIL, T_SHUKKA_DETAIL>.Copy(dto.detailList[j]));
                                }
                                foreach (string key in dto.detailZaikoUkeireDetails.Keys)
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
                                foreach (var contena in dto.contenaResults)
                                {
                                    newDto.contenaResults.Add(CopyEntity<T_CONTENA_RESULT, T_CONTENA_RESULT>.Copy(contena));
                                }

                                newDto.denpyouNo = dto.denpyouNo;
                                newDto.rowIndex = dto.rowIndex;
                                newDto.seikyuu = dto.seikyuu;
                                newDto.shiharai = dto.shiharai;
                                newDto.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;
                                newDto.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_SHUKKA_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);
                                newDto.entry.CREATE_DATE = dto.entry.CREATE_DATE;
                                newDto.entry.CREATE_PC = dto.entry.CREATE_PC;
                                newDto.entry.CREATE_USER = dto.entry.CREATE_USER;

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
                                        var dataBinderZaikoUkeireDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
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
                                    entity.DENSHU_KBN_CD = SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
                                    // 自動設定
                                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(entity);
                                    dataBinderContenaResult.SetSystemProperty(entity, false);
                                    entity.CREATE_DATE = dto.entry.CREATE_DATE;
                                    entity.CREATE_PC = dto.entry.CREATE_PC;
                                    entity.CREATE_USER = dto.entry.CREATE_USER;
                                }
                                foreach (List<T_ZAIKO_SHUKKA_DETAIL> entityList in newDto.detailZaikoShukkaDetails.Values)
                                {
                                    foreach (T_ZAIKO_SHUKKA_DETAIL entity in entityList)
                                    {
                                        entity.SEQ = newDto.entry.SEQ;
                                        // 自動設定
                                        var dataBinderZaikoUkeireDetail = new DataBinderLogic<T_ZAIKO_SHUKKA_DETAIL>(entity);
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

                                // 画面入力値設定
                                for (int j = 0; j < newDto.detailList.Count; j++)
                                {
                                    T_SHUKKA_DETAIL detail = newDto.detailList[j];
                                    detail.SEQ = newDto.entry.SEQ;

                                    int rowIndex = newDto.rowIndex + j;
                                    if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                    if (this.ToNBoolean(cell.Value) != true)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    if (this.headerForm.cbx_Hinmei.Checked)
                                    {
                                        detail.HINMEI_CD = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value);
                                        detail.HINMEI_NAME = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value);
                                        detail.DENPYOU_KBN_CD = Convert.ToInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value);
                                    }
                                    if (this.headerForm.cbx_Suuryou.Checked)
                                    {
                                        decimal? suuryou = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value);
                                        if (suuryou == null)
                                        {
                                            detail.SUURYOU = SqlDecimal.Null;
                                        }
                                        else
                                        {
                                            detail.SUURYOU = suuryou.Value;
                                        }
                                    }
                                    if (this.headerForm.cbx_Unit.Checked)
                                    {
                                        short? tani = this.ToNInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value);
                                        if (tani == null)
                                        {
                                            detail.UNIT_CD = SqlInt16.Null;
                                        }
                                        else
                                        {
                                            detail.UNIT_CD = tani.Value;
                                        }
                                    }
                                    decimal? tanka = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value);
                                    if (tanka == null)
                                    {
                                        detail.TANKA = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.TANKA = tanka.Value;
                                    }
                                    decimal? kingaku = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value);
                                    if (kingaku == null)
                                    {
                                        detail.KINGAKU = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.KINGAKU = kingaku.Value;
                                    }
                                    if (this.headerForm.cbx_MeisaiBikou.Checked)
                                    {
                                        detail.MEISAI_BIKOU = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value);
                                    }
                                }
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    detailHidden.SEQ = newDto.entry.SEQ;
                                    // 明細金額計算用に、画面表示時と同じロジックで金額を設定
                                    detailHidden.KINGAKU = (detailHidden.KINGAKU.IsNull ? 0 : detailHidden.KINGAKU.Value) + (detailHidden.HINMEI_KINGAKU.IsNull ? 0 : detailHidden.HINMEI_KINGAKU.Value);

                                    newDto.detailListHidden.Add(CopyEntity<T_SHUKKA_DETAIL, T_SHUKKA_DETAIL>.Copy(detailHidden));
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
                        for (int i = 0; i < this.tusResult.Count; i++)
                        {
                            if (this.tusResult[i].check)
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
                                newDto.seikyuu = dto.seikyuu;
                                newDto.shiharai = dto.shiharai;
                                newDto.hiRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;
                                newDto.nenRenbanRegistKbn = Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.NONE;

                                if (!this.RegistCheck(newDto, dto, i))
                                {
                                    return false;
                                }

                                // 画面入力値以外の内容設定
                                newDto.entry.SEQ = newDto.entry.SEQ.Value + 1;
                                newDto.entry.DELETE_FLG = false;
                                // 更新前伝票は論理削除
                                dto.entry.DELETE_FLG = true;

                                var dataBinderEntryResult = new DataBinderLogic<T_UR_SH_ENTRY>(newDto.entry);
                                dataBinderEntryResult.SetSystemProperty(newDto.entry, false);
                                newDto.entry.CREATE_DATE = dto.entry.CREATE_DATE;
                                newDto.entry.CREATE_PC = dto.entry.CREATE_PC;
                                newDto.entry.CREATE_USER = dto.entry.CREATE_USER;

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

                                // 画面入力値設定
                                for (int j = 0; j < newDto.detailList.Count; j++)
                                {
                                    T_UR_SH_DETAIL detail = newDto.detailList[j];
                                    detail.SEQ = newDto.entry.SEQ;

                                    int rowIndex = newDto.rowIndex + j;
                                    if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                    if (this.ToNBoolean(cell.Value) != true)
                                    {
                                        detail.KINGAKU = (detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value) + (detail.HINMEI_KINGAKU.IsNull ? 0 : detail.HINMEI_KINGAKU.Value);
                                        continue;
                                    }
                                    if (this.headerForm.cbx_Hinmei.Checked)
                                    {
                                        detail.HINMEI_CD = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_CD].Value);
                                        detail.HINMEI_NAME = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_HINMEI_NAME].Value);
                                        detail.DENPYOU_KBN_CD = Convert.ToInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value);
                                    }
                                    if (this.headerForm.cbx_Suuryou.Checked)
                                    {
                                        decimal? suuryou = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_SUURYOU].Value);
                                        if (suuryou == null)
                                        {
                                            detail.SUURYOU = SqlDecimal.Null;
                                        }
                                        else
                                        {
                                            detail.SUURYOU = suuryou.Value;
                                        }
                                    }
                                    if (this.headerForm.cbx_Unit.Checked)
                                    {
                                        short? tani = this.ToNInt16(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_UNIT_CD].Value);
                                        if (tani == null)
                                        {
                                            detail.UNIT_CD = SqlInt16.Null;
                                        }
                                        else
                                        {
                                            detail.UNIT_CD = tani.Value;
                                        }
                                    }
                                    decimal? tanka = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_TANKA].Value);
                                    if (tanka == null)
                                    {
                                        detail.TANKA = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.TANKA = tanka.Value;
                                    }
                                    decimal? kingaku = this.ToNDecimal(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_NEW_KINGAKU].Value);
                                    if (kingaku == null)
                                    {
                                        detail.KINGAKU = SqlDecimal.Null;
                                    }
                                    else
                                    {
                                        detail.KINGAKU = kingaku.Value;
                                    }
                                    if (this.headerForm.cbx_MeisaiBikou.Checked)
                                    {
                                        detail.MEISAI_BIKOU = Convert.ToString(this.form.mrDetail.Rows[rowIndex].Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value);
                                    }
                                }
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    detailHidden.SEQ = newDto.entry.SEQ;
                                    // 明細金額計算用に、画面表示時と同じロジックで金額を設定
                                    detailHidden.KINGAKU = (detailHidden.KINGAKU.IsNull ? 0 : detailHidden.KINGAKU.Value) + (detailHidden.HINMEI_KINGAKU.IsNull ? 0 : detailHidden.HINMEI_KINGAKU.Value);

                                    newDto.detailListHidden.Add(CopyEntity<T_UR_SH_DETAIL, T_UR_SH_DETAIL>.Copy(detailHidden));
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
            this.headerForm.ReadDataNumber.Text = "0";
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
                if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
                {
                    dto.hinmeiCd = this.form.HINMEI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.SHURUI_CD.Text))
                {
                    dto.shuruiCd = this.form.SHURUI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.BUNRUI_CD.Text))
                {
                    dto.bunruiCd = this.form.BUNRUI_CD.Text;
                }
                if (!string.IsNullOrEmpty(this.form.UNIT_CD.Text))
                {
                    dto.unitCd = Convert.ToInt16(this.form.UNIT_CD.Text);
                }
                if (!string.IsNullOrEmpty(this.form.DENPYOU_KBN_CD.Text))
                {
                    dto.denpyouKbnCd = Convert.ToInt16(this.form.DENPYOU_KBN_CD.Text);
                }
                if (!string.IsNullOrEmpty(this.form.TORIHIKI_KBN_CD.Text)
                    && this.form.TORIHIKI_KBN_CD.Text != "3")
                {
                    if (this.form.TORIHIKI_KBN_CD.Text == "1")
                    {
                        dto.torihikiKbnCd = 2;
                    }
                    else
                    {
                        dto.torihikiKbnCd = 1;
                    }
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
                                    if (!dto.denpyouKbnCd.IsNull)
                                    {
                                        if (detail.DENPYOU_KBN_CD == dto.denpyouKbnCd)
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else if (!dto.torihikiKbnCd.IsNull)
                                    {
                                        if((detail.DENPYOU_KBN_CD == 1 && e.URIAGE_TORIHIKI_KBN_CD == dto.torihikiKbnCd)
                                           || (detail.DENPYOU_KBN_CD == 2 && e.SHIHARAI_TORIHIKI_KBN_CD == dto.torihikiKbnCd))
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else
                                    {
                                        result.detailList.Add(detail);
                                    }

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
                                i += result.detailList.Count;
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
                                    if (!dto.denpyouKbnCd.IsNull)
                                    {
                                        if (detail.DENPYOU_KBN_CD == dto.denpyouKbnCd)
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else if (!dto.torihikiKbnCd.IsNull)
                                    {
                                        if((detail.DENPYOU_KBN_CD == 1 && e.URIAGE_TORIHIKI_KBN_CD == dto.torihikiKbnCd)
                                           || (detail.DENPYOU_KBN_CD == 2 && e.SHIHARAI_TORIHIKI_KBN_CD == dto.torihikiKbnCd))
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else
                                    {
                                        result.detailList.Add(detail);
                                    }

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
                                i += result.detailList.Count;
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
                                    if (!dto.denpyouKbnCd.IsNull)
                                    {
                                        if (detail.DENPYOU_KBN_CD == dto.denpyouKbnCd)
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else if (!dto.torihikiKbnCd.IsNull)
                                    {
                                        if((detail.DENPYOU_KBN_CD == 1 && e.URIAGE_TORIHIKI_KBN_CD == dto.torihikiKbnCd)
                                           || (detail.DENPYOU_KBN_CD == 2 && e.SHIHARAI_TORIHIKI_KBN_CD == dto.torihikiKbnCd))
                                        {
                                            result.detailList.Add(detail);
                                        }
                                        else
                                        {
                                            result.detailListHidden.Add(detail);
                                        }
                                    }
                                    else
                                    {
                                        result.detailList.Add(detail);
                                    }
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
                                i += result.detailList.Count;
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
                    csvDt.Columns.Add("伝票日付");
                    csvDt.Columns.Add("取引先CD");
                    csvDt.Columns.Add("取引先名");
                    csvDt.Columns.Add("業者CD");
                    csvDt.Columns.Add("業者名");
                    csvDt.Columns.Add("現場CD");
                    csvDt.Columns.Add("現場名");
                    csvDt.Columns.Add("伝票区分");
                    csvDt.Columns.Add("品名CD");
                    csvDt.Columns.Add("品名");
                    csvDt.Columns.Add("数量");
                    csvDt.Columns.Add("単位CD");
                    csvDt.Columns.Add("単位名");
                    csvDt.Columns.Add("単価");
                    csvDt.Columns.Add("金額");
                    csvDt.Columns.Add("明細備考");
                    csvDt.Columns.Add("税計算区分");
                    csvDt.Columns.Add("税区分");

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
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    this.accessor.InsertUkeireDetailData(detailHidden);
                                }
                                if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberDay(dto.numberDay);
                                }
                                else if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberDay(dto.numberDay);
                                }
                                if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberYear(dto.numberYear);
                                }
                                else if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberYear(dto.numberYear);
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
                                    for (int i = 0; i < beforeDto.detailList.Count; i++)
                                    {
                                        int rowIndex = beforeDto.rowIndex + i;
                                        if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                        {
                                            continue;
                                        }
                                        GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                        if (this.ToNBoolean(cell.Value) != true)
                                        {
                                            continue;
                                        }
                                        T_UKEIRE_DETAIL beforeDetail = beforeDto.detailList[i];
                                        var dr = csvDt.NewRow();
                                        dr["データ区分"] = "前";
                                        dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                        dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                        dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                        dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(beforeDetail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = beforeDetail.HINMEI_CD;
                                        dr["品名"] = beforeDetail.HINMEI_NAME;
                                        dr["数量"] = beforeDetail.SUURYOU.IsNull ? string.Empty : beforeDetail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : beforeDetail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(beforeDto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = beforeDto.detailList[i].TANKA.IsNull ? string.Empty : beforeDetail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((beforeDto.detailList[i].KINGAKU.IsNull ? 0 : beforeDto.detailList[i].KINGAKU.Value) + (beforeDto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : beforeDto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = beforeDetail.MEISAI_BIKOU;
                                        short zeikeisanKbnCd = beforeDetail.DENPYOU_KBN_CD.Value == 1 ? beforeDto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : beforeDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        short zeiKbn = 0;
                                        if (!beforeDetail.HINMEI_ZEI_KBN_CD.IsNull && beforeDetail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = beforeDetail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (beforeDetail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = beforeDto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = beforeDto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);

                                        T_UKEIRE_DETAIL detail = dto.detailList[i];
                                        dr = csvDt.NewRow();
                                        dr["データ区分"] = "後";
                                        dr["伝票番号"] = dto.denpyouNo.ToString();
                                        dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                        dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = dto.entry.GENBA_CD;
                                        dr["現場名"] = dto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(detail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = detail.HINMEI_CD;
                                        dr["品名"] = detail.HINMEI_NAME;
                                        dr["数量"] = detail.SUURYOU.IsNull ? string.Empty : detail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = detail.UNIT_CD.IsNull ? string.Empty : detail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = detail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(dto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = dto.detailList[i].TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((dto.detailList[i].KINGAKU.IsNull ? 0 : dto.detailList[i].KINGAKU.Value) + (dto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : dto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = detail.MEISAI_BIKOU;
                                        zeikeisanKbnCd = detail.DENPYOU_KBN_CD.Value == 1 ? dto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        zeiKbn = 0;
                                        if (!detail.HINMEI_ZEI_KBN_CD.IsNull && detail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = detail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (detail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = dto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = dto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);
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
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    this.accessor.InsertShukkaDetailData(detailHidden);
                                }
                                if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberDay(dto.numberDay);
                                }
                                else if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberDay(dto.numberDay);
                                }
                                if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberYear(dto.numberYear);
                                }
                                else if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberYear(dto.numberYear);
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

                                if (this.headerForm.txtCsvOutputKbn.Text == "1")
                                {
                                    for (int i = 0; i < beforeDto.detailList.Count; i++)
                                    {
                                        int rowIndex = beforeDto.rowIndex + i;
                                        if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                        {
                                            continue;
                                        }
                                        GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                        if (this.ToNBoolean(cell.Value) != true)
                                        {
                                            continue;
                                        }
                                        T_SHUKKA_DETAIL beforeDetail = beforeDto.detailList[i];
                                        var dr = csvDt.NewRow();
                                        dr["データ区分"] = "前";
                                        dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                        dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                        dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                        dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(beforeDetail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = beforeDetail.HINMEI_CD;
                                        dr["品名"] = beforeDetail.HINMEI_NAME;
                                        dr["数量"] = beforeDetail.SUURYOU.IsNull ? string.Empty : beforeDetail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : beforeDetail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(beforeDto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = beforeDto.detailList[i].TANKA.IsNull ? string.Empty : beforeDetail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((beforeDto.detailList[i].KINGAKU.IsNull ? 0 : beforeDto.detailList[i].KINGAKU.Value) + (beforeDto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : beforeDto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = beforeDetail.MEISAI_BIKOU;
                                        short zeikeisanKbnCd = beforeDetail.DENPYOU_KBN_CD.Value == 1 ? beforeDto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : beforeDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        short zeiKbn = 0;
                                        if (!beforeDetail.HINMEI_ZEI_KBN_CD.IsNull && beforeDetail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = beforeDetail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (beforeDetail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = beforeDto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = beforeDto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);

                                        T_SHUKKA_DETAIL detail = dto.detailList[i];
                                        dr = csvDt.NewRow();
                                        dr["データ区分"] = "後";
                                        dr["伝票番号"] = dto.denpyouNo.ToString();
                                        dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                        dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = dto.entry.GENBA_CD;
                                        dr["現場名"] = dto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(detail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = detail.HINMEI_CD;
                                        dr["品名"] = detail.HINMEI_NAME;
                                        dr["数量"] = detail.SUURYOU.IsNull ? string.Empty : detail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = detail.UNIT_CD.IsNull ? string.Empty : detail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = detail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(dto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = dto.detailList[i].TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((dto.detailList[i].KINGAKU.IsNull ? 0 : dto.detailList[i].KINGAKU.Value) + (dto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : dto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = detail.MEISAI_BIKOU;
                                        zeikeisanKbnCd = detail.DENPYOU_KBN_CD.Value == 1 ? dto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        zeiKbn = 0;
                                        if (!detail.HINMEI_ZEI_KBN_CD.IsNull && detail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = detail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (detail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = dto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = dto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);
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
                                foreach (var detailHidden in dto.detailListHidden)
                                {
                                    this.accessor.InsertUrshDetailData(detailHidden);
                                }
                                if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberDay(dto.numberDay);
                                }
                                else if (dto.hiRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberDay(dto.numberDay);
                                }
                                if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.INSERT))
                                {
                                    this.accessor.InsertNumberYear(dto.numberYear);
                                }
                                else if (dto.nenRenbanRegistKbn.Equals(Shougun.Function.ShougunCSCommon.Const.SalesPaymentConstans.REGIST_KBN.UPDATE))
                                {
                                    this.accessor.UpdateNumberYear(dto.numberYear);
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
                                    for (int i = 0; i < beforeDto.detailList.Count; i++)
                                    {
                                        int rowIndex = beforeDto.rowIndex + i;
                                        if (!this.form.mrDetail.Rows[rowIndex].Visible)
                                        {
                                            continue;
                                        }
                                        GcCustomCheckBoxCell cell = (this.form.mrDetail.Rows[rowIndex].Cells["CHECK"] as GcCustomCheckBoxCell);
                                        if (this.ToNBoolean(cell.Value) != true)
                                        {
                                            continue;
                                        }
                                        T_UR_SH_DETAIL beforeDetail = beforeDto.detailList[i];
                                        var dr = csvDt.NewRow();
                                        dr["データ区分"] = "前";
                                        dr["伝票番号"] = beforeDto.denpyouNo.ToString();
                                        dr["伝票日付"] = beforeDto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = beforeDto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = beforeDto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = beforeDto.entry.GYOUSHA_CD;
                                        dr["業者名"] = beforeDto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = beforeDto.entry.GENBA_CD;
                                        dr["現場名"] = beforeDto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(beforeDetail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = beforeDetail.HINMEI_CD;
                                        dr["品名"] = beforeDetail.HINMEI_NAME;
                                        dr["数量"] = beforeDetail.SUURYOU.IsNull ? string.Empty : beforeDetail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : beforeDetail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = beforeDetail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(beforeDto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = beforeDto.detailList[i].TANKA.IsNull ? string.Empty : beforeDetail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((beforeDto.detailList[i].KINGAKU.IsNull ? 0 : beforeDto.detailList[i].KINGAKU.Value) + (beforeDto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : beforeDto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = beforeDetail.MEISAI_BIKOU;
                                        short zeikeisanKbnCd = beforeDetail.DENPYOU_KBN_CD.Value == 1 ? beforeDto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : beforeDto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        short zeiKbn = 0;
                                        if (!beforeDetail.HINMEI_ZEI_KBN_CD.IsNull && beforeDetail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = beforeDetail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (beforeDetail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = beforeDto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = beforeDto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);

                                        T_UR_SH_DETAIL detail = dto.detailList[i];
                                        dr = csvDt.NewRow();
                                        dr["データ区分"] = "後";
                                        dr["伝票番号"] = dto.denpyouNo.ToString();
                                        dr["伝票日付"] = dto.entry.DENPYOU_DATE.Value.ToShortDateString();
                                        dr["取引先CD"] = dto.entry.TORIHIKISAKI_CD;
                                        dr["取引先名"] = dto.entry.TORIHIKISAKI_NAME;
                                        dr["業者CD"] = dto.entry.GYOUSHA_CD;
                                        dr["業者名"] = dto.entry.GYOUSHA_NAME;
                                        dr["現場CD"] = dto.entry.GENBA_CD;
                                        dr["現場名"] = dto.entry.GENBA_NAME;
                                        dr["伝票区分"] = this.accessor.GetDenpyouKbnNameFast(detail.DENPYOU_KBN_CD.Value.ToString());
                                        dr["品名CD"] = detail.HINMEI_CD;
                                        dr["品名"] = detail.HINMEI_NAME;
                                        dr["数量"] = detail.SUURYOU.IsNull ? string.Empty : detail.SUURYOU.Value.ToString("#0.####");
                                        dr["単位CD"] = detail.UNIT_CD.IsNull ? string.Empty : detail.UNIT_CD.Value.ToString();
                                        dr["単位名"] = detail.UNIT_CD.IsNull ? string.Empty : this.accessor.GetUnitNameFast(dto.detailList[i].UNIT_CD.Value.ToString());
                                        dr["単価"] = dto.detailList[i].TANKA.IsNull ? string.Empty : detail.TANKA.Value.ToString("#0.####");
                                        dr["金額"] = ((dto.detailList[i].KINGAKU.IsNull ? 0 : dto.detailList[i].KINGAKU.Value) + (dto.detailList[i].HINMEI_KINGAKU.IsNull ? 0 : dto.detailList[i].HINMEI_KINGAKU.Value)).ToString("#0.####");
                                        dr["明細備考"] = detail.MEISAI_BIKOU;
                                        zeikeisanKbnCd = detail.DENPYOU_KBN_CD.Value == 1 ? dto.entry.URIAGE_ZEI_KEISAN_KBN_CD.Value : dto.entry.SHIHARAI_ZEI_KEISAN_KBN_CD.Value;
                                        dr["税計算区分"] = this.accessor.GetZeikeisanKbnNameFast(zeikeisanKbnCd);
                                        zeiKbn = 0;
                                        if (!detail.HINMEI_ZEI_KBN_CD.IsNull && detail.HINMEI_ZEI_KBN_CD != 0)
                                        {
                                            zeiKbn = detail.HINMEI_ZEI_KBN_CD.Value;
                                        }
                                        else
                                        {
                                            if (detail.DENPYOU_KBN_CD.Value == 1)
                                            {
                                                zeiKbn = dto.entry.URIAGE_ZEI_KBN_CD.Value;
                                            }
                                            else
                                            {
                                                zeiKbn = dto.entry.SHIHARAI_ZEI_KBN_CD.Value;
                                            }
                                        }
                                        dr["税区分"] = this.accessor.GetZeiKbnNameFast(zeiKbn);
                                        csvDt.Rows.Add(dr);
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
                    this.headerForm.ReadDataNumber.Text = "0";
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
        #endregion

        #region 明細部Validating相関処理
        /// <summary>
        /// 品名コードの存在チェック（伝種区分が受入、または共通のみ可）
        /// </summary>
        /// <param name="targetRow"></param>
        /// <returns>true: 入力された品名コードが存在する, false: 入力された品名コードが存在しない</returns>
        internal bool CheckHinmeiCd(Row targetRow, short denshuKbnCd, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow, denshuKbnCd, catchErr);

                if ((targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString());
                if (hinmeis == null || hinmeis.Count() < 1)
                {
                    // 品名コードがマスタに存在しない場合
                    // ただし、部品で存在チェックが行われるため、実際にここを通ることはない
                    return returnVal;
                }
                else
                {
                    M_HINMEI hinmei = hinmeis[0];
                    // 品名コードがマスタに存在する場合
                    if ((hinmei.DENSHU_KBN_CD.Value != denshuKbnCd
                        && hinmei.DENSHU_KBN_CD != SalesPaymentConstans.DENSHU_KBN_CD_KYOTU))
                    {
                        // 入力された品名コードに紐づく伝種区分が受入、共通以外の場合はエラーメッセージ表示
                        targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value = null;
                        targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = null;
                        targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = null;
                        targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = null;
                        targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value = null;
                        targetRow.Cells[ConstCls.COLUMN_UNIT_NAME].Value = null;
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E058", "");
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CheckHinmeiCd", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHinmeiCd", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }

        /// <summary>
        /// 伝票区分設定
        /// 明細の品名から伝票区分を設定する
        /// </summary>
        internal bool SetDenpyouKbn()
        {
            LogUtility.DebugMethodStart();

            Row targetRow = this.form.mrDetail.CurrentRow;

            if (targetRow == null)
            {
                return true;
            }

            // 初期化
            targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = string.Empty;
            targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;

            if (targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null
                || string.IsNullOrEmpty(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString()))
            {
                return true;
            }

            M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString());

            if (hinmeis == null || hinmeis.Count() < 1)
            {
                // 存在しない品名が選択されている場合
                return true;
            }
            var targetHimei = hinmeis[0];

            switch (targetHimei.DENPYOU_KBN_CD.ToString())
            {
                case SalesPaymentConstans.DENPYOU_KBN_CD_URIAGE_STR:
                case SalesPaymentConstans.DENPYOU_KBN_CD_SHIHARAI_STR:
                    targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = targetHimei.DENPYOU_KBN_CD.Value;
                    targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.accessor.GetDenpyouKbnNameFast(targetHimei.DENPYOU_KBN_CD.Value.ToString());
                    break;

                default:
                    // ポップアップを打ち上げ、ユーザに選択してもらう
                    CellPosition pos = this.form.mrDetail.CurrentCellPosition;
                    CustomControlExtLogic.PopUp((ICustomControl)this.form.mrDetail.Rows[pos.RowIndex].Cells[ConstCls.COLUMN_DENPYOU_KBN_CD]);

                    var denpyouKbnCd = targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value;
                    if (denpyouKbnCd == null
                        || string.IsNullOrEmpty(denpyouKbnCd.ToString()))
                    {
                        // ポップアップでキャンセルが押された
                        // ※ポップアップで何を押されたか判断できないので、CDの存在チェックで対応
                        targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = string.Empty;
                        targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = string.Empty;

                        //ポップアップキャンセルフラグをTrueにする。
                        this.form.bCancelDenpyoPopup = true;

                        return false;
                    }

                    break;
            }

            LogUtility.DebugMethodEnd();

            return true;
        }

        /// <summary>
        /// 単位CD検索&設定
        /// </summary>
        /// <param name="hinmeiChangedFlg">品名CDが更新された後の処理かどうか</param>
        internal bool SearchAndCalcForUnit(bool hinmeiChangedFlg, Row targetRow, short denshuKbnCd)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(hinmeiChangedFlg, targetRow, denshuKbnCd);

                if (targetRow == null)
                {
                    return true;
                }

                // 単価前回値取得
                var oldTanka = targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value == null ? string.Empty : targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value.ToString();

                M_UNIT targetUnit = null;

                if (hinmeiChangedFlg)
                {
                    M_HINMEI[] hinmeis = null;
                    // 品名CD更新時
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value)))
                    {
                        return true;
                    }
                    else
                    {
                        hinmeis = this.accessor.GetAllValidHinmeiData(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value));
                    }

                    if (hinmeis == null || hinmeis.Count() < 1)
                    {
                        return true;
                    }

                    M_HINMEI hinmei = hinmeis[0];

                    M_UNIT units = null;
                    short UnitCd = 0;
                    if (short.TryParse(hinmei.UNIT_CD.ToString(), out UnitCd))
                        units = this.accessor.GetUnitDataByCode(UnitCd);

                    if (units == null)
                    {
                        return true;
                    }
                    else
                    {
                        targetUnit = units;
                    }

                    if (string.IsNullOrEmpty(targetUnit.UNIT_NAME))
                    {
                        return true;
                    }

                    if (this.headerForm.cbx_Unit.Checked)
                    {
                        targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value = targetUnit.UNIT_CD.ToString();
                        targetRow.Cells[ConstCls.COLUMN_UNIT_NAME].Value = targetUnit.UNIT_NAME_RYAKU.ToString();
                    }
                }
                else
                {
                    // 単位CD更新時
                }

                short denpyouKbn = 0;
                short unitCd = 0;
                if (!short.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value), out denpyouKbn)
                    || !short.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value), out unitCd))
                {
                    return true;
                }

                if (!this.headerForm.cbx_Tanka.Checked)
                {
                    return true;
                }
                switch (denshuKbnCd)
                {
                    case 1:
                    case 3:
                        /**
                         * 単価設定
                         */
                        var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                            denshuKbnCd,
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_GENBA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value),
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value)
                            );

                        // 個別品名単価から情報が取れない場合は基本品名単価の検索
                        if (kobetsuhinmeiTanka == null)
                        {
                            var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                                denshuKbnCd,
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NIOROSHI_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NIOROSHI_GENBA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value),
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value)
                                );
                            if (kihonHinmeiTanka != null)
                            {
                                targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = kihonHinmeiTanka.TANKA.Value;
                            }
                            else
                            {
                                targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;
                            }
                        }
                        else
                        {
                            targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = kobetsuhinmeiTanka.TANKA.Value;
                        }
                        break;
                    case 2:
                        /**
                         * 単価設定
                         */
                        kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                            denshuKbnCd,
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_TORIHIKISAKI_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_GENBA_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value),
                            null,
                            null,
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value),
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value)
                            );

                        // 個別品名単価から情報が取れない場合は基本品名単価の検索
                        if (kobetsuhinmeiTanka == null)
                        {
                            var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                                denshuKbnCd,
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_UNPAN_GYOUSHA_CD].Value),
                            null,
                            null,
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value),
                            Convert.ToInt16(targetRow.Cells[ConstCls.COLUMN_UNIT_CD].Value),
                            Convert.ToString(targetRow.Cells[ConstCls.COLUMN_DENPYOU_DATE].Value)
                                );
                            if (kihonHinmeiTanka != null)
                            {
                                targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = kihonHinmeiTanka.TANKA.Value;
                            }
                            else
                            {
                                targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = string.Empty;
                            }
                        }
                        else
                        {
                            targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = kobetsuhinmeiTanka.TANKA.Value;
                        }
                        break;
                }

                /**
                 * 金額設定
                 */

                var newTanka = targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value == null ? string.Empty : targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value.ToString();

                // 単価に変更があった場合のみ再計算
                if (!oldTanka.Equals(newTanka))
                {
                    if (!this.CalcDetaiKingaku(targetRow, denshuKbnCd))
                    {
                        return false;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchAndCalcForUnit", ex);
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
        /// 明細金額計算
        /// </summary>
        internal bool CalcDetaiKingaku(Row targetRow, short denshuKbnCd)
        {
            /* 登録実行時に金額計算のチェック(CheckDetailKingakuメソッド)が実行されます。 　　         */
            /* チェックの計算方法は本メソッドと同じため、修正する際はチェック処理も修正してください。 */
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow, denshuKbnCd);

                if (targetRow == null)
                {
                    return true;
                }

                if (!this.headerForm.cbx_Tanka.Checked
                    && (targetRow.Cells["mr_TANKA"].Value == null
                    || string.IsNullOrEmpty(targetRow.Cells["mr_TANKA"].Value.ToString())))
                {
                    return true;
                }
                int index = this.GetListIndex(denshuKbnCd, targetRow.Index);

                decimal suuryou = 0;
                decimal tanka = 0;
                // 金額端数の初期値は四捨五入としておく
                short kingakuHasuuCd = 3;

                decimal.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_SUURYOU].FormattedValue), out suuryou);
                decimal.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].FormattedValue), out tanka);

                // 金額端数取得
                if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value != null)
                {
                    switch (denshuKbnCd)
                    {
                        case 1:
                            if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                            {
                                short.TryParse(Convert.ToString(this.tuResult[index].seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            else if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                            {
                                short.TryParse(Convert.ToString(this.tuResult[index].shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            break;
                        case 2:
                            if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                            {
                                short.TryParse(Convert.ToString(this.tsResult[index].seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            else if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                            {
                                short.TryParse(Convert.ToString(this.tsResult[index].shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            break;
                        case 3:
                            if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                            {
                                short.TryParse(Convert.ToString(this.tusResult[index].seikyuu.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            else if (targetRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                            {
                                short.TryParse(Convert.ToString(this.tusResult[index].shiharai.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                            }
                            break;
                    }
                }

                if (decimal.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_SUURYOU].FormattedValue), out suuryou)
                    && decimal.TryParse(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_NEW_TANKA].FormattedValue), out tanka))
                {
                    decimal kingaku = CommonCalc.FractionCalc(suuryou * tanka, kingakuHasuuCd);

                    /* 桁が10桁以上になる場合は9桁で表示する。ただし、結果としては違算なので、登録時金額チェックではこの処理は行わずエラーとしている */
                    if (kingaku.ToString().Length > 9)
                    {
                        kingaku = Convert.ToDecimal(kingaku.ToString().Substring(0, 9));
                    }

                    targetRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = kingaku;
                }
                else
                {
                    targetRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = null;
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

        /// <summary>
        /// 在庫品名振分検索
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>品名変更後、複数在庫品名振分検索</remarks>
        internal void ZaikoHinmeiHuriwakesSearch(Row row, short denshuKbnCd)
        {
            LogUtility.DebugMethodStart(row, denshuKbnCd);

            // 在庫管理の場合のみ設定する
            if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
            {
                if (row != null &&
                    !Convert.IsDBNull(row.Cells[ConstCls.COLUMN_HINMEI_CD].Value) &&
                    row.Cells[ConstCls.COLUMN_HINMEI_CD].Value != null &&
                    !string.IsNullOrEmpty(row.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString()))
                {
                    var zaikoHiritsus = this.accessor.GetZaikoHiritsus(row.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString());
                    var zaikoHinmeiHuriwakes = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                    if (zaikoHiritsus != null)
                    {
                        foreach (var dr in zaikoHiritsus.AsEnumerable())
                        {
                            var entity = new T_ZAIKO_HINMEI_HURIWAKE();

                            // 在庫比率は適用期間内で、在庫品名は適用期間外の場合
                            entity.ZAIKO_HINMEI_CD = Convert.IsDBNull(dr["ZAIKO_HINMEI_CD"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_CD"]);
                            entity.ZAIKO_HINMEI_NAME = Convert.IsDBNull(dr["ZAIKO_HINMEI_NAME"]) ? string.Empty : Convert.ToString(dr["ZAIKO_HINMEI_NAME"]);
                            entity.ZAIKO_HIRITSU = Convert.IsDBNull(dr["ZAIKO_HIRITSU"]) ? (short)0 : Convert.ToInt16(dr["ZAIKO_HIRITSU"]);
                            entity.ZAIKO_TANKA = Convert.IsDBNull(dr["ZAIKO_TANKA"]) ? decimal.Zero : Convert.ToDecimal(dr["ZAIKO_TANKA"]);

                            zaikoHinmeiHuriwakes.Add(entity);
                        }
                    }

                    int index = this.GetListIndex(denshuKbnCd, row.Index);
                    switch (denshuKbnCd)
                    {
                        case 1:
                            T_UKEIRE_DETAIL tud = this.tuResult[index].detailList[row.Index - this.tuResult[index].rowIndex];
                            string key = string.Format("{0}_{1}_{2}", tud.SYSTEM_ID.Value.ToString(), tud.DETAIL_SYSTEM_ID.Value.ToString(), tud.SEQ.Value.ToString());
                            this.tuResult[index].detailZaikoHinmeiHuriwakes[key] = zaikoHinmeiHuriwakes;
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 品名入力値チェック
        /// </summary>
        /// <returns>true: 問題なし, false:問題あり</returns>
        internal bool ValidateHinmeiName(out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();


                Row targetRow = this.form.mrDetail.CurrentRow;

                if (targetRow == null)
                {
                    return returnVal;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value)))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].EditedFormattedValue)))
                    {
                        CellPosition pos = this.form.mrDetail.CurrentCellPosition;
                        this.form.mrDetail.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(pos.RowIndex, ConstCls.COLUMN_HINMEI_NAME);

                        var cell = targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME] as ICustomAutoChangeBackColor;
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E012", "品名");
                        return returnVal;
                    }
                }

                returnVal = true;

            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ValidateHinmeiName", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ValidateHinmeiName", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        /// <summary>
        /// 明細欄の品名をセットします
        /// </summary>
        /// <param name="row">現在のセルを含む行（CurrentRow）</param>
        internal bool SetHinmeiName(Row row, short denshuKbn)
        {
            try
            {
                if (row == null)
                {
                    return true;
                }
                bool catchErr = true;
                bool retChousei = this.CheckHinmeiCd(row, denshuKbn, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                {
                    // 入力された品名コードが存在するとき
                    if (row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value != null)
                    {
                        if (string.IsNullOrEmpty(row.Cells[ConstCls.COLUMN_HINMEI_NAME].Value.ToString()))
                        {
                            // 品名が空の場合再セット
                            this.GetHinmei(row, denshuKbn, out catchErr);
                            if (!catchErr)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // 品名が空の場合再セット
                        this.GetHinmei(row, denshuKbn, out catchErr);
                        if (!catchErr)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetHinmeiName", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHinmeiName", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }
        #endregion

        #region 権限チェック
        /// <summary>
        ///
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
                    this.form.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
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
                    var retData = this.accessor.GetGenbaDataByCode(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
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

        #region TabStop情報取得処理
        private void MapInit(string kbn)
        {
            this.map.Clear();
            List<string> columnName = new List<string>();
            if (kbn == "1")
            {
                columnName = this.columnUkeire.ToList();
            }
            else if (kbn == "2")
            {
                columnName = this.columnShukka.ToList();
            }
            else if (kbn == "3")
            {
                columnName = this.columnUrsh.ToList();
            }
            if (!this.headerForm.cbx_Hinmei.Checked)
            {
                columnName.Remove(ConstCls.COLUMN_HINMEI_CD);
                columnName.Remove(ConstCls.COLUMN_HINMEI_NAME);
            }
            if (!this.headerForm.cbx_MeisaiBikou.Checked)
            {
                columnName.Remove(ConstCls.COLUMN_MEISAI_BIKOU);
            }
            if (!this.headerForm.cbx_Suuryou.Checked)
            {
                columnName.Remove(ConstCls.COLUMN_SUURYOU);
            }
            if (!this.headerForm.cbx_Tanka.Checked)
            {
                columnName.Remove(ConstCls.COLUMN_NEW_TANKA);
                columnName.Remove(ConstCls.COLUMN_NEW_KINGAKU);
            }
            if (!this.headerForm.cbx_Unit.Checked)
            {
                columnName.Remove(ConstCls.COLUMN_UNIT_CD);
            }
            for (int i = 0; i < columnName.Count; i++)
            {
                TabGoDto dto = new TabGoDto();
                dto.ControlName = columnName[i];
                if (i == 0)
                {
                    dto.PreviousControlName = columnName[columnName.Count - 1];
                    dto.isFirst = true;
                }
                else
                {
                    dto.PreviousControlName = columnName[i - 1];
                    dto.isFirst = false;
                }
                if (i == columnName.Count - 1)
                {
                    dto.NextControlName = columnName[0];
                    dto.isLast = true;
                }
                else
                {
                    dto.NextControlName = columnName[i + 1];
                    dto.isLast = false;
                }
                this.map.Add(dto.ControlName, dto);
            }
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
                    var retData = this.accessor.GetGenbaDataByCode(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
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

        internal int GetListIndex(short denshuKbnCd, int rowIndex)
        {
            int ret = -1;
            switch (denshuKbnCd)
            {
                case 1:
                    for (int i = 0; i < this.tuResult.Count; i++)
                    {
                        if (this.tuResult[i].rowIndex <= rowIndex && rowIndex < this.tuResult[i].rowIndex + this.tuResult[i].detailList.Count)
                        {
                            ret = i;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < this.tsResult.Count; i++)
                    {
                        if (this.tsResult[i].rowIndex <= rowIndex && rowIndex < this.tsResult[i].rowIndex + this.tsResult[i].detailList.Count)
                        {
                            ret = i;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = 0; i < this.tusResult.Count; i++)
                    {
                        if (this.tusResult[i].rowIndex <= rowIndex && rowIndex < this.tusResult[i].rowIndex + this.tusResult[i].detailList.Count)
                        {
                            ret = i;
                            break;
                        }
                    }
                    break;
            }
            return ret;
        }

        internal string AddYobi(DateTime date)
        {
            string ret = "";
            string yobi = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    yobi = "(月)";
                    break;
                case DayOfWeek.Tuesday:
                    yobi = "(火)";
                    break;
                case DayOfWeek.Wednesday:
                    yobi = "(水)";
                    break;
                case DayOfWeek.Thursday:
                    yobi = "(木)";
                    break;
                case DayOfWeek.Friday:
                    yobi = "(金)";
                    break;
                case DayOfWeek.Saturday:
                    yobi = "(土)";
                    break;
                case DayOfWeek.Sunday:
                    yobi = "(日)";
                    break;
            }
            ret = date.ToShortDateString() + yobi;
            return ret;
        }
        #endregion

        #region 受入税計算
        internal void ZeiKeisan(ResultDto<T_UKEIRE_ENTRY, T_UKEIRE_DETAIL> dto)
        {
            var denpyouHakouPopUpDTO = this.createParameterDTOClass(dto.entry, dto.seikyuu, dto.shiharai);
            decimal URIAGE_AMOUNT_TOTAL = 0;
            decimal SHIHARAI_KINGAKU_TOTAL = 0;
            var detailListAll = new List<T_UKEIRE_DETAIL>(dto.detailList);
            detailListAll.AddRange(dto.detailListHidden);

            foreach (var detail in detailListAll)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
                }
                else
                {
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
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
            foreach (T_UKEIRE_DETAIL detail in detailListAll)
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
            var detailListAll = new List<T_SHUKKA_DETAIL>(dto.detailList);
            detailListAll.AddRange(dto.detailListHidden);
            foreach (var detail in detailListAll)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
                }
                else
                {
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
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
            foreach (T_SHUKKA_DETAIL detail in detailListAll)
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
            var detailListAll = new List<T_UR_SH_DETAIL>(dto.detailList);
            detailListAll.AddRange(dto.detailListHidden);
            foreach (var detail in detailListAll)
            {
                if (detail.DENPYOU_KBN_CD.Value == 1)
                {
                    URIAGE_AMOUNT_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
                }
                else
                {
                    SHIHARAI_KINGAKU_TOTAL += detail.KINGAKU.IsNull ? 0 : detail.KINGAKU.Value;
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
            foreach (T_UR_SH_DETAIL detail in detailListAll)
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

        #region 品名名称取得処理
        internal bool GetHinmei(Row targetRow, short denshuKbn, out bool catchErr)
        {
            catchErr = true;
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart(targetRow, denshuKbn);

                if ((targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString())))
                {
                    // 品名コードの入力がない場合
                    return returnVal;
                }

                M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString());

                var gyoushaCd = (targetRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value == null) ? string.Empty : targetRow.Cells[ConstCls.COLUMN_GYOUSHA_CD].Value.ToString();
                var genbaCd = (targetRow.Cells[ConstCls.COLUMN_GENBA_CD].Value == null) ? string.Empty : targetRow.Cells[ConstCls.COLUMN_GENBA_CD].Value.ToString();
                M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(gyoushaCd, genbaCd, targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString(), denshuKbn);
                if (kobetsuHinmeis != null)
                {
                    targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
                }
                else
                {
                    if (hinmeis != null && hinmeis.Count() > 0)
                    {
                        targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                    }
                    else
                    {
                        returnVal = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetHinmei", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetHinmei", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }

        internal bool GetHinmeiForPop(Row targetRow, short denshuKbn)
        {
            LogUtility.DebugMethodStart(targetRow, denshuKbn);
            bool returnVal = false;

            if ((targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value == null) || (string.IsNullOrEmpty(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString())))
            {
                // 品名コードの入力がない場合
                return returnVal;
            }

            M_HINMEI[] hinmeis = this.accessor.GetAllValidHinmeiData(targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString());

            M_KOBETSU_HINMEI kobetsuHinmeis = this.accessor.GetKobetsuHinmeiDataByCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, targetRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value.ToString(), denshuKbn);
            if (kobetsuHinmeis != null)
            {
                targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = kobetsuHinmeis.SEIKYUU_HINMEI_NAME;
            }
            else
            {
                if (hinmeis != null && hinmeis.Count() > 0)
                {
                    targetRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = hinmeis[0].HINMEI_NAME;
                }
            }
            LogUtility.DebugMethodEnd();
            return returnVal;
        }
        #endregion

        #region 在庫処理
        /// <summary>
        /// 在庫品名クリア
        /// </summary>
        /// <param name="row"></param>
        /// <remarks>品名をクリアする時、在庫品名情報もクリアする</remarks>
        internal bool ZaikoHinmeiHuriwakesClear(Row row, short denshuKbnCd)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(row, denshuKbnCd);

                // 在庫管理の場合のみ設定する
                if (CommonShogunData.SYS_INFO.ZAIKO_KANRI.Value == 1)
                {
                    int index = this.GetListIndex(denshuKbnCd, row.Index);
                    if (index < 0)
                    {
                        return false;
                    }
                    switch (denshuKbnCd)
                    {
                        case 1:
                            T_UKEIRE_DETAIL tud = this.tuResult[index].detailList[row.Index - index];
                            string key = string.Format("{0}_{1}_{2}", tud.SYSTEM_ID.Value.ToString(), tud.DETAIL_SYSTEM_ID.Value.ToString(), tud.SEQ.Value.ToString());
                            // 前の在庫品名振分データを廃棄し、空リストを設定する。
                            this.tuResult[index].detailZaikoHinmeiHuriwakes[key] = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                            break;
                        case 2:
                            T_SHUKKA_DETAIL tsd = this.tsResult[index].detailList[row.Index - index];
                            key = string.Format("{0}_{1}_{2}", tsd.SYSTEM_ID.Value.ToString(), tsd.DETAIL_SYSTEM_ID.Value.ToString(), tsd.SEQ.Value.ToString());
                            // 前の在庫品名振分データを廃棄し、空リストを設定する。
                            this.tsResult[index].detailZaikoHinmeiHuriwakes[key] = new List<T_ZAIKO_HINMEI_HURIWAKE>();
                            break;
                    }
                }

                ret = true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesClear", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZaikoHinmeiHuriwakesClear", ex);
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

        #region 一括指定ポップアップ
        /// <summary> 条件指定ポップアップ </summary>
        /// <param name="param">param</param>
        internal bool ShowPopUp()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var SendParam = new SendPopupParam();

                SendParam.cbxHinmei = this.headerForm.cbx_Hinmei.Checked;
                SendParam.cbxSuuryou = this.headerForm.cbx_Suuryou.Checked;
                SendParam.cbxTanka = this.headerForm.cbx_Tanka.Checked;
                SendParam.cbxMeisaiBikou = this.headerForm.cbx_MeisaiBikou.Checked;
                SendParam.cbxUnit = this.headerForm.cbx_Unit.Checked;
                SendParam.denshuKbnCd = this.dto.denshuKbnCd;
                var popUpForm = new DenpyouiTankakkatsuPopupForm(SendParam);

                if (popUpForm.ShowDialog() == DialogResult.OK)
                {
                    this.NyuuryokuParam = popUpForm.NyuuryokuParam;

                    if (this.NyuuryokuParam == null)
                    {
                        return false;
                    }

                    decimal tanka = 0;
                    decimal newTanka = 0;
                    foreach (Row dgvRow in this.form.mrDetail.Rows)
                    {
                        if (dgvRow.Cells[ConstCls.COLUMN_CHECK].Value != null && (bool)dgvRow.Cells[ConstCls.COLUMN_CHECK].Value == true)
                        {
                            bool hinmeiChanged = false;
                            bool changed = (!string.IsNullOrEmpty(this.NyuuryokuParam.hinmeiCd) && this.NyuuryokuParam.hinmeiCd != Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value))
                                || (!string.IsNullOrEmpty(this.NyuuryokuParam.denpyouKbnCd) && this.NyuuryokuParam.denpyouKbnCd != Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value))
                                || (!string.IsNullOrEmpty(this.NyuuryokuParam.unitCd) && this.NyuuryokuParam.unitCd != Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_UNIT_CD].Value));

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.hinmeiCd))
                            {
                                if (this.NyuuryokuParam.hinmeiCd != Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value))
                                {
                                    hinmeiChanged = true;
                                }
                                if (!this.headerForm.cbx_Unit.Checked)
                                {
                                    hinmeiChanged = false;
                                }
                                dgvRow.Cells[ConstCls.COLUMN_HINMEI_CD].Value = this.NyuuryokuParam.hinmeiCd;
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.hinmeiName))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_HINMEI_NAME].Value = this.NyuuryokuParam.hinmeiName;
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.denpyouKbnCd))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_CD].Value = this.NyuuryokuParam.denpyouKbnCd;
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.denpyouKbnName))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_DENPYOU_KBN_NAME].Value = this.NyuuryokuParam.denpyouKbnName;
                            }

                            if (!this.NyuuryokuParam.suuryou.IsNull && !dgvRow.Cells[ConstCls.COLUMN_SUURYOU].ReadOnly)
                            {
                                dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value = this.NyuuryokuParam.suuryou.Value;
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.unitCd))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_UNIT_CD].Value = this.NyuuryokuParam.unitCd;
                                hinmeiChanged = false;

                                if (this.dto.denshuKbnCd == "1")
                                {
                                    bool suuryouReadOnly = false;
                                    if (Convert.ToString(dgvRow[ConstCls.COLUMN_UNIT_CD].Value) == "1" || Convert.ToString(dgvRow[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                                    {
                                        int rowIndex = this.GetListIndex(1, dgvRow.Index);
                                        T_UKEIRE_DETAIL detail = this.tuResult[rowIndex].detailList[dgvRow.Index - this.tuResult[rowIndex].rowIndex];
                                        if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                                        {
                                            suuryouReadOnly = true;
                                            dgvRow[ConstCls.COLUMN_SUURYOU].Value = detail.SUURYOU.IsNull ? "" : detail.SUURYOU.Value.ToString();
                                        }
                                        else
                                        {
                                            suuryouReadOnly = false;
                                        }
                                    }
                                    else
                                    {
                                        suuryouReadOnly = false;
                                    }
                                    if (this.headerForm.cbx_Suuryou.Checked)
                                    {
                                        if (suuryouReadOnly)
                                        {
                                            dgvRow[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                        }
                                        else
                                        {
                                            dgvRow[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                        }
                                        dgvRow.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                                    }
                                }
                                else if (this.dto.denshuKbnCd == "2")
                                {
                                    bool suuryouReadOnly = false;
                                    if (Convert.ToString(dgvRow[ConstCls.COLUMN_UNIT_CD].Value) == "1" || Convert.ToString(dgvRow[ConstCls.COLUMN_UNIT_CD].Value) == "3")
                                    {
                                        int rowIndex = this.GetListIndex(1, dgvRow.Index);
                                        T_UKEIRE_DETAIL detail = this.tuResult[rowIndex].detailList[dgvRow.Index - this.tuResult[rowIndex].rowIndex];
                                        if (!detail.STACK_JYUURYOU.IsNull && !detail.EMPTY_JYUURYOU.IsNull)
                                        {
                                            suuryouReadOnly = true;
                                            dgvRow[ConstCls.COLUMN_SUURYOU].Value = detail.SUURYOU.IsNull ? "" : detail.SUURYOU.Value.ToString();
                                        }
                                        else
                                        {
                                            suuryouReadOnly = false;
                                        }
                                    }
                                    else
                                    {
                                        suuryouReadOnly = false;
                                    }
                                    if (this.headerForm.cbx_Suuryou.Checked)
                                    {
                                        if (suuryouReadOnly)
                                        {
                                            dgvRow[ConstCls.COLUMN_SUURYOU].ReadOnly = true;
                                        }
                                        else
                                        {
                                            dgvRow[ConstCls.COLUMN_SUURYOU].ReadOnly = false;
                                        }
                                        dgvRow.Cells[ConstCls.COLUMN_SUURYOU].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Selected);
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.unitName))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_UNIT_NAME].Value = this.NyuuryokuParam.unitName;
                            }

                            if (!string.IsNullOrEmpty(this.NyuuryokuParam.meisaiBikou))
                            {
                                dgvRow.Cells[ConstCls.COLUMN_MEISAI_BIKOU].Value = this.NyuuryokuParam.meisaiBikou;
                            }
                            if (changed && this.headerForm.cbx_Tanka.Checked)
                            {
                                this.SearchAndCalcForUnit(hinmeiChanged, dgvRow, Convert.ToInt16(this.dto.denshuKbnCd));
                                this.CalcDetaiKingaku(dgvRow, Convert.ToInt16(this.dto.denshuKbnCd));
                            }
                            if (!this.NyuuryokuParam.tanka.IsNull)
                            {
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = this.NyuuryokuParam.tanka.Value;
                                tanka = this.NyuuryokuParam.tanka.Value;
                                if (!string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value)))
                                {
                                    decimal suuryou = Convert.ToDecimal(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value);
                                    dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = suuryou * (tanka);
                                }
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            }
                            else if (!this.NyuuryokuParam.tankaZougenn.IsNull)
                            {
                                tanka = this.ToNDecimal(dgvRow.Cells[ConstCls.COLUMN_TANKA].Value) ?? 0;
                                newTanka = this.NyuuryokuParam.tankaZougenn.Value;
                                if (tanka + newTanka < -9999999.999m)
                                {
                                    dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = -9999999.999m;
                                }
                                else if (tanka + newTanka > 9999999.999m)
                                {
                                    dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = 9999999.999m;
                                }
                                else
                                {
                                    dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = tanka + newTanka;
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value)))
                                {
                                    decimal suuryou = Convert.ToDecimal(dgvRow.Cells[ConstCls.COLUMN_SUURYOU].Value);
                                    dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = suuryou * (tanka + newTanka);
                                }
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = false;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = true;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            }
                            else if (!this.NyuuryokuParam.kingkaku.IsNull)
                            {
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Value = null;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Value = this.NyuuryokuParam.kingkaku.Value;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].ReadOnly = true;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].ReadOnly = false;
                                dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_TANKA].Selected);
                                dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].UpdateBackColor(dgvRow.Cells[ConstCls.COLUMN_NEW_KINGAKU].Selected);
                            }
                        }
                    }
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
        #endregion

        #endregion
    }
}