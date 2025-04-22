using r_framework.Const;
using Seasar.Quill;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.APP.Base;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;
        private ManifestoJissekiIchiran.LogicClass MILogic = null;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        public UIHeader HeaderForm { get; private set; }

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.T_MANIFEST_JISSEKI_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            //メッセージクラス
            this.messageShowLogic = new MessageBoxShowLogic();

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.isLoaded)
            {
                // 初期化、初期表示
                if (!this.MILogic.WindowInit()) { return; }

                // キー入力設定
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                // 非表示列登録
                this.SetHiddenColumns(this.MILogic.HIDDEN_SYSTEM_ID, this.MILogic.HIDDEN_SEQ, this.MILogic.HIDDEN_LATEST_SEQ,
                    this.MILogic.HIDDEN_KANRI_ID, this.MILogic.HIDDEN_HAIKI_KBN, this.MILogic.HIDDEN_DETAIL_SYSTEM_ID);

                //表示の初期化
                if (!this.MILogic.ClearScreen("Initial")) { return; }

                // フィルタ表示
                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(3, 145);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 25);

                this.customSortHeader1.Location = new System.Drawing.Point(3, 170);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 25);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 205);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 200);

                // 汎用検索は一旦廃止
                this.searchString.Visible = false;
            }

            this.isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
            }

            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.HeaderForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
                this.bt_ptn1.Top += 0;
                this.bt_ptn2.Top += 0;
                this.bt_ptn3.Top += 0;
                this.bt_ptn4.Top += 0;
                this.bt_ptn5.Top += 0;
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

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData()
        {
            if (this.Table != null)
            {
                int alertNum;
                int.TryParse(this.HeaderForm.AlertNumber.Text, System.Globalization.NumberStyles.AllowThousands, null, out alertNum);
                this.logic.AlertCount = alertNum;
                this.logic.CreateDataGridView(this.Table);
            }
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 交付年月日（開始）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_FROM_MouseDown(object sender, MouseEventArgs e)
        {
            if (KOUFU_DATE_FROM.Value == null)
            {
                this.KOUFU_DATE_FROM.Value = this.MILogic.footer.sysDate.Date;
            }
        }

        private void KOUFU_DATE_FROM_ValueChanged(object sender, EventArgs e)
        {
            if (KOUFU_DATE_FROM.Value != null)
            {
                this.MILogic.KoufuDateFrom = KOUFU_DATE_FROM.Value.ToString();
            }
            else
            {
                this.MILogic.KoufuDateFrom = "";
            }

        }

        private void KOUFU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            this.KOUFU_DATE_TO.IsInputErrorOccured = false;
            this.KOUFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        /// <summary>
        /// 交付年月日（終了）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KOUFU_DATE_TO_MouseDown(object sender, MouseEventArgs e)
        {
            if (KOUFU_DATE_TO.Value == null)
            {
                this.KOUFU_DATE_TO.Value = this.MILogic.footer.sysDate.Date;
            }
        }

        private void KOUFU_DATE_TO_ValueChanged(object sender, EventArgs e)
        {
            if (KOUFU_DATE_TO.Value != null)
            {
                this.MILogic.KoufuDateTo = KOUFU_DATE_TO.Value.ToString();
            }
            else
            {
                this.MILogic.KoufuDateTo = "";
            }
        }

        private void KOUFU_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.KOUFU_DATE_FROM.IsInputErrorOccured = false;
            this.KOUFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        // 処分方法チェック
        private void SHOBUN_HOUHOU_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SHOBUN_HOUHOU_CD.Text))
            {
                this.SHOBUN_HOUHOU_MI.Checked = false;
            }
        }

        public void SHOBUN_HOUHOU_CD_PopupAfter()
        {
            if (!string.IsNullOrEmpty(this.SHOBUN_HOUHOU_CD.Text))
            {
                this.SHOBUN_HOUHOU_MI.Checked = false;
            }
        }

        private void SHOBUN_HOUHOU_MI_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SHOBUN_HOUHOU_MI.CheckState == CheckState.Checked)
            {
                this.SHOBUN_HOUHOU_CD.Text = string.Empty;
                this.SHOBUN_HOUHOU_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            CSVExport CSVExp = new CSVExport();
            CSVExp.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_MANIFEST_JISSEKI_ICHIRAN), this);
        }

        /// <summary>
        /// 条件ｸﾘｱ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {
            this.MILogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            // Ditailの行数チェックはFWでできないので自前でチェック
            if (!this.MILogic.SearchCheck())
            {
                switch (this.MILogic.Search())
                {
                    case -1:
                        return;
                    case 0:
                        MessageBoxUtility.MessageBoxShow("C001");
                        this.MILogic.Set_Search_TME();
                        break;
                    default:
                        this.MILogic.Set_Search_TME();
                        break;
                }
            }
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// フィルタ(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
            //読込データ件数
            if (this.customDataGridView1 != null)
            {
                this.HeaderForm.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.HeaderForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            var sysID = this.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.ShowData();
            }
        }

        /// <summary>
        /// CSV直接出力(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            if (!this.MILogic.SearchCheck())
            {
                switch (this.MILogic.Search())
                {
                    case -1:
                        return;
                    case 0:
                        MessageBoxUtility.MessageBoxShow("C001");
                        return;
                }

                bool ret = this.MILogic.FileOutput(this.MILogic.Search_TME);
                if (ret)
                {
                    this.messageShowLogic.MessageBoxShow("I000");
                }
            }
        }

        /// <summary>
        /// 排出事業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuGyousyaCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            //排出事業者チェック
            switch (this.MILogic.ChkGyosya(this.cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 排出業者CDが変更されているはずなので、関連する排出事業場をクリア
                    this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;
                    break;

                case 1://空
                    //排出業者削除
                    this.ctxt_HaisyutuGyousyaName.Text = string.Empty;
                    //排出業場削除
                    this.cantxt_HaisyutuJigyoubaName.Text = string.Empty;
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName,
                null, null, null,
                null, null, null,
                true, false, false, false, true);
        }

        /// <summary>
        /// 排出事業場CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HaisyutuJigyoubaName_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            switch (this.MILogic.ChkJigyouba(this.cantxt_HaisyutuJigyoubaName, this.cantxt_HaisyutuGyousyaCd, "HAISHUTSU_NIZUMI_GENBA_KBN"))
            {
                case 0://正常

                    break;

                case 1://空
                    //排出業場削除
                    this.ctxt_HaisyutuJigyoubaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, ctxt_HaisyutuGyousyaName,
                null, null, null,
                null, null, null,
                true, false, false, false, true);

            //事業場　設定
            this.MILogic.SetAddressJigyouba("Ryakushou_Name", cantxt_HaisyutuGyousyaCd, cantxt_HaisyutuJigyoubaName, ctxt_HaisyutuJigyoubaName, true, false, false, false, true);
        }

        /// <summary>
        /// 運搬受託者 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyutakuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkGyosya(this.cantxt_UnpanJyutakuNameCd, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬受託者削除
                    this.ctxt_UnpanJyutakuName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //排出業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_UnpanJyutakuNameCd, ctxt_UnpanJyutakuName,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_SyobunJyutakuNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkGyosya(this.cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 処分業者CDが変更されているはずなので、関連する処分先の事業場をクリア
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    break;

                case 1://空
                    //処分受託者削除
                    this.ctxt_SyobunJyutakuName.Text = string.Empty;
                    //運搬先の事業場削除
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_UnpanJyugyobaNameCd_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkJigyouba(this.cantxt_UnpanJyugyobaNameCd, this.cantxt_SyobunJyutakuNameCd, "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN", this.ctxt_UnpanJyugyobaName))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬先の事業場削除
                    this.ctxt_UnpanJyugyobaName.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", cantxt_SyobunJyutakuNameCd, ctxt_SyobunJyutakuName,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 報告書分類 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cantxt_HokokushoBunrui_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.MILogic.ChkHoukokushoBunrui(this.cantxt_HokokushoBunrui))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.ctxt_HokokushoBunrui.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        #endregion

        #region 値保持

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();

        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this.Parent))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }

        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }
        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender, string newText)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, newText); //一致する場合変更なし
        }

        #endregion

        private bool KeyDowns = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (this.SHOBUN_HOUHOU_MI.Focused)
                {
                    KeyDowns = true;
                }
                else
                {
                    KeyDowns = false;
                }
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (KeyDowns)
                {
                    this.HeaderForm.KYOTEN_CD.Focus();
                    KeyDowns = false;
                }
            }

            base.OnKeyUp(e);
        }
    }
}