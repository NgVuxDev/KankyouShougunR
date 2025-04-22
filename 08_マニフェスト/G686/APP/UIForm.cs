using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataGridViewCheckBoxColumnHeeader;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill.Attrs;
using Shougun.Core.PapeMranifest.ManifestIkkatsuKousin;
using System.Collections.Generic;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace Shougun.Core.PaperManifest.ManifestIkkatsuKousin
{
    [Implementation]
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        private LogicClass ikkatsuKousinLogic;

        private string selectQuery = string.Empty;

        private string orderByQuery = string.Empty;

        private Boolean isLoaded;
        
        public UIForm()
            : base(DENSHU_KBN.MANIFEST_IKKATSU_UPD_ICHIRAN, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.ikkatsuKousinLogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            //Main画面で社員コード値を取得すること
            ikkatsuKousinLogic.syainCode = SystemProperty.Shain.CD;
            //伝種区分を取得すること
            DENSHU_KBN time = (DENSHU_KBN)Enum.Parse(typeof(DENSHU_KBN), "MANIFEST_IKKATSU_UPD_ICHIRAN", true);
            ikkatsuKousinLogic.denShu_Kbn = (int)time;

            isLoaded = false;
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.customDataGridView1.IsBrowsePurpose = false;

            if (isLoaded == false)
            {
                this.bt_ptn1.Top -= 7;
                this.bt_ptn2.Top -= 7;
                this.bt_ptn3.Top -= 7;
                this.bt_ptn4.Top -= 7;
                this.bt_ptn5.Top -= 7;

                if (!ikkatsuKousinLogic.WindowInit())
                {
                    return;
                }
            }

            // パターン読み込み（初回のみデフォルト選択）
            this.PatternReload(!isLoaded);

            this.ikkatsuKousinLogic.selectQuery = this.SelectQuery;
            this.ikkatsuKousinLogic.orderByQuery = this.OrderByQuery;
            this.ikkatsuKousinLogic.joinQuery = this.JoinQuery;

            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {

                    this.logic.CreateDataGridView(this.Table);

                    if (isLoaded == false)
                    {
                        ////選択チェックボックス作成
                        DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                        column.Name = "CHECKBOX";
                        column.DataPropertyName = "CHECKBOX";
                        column.ReadOnly = false;
                        column.Width = 50;
                        column.DefaultCellStyle.Tag = "処理対象とする場合はチェックしてください";
                        DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                        newheader.ToolTipText = "処理対象とする場合はチェックしてください";
                        newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                                datagridviewcheckboxHeaderEventHander(this.ikkatsuKousinLogic.ch_OnCheckBoxClicked);
                        column.HeaderCell = newheader;
                        column.HeaderText = string.Empty;
                        this.customDataGridView1.Columns.Insert(0, column);

                        this.customSortHeader1.Location = new System.Drawing.Point(3, 275);
                        this.customSortHeader1.Size = new System.Drawing.Size(997, 25);

                        this.customDataGridView1.Location = new System.Drawing.Point(3, 300);
                        this.customDataGridView1.Size = new System.Drawing.Size(997, 150);
                    }
                }
            }

            //並び順ソートヘッダー
            this.customSortHeader1.ClearCustomSortSetting();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            isLoaded = true;
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
        /// 一括入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void F_Put(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Kensaku(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Touroku(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #region 検索結果表示

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            this.Table = this.ikkatsuKousinLogic.SearchResult;
            this.customDataGridView1.Columns["CHECKBOX"].Visible = false;
            this.customSortHeader1.SortDataTable(Table); // まず抽出データをソートしてから
            this.customDataGridView1.Columns["CHECKBOX"].Visible = true;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                foreach (var row in this.customDataGridView1.Rows.Cast<DataGridViewRow>())
                {
                    foreach (var cell in row.Cells.Cast<DataGridViewCell>())
                    {
                        cell.UpdateBackColor(false);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }

        /// <summary>
        /// 交付区分Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNum_KofuKbn_Validated(object sender, EventArgs e)
        {
            CustomNumericTextBox2 text = sender as CustomNumericTextBox2;
            if (string.IsNullOrEmpty(text.Text))
            {
                //抽出対象区分が空の場合、メッセージ「抽出対象区分は必須項目です。入力してください。」を表示する
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShow("E001", text.DisplayItemName);
                //フォーカスを抽出対象区分へ移動
                text.Select();
            }
        }

        /// <summary>
        /// マニフェスト種類TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.ikkatsuKousinLogic.HAIKI_KBN_CD_CHANGE(this.HAIKI_KBN_CD.Text);
            this.HAIKI_SHURUI_CD.Text = string.Empty;
            this.HAIKI_SHURUI_NAME.Text = string.Empty;
            this.HAIKI_NAME_CD.Text = string.Empty;
            this.HAIKI_NAME.Text = string.Empty;

            this.HAIKI_SHURUI_CD.popupWindowSetting.Clear();
            this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Clear();
            JoinMethodDto dtowhere = new JoinMethodDto();
            dtowhere.IsCheckLeftTable = false;
            dtowhere.IsCheckRightTable = false;
            dtowhere.Join = JOIN_METHOD.WHERE;
            dtowhere.LeftTable = "M_HAIKI_SHURUI";

            SearchConditionsDto serdto = new SearchConditionsDto();
            serdto.And_Or = CONDITION_OPERATOR.AND;
            serdto.Condition = JUGGMENT_CONDITION.EQUALS;
            serdto.LeftColumn = "HAIKI_KBN_CD";
            serdto.ValueColumnType = DB_TYPE.SMALLINT;
            serdto.Value = this.HAIKI_KBN_CD.Text;
            dtowhere.SearchCondition.Add(serdto);
            this.HAIKI_SHURUI_CD.popupWindowSetting.Add(dtowhere);
            this.cbtn_HaikibutuShuruiSan.popupWindowSetting.Add(dtowhere);
        }

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

        #region 業者と現場設定

        /// <summary>
        /// 排出事業者CD Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HST_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            //排出事業者チェック
            switch (this.ikkatsuKousinLogic.ChkGyosya(HST_GYOUSHA_CD, "HAISHUTSU_NIZUMI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 排出業者CDが変更されているはずなので、関連する排出事業場をクリア
                    this.HST_GENBA_CD.Text = string.Empty;
                    this.HST_GENBA_NAME.Text = string.Empty;
                    break;

                case 1://空
                    //排出業者削除
                    this.HST_GYOUSHA_NAME.Text = string.Empty;
                    //排出業場削除
                    this.HST_GENBA_CD.Text = string.Empty;
                    this.HST_GENBA_NAME.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", HST_GYOUSHA_CD, HST_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                true, false, false, false, true);
        }

        /// <summary>
        /// 排出事業場CDイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HST_GENBA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }
            switch (this.ikkatsuKousinLogic.ChkJigyouba(this.HST_GENBA_CD, this.HST_GYOUSHA_CD, "HAISHUTSU_NIZUMI_GENBA_KBN"))
            {
                case 0://正常

                    break;

                case 1://空
                    //排出業場削除
                    this.HST_GENBA_NAME.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", HST_GYOUSHA_CD, HST_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                true, false, false, false, true);

            //事業場　設定
            this.ikkatsuKousinLogic.SetAddressJigyouba("Ryakushou_Name", HST_GYOUSHA_CD, HST_GENBA_CD, HST_GENBA_NAME, true, false, false, false, true);
        }

        /// <summary>
        /// 処分受託者(処分業者) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkGyosya(this.SBN_GYOUSHA_CD, "SHOBUN_NIOROSHI_GYOUSHA_KBN"))
            {
                case 0://正常
                    // 処分事業場CDが変更されているはずなので、関連する処分事業場をクリア
                    this.SBN_GENBA_CD.Text = string.Empty;
                    this.SBN_GENBA_NAME.Text = string.Empty;
                    break;

                case 1://空
                    //処分受託者削除
                    this.SBN_GYOUSHA_NAME.Text = string.Empty;
                    //処分事業場削除
                    this.SBN_GENBA_CD.Text = string.Empty;
                    this.SBN_GENBA_NAME.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", SBN_GYOUSHA_CD, SBN_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 運搬先の事業場(処分業者の処理施設) 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SBN_GENBA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkJigyouba(this.SBN_GENBA_CD, this.SBN_GYOUSHA_CD, "SHOBUN_NIOROSHI_GENBA_KBN", "SAISHUU_SHOBUNJOU_KBN", this.SBN_GENBA_NAME))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬先の事業場削除
                    this.SBN_GENBA_NAME.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", SBN_GYOUSHA_CD, SBN_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 運搬受託者（区間１） 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPN_JYUTAKUSHA_CD1_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkGyosya(this.UPN_JYUTAKUSHA_CD1, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬受託者（区間１）削除
                    this.UPN_JYUTAKUSHA_NAME1.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", UPN_JYUTAKUSHA_CD1, UPN_JYUTAKUSHA_NAME1,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 運搬受託者（区間２） 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPN_JYUTAKUSHA_CD2_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkGyosya(this.UPN_JYUTAKUSHA_CD2, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬受託者（区間２）削除
                    this.UPN_JYUTAKUSHA_NAME2.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", UPN_JYUTAKUSHA_CD2, UPN_JYUTAKUSHA_NAME2,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 運搬受託者（区間３） 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPN_JYUTAKUSHA_CD3_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkGyosya(this.UPN_JYUTAKUSHA_CD3, "UNPAN_JUTAKUSHA_KAISHA_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬受託者（区間３）削除
                    this.UPN_JYUTAKUSHA_NAME3.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }

            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", UPN_JYUTAKUSHA_CD3, UPN_JYUTAKUSHA_NAME3,
                null, null, null,
                null, null, null,
                false, false, true, false, true);
        }

        /// <summary>
        /// 最終処分業者 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkGyosya(this.LAST_SHOBUN_GYOUSHA_CD, "SHOBUN_NIOROSHI_GYOUSHA_KBN", "SAISHUU_SHOBUNJOU_KBN"))
            {
                case 0://正常
                    // 処分事業場CDが変更されているはずなので、関連する処分事業場をクリア
                    this.LAST_SHOBUN_GENBA_CD.Text = string.Empty;
                    this.LAST_SHOBUN_GENBA_NAME.Text = string.Empty;
                    break;

                case 1://空
                    //処分受託者削除
                    this.LAST_SHOBUN_GYOUSHA_NAME.Text = string.Empty;
                    //処分事業場削除
                    this.LAST_SHOBUN_GENBA_CD.Text = string.Empty;
                    this.LAST_SHOBUN_GENBA_NAME.Text = string.Empty;
                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", LAST_SHOBUN_GYOUSHA_CD, LAST_SHOBUN_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                false, true, false, false, true);
        }

        /// <summary>
        /// 最終処分場所 名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LAST_SHOBUN_GENBA_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkJigyouba(this.LAST_SHOBUN_GENBA_CD, this.LAST_SHOBUN_GYOUSHA_CD, "SAISHUU_SHOBUNJOU_KBN"))
            {
                case 0://正常
                    break;

                case 1://空
                    //運搬先の事業場削除
                    this.LAST_SHOBUN_GENBA_NAME.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
            //業者　設定
            ManifestoLogic.SetAddrGyousha("Ryakushou_Name", LAST_SHOBUN_GYOUSHA_CD, LAST_SHOBUN_GYOUSHA_NAME,
                null, null, null,
                null, null, null,
                false, true, false, false, true);

            //事業場　設定
            this.ikkatsuKousinLogic.SetAddressJigyouba("Ryakushou_Name", LAST_SHOBUN_GYOUSHA_CD, LAST_SHOBUN_GENBA_CD, LAST_SHOBUN_GENBA_NAME, false, true, false, false, true);
        }

        #endregion

        #region 日付Toダブルクリック
        private void KOUFU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.KOUFU_DATE_FROM;
            var ToTextBox = this.KOUFU_DATE_TO;
            ToTextBox.Text = FromTextBox.Text;
        }

        private void SBN_END_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.SBN_END_DATE_FROM;
            var ToTextBox = this.SBN_END_DATE_TO;
            ToTextBox.Text = FromTextBox.Text;
        }

        private void UPN_END_DATE1_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.UPN_END_DATE1_FROM;
            var ToTextBox = this.UPN_END_DATE1_TO;
            ToTextBox.Text = FromTextBox.Text;
        }

        private void UPN_END_DATE2_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.UPN_END_DATE2_FROM;
            var ToTextBox = this.UPN_END_DATE2_TO;
            ToTextBox.Text = FromTextBox.Text;
        }

        private void UPN_END_DATE3_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.UPN_END_DATE3_FROM;
            var ToTextBox = this.UPN_END_DATE3_TO;
            ToTextBox.Text = FromTextBox.Text;
        }

        private void LAST_SBN_END_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var FromTextBox = this.LAST_SBN_END_DATE_FROM;
            var ToTextBox = this.LAST_SBN_END_DATE_TO;
            ToTextBox.Text = FromTextBox.Text;
        }
        #endregion

        #region 日付FromTo Leaveイベント
        private void KOUFU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.KOUFU_DATE_FROM.Text))
            {
                this.KOUFU_DATE_FROM.IsInputErrorOccured = false;
                this.KOUFU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void KOUFU_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.KOUFU_DATE_TO.Text))
            {
                this.KOUFU_DATE_TO.IsInputErrorOccured = false;
                this.KOUFU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void SBN_END_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SBN_END_DATE_FROM.Text))
            {
                this.SBN_END_DATE_FROM.IsInputErrorOccured = false;
                this.SBN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.cbx_sbn.Checked = false;
            }
        }

        private void SBN_END_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SBN_END_DATE_TO.Text))
            {
                this.SBN_END_DATE_TO.IsInputErrorOccured = false;
                this.SBN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;
                this.cbx_sbn.Checked = false;
            }
        }

        private void UPN_END_DATE1_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE1_FROM.Text))
            {
                this.UPN_END_DATE1_FROM.IsInputErrorOccured = false;
                this.UPN_END_DATE1_FROM.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn1.Checked = false;
            }
        }

        private void UPN_END_DATE1_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE1_TO.Text))
            {
                this.UPN_END_DATE1_TO.IsInputErrorOccured = false;
                this.UPN_END_DATE1_TO.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn1.Checked = false;
            }
        }

        private void UPN_END_DATE2_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE2_FROM.Text))
            {
                this.UPN_END_DATE2_FROM.IsInputErrorOccured = false;
                this.UPN_END_DATE2_FROM.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn2.Checked = false;
            }
        }

        private void UPN_END_DATE2_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE2_TO.Text))
            {
                this.UPN_END_DATE2_TO.IsInputErrorOccured = false;
                this.UPN_END_DATE2_TO.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn2.Checked = false;
            }
        }

        private void UPN_END_DATE3_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE3_FROM.Text))
            {
                this.UPN_END_DATE3_FROM.IsInputErrorOccured = false;
                this.UPN_END_DATE3_FROM.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn3.Checked = false;
            }
        }

        private void UPN_END_DATE3_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.UPN_END_DATE3_TO.Text))
            {
                this.UPN_END_DATE3_TO.IsInputErrorOccured = false;
                this.UPN_END_DATE3_TO.BackColor = Constans.NOMAL_COLOR;
                this.cbx_upn3.Checked = false;
            }
        }

        private void LAST_SBN_END_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LAST_SBN_END_DATE_FROM.Text))
            {
                this.LAST_SBN_END_DATE_FROM.IsInputErrorOccured = false;
                this.LAST_SBN_END_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.cbx_lastSbn.Checked = false;
            }
        }

        private void LAST_SBN_END_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LAST_SBN_END_DATE_TO.Text))
            {
                this.LAST_SBN_END_DATE_TO.IsInputErrorOccured = false;
                this.LAST_SBN_END_DATE_TO.BackColor = Constans.NOMAL_COLOR;
                this.cbx_lastSbn.Checked = false;
            }
        }
        #endregion

        #region チェックボックス変更イベント
        /// <summary>
        /// チェックボックスがONになったら、日付のFROMTOをクリアする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_sbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_sbn.Checked)
            {
                this.SBN_END_DATE_FROM.Text = string.Empty;
                this.SBN_END_DATE_TO.Text = string.Empty;
            }
        }

        private void cbx_upn1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_upn1.Checked)
            {
                this.UPN_END_DATE1_FROM.Text = string.Empty;
                this.UPN_END_DATE1_TO.Text = string.Empty;
            }
        }

        private void cbx_upn2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_upn2.Checked)
            {
                this.UPN_END_DATE2_FROM.Text = string.Empty;
                this.UPN_END_DATE2_TO.Text = string.Empty;
            }
        }

        private void cbx_upn3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_upn3.Checked)
            {
                this.UPN_END_DATE3_FROM.Text = string.Empty;
                this.UPN_END_DATE3_TO.Text = string.Empty;
            }
        }

        private void cbx_lastSbn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_lastSbn.Checked)
            {
                this.LAST_SBN_END_DATE_FROM.Text = string.Empty;
                this.LAST_SBN_END_DATE_TO.Text = string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// 廃棄物種類 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkHaikibutuShurui(this.HAIKI_KBN_CD, this.HAIKI_SHURUI_CD))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.HAIKI_SHURUI_NAME.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        /// <summary>
        /// 廃棄物名称 Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_NAME_CD_Validated(object sender, EventArgs e)
        {
            if (!isChanged(sender))//変更がない場合は何もしない
            {
                return;
            }

            switch (this.ikkatsuKousinLogic.ChkHaikibutuName(this.HAIKI_NAME_CD))
            {
                case 0://正常
                    break;

                case 1://空
                    //報告書分類削除
                    this.HAIKI_NAME.Text = string.Empty;

                    return;

                case 2://エラー
                    return;
            }
        }

        #region 検索ポップアップイベント
        private string popupBeforeHaisyutuGyoushaCd = string.Empty;
        private string popupBeforeSyobunJyutakuNameCd = string.Empty;
        private string popupBeforeTsumikaehokanGyoushaCd = string.Empty;
        /// <summary>
        /// 排出事業者検索ポップアップのPopupBeforeExecuteMethod
        /// </summary>
        public void Btn_haishutsuPopupBeforeExecuteMethod()
        {
            popupBeforeHaisyutuGyoushaCd = this.HST_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業者検索ポップアップのPopupAfterExecuteMethod
        /// </summary>
        public void Btn_haishutsuPopupAfterExecuteMethod()
        {
            if (!popupBeforeHaisyutuGyoushaCd.Equals(this.HST_GYOUSHA_CD.Text))
            {
                this.HST_GENBA_CD.Text = string.Empty;
                this.HST_GENBA_NAME.Text = string.Empty;
            }
        }
        #endregion

    }
}