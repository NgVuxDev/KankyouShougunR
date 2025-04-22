// $Id$
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using ItakuKeiyakushoIchiran.Logic;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;
using System.ComponentModel;

namespace ItakuKeiyakushoIchiran.APP
{
    public partial class ItakuKeiyakushoIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass businessLogic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        public string beforeGenbaCD = string.Empty;

        /// <summary>
        /// 前回運搬業者コード
        /// </summary>
        public string beforeUnpanGyousaCD = string.Empty;

        /// <summary>
        /// 前回処分受託者（処分）コード
        /// </summary>
        public string beforeShobunJyutakushaShobunCD = string.Empty;

        /// <summary>
        /// 前回処分事業場コード
        /// </summary>
        public string beforeShobunGenbaCD = string.Empty;

        /// <summary>
        /// 前回処分受託者（最終）コード
        /// </summary>
        public string beforeShobunJyutakushaSaishuCD = string.Empty;

        /// <summary>
        /// 前回最終処分場コード
        /// </summary>
        public string beforeSaishuShobunCD = string.Empty;

        /// <summary>
        /// 前回運搬業者（積替）コード
        /// </summary>
        public string beforeUnpanTsumikaeCD = string.Empty;

        /// <summary>
        /// 前回積替保管場所コード
        /// </summary>
        public string beforeTsumikaehokanCD = string.Empty;

        /// <summary>
        /// 前回処分方法（処分）コード
        /// </summary>
        public string beforeShobunHouhouShobunCD = string.Empty;

        /// <summary>
        /// 前回処分方法（最終）コード
        /// </summary>
        public string beforeShobunHouhouSaishuCD = string.Empty;

        /// <summary>
        /// 前回報告書分類コード
        /// </summary>
        public string beforeHoukokushoBunruiCD = string.Empty;

        /// <summary>
        /// 前回営業担当者コード
        /// </summary>
        public string beforeEigyouTantouCD = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public ItakuKeiyakushoIchiranForm(DENSHU_KBN denshuKbn)
            : base(denshuKbn)
        {
            InitializeComponent();
            this.logic.SettingAssembly = Assembly.GetExecutingAssembly();
            this.ShainCd = SystemProperty.Shain.CD; // 社員CDを取得すること
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 初回のみ
            if (this.businessLogic == null)
            {
                // ビジネスロジックの初期化
                this.businessLogic = new LogicClass(this);

                // 画面初期化
                bool catchErr = this.businessLogic.WindowInit();
                if (catchErr)
                {
                    return;
                }

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                // 非表示にする列名を登録
                this.SetHiddenColumns(this.businessLogic.KEY_ID1, this.businessLogic.KEY_ID2, 
                    this.businessLogic.KEY_ID3, this.businessLogic.KEY_ID4, this.businessLogic.KEY_ID5, 
                    this.businessLogic.KEY_ID6, this.businessLogic.KEY_ID7, this.businessLogic.KEY_ID8,
                    this.businessLogic.KEY_ID9, this.businessLogic.KEY_ID10, this.businessLogic.KEY_ID11);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }

            // フィルタ表示
            this.customSearchHeader1.Visible = true;
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 200);
            this.customSearchHeader1.Size = new System.Drawing.Size(997, 25);

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            this.PatternReload();

            // パターンヘッダのみ表示
            this.ShowData();
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
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual bool ShowData()
        {
            try
            {
                if (!this.DesignMode)
                {
                    DialogResult dlgResult = System.Windows.Forms.DialogResult.Yes;

                    // アラート件数を設定
                    this.logic.AlertCount = this.businessLogic.GetAlertCount();

                    if (dlgResult == DialogResult.Yes && this.Table != null && this.PatternNo != 0)
                    {
                        // 明細に表示
                        this.logic.CreateDataGridView(this.Table);

                        // 検索件数を設定し、画面に表示
                        var parentForm = base.Parent;
                        var readDataNumber = (TextBox)controlUtil.FindControl(parentForm, "ReadDataNumber");
                        if (readDataNumber != null)
                        {
                            readDataNumber.Text = this.Table.Rows.Count.ToString();
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowData", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 日付選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_SELECT_TextChanged(object sender, EventArgs e)
            {
            if (this.DATE_SELECT.Text != "8")
            {
                this.DATE_FROM.Enabled = true;
                this.DATE_TO.Enabled = true;
            }
            switch (this.DATE_SELECT.Text)
            {
                case "1":
                    this.labelInfo.Text = "作成日";
                    this.DATE_FROM.Tag = "[作成日]開始日を入力してください";
                    this.DATE_TO.Tag = "[作成日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[作成日]開始日";
                    this.DATE_TO.DisplayItemName = "[作成日]終了日";
                    break;
                case "2":
                    this.labelInfo.Text = "送付日";
                    this.DATE_FROM.Tag = "[送付日]開始日を入力してください";
                    this.DATE_TO.Tag = "[送付日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[送付日]開始日";
                    this.DATE_TO.DisplayItemName = "[送付日]終了日";
                    break;
                case "3":
                    this.labelInfo.Text = "返送日";
                    this.DATE_FROM.Tag = "[返送日]開始日を入力してください";
                    this.DATE_TO.Tag = "[返送日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[返送日]開始日";
                    this.DATE_TO.DisplayItemName = "[返送日]終了日";
                    break;
                case "4":
                    this.labelInfo.Text = "保管日";
                    this.DATE_FROM.Tag = "[保管日]開始日を入力してください";
                    this.DATE_TO.Tag = "[保管日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[保管日]開始日";
                    this.DATE_TO.DisplayItemName = "[保管日]終了日";
                    break;
                case "5":
                    this.labelInfo.Text = "有効期間開始";
                    this.DATE_FROM.Tag = "[有効期間開始]開始日を入力してください";
                    this.DATE_TO.Tag = "[有効期間開始]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[有効期間開始]開始日";
                    this.DATE_TO.DisplayItemName = "[有効期間開始]終了日";
                    break;
                case "6":
                    this.labelInfo.Text = "有効期間終了";
                    this.DATE_FROM.Tag = "[有効期間終了]開始日を入力してください";
                    this.DATE_TO.Tag = "[有効期間終了]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[有効期間終了]開始日";
                    this.DATE_TO.DisplayItemName = "[有効期間終了]終了日";
                    break;
                case "7":
                    this.labelInfo.Text = "自動更新終了日";
                    this.DATE_FROM.Tag = "[自動更新終了日]開始日を入力してください";
                    this.DATE_TO.Tag = "[自動更新終了日]終了日を入力してください";
                    this.DATE_FROM.DisplayItemName = "[自動更新終了日]開始日";
                    this.DATE_TO.DisplayItemName = "[自動更新終了日]終了日";
                    break;
                case "8":
                    this.labelInfo.Text = "日付なし";
                    this.DATE_FROM.Tag = string.Empty;
                    this.DATE_TO.Tag = string.Empty;
                    this.DATE_FROM.DisplayItemName = string.Empty;
                    this.DATE_TO.DisplayItemName = string.Empty;
                    this.DATE_FROM.Value = null;
                    this.DATE_TO.Value = null;
                    this.DATE_FROM.Enabled = false;
                    this.DATE_TO.Enabled = false;

                    break;
            }
        }

        /// <summary>
        /// 初期日付設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_FROM_Enter(object sender, EventArgs e)
        {
            if (this.DATE_FROM.Value == null)
            {
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //this.DATE_FROM.Value = DateTime.Today;
                this.DATE_FROM.Value = this.businessLogic.parentForm.sysDate.Date;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        /// <summary>
        /// 初期日付設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_TO_Enter(object sender, EventArgs e)
        {
            if (this.DATE_TO.Value == null)
            {
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //this.DATE_TO.Value = DateTime.Today;
                this.DATE_TO.Value = this.businessLogic.parentForm.sysDate.Date;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
            }
        }

        // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 start
        private void DATE_FROM_Leave(object sender, EventArgs e)
        {
            //this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void DATE_TO_Leave(object sender, EventArgs e)
        {
            //this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141028 「From　>　To」のアラート表示タイミング変更 end
   
        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.GYOUSHA_CD.Text = this.GYOUSHA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforGyousaCD == this.GYOUSHA_CD.Text)
            {
                return;
            }

            this.GENBA_CD.Text = string.Empty;
            this.GENBA_NAME_RYAKU.Text = string.Empty;

            this.businessLogic.CheckGyousha();
        }

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.GENBA_CD.Text = this.GENBA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeGenbaCD == this.GENBA_CD.Text)
            {
                return;
            }

            this.businessLogic.CheckGenba();
        }

        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                // 表示条件保存
                this.businessLogic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                // 画面が閉じれなくなるのでログのみ
                LogUtility.Fatal("OnClosing", ex);
            }
        }

        /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　start
        private void KeiyakushoShuruiCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.businessLogic.CheckListPopup(1);
            }
        }

        private void Keiyakusho_MouseClick(object sender, MouseEventArgs e)
        {
            this.businessLogic.CheckListPopup(1);
        }

        private void Keiyaku_Jyoukyou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.businessLogic.CheckListPopup(2);
            }
        }

        private void Jyoukyou_MouseClick(object sender, MouseEventArgs e)
        {
            this.businessLogic.CheckListPopup(2);
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPANGYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.UNPANGYOUSHA_CD.Text))
            {
                this.UNPANGYOUSHA_NAME.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.UNPANGYOUSHA_CD.Text = this.UNPANGYOUSHA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeUnpanGyousaCD == this.UNPANGYOUSHA_CD.Text)
            {
                return;
            }

            try
            {
                this.businessLogic.CheckUnpanGyousha();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }
        
        /// <summary>
        /// 処分受託者（処分）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_CD_Validating(object sender, CancelEventArgs e)
        {
            this.SetShobunJyutakusha();
        }

        /// <summary>
        /// 処分受託者設定
        /// </summary>
        public void SetShobunJyutakusha()
        {
            try
            {
                // 番号が削除された場合
                if (string.IsNullOrEmpty(this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text))
                {
                    this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Text = string.Empty;
                    this.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
                    return;
                }

                //フォーマット6桁
                this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text.PadLeft(6, '0');

                // 番号が変更されていない場合、処理しない
                if (this.beforeShobunJyutakushaShobunCD == this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
                {
                    return;
                }
                else
                {
                    //業者が変わったので現場クリア
                    this.SHOBUN_GENBA_CD.Text = string.Empty;
                    this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
                }

                this.businessLogic.ShobunJyutakushaShobunCD();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetShobunJyutakusha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShobunJyutakusha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return;
            }
        }

        /// <summary>
        /// 処分事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_GENBA_CD_Validating(object sender, CancelEventArgs e)
        {
            //フォーマット6桁
            if (string.IsNullOrEmpty(this.SHOBUN_GENBA_CD.Text))
            {
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
                return;
            }

            this.SHOBUN_GENBA_CD.Text = this.SHOBUN_GENBA_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunGenbaCD == this.SHOBUN_GENBA_CD.Text)
            {
                return;
            }

            try
            {
                this.businessLogic.CheckShobunGenba();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 処分受託者（最終）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_CD_Validating(object sender, CancelEventArgs e)
        {
            this.SetSaishuShobunJyutakusha();
        }

        /// <summary>
        /// 最終処分受託者設定
        /// </summary>
        public void SetSaishuShobunJyutakusha()
        {
            try
            {
                // 番号が削除された場合
                if (string.IsNullOrEmpty(this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text))
                {
                    this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Text = string.Empty;
                    this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
                    return;
                }

                //フォーマット6桁
                this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text.PadLeft(6, '0');

                // 番号が変更されていない場合、処理しない
                if (this.beforeShobunJyutakushaSaishuCD == this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
                {
                    return;
                }
                else
                {
                    //業者が変わったので現場クリア
                    this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                    this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
                }

                this.businessLogic.ShobunJyutakushaSaishuCD();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetSaishuShobunJyutakusha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSaishuShobunJyutakusha", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return;
            }
        }

        /// <summary>
        /// 最終処分場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SAISHUU_SHOBUNJOU_CD_Validating(object sender, CancelEventArgs e)
        {
            //フォーマット6桁
            if (string.IsNullOrEmpty(this.SAISHUU_SHOBUNJOU_CD.Text))
            {
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
                return;
            }

            this.SAISHUU_SHOBUNJOU_CD.Text = this.SAISHUU_SHOBUNJOU_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeSaishuShobunCD == this.SAISHUU_SHOBUNJOU_CD.Text)
            {
                return;
            }

            try
            {
                this.businessLogic.CheckSaishuGenba();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 運搬業者（積替）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPAN_TSUMIKAE_CD_Validating(object sender, CancelEventArgs e)
        {
            this.SetUnpanTsumikae();
        }

        /// <summary>
        /// 運搬業者（積替）設定
        /// </summary>
        public void SetUnpanTsumikae()
        {
            try
            {
                // 番号が削除された場合
                if (string.IsNullOrEmpty(this.UNPAN_TSUMIKAE_CD.Text))
                {
                    this.UNPAN_TSUMIKAE_NAME.Text = string.Empty;
                    this.TSUMIKAEHOKAN_CD.Text = string.Empty;
                    this.TSUMIKAEHOKAN_NAME.Text = string.Empty;
                    return;
                }

                //フォーマット6桁
                this.UNPAN_TSUMIKAE_CD.Text = this.UNPAN_TSUMIKAE_CD.Text.PadLeft(6, '0');

                // 番号が変更されていない場合、処理しない
                if (this.beforeUnpanTsumikaeCD == this.UNPAN_TSUMIKAE_CD.Text)
                {
                    return;
                }
                else
                {
                    //業者が変わったので現場クリア
                    this.TSUMIKAEHOKAN_CD.Text = string.Empty;
                    this.TSUMIKAEHOKAN_NAME.Text = string.Empty;
                }

                try
                {
                    this.businessLogic.CheckUnpanTsumikaeCd();
                }
                catch (Exception ex)
                {
                    LogUtility.Fatal(ex);
                    throw ex;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetUnpanTsumikae", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetUnpanTsumikae", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 積替保管場所更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TSUMIKAEHOKAN_CD_Validating(object sender, CancelEventArgs e)
        {
            //フォーマット6桁
            if (string.IsNullOrEmpty(this.TSUMIKAEHOKAN_CD.Text))
            {
                this.TSUMIKAEHOKAN_NAME.Text = string.Empty;
                return;
            }

            this.TSUMIKAEHOKAN_CD.Text = this.TSUMIKAEHOKAN_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeTsumikaehokanCD == this.TSUMIKAEHOKAN_CD.Text)
            {
                return;
            }

            try
            {
                this.businessLogic.CheckTsumikaehokan();
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 処分方法（処分）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_HOUHOU_SHOBUN_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.SHOBUN_HOUHOU_SHOBUN_CD.Text))
            {
                this.beforeShobunHouhouShobunCD = string.Empty;

                this.SHOBUN_HOUHOU_SHOBUN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.SHOBUN_HOUHOU_SHOBUN_CD.Text = this.SHOBUN_HOUHOU_SHOBUN_CD.Text.PadLeft(3, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunHouhouShobunCD == this.SHOBUN_HOUHOU_SHOBUN_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
            }

            try
            {
                this.beforeShobunHouhouShobunCD = this.SHOBUN_HOUHOU_SHOBUN_CD.Text;
                if (!this.businessLogic.CheckShobunHouhouShobun())
                {
                    this.beforeShobunHouhouShobunCD = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 処分方法（最終）更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_HOUHOU_SAISHU_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.SHOBUN_HOUHOU_SAISHU_CD.Text))
            {
                this.beforeShobunHouhouSaishuCD = string.Empty;

                this.SHOBUN_HOUHOU_SAISHU_NAME_RYAKU.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.SHOBUN_HOUHOU_SAISHU_CD.Text = this.SHOBUN_HOUHOU_SAISHU_CD.Text.PadLeft(3, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeShobunHouhouSaishuCD == this.SHOBUN_HOUHOU_SAISHU_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
            }

            try
            {
                this.beforeShobunHouhouSaishuCD = this.SHOBUN_HOUHOU_SAISHU_CD.Text;
                if (!this.businessLogic.CheckShobunHouhouSaishu())
                {
                    this.beforeShobunHouhouSaishuCD = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 報告書分類更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HOUKOKUSHO_BUNRUI_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.HOUKOKUSHO_BUNRUI_CD.Text))
            {
                this.beforeHoukokushoBunruiCD = string.Empty;

                this.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.HOUKOKUSHO_BUNRUI_CD.Text = this.HOUKOKUSHO_BUNRUI_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeHoukokushoBunruiCD == this.HOUKOKUSHO_BUNRUI_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
            }

            try
            {
                this.beforeHoukokushoBunruiCD = this.HOUKOKUSHO_BUNRUI_CD.Text;
                if (!this.businessLogic.CheckHoukokushoBunrui())
                {
                    this.beforeHoukokushoBunruiCD = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EIGYOU_TANTOU_CD_Validating(object sender, CancelEventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.EIGYOU_TANTOU_CD.Text))
            {
                this.beforeEigyouTantouCD = string.Empty;

                this.EIGYOU_TANTOU_NAME_RYAKU.Text = string.Empty;
                return;
            }

            //フォーマット6桁
            this.EIGYOU_TANTOU_CD.Text = this.EIGYOU_TANTOU_CD.Text.PadLeft(6, '0');

            // 番号が変更されていない場合、処理しない
            if (this.beforeEigyouTantouCD == this.EIGYOU_TANTOU_CD.Text)
            {
                return;
            }
            else
            {
                //業者が変わったので現場クリア
            }

            try
            {
                this.beforeEigyouTantouCD = this.EIGYOU_TANTOU_CD.Text;
                if (!this.businessLogic.CheckEigyouTantou())
                {
                    this.beforeEigyouTantouCD = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
        }

        private void KEIYAKUSHO_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KEIYAKUSHO_SHURUI_CD.Text))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = string.Empty;
                return;
            }

            string strKeiyakushoShuruiCd = this.KEIYAKUSHO_SHURUI_CD.Text;
            if (strKeiyakushoShuruiCd.Equals("1"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = "収集運搬契約";
            }
            else if(strKeiyakushoShuruiCd.Equals("2"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = "処分契約";
            }
            else if (strKeiyakushoShuruiCd.Equals("3"))
            {
                this.KEIYAKUSHO_SHURUI_NAME.Text = "収集運搬/処分契約";
            }
        }

        private void KEIYAKU_JYOUKYOU_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KEIYAKU_JYOUKYOU_CD.Text))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = string.Empty;
                return;
            }

            string strKeiyakuJyoukyouCd = this.KEIYAKU_JYOUKYOU_CD.Text;
            if (strKeiyakuJyoukyouCd.Equals("1"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = "作成";
            }
            else if (strKeiyakuJyoukyouCd.Equals("2"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = "送付";
            }
            else if (strKeiyakuJyoukyouCd.Equals("3"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = "返送";
            }
            else if (strKeiyakuJyoukyouCd.Equals("4"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = "保管";
            }
            else if (strKeiyakuJyoukyouCd.Equals("5"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = "解約済";
            }
        }

        private void DATE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.DATE_TO.Text = this.DATE_FROM.Text;
        }
        /// 20141210 Houkakou 「委託契約書一覧」検索項目を追加する　end

        /// <summary>
        /// 排出事業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業場
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeGenbaCD = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 運搬業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPANGYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeUnpanGyousaCD = this.UNPANGYOUSHA_CD.Text;
        }

        /// <summary>
        /// 運搬業者(積替)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UNPAN_TSUMIKAE_CD_Enter(object sender, EventArgs e)
        {
            this.beforeUnpanTsumikaeCD = this.UNPAN_TSUMIKAE_CD.Text;
        }

        /// <summary>
        /// 積替保管場所
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TSUMIKAEHOKAN_CD_Enter(object sender, EventArgs e)
        {
            this.beforeTsumikaehokanCD = this.TSUMIKAEHOKAN_CD.Text;
        }

        /// <summary>
        /// 処分受託者（処分）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SHOBUN_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunJyutakushaShobunCD = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
        }

        /// <summary>
        /// 処分事業場
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunGenbaCD = this.SHOBUN_GENBA_CD.Text;
        }

        /// <summary>
        /// 処分受託者（最終）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SHOBUN_JYUTAKUSHA_SAISHU_CD_Enter(object sender, EventArgs e)
        {
            this.beforeShobunJyutakushaSaishuCD = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
        }

        /// <summary>
        /// 処分事業場(最終)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SAISHUU_SHOBUNJOU_CD_Enter(object sender, EventArgs e)
        {
            this.beforeSaishuShobunCD = this.SAISHUU_SHOBUNJOU_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.beforGyousaCD = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.beforGyousaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者(処分) PopupBeforeExecuteMethod
        /// </summary>
        public void SHOBUN_JYUTAKUSHA_SHOBUN_PopupBeforeExecuteMethod()
        {
            this.beforeShobunJyutakushaShobunCD = this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text;
        }

        /// <summary>
        /// 処分受託者(処分) PopupAfterExecuteMethod
        /// </summary>
        public void SHOBUN_JYUTAKUSHA_SHOBUN_PopupAfterExecuteMethod()
        {
            if (this.beforeShobunJyutakushaShobunCD != this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Text)
            {
                this.SHOBUN_GENBA_CD.Text = string.Empty;
                this.SHOBUN_GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者(最終) PopupBeforeExecuteMethod
        /// </summary>
        public void SHOBUN_JYUTAKUSHA_SAISHU_PopupBeforeExecuteMethod()
        {
            this.beforeShobunJyutakushaSaishuCD = this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text;
        }

        /// <summary>
        /// 処分受託者(最終) PopupAfterExecuteMethod
        /// </summary>
        public void SHOBUN_JYUTAKUSHA_SAISHU_PopupAfterExecuteMethod()
        {
            if (this.beforeShobunJyutakushaSaishuCD != this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Text)
            {
                this.SAISHUU_SHOBUNJOU_CD.Text = string.Empty;
                this.SAISHUU_SHOBUNJOU_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 運搬業者(積替) PopupBeforeExecuteMethod
        /// </summary>
        public void UNPAN_TSUMIKAE_PopupBeforeExecuteMethod()
        {
            this.beforeUnpanTsumikaeCD = this.UNPAN_TSUMIKAE_CD.Text;
        }

        /// <summary>
        /// 運搬業者(積替) PopupAfterExecuteMethod
        /// </summary>
        public void UNPAN_TSUMIKAE_PopupAfterExecuteMethod()
        {
            if (this.beforeUnpanTsumikaeCD != this.UNPAN_TSUMIKAE_CD.Text)
            {
                this.TSUMIKAEHOKAN_CD.Text = string.Empty;
                this.TSUMIKAEHOKAN_NAME.Text = string.Empty;
            }
        }
    }
}

