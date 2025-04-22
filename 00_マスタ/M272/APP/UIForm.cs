using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.RiyouRirekiKanri.Logic;
using r_framework.Logic;

namespace Shougun.Core.Master.RiyouRirekiKanri.APP
{
    /// <summary>
    /// M272：利用履歴管理 フォーム
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 内部変数
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region プロパティ
        /// <summary>
        /// 親フォーム
        /// </summary>
        internal BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal HeaderForm HeaderForm { get; private set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_RIYOU_RIREKI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {
                base.OnLoad(e);

                if (!this.DesignMode)
                {
                    this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                    this.HeaderForm = (HeaderForm)this.ParentBaseForm.headerForm;

                    // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                    if (this.logic != null)
                    {
                        this.logic.Dispose();
                        this.logic = null;
                    }
                    this.logic = new LogicCls(this);

                    // 画面初期化
                    this.logic.WindowInit();

					// Anchorの設定は必ずOnLoadで行うこと
                    if (this.pnlUketsuke != null)
                    {
                        this.pnlUketsuke.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlUkeire != null)
                    {
                        this.pnlUkeire.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlShukka != null)
                    {
                        this.pnlShukka.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlUriageShiharai != null)
                    {
                        this.pnlUriageShiharai.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlDainou != null)
                    {
                        this.pnlDainou.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlNyuukin != null)
                    {
                        this.pnlNyuukin.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                    if (this.pnlShukkin != null)
                    {
                        this.pnlShukkin.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd(e);
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
            if (!isShown)
            {
                this.Height -= 12;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        private void SetPopupSendParams(string[] items)
        {
            this.CONDITION_ITEM1.PopupSendParams = items;
            this.CONDITION_ITEM1.Text = string.Empty;
            this.CONDITION_VALUE1.Text = string.Empty;
            this.CONDITION_ITEM2.PopupSendParams = items;
            this.CONDITION_ITEM2.Text = string.Empty;
            this.CONDITION_VALUE2.Text = string.Empty;
            this.CONDITION_ITEM3.PopupSendParams = items;
            this.CONDITION_ITEM3.Text = string.Empty;
            this.CONDITION_VALUE3.Text = string.Empty;
            this.CONDITION_ITEM4.PopupSendParams = items;
            this.CONDITION_ITEM4.Text = string.Empty;
            this.CONDITION_VALUE4.Text = string.Empty;
            this.CONDITION_ITEM5.PopupSendParams = items;
            this.CONDITION_ITEM5.Text = string.Empty;
            this.CONDITION_VALUE5.Text = string.Empty;
            this.CONDITION_ITEM6.PopupSendParams = items;
            this.CONDITION_ITEM6.Text = string.Empty;
            this.CONDITION_VALUE6.Text = string.Empty;
        }

        private void SetPanel(r_framework.CustomControl.CustomPanel pnl, bool visible)
        {
            if (pnl.Visible != visible)
            {
                pnl.Visible = visible;
            }
            pnl.Location = new Point(0, 149);
        }

        private void SelectPanel()
        {
            switch (this.txtDenpyouKind.Text)
            {
                case "1":
                    SetPanel(this.pnlUketsuke, true);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, false);
                    break;
                //case "2":
                //    SetPanel(this.pnlUketsuke, false);
                //    SetPanel(this.pnlKeiryou, true);
                //    SetPanel(this.pnlUkeire, false);
                //    SetPanel(this.pnlShukka, false);
                //    SetPanel(this.pnlUriageShiharai, false);
                //    SetPanel(this.pnlNyuukin, false);
                //    SetPanel(this.pnlShukkin, false);
                //    break;
                case "2":
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        this.MultiRow_UkeireMeisai.Template = this.ukeireDetail1;
                    }
                    else
                    {
                        this.MultiRow_UkeireMeisai.Template = this.ukeireDetail2;
                    }
                    SetPanel(this.pnlUkeire, true);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, false);
                    break;
                case "3":
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        this.MultiRow_ShukkaMeisai.Template = this.shukkaDetail1;
                    }
                    else
                    {
                        this.MultiRow_ShukkaMeisai.Template = this.shukkaDetail2;
                    }
                    SetPanel(this.pnlShukka, true);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, false);
                    break;
                case "4":
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, true);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, false);
                    break;
                case "5":
                    //代納
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, true);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, false);
                    break;
                case "6":
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, true);
                    SetPanel(this.pnlShukkin, false);
                    break;
                case "7":
                    SetPanel(this.pnlUketsuke, false);
                    //SetPanel(this.pnlKeiryou, false);
                    SetPanel(this.pnlUkeire, false);
                    SetPanel(this.pnlShukka, false);
                    SetPanel(this.pnlUriageShiharai, false);
                    SetPanel(this.pnlDainou, false);
                    SetPanel(this.pnlNyuukin, false);
                    SetPanel(this.pnlShukkin, true);
                    break;
                default:
                    break;
            }
        }


        private void txtDenpyouKind_TextChanged(object sender, EventArgs e)
        {
            SelectPanel();
            this.logic.SortColumns = new List<CustomSortColumn>();
            switch (this.txtDenpyouKind.Text)
            {
                case "1":
                    this.SetPopupSendParams(new string[] { this.Uketsuke_Denpyou.Name });
                    break;
                //case "2":
                //    // 連番区分に基づいて、年連番OR日連番を切り替える
                //    this.logic.RenbanSwitchingKeiryou();
                //    this.SetPopupSendParams(new string[] { this.Keiryou_Denpyou.Name });
                //    break;
                case "2":
                    // マニ登録形態区分
                    this.logic.ChangePropertyForGC(this.MultiRow_UkeireMeisai);
                    this.SetPopupSendParams(new string[] { this.Ukeire_Denpyou.Name });
                    break;
                case "3":
                    // マニ登録形態区分
                    this.logic.ChangePropertyForGC(this.MultiRow_ShukkaMeisai);
                    this.SetPopupSendParams(new string[] { this.Shukka_Denpyou.Name });
                    break;
                case "4":
                    this.SetPopupSendParams(new string[] { this.UriageShiharai_Denpyou.Name });
                    break;
                case"5":
                    //代納
                    this.SetPopupSendParams(new string[] { this.dgvDainou_Denpyou_Sort.Name });
                    break;
                case "6":
                    this.SetPopupSendParams(new string[] { this.Nyuukin_Denpyou.Name });
                    break;
                case "7":
                    this.SetPopupSendParams(new string[] { this.Shukkin_Denpyou.Name });
                    break;
                default:
                    this.SetPopupSendParams(new string[] { });
                    break;
            }
            // ソート条件のクリア
            this.logic.ClearCustomSortSetting();
        }

        #region 伝票クリック処理
        #region 受付
        /// <summary>
        /// 受付一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Uketsuke_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //受付区分
                string uketsukeKbn = this.Uketsuke_Denpyou.Rows[rowIndex].Cells["UKETUKE_DENPYOU_UKETSUKE_KBN"].Value.ToString();
                //システムID
                long systemId = Convert.ToInt64(this.Uketsuke_Denpyou.Rows[rowIndex].Cells["UKETUKE_DENPYOU_SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.Uketsuke_Denpyou.Rows[rowIndex].Cells["UKETUKE_DENPYOU_SEQ"].Value);

                //明細
                this.logic.UketsukeDetailDate_Select(uketsukeKbn, systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 計量
        //// <summary>
        ///// 計量伝票一覧の行選択時 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Keiryou_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);
        //    try
        //    {
        //        //選択行
        //        int rowIndex = e.RowIndex;
        //        //システムID
        //        long systemId = this.logic.KeiryouSearchResult.Rows[rowIndex].Field<long>("SYSTEM_ID");
        //        //SEQ
        //        int seq = this.logic.KeiryouSearchResult.Rows[rowIndex].Field<int>("SEQ");
        //        //明細
        //        this.logic.KeiryouDetailDate_Select(systemId, seq);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    LogUtility.DebugMethodEnd();
        //}
        #endregion
        #region 受入
        // <summary>
        /// 受入伝票一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ukeire_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = Convert.ToInt64(this.Ukeire_Denpyou.Rows[rowIndex].Cells["UKEIRE_DENPYOU_SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.Ukeire_Denpyou.Rows[rowIndex].Cells["UKEIRE_DENPYOU_SEQ"].Value);
                //明細
                this.logic.UkeireDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 出荷
        // <summary>
        /// 出荷伝票一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukka_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = Convert.ToInt64(this.Shukka_Denpyou.Rows[rowIndex].Cells["SHUKKA_DENPYOU_SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.Shukka_Denpyou.Rows[rowIndex].Cells["SHUKKA_DENPYOU_SEQ"].Value);
                //明細
                this.logic.ShukkaDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 売上/支払
        // <summary>
        /// 売上/支払伝票一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UriageShiharai_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = Convert.ToInt64(this.UriageShiharai_Denpyou.Rows[rowIndex].Cells["URIAGESIHARAI_DENPYOU_SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.UriageShiharai_Denpyou.Rows[rowIndex].Cells["URIAGESIHARAI_DENPYOU_SEQ"].Value);
                //明細
                this.logic.UrShDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 入金
        // <summary>
        /// 入金伝票一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nyuukin_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = Convert.ToInt64(this.Nyuukin_Denpyou.Rows[rowIndex].Cells["NYUKIN_DENPYOU__SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.Nyuukin_Denpyou.Rows[rowIndex].Cells["NYUKIN_DENPYOU__SEQ"].Value);
                //明細
                this.logic.NyuukinDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 出金
        // <summary>
        /// 出金伝票一覧の行選択時 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shukkin_Denpyou_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //システムID
                long systemId = Convert.ToInt64(this.Shukkin_Denpyou.Rows[rowIndex].Cells["SHUKIN_DENPYOU_SYSTEM_ID"].Value);
                //SEQ
                int seq = Convert.ToInt32(this.Shukkin_Denpyou.Rows[rowIndex].Cells["SHUKIN_DENPYOU_SEQ"].Value);
                //明細
                this.logic.ShukkinDetailDate_Select(systemId, seq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #region 代納
        private void MultiRow_DaiNouDenpyou_RowEnter(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                //選択行
                int rowIndex = e.RowIndex;
                //支払
                //システムID
                long systemIdUkeire = Convert.ToInt64(this.MultiRow_DaiNouDenpyou.Rows[rowIndex].Cells["UKEIRE_SYSTEM_ID"].Value);
                //SEQ
                int seqUkeire = Convert.ToInt32(this.MultiRow_DaiNouDenpyou.Rows[rowIndex].Cells["UKEIRE_SEQ"].Value);
                //システムID
                //売上
                long systemIdShukka = Convert.ToInt64(this.MultiRow_DaiNouDenpyou.Rows[rowIndex].Cells["SHUKKA_SYSTEM_ID"].Value);;
                //SEQ
                int seqShukka = Convert.ToInt32(this.MultiRow_DaiNouDenpyou.Rows[rowIndex].Cells["SHUKKA_SEQ"].Value);
                //売上/支払明細
                this.logic.DainouDetailDate_Select(systemIdUkeire, seqUkeire, systemIdShukka, seqShukka);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            LogUtility.DebugMethodEnd(sender, e);
        }
        #endregion
        #endregion

        #region 伝票ダブルクリック処理
        #region 受付
        private void Uketsuke_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Uketsuke_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void Uketsuke_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            string gamenKbn = this.Uketsuke_Denpyou.Rows[this.Uketsuke_Denpyou.CurrentRow.Index].Cells["UKETUKE_DENPYOU_UKETSUKE_KBN"].Value.ToString();
            No = this.Uketsuke_Denpyou.Rows[this.Uketsuke_Denpyou.CurrentRow.Index].Cells["UKETUKE_DENPYOU_UKETUKE_NO"].Value.ToString();
            SEQ = this.Uketsuke_Denpyou.Rows[this.Uketsuke_Denpyou.CurrentRow.Index].Cells["UKETUKE_DENPYOU_SEQ"].Value.ToString();
            //FormManager.OpenForm("G459", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No);
            //画面遷移
            switch (gamenKbn)
            {
                case "収集":
                    //受付（収集）入力
                    FormManager.OpenFormWithAuth("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, SEQ);
                    break;
                case "出荷":
                    //受付（出荷）入力
                    FormManager.OpenFormWithAuth("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, SEQ);
                    break;
                case "持込":
                    //受付（持込）入力
                    FormManager.OpenFormWithAuth("G018", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, SEQ);
                    break;
            }
        }
        #endregion
        #region 計量
        //private void Keiryou_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    if (e.RowIndex >= 0)
        //    {
        //        Keiryou_Denpyou_OpenForm();
        //    }

        //    LogUtility.DebugMethodEnd();
        //}
        //internal void Keiryou_Denpyou_OpenForm()
        //{
        //    String No = "";
        //    String SEQ = "";
        //    No = this.Keiryou_Denpyou.Rows[this.Keiryou_Denpyou.CurrentRow.Index].Cells["KEIRYOU_DENPYOU_KEIRYOU_NO"].Value.ToString();
        //    SEQ = this.Keiryou_Denpyou.Rows[this.Keiryou_Denpyou.CurrentRow.Index].Cells["KEIRYOU_DENPYOU_SEQ"].Value.ToString();
        //    FormManager.OpenFormWithAuth("G045", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, null, null, SEQ);
        //}
        #endregion
        #region 受入
        private void Ukeire_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Ukeire_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void Ukeire_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            No = this.Ukeire_Denpyou.Rows[this.Ukeire_Denpyou.CurrentRow.Index].Cells["UKEIRE_DENPYOU_UKEIRE_NO"].Value.ToString();
            SEQ = this.Ukeire_Denpyou.Rows[this.Ukeire_Denpyou.CurrentRow.Index].Cells["UKEIRE_DENPYOU_SEQ"].Value.ToString();
            if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
            {
                FormManager.OpenFormWithAuth("G051", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, null, null, null, SEQ);
            }
            else
            {
                FormManager.OpenFormWithAuth("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, null, null, null, SEQ);
            }
        }
        #endregion
        #region 出荷
        private void Shukka_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Shukka_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void Shukka_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            No = this.Shukka_Denpyou.Rows[this.Shukka_Denpyou.CurrentRow.Index].Cells["SHUKKA_DENPYOU_SHUKKA_NO"].Value.ToString();
            SEQ = this.Shukka_Denpyou.Rows[this.Shukka_Denpyou.CurrentRow.Index].Cells["SHUKKA_DENPYOU_SEQ"].Value.ToString();
            if (this.logic.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
            {
                FormManager.OpenFormWithAuth("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, null, null, null, SEQ);
            }
            else
            {
                FormManager.OpenFormWithAuth("G722", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, null, null, null, SEQ);
            }
        }
        #endregion
        #region 売上/支払
        private void UriageShiharai_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                UriageShiharai_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void UriageShiharai_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            String SYSTEM_ID = "";
            No = this.UriageShiharai_Denpyou.Rows[this.UriageShiharai_Denpyou.CurrentRow.Index].Cells["URIAGESIHARAI_NO"].Value.ToString();
            SEQ = this.UriageShiharai_Denpyou.Rows[this.UriageShiharai_Denpyou.CurrentRow.Index].Cells["URIAGESIHARAI_DENPYOU_SEQ"].Value.ToString();
            SYSTEM_ID = this.UriageShiharai_Denpyou.Rows[this.UriageShiharai_Denpyou.CurrentRow.Index].Cells["URIAGESIHARAI_DENPYOU_SYSTEM_ID"].Value.ToString();
            // 20150602 代納伝票対応(代納不具合一覧52) Start
            T_UR_SH_ENTRY data = new T_UR_SH_ENTRY();
            data.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            data.SEQ = Convert.ToInt32(SEQ);
            data = this.logic.UrShDao.GetUrShEntry(data).FirstOrDefault();
            if (data != null)
            {
                if (data.DAINOU_FLG.IsNull || data.DAINOU_FLG.IsFalse)
                {
                    FormManager.OpenFormWithAuth("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, SEQ);
                }
                else
                {
                    FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, SEQ);
                }
            }
            // 20150602 代納伝票対応(代納不具合一覧52) End
        }
        #endregion
        #region 入金
        /// <summary>
        /// 入金伝票ダブルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DataGridViewCellMouseEventArgs</param>
        private void Nyuukin_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Nyuukin_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void Nyuukin_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            String SYSTEM_ID = "";
            No = this.Nyuukin_Denpyou.Rows[this.Nyuukin_Denpyou.CurrentRow.Index].Cells["NYUKIN_DENPYOU_NYUUKIN_NUMBER"].Value.ToString();
            SEQ = this.Nyuukin_Denpyou.Rows[this.Nyuukin_Denpyou.CurrentRow.Index].Cells["NYUKIN_DENPYOU__SEQ"].Value.ToString();
            SYSTEM_ID = this.Nyuukin_Denpyou.Rows[this.Nyuukin_Denpyou.CurrentRow.Index].Cells["NYUKIN_DENPYOU__SYSTEM_ID"].Value.ToString();
            T_NYUUKIN_ENTRY data = new T_NYUUKIN_ENTRY();
            data.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            data.SEQ = Convert.ToInt32(SEQ);
            data = this.logic.NyuukinDao.GetNyuukinEntry(data).FirstOrDefault();
            if (data != null && data.TOK_INPUT_KBN.IsFalse)
            {
                FormManager.OpenFormWithAuth("G619", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, SEQ);
            }
            else
            {
                FormManager.OpenFormWithAuth("G459", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, SEQ);
            }

        }
        #endregion
        #region 出金
        /// <summary>
        /// 出金伝票ダブルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DataGridViewCellMouseEventArgs</param>
        private void Shukkin_Denpyou_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Shukkin_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }
        internal void Shukkin_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            No = this.Shukkin_Denpyou.Rows[this.Shukkin_Denpyou.CurrentRow.Index].Cells["SHUKIN_DENPYOU_SHUKKIN_NUMBER"].Value.ToString();
            SEQ = this.Shukkin_Denpyou.Rows[this.Shukkin_Denpyou.CurrentRow.Index].Cells["SHUKIN_DENPYOU_SEQ"].Value.ToString();
            FormManager.OpenFormWithAuth("G090", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, SEQ);
        }
        #endregion
        #region 代納
        private void MultiRow_DaiNouDenpyou_CellMouseDoubleClick(object sender, GrapeCity.Win.MultiRow.CellMouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.RowIndex >= 0)
            {
                Dainou_Denpyou_OpenForm();
            }

            LogUtility.DebugMethodEnd();
        }

        internal void Dainou_Denpyou_OpenForm()
        {
            String No = "";
            String SEQ = "";
            String SYSTEM_ID = "";
            No = this.MultiRow_DaiNouDenpyou.Rows[this.MultiRow_DaiNouDenpyou.CurrentRow.Index].Cells["DENPYOU_NUMBER"].Value.ToString();
            SEQ = this.MultiRow_DaiNouDenpyou.Rows[this.MultiRow_DaiNouDenpyou.CurrentRow.Index].Cells["SEQ"].Value.ToString();
            SYSTEM_ID = this.MultiRow_DaiNouDenpyou.Rows[this.MultiRow_DaiNouDenpyou.CurrentRow.Index].Cells["SYSTEM_ID"].Value.ToString();
            // 20150602 代納伝票対応(代納不具合一覧52) Start
            T_UR_SH_ENTRY data = new T_UR_SH_ENTRY();
            data.SYSTEM_ID = Convert.ToInt64(SYSTEM_ID);
            data.SEQ = Convert.ToInt32(SEQ);
            data = this.logic.UrShDao.GetUrShEntry(data).FirstOrDefault();
            if (data != null)
            {
                if (!data.DAINOU_FLG.IsNull && data.DAINOU_FLG.IsTrue)
                {
                    FormManager.OpenFormWithAuth("G161", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No, null, null, null, null, null, SEQ);
                }
            }
            // 20150602 代納伝票対応(代納不具合一覧52) End
        }
        #endregion
        #endregion

        #region (削除：連結しない）セル連結イベント

        ///// <summary>
        ///// ヘッダセル結合処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void customDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    //try
        //    //{
        //    // レスポンス悪くなるので、コメントアウト
        //    //LogUtility.DebugMethodStart(sender, e);

        //    // グリッド取得
        //    DataGridView dgv = (DataGridView)sender;

        //    // グリッド、またはヘッダー以外は処理なし
        //    if (dgv == null || e.RowIndex > -1)
        //    {
        //        return;
        //    }


        //    // 1列目、３列目、５列目をそれぞれ横の列とくっつける
        //    if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 4)
        //    {
        //        // セルの矩形を取得
        //        Rectangle rect = e.CellBounds;

        //        // 横幅調整(右の列幅を足す）
        //        rect.Width += dgv.Columns[e.ColumnIndex+1].Width;
        //        rect.Y = e.CellBounds.Y + 1;

        //        // 背景、枠線、セルの値を描画
        //        using (SolidBrush brush = new SolidBrush(dgv.ColumnHeadersDefaultCellStyle.BackColor))
        //        {
        //            // 背景の描画
        //            e.Graphics.FillRectangle(brush, rect);

        //            using (Pen pen = new Pen(dgv.GridColor))
        //            {
        //                // 枠線の描画
        //                e.Graphics.DrawRectangle(pen, rect);
        //            }
        //        }

        //        // セルに表示するテキストを描画
        //        string displayString = dgv.Columns[e.ColumnIndex + 1].HeaderText;
        //        TextRenderer.DrawText(e.Graphics,
        //                        displayString,
        //                        e.CellStyle.Font,
        //                        rect,
        //                        e.CellStyle.ForeColor,
        //                        TextFormatFlags.HorizontalCenter
        //                        | TextFormatFlags.VerticalCenter);
        //    }
        //    else if( e.ColumnIndex >= 6 )
        //    {
        //        // 結合セル以外は既定の描画を行う
        //        e.Paint(e.ClipBounds, e.PaintParts);
        //    }

        //    // イベントハンドラ内で処理を行ったことを通知
        //    e.Handled = true;

        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // レスポンス悪くなるので、コメントアウト
        //    //    //// 例外エラー
        //    //    //LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
        //    //    //throw;
        //    //}
        //    //finally
        //    //{
        //    //    // レスポンス悪くなるので、コメントアウト
        //    //    //LogUtility.DebugMethodEnd();
        //    //}
        //}

        #endregion

        /// <summary>
        /// ポップアップ後のフォーカス処理
        /// </summary>
        public void PopUpAfterFocus()
        {
            Control cControl = this.ActiveControl;
            if (!string.IsNullOrEmpty(cControl.Text))
            {
                SendKeys.Send("{Enter}");
            }
        }
    }
}
