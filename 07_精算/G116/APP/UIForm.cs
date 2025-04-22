using System;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using r_framework.Utility;
using r_framework.Dto;
using System.Collections.ObjectModel;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
{
    public partial class UIForm : SuperForm
    {
        #region プロパティ
        /// <summary>
        ///　帳票出力用支払データ
        /// </summary>
        public DataTable ShiharaiDt { get; set; }
        #endregion プロパティ

        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// ヘッダ
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// CheckedChangedイベントの処理を行うかどうか
        /// </summary>
        internal bool IsCheckedChangedEventRun = true;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_SHIHARAI_MEISAISHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.dgvSeisanDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            //this.logic = new LogicClass(this, headerForm);
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            chkHakko.SendToBack();
            checkBoxAll_zumi.SendToBack();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="dto">画面初期表示DTO</param>
        public UIForm(UIHeader headerForm, DTOClass dto)
            : base(WINDOW_ID.T_SHIHARAI_MEISAISHO_HAKKO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
            this.dgvSeisanDenpyouItiran.IsBrowsePurpose = true;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            //this.logic = new LogicClass(this, headerForm);
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            this.logic.InitDto = dto;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            chkHakko.SendToBack();
            checkBoxAll_zumi.SendToBack();
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
            this.SetPopUpTorihikisakiCd();
            // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.dgvSeisanDenpyouItiran != null)
            {
                this.dgvSeisanDenpyouItiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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
        /// プレビュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Function5Click(object sender, EventArgs e)
        {
            try
            {
                this.logic.Function5ClickLogic();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Function8Click(object sender, EventArgs e)
        {
            this.logic.Function8ClickLogic();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Function9Click(object sender, EventArgs e)
        {
            this.logic.Function9ClickLogic();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.logic.SearchResult = new DataTable();
            parentForm.Close();
        }

        #region グリッド発行列のチェックボックス設定
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.logic.SeisanDenpyouItiranCellPaintingLogic(e);

        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeisanDenpyouItiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.SeisanDenpyouItiranCellClickLogic(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void dgvSeisanDenpyouItiran_Enter(object sender, EventArgs e)
        //{
        //    if (this.dgvSeisanDenpyouItiran.CurrentRow != null)
        //    {
        //        this.dgvSeisanDenpyouItiran.CurrentCell = this.dgvSeisanDenpyouItiran.CurrentRow.Cells["colDenpyoNumber"];
        //    }
        //}
        /// <summary>
        /// 発行列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.checkBoxAllCheckedChangedLogic();
        }

        /// <summary>
        /// 発行済みチェック列すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_zumi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsCheckedChangedEventRun)
            {
                this.logic.checkBoxAllZumiCheckedChangedLogic();
            }
        }

        #endregion グリッド発行列のチェックボックス設定

        #region ラジオボタン項目未選択時の自動設定
        /// <summary>
        /// 各ラジオボタンに対応したテキストボックスのValidatedイベント発生時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void SetOfRadioButtonNotSelected(object sender, EventArgs e)
        //{
        //    r_framework.CustomControl.CustomNumericTextBox2 textBox = (r_framework.CustomControl.CustomNumericTextBox2)sender;
        //}
        #endregion ラジオボタン項目未選択時の自動設定

        #region 印刷日変更処理
        /// <summary>
        /// 印刷日変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInsatsubi_TextChanged(object sender, EventArgs e)
        {
            this.logic.CdtSiteiPrintHidukeEnable(txtInsatsubi.Text);
        }
        #endregion 印刷日変更処理

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 start
        /// <summary>
        /// Set Torihikisaki popup join method
        /// </summary>
        private void SetPopUpTorihikisakiCd()
        {
            if (r_framework.Configuration.AppConfig.AppOptions.IsInxsShiharai())
            {
                JoinMethodDto joinData = new JoinMethodDto();
                joinData.Join = JOIN_METHOD.WHERE;
                joinData.LeftTable = "M_TORIHIKISAKI_SHIHARAI";

                Collection<SearchConditionsDto> searchConditions = new Collection<SearchConditionsDto>();
                SearchConditionsDto searchDto = new SearchConditionsDto();
                searchDto.And_Or = CONDITION_OPERATOR.AND;
                searchDto.Condition = JUGGMENT_CONDITION.NOT_EQUALS;
                searchDto.LeftColumn = "INXS_SHIHARAI_KBN";
                searchDto.Value = "1"; //[取引先入力][INXS支払区分] = ２．しない
                searchDto.ValueColumnType = DB_TYPE.SMALLINT;
                searchConditions.Add(searchDto);
                joinData.SearchCondition = searchConditions;
                Collection<JoinMethodDto> popupWindowSetting = new Collection<JoinMethodDto>();
                if (this.TORIHIKISAKI_CD.popupWindowSetting != null)
                {
                    popupWindowSetting = (Collection<JoinMethodDto>)this.TORIHIKISAKI_CD.popupWindowSetting;
                }

                popupWindowSetting.Add(joinData);
                this.TORIHIKISAKI_CD.popupWindowSetting = popupWindowSetting;

                Collection<JoinMethodDto> popupWindowSettingButton = new Collection<JoinMethodDto>();
                if (this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting != null)
                {
                    popupWindowSettingButton = (Collection<JoinMethodDto>)this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting;
                }

                popupWindowSettingButton.Add(joinData);
                this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = popupWindowSettingButton;
            }
        }
        // 20210224 【マージ】INXS支払明細書アップロード機能を環境将軍R V2.22にマージ依頼 #147339 end
    }
}
