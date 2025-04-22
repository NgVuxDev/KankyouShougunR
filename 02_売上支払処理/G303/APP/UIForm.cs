using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using Shougun.Core.Common.IchiranCommon.APP;
using Shougun.Core.SalesPayment.Tairyuichiran.Const;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.Tairyuichiran
{
    public partial class UIForm : IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass logicTairyuichiran;

        private Boolean isLoaded;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

        /// <summary>
        /// 検索中フラグ
        /// </summary>
        internal bool isSearch = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public UIForm()
            : base(DENSHU_KBN.TAIRYU_ICHIRAN, false)
        {
            this.InitializeComponent();

            // グリッドのタブインデックスをセット（デザインで設定できないため）
            customDataGridView1.TabIndex = 19;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logicTairyuichiran = new LogicClass(this);

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            isLoaded = false;
        }

        #region 初期処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 重量値表示
            truckScaleWeight1.ProcessWeight();

            if (!isLoaded)
            {
                if (!this.logicTairyuichiran.WindowInit())
                {
                    return;
                }
                this.customDataGridView1.Location = new System.Drawing.Point(3, 183);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 266);
            }

            this.PatternReload();
             
            // パターンから取得された内容を反映する
            this.setLogicSelect();

            if (!this.DesignMode)
            {
                // ヘッダのみ作成
                this.logic.CreateDataGridView(this.Table);
            }

            isLoaded = true;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

        #endregion 初期処理

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            this.Table = this.logicTairyuichiran.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);

                this.HideSystemColumn();
            }
        }

        /// <summary>
        /// システム上の必須項目を非表示にします
        /// </summary>
        internal void HideSystemColumn()
        {
            foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
            {
                if (col.Name == UIConstans.HIDDEN_DENPYOU_NUMBER ||
                    col.Name == UIConstans.HIDDEN_DETAIL_SYSTEM_ID)
                {
                    col.Visible = false;
                }
            }
        }

        #region イベント処理

        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Kensaku_Click(object sender, EventArgs e)
        {
            this.logicTairyuichiran.bt_func8_Click(sender, e);
        }
        /// <summary>
        /// OnKeyDownのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // ENTERキーで行う処理がある項目は個別で処理するためベースのkeydownを動かさない
                if (this.txtUkeireNumber.Focused)
                {
                    txtUkeireNumber_KeyDown(txtUkeireNumber, e);
                    e.Handled = true;
                    return;
                }
                else if (this.txtSyukkaNumber.Focused)
                {
                    txtSyukkaNumber_KeyDown(txtSyukkaNumber, e);

                    e.Handled = true;

                    return;
                }
            }

            base.OnKeyDown(e);

        }

        /// <summary>
        /// 受入番号Enter入力時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUkeireNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (!string.IsNullOrEmpty(txtUkeireNumber.Text)))
            {
                this.logicTairyuichiran.EnterUkeireNo();
            }
        }
        /// <summary>
        /// 出荷番号Enter入力時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSyukkaNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (!string.IsNullOrEmpty(txtSyukkaNumber.Text)))
            {
                this.logicTairyuichiran.EnterShukkaNo();
            }
        }
        #endregion イベント処理

        #region メソッド

        /// <summary>
        /// IchiranSuperFormで取得されたパターン一覧のSelectQeuryをセット
        /// </summary>
        public void setLogicSelect()
        {
            this.logicTairyuichiran.selectQuery = this.logic.SelectQeury;
            this.logicTairyuichiran.orderByQuery = this.logic.OrderByQuery;
            this.logicTairyuichiran.joinQuery = this.logic.JoinQuery;
        }

        #endregion

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

                if (!this.UNPAN_GYOUSHA_CD.Enabled)
                {
                    return;
                }

                // ブランクの場合、処理しない
                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
                {
                    this.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                    return;
                }

                this.logicTairyuichiran.UNPAN_GYOUSHA_CDValidated();

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
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 車輌CDEnter処理
            this.logicTairyuichiran.sharyouCdEnter(sender, e);
        }

        #region 車輌有効性チェック
        /// <summary>
        /// 車輌有効性チェック 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logicTairyuichiran.ChechSharyouCd())
                {
                    // フォーカス設定
                    this.SHARYOU_CD.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SHARYOU_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

    }
}
