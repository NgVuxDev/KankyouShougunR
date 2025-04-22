using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using MasterCommon.Utility;
using System.Drawing;
using Shougun.Core.Master.KobetsuHimeiTankaUpdate.Logic;
using Shougun.Core.Common.IchiranCommon.APP;
using NyuukinsakiIchiran.APP;

namespace Shougun.Core.Master.KobetsuHimeiTankaUpdate.APP
{
    /// <summary>
    /// ○○入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// ヘッダーオブジェクト
        /// </summary>
        internal HeaderForm headerForm;

        /// <summary>
        /// 共通情報(SysInfoなど)
        /// </summary>
        public CommonInformation CommInfo { get; private set; }

        /// <summary>
        ///
        /// </summary>
        internal BusinessBaseForm BaseForm { get; private set; }

        /// <summary>
        ///
        /// </summary>
        private string prevZaikoHinmeiCd = string.Empty;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyoushaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        internal string beforGenbaCD = string.Empty;

        /// <summary>
        /// 前回荷降業者コード
        /// </summary>
        public string beforNioroshiGyoushaCD = string.Empty;

        private string preNioroshiGyoushaCd { get; set; }
        private string curNioroshiGyoushaCd { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        internal string xinTanka = string.Empty;

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        internal MessageBoxShowLogic msglogic = new MessageBoxShowLogic();

        /// <summary>
        ///
        /// </summary>
        private bool isInitialFlg = false;

        #region 画面初期処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_KOBETSU_HUNNMETANNKA_YIKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            this.InitializeComponent();           
            
        }

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

                this.BaseForm = this.Parent as BusinessBaseForm;
                // ヘッダフォームを取得
                this.headerForm = (HeaderForm)((BusinessBaseForm)this.ParentForm).headerForm;

                if (!this.logic.WindowInit())
                {
                    return;
                }

                if (this.Ichiran != null)
                {
                    this.headerForm.ReadDataNumber.Text = this.Ichiran.Rows.Count.ToString();
                }
                else
                {
                    this.headerForm.ReadDataNumber.Text = "0";
                }

                //this.InitiSearch(null, e);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面初回表出処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!this.isInitialFlg)
            {
                this.Height -= 7;
                this.isInitialFlg = true;
            }
            base.OnShown(e);
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            curGyoushaCd = this.GYOUSHA_CD.Text;
            if (preGyoushaCd != curGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        #endregion

       

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.ClearCondition();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.logic.TankaCheck())
                {
                    return;
                }

                if (this.logic.DateStrarCheck())
                {
                    return;
                }

                if (this.logic.DateEndCheck())
                {
                    return;
                }

                if (codeCheck())
                {
                    int count = this.logic.Search();
                    if (count == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("C001");
                        DataTable dt = (DataTable)this.Ichiran.DataSource;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.Rows.Clear();
                        }

                        this.checkBoxAll.Checked = false;
                        this.Ichiran.DataSource = dt; 
                    }
                    else if (count > 0)
                    {
                        // アラート件数を設定
                        this.logic.AlertCount = this.logic.GetAlertCount();

                        if (this.logic.CreateDataGridView(this.logic.SearchResult))
                        {
                            if (this.logic.SetIchiran())
                            {
                                return;
                            }
                        }
                        else
                        {
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("C001");
                            this.headerForm.ReadDataNumber.Text = "0";
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                this.checkBoxAll.Checked = false;

                this.logic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初期検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void InitiSearch(object sender, EventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    try
        //    {
        //        if (codeCheck())
        //        {
        //            // アラート件数を設定し、検索実行
        //            this.headerForm.AlertNumber.Text = this.logic.GetAlertCount().ToString();

        //            int count = this.logic.Search();
        //            if (count == 0)
        //            {
        //                DataTable dt = (DataTable)this.Ichiran.DataSource;
        //                if (dt != null && dt.Rows.Count > 0)
        //                {
        //                    dt.Rows.Clear();
        //                }

        //                this.Ichiran.DataSource = dt;
        //            }
        //            else if (count > 0)
        //            {
        //                if (this.logic.SetIchiran())
        //                {
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("Search", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}

        /// <summary>
        /// 一括入力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void TikkatsuNyuuryoku(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (TikkatsuNyuuryokuCheck())
                {                   
                    int row = this.Ichiran.Rows.Count;
                    for (int i = 0; i < row; i++)
                    {
                        if (this.KOSHINNHOUHOU_VALUE.Text == "1")
                        {
                            if (this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null
                            && !string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString())
                            && bool.Parse(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                            {
                                this.Ichiran.Rows[i].Cells["XIN_TANKA"].Value = this.IKKATSU_TANNKA.Text;
                                this.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value = this.TANK_TEKIYOU.Value;
                            }
                        }
                        else if (this.KOSHINNHOUHOU_VALUE.Text == "2")
                        {
                            if (this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null
                           && !string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString())
                           && bool.Parse(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                            {
                                decimal utsutsuTanka = 0;

                                if (this.Ichiran.Rows[i].Cells["UTSUTSU_TANKA"].Value != null)
                                {
                                    utsutsuTanka = Convert.ToDecimal(this.Ichiran.Rows[i].Cells["UTSUTSU_TANKA"].Value);
                                }

                                this.Ichiran.Rows[i].Cells["XIN_TANKA"].Value = Convert.ToDecimal(this.IKKATSU_TANNKA.Text) + utsutsuTanka;
                                this.Ichiran.Rows[i].Cells["XIN_TEKIYOU_BEGIN"].Value = this.TANK_TEKIYOU.Value;
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索部CODEのチェック処理
        /// </summary>
        /// <param name="syoriKbn">処理区分</param>
        /// <returns>チェック結果</returns>
        private bool codeCheck()
        {
            try
            {
                var messageShowLogic = new MessageBoxShowLogic();

                // 伝票を必須入力項目から任意入力とし
                if (string.IsNullOrEmpty(this.DenpyouKubun.Text))
                {   
                    this.DenpyouKubun.Focus();
                    messageShowLogic.MessageBoxShow("E001", "伝票");

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("codeCheck", ex);
                this.msglogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 一括入力のチェック処理
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool TikkatsuNyuuryokuCheck()
        {
            try
            {
                var messageShowLogic = new MessageBoxShowLogic();


                if (this.Ichiran.RowCount <= 0)
                {
                    messageShowLogic.MessageBoxShow("E051", "一括入力する単価データ");
                    return false;
                }

                int row = this.Ichiran.Rows.Count;
                int count = 0;
                for (int i = 0; i < row; i++)
                {
                    if (this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value != null
                            && !string.IsNullOrEmpty(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString())
                            && bool.Parse(this.Ichiran.Rows[i].Cells["CHECK_BOX"].Value.ToString()))
                    {
                        count++;
                    }
                }

                if (count <= 0)
                {
                    messageShowLogic.MessageBoxShow("E051", "一括入力する単価データ");
                    return false;
                }

                // 新単価を必須入力項目から任意入力とし
                if (string.IsNullOrEmpty(this.IKKATSU_TANNKA.Text))
                {
                    this.IKKATSU_TANNKA.Focus();
                    messageShowLogic.MessageBoxShow("E001", "新単価");

                    return false;
                }

                // 単価適用開始日を必須入力項目から任意入力とし
                if (string.IsNullOrEmpty(this.TANK_TEKIYOU.Text))
                {
                    this.TANK_TEKIYOU.Focus();
                    messageShowLogic.MessageBoxShow("E001", "単価適用開始日");

                    return false;
                }

                // 単価適用開始日を必須入力項目から任意入力とし
                if (string.IsNullOrEmpty(this.KOSHINNHOUHOU_VALUE.Text))
                {
                    this.KOSHINNHOUHOU_VALUE.Focus();
                    messageShowLogic.MessageBoxShow("E001", "更新方法");

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TikkatsuNyuuryokuCheck", ex);
                this.msglogic.MessageBoxShow("E245", "");
                return false;
            }
        }

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

                bool catchErr = false;

                if (!this.logic.RegistCheck())
                {                  
                    return;
                }
                catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(false);

                if(!this.logic.isRegist)
                {
                    return;
                }

                if(this.logic.entitysFlg)
                {
                    int count = this.logic.Search();

                    this.checkBoxAll.Checked = false;

                    if (count == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("C001");
                        DataTable dt = (DataTable)this.Ichiran.DataSource;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dt.Rows.Clear();
                        }

                        this.Ichiran.DataSource = dt;
                    }
                    else if (count > 0)
                    {
                        if (this.logic.SetIchiran()) { return; }
                    }
                    else
                    {
                        return;
                    }
                }  
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (!this.logic.Cancel())
                {
                    return;
                }
                this.DenpyouKubun.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 閉じる処理
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
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者 NioroshiGyoushaBtnPopupBeforeMethod
        /// </summary>
        public void NioroshiGyoushaBtnPopupBeforeMethod()
        {
            preNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 NioroshiGyoushaBtnPopupMethod
        /// </summary>
        public void NioroshiGyoushaBtnPopupMethod()
        {
            curNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            if (preNioroshiGyoushaCd != curNioroshiGyoushaCd)
            {
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        ///　運搬名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            }

            //  運搬名称の取得
            this.logic.SearchUnpanGyoushaName(e);
        }

        /// <summary>
        /// 荷降先業者名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }

            // 荷降先名称の取得
            this.logic.SearchNioroshiGyoushaName(e);
        }

        /// <summary>
        /// 荷降先現場名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
            }

            // 荷降先現場名称の取得
            this.logic.SearchNioroshiGenbaName(e);
        }

        /// <summary>
        /// 品名名称設定処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HINMEI_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.HINMEI_CD.Text))
            {
                this.HINMEI_NAME.Text = string.Empty;
            }

            // 品名名称の取得
            this.logic.SearchHinmeiName(e);
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxAll.Focus();
            if (this.Ichiran.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {
                row.Cells[0].Value = checkBoxAll.Checked;
            }
        }

        private void checkBoxAll2_Click(object sender, EventArgs e)
        {
            if (this.checkBoxAll.Checked)
            {
                this.checkBoxAll.Checked = false;
            }
            else
            {
                this.checkBoxAll.Checked = true;
            }
        }

        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (String.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = String.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = string.Empty;
            }
            else if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 現場CDValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_RNAME.Text = string.Empty;
                return;
            }

            var msgLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                msgLogic.MessageBoxShow("E051", "業者");
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_CD.Focus();
                return;
            }

            this.logic.CheckGenba();
        }

        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (this.Ichiran.Rows[this.Ichiran.CurrentRow.Index].Cells["XIN_TANKA"].Value != null
                && !string.IsNullOrEmpty(this.Ichiran.Rows[this.Ichiran.CurrentRow.Index].Cells["XIN_TANKA"].Value.ToString()))
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["CHECK_BOX"].Value = true;
                }
                else
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["CHECK_BOX"].Value = false;
                    this.Ichiran.Rows[this.Ichiran.CurrentRow.Index].Cells["XIN_TANKA"].Value = "";
                }
            }

            this.logic.setTankaAndTekiyouData();
        }

        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string name = this.Ichiran.Columns[e.ColumnIndex].Name;
            if (name.Equals("MEISAI_BIKOU"))
            {
                this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
            else
            {
                this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            }
        }
    }
}