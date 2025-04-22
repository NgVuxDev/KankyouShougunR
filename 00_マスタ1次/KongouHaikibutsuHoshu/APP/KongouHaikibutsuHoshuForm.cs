// $Id: KongouHaikibutsuHoshuForm.cs 18914 2014-04-10 04:42:12Z sc.m.moriya@willwave.jp $
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KongouHaikibutsuHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace KongouHaikibutsuHoshu.APP
{
    /// <summary>
    /// 混合廃棄物保守画面
    /// </summary>
    [Implementation]
    public partial class KongouHaikibutsuHoshuForm : SuperForm
    {
        /// <summary>
        /// 混合廃棄物保守画面ロジック
        /// </summary>
        private KongouHaikibutsuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();
        /// <summary>
        /// 前回廃棄物区分コード
        /// </summary>
        public string beforHaikikubunCD = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KongouHaikibutsuHoshuForm()
            : base(WINDOW_ID.M_KONGOU_HAIKIBUTSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KongouHaikibutsuHoshuLogic(this);

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
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(this.HAIKI_KBN_CD.Text) && !string.IsNullOrWhiteSpace(this.KONGOU_SHURUI_CD.Text))
            {
                this.Search(null, e);
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.HAIKI_KBN_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "廃棄物区分");
                this.HAIKI_KBN_CD.Focus();
            }
            else if (string.IsNullOrEmpty(this.KONGOU_SHURUI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "混合種類");
                this.KONGOU_SHURUI_CD.Focus();
            }
            else
            {
                this.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1979

                // 廃棄物種類CDの設定変更
                this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.FocusOutCheckMethod = new Collection<SelectCheckDto>();
                this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.popupWindowSetting.Clear();
                JoinMethodDto dtoWhere = new JoinMethodDto();
                if (IsDenshiHaikiKbn())
                {
                    // 電子
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.CharactersNumber = new decimal(new int[] { 7, 0, 0, 0 });
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.MaxLength = 7;
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.PopupWindowId = WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.GetCodeMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME";

                    // popupWindowSetting設定
                    {
                        dtoWhere.IsCheckLeftTable = false;
                        dtoWhere.IsCheckRightTable = false;
                        dtoWhere.Join = JOIN_METHOD.WHERE;
                        dtoWhere.LeftTable = "M_DENSHI_HAIKI_SHURUI";

                        SearchConditionsDto searchDto = new SearchConditionsDto();
                        searchDto.And_Or = CONDITION_OPERATOR.AND;
                        searchDto.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchDto.LeftColumn = "TEKIYOU_FLG";
                        searchDto.Value = "FALSE";
                        searchDto.ValueColumnType = DB_TYPE.NONE;
                        dtoWhere.SearchCondition.Add(searchDto);
                    }
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.popupWindowSetting.Add(dtoWhere);
                }
                else
                {
                    // 紙
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.CharactersNumber = new decimal(new int[] { 4, 0, 0, 0 });
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.MaxLength = 4;
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.PopupWindowId = WINDOW_ID.M_HAIKI_SHURUI;
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.GetCodeMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";

                    // popupWindowSetting設定
                    {
                        dtoWhere.IsCheckLeftTable = false;
                        dtoWhere.IsCheckRightTable = false;
                        dtoWhere.Join = JOIN_METHOD.WHERE;
                        dtoWhere.LeftTable = "M_HAIKI_SHURUI";

                        SearchConditionsDto searchDto1 = new SearchConditionsDto();
                        searchDto1.And_Or = CONDITION_OPERATOR.AND;
                        searchDto1.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchDto1.LeftColumn = "HAIKI_KBN_CD";
                        searchDto1.Value = "HAIKI_KBN_CD";
                        searchDto1.ValueColumnType = DB_TYPE.SMALLINT;
                        dtoWhere.SearchCondition.Add(searchDto1);

                        SearchConditionsDto searchDto2 = new SearchConditionsDto();
                        searchDto2.And_Or = CONDITION_OPERATOR.AND;
                        searchDto2.Condition = JUGGMENT_CONDITION.EQUALS;
                        searchDto2.LeftColumn = "TEKIYOU_FLG";
                        searchDto2.Value = "FALSE";
                        searchDto2.ValueColumnType = DB_TYPE.NONE;
                        dtoWhere.SearchCondition.Add(searchDto2);
                    }
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.popupWindowSetting.Add(dtoWhere);

                    // FocusOutCheckMethod設定
                    var checkDto = new SelectCheckDto();
                    checkDto.CheckMethodName = "廃棄物種類コードチェックandセッティング";
                    checkDto.SendParams = new string[] { "HAIKI_KBN_CD", "KONGOU_SHURUI_CD" };
                    this.KongouHaikibutsuHoshuDetail1.HAIKI_SHURUI_CD.FocusOutCheckMethod.Add(checkDto);
                }
                this.Ichiran.Template = this.KongouHaikibutsuHoshuDetail1;

                int count = this.logic.Search();
                if (count == 0)
                {
                    messageShowLogic.MessageBoxShow("C001");
                }
                else if (count > 0)
                {
                    bool catchErr = this.logic.SetIchiran();
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.HAIKI_KBN_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "廃棄物区分");
                this.HAIKI_KBN_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.KONGOU_SHURUI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "混合種類");
                this.KONGOU_SHURUI_CD.Focus();
                return;
            }

            if (!base.RegistErrorFlag && this.logic.CheckHiritsu())
            {
                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            var messageShowLogic = new MessageBoxShowLogic();
            if (string.IsNullOrEmpty(this.HAIKI_KBN_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "廃棄物区分");
                this.HAIKI_KBN_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.KONGOU_SHURUI_CD.Text))
            {
                messageShowLogic.MessageBoxShow("E012", "混合種類");
                this.KONGOU_SHURUI_CD.Focus();
                return;
            }

            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(true);
                if (catchErr)
                {
                    return;
                }
                this.logic.LogicalDelete();
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            // 廃棄物区分指定の初期設定
            Properties.Settings.Default.HaikiKbnCd_Text = this.HAIKI_KBN_CD.Text;
            Properties.Settings.Default.HaikiKbnName_Text = this.HAIKI_KBN_NAME_RYAKU.Text;

            // 混合種類指定の初期設定
            Properties.Settings.Default.KongouShuruiCd_Text = this.KONGOU_SHURUI_CD.Text;
            Properties.Settings.Default.KongouShuruiName_Text = this.KONGOU_SHURUI_NAME_RYAKU.Text;

            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 日付コントロールの初期設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEndEdit(object sender, GrapeCity.Win.MultiRow.CellEndEditEventArgs e)
        {
            GcMultiRow gcMultiRow = (GcMultiRow)sender;
            if (e.EditCanceled == false)
            {
                if (gcMultiRow.CurrentCell is GcCustomDataTimePicker)
                {
                    if (gcMultiRow.CurrentCell.Value == null
                        || string.IsNullOrEmpty(gcMultiRow.CurrentCell.Value.ToString()))
                    {
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        //gcMultiRow.CurrentCell.Value = DateTime.Today;
                        gcMultiRow.CurrentCell.Value = this.logic.parentForm.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                    }
                }
            }
        }

        /// <summary>
        /// 廃棄物区分CD、混合種類CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.KongouHaikibutsuHoshuConstans.HAIKI_SHURUI_CD))
            {
                if (IsDenshiHaikiKbn())
                {
                    bool isNoErrDenshi = this.logic.ExistCheck(e.RowIndex, e.CellIndex);
                    if (!isNoErrDenshi)
                    {
                        e.Cancel = true;

                        GcMultiRow gc = sender as GcMultiRow;
                        if (gc != null && gc.EditingControl != null)
                        {
                            ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                        }

                        return;
                    }
                }

                bool isNoErr = this.logic.DuplicationCheck();
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                    }

                    return;
                }
            }
        }
        /// <summary>
        /// セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {
            // 廃棄物区分、混合種類が空白の場合、明細入力ができないようにする
            if ((this.HAIKI_KBN_CD.TextLength <= 0) || (this.KONGOU_SHURUI_CD.TextLength <= 0) ||
                (this.logic.SearchResultAll == null))
            {
                this.Ichiran.CurrentRow.Selectable = false;
            }
            else
            {
                this.Ichiran.CurrentRow.Selectable = true;
            }

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
            }

            if (IsDenshiHaikiKbn() && e.CellName.Equals("HAIKI_SHURUI_CD"))
            {
                var cell = this.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex] as GcCustomTextBoxCell;
                var DsMasterLogic = new DenshiMasterDataLogic();
                var dto = new DenshiSearchParameterDtoCls();
                if (cell != null)
                {
                    //dto.EDI_MEMBER_ID = parentForm.logic.dt_R18.HST_SHA_EDI_MEMBER_ID;
                    cell.PopupDataHeaderTitle = new string[] { "廃棄物種類CD", "廃棄物種類名", "報告書分類CD", "報告書分類名" };
                    cell.PopupGetMasterField = "HAIKISHURUICD,HAIKI_SHURUI_NAME,HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME";
                    cell.PopupSetFormField = "HAIKI_SHURUI_CD, HAIKI_SHURUI_NAME_RYAKU";
                    cell.PopupDataSource = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                    cell.PopupDataSource.TableName = "電子廃棄物種類";
                }
            }
        }

        /// <summary>
        /// セル状態変化取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // チェックボックスセルで変化した瞬間に変更の確定を行う
            if (this.Ichiran.CurrentCell.Name.Equals(Const.KongouHaikibutsuHoshuConstans.DELETE_FLG) && this.Ichiran.IsCurrentCellDirty)
            {
                this.Ichiran.CommitEdit(DataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// セル値変化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            // 削除フラグの状態を全行で同期する
            if (this.Ichiran.CurrentCell.Name.Equals(Const.KongouHaikibutsuHoshuConstans.DELETE_FLG))
            {
                bool check = (bool)this.Ichiran[e.RowIndex, e.CellIndex].Value;
                foreach (Row temp in this.Ichiran.Rows)
                {
                    if (temp.IsNewRow)
                    {
                        continue;
                    }

                    temp[0].Value = check;
                }
            }
        }

        /// <summary>
        /// 廃棄区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;
            Ichiran.DataSource = null;
            Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1979
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SearchString = null;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 廃棄区分フォーカスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_Enter(object sender, EventArgs e)
        {
            this.beforHaikikubunCD = this.HAIKI_KBN_CD.Text;
        }

        /// <summary>
        /// 廃棄区分CDチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAIKI_KBN_CD_Validated(object sender, System.EventArgs e)
        {
            if (!beforHaikikubunCD.Equals(this.HAIKI_KBN_CD.Text))
            {
                this.KONGOU_SHURUI_CD.Text = string.Empty;
                this.KONGOU_SHURUI_NAME_RYAKU.Text = string.Empty;
                this.beforHaikikubunCD = this.HAIKI_KBN_CD.Text;
            }
        }

        /// <summary>
        /// 混合種類CDテキスト変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KONGOU_SHURUI_CD_TextChanged(object sender, EventArgs e)
        {
            this.KONGOU_SHURUI_NAME_RYAKU.Text = string.Empty;
            Ichiran.DataSource = null;
            Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1979
            this.logic.SearchResult = null;
            this.logic.SearchResultAll = null;
            this.logic.SearchString = null;
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
        }

        /// <summary>
        /// 廃棄物区分CDが「4:電子」か判定
        /// </summary>
        /// <returns></returns>
        private bool IsDenshiHaikiKbn()
        {
            if ("04".Equals(this.HAIKI_KBN_CD.Text))
            {
                return true;
            }

            return false;
        }
    }
}
