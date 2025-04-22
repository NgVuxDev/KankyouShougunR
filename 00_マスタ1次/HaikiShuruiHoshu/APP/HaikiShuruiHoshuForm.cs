using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using HaikiShuruiHoshu.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Collections.Generic;
using r_framework.Utility;

namespace HaikiShuruiHoshu.APP
{
    /// <summary>
    /// 廃棄種類保守画面
    /// </summary>
    [Implementation]
    public partial class HaikiShuruiHoshuForm : SuperForm
    {
        /// <summary>
        /// 廃棄種類保守画面ロジック
        /// </summary>
        private HaikiShuruiHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        // thongh 20150803 #11901 start
        /// <summary>
        /// 前回値チェック用変数(廃棄物区分用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForHaiki_Kbn = new Dictionary<string, string>();
        // thongh 20150803 #11901 end

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HaikiShuruiHoshuForm(string Cd)
            : base(WINDOW_ID.M_HAIKI_SHURUI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new HaikiShuruiHoshuLogic(this);

            //
            this.logic.HaiKBNCd = Cd;

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

            if (!string.IsNullOrWhiteSpace(this.HAIKI_KBN_CD.Text))
            {
                this.HAIKI_KBN_CD_TextChanged(null, e);
                this.Search(null, e);
            }

            this.HAIKI_KBN_CD.Focus();

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
            else
            {
                this.Ichiran.AllowUserToAddRows = true; // thongh 2015/12/28 #1979
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

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
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
            if (!base.RegistErrorFlag)
            {
                if (this.logic.CheckDelete())
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

        //プレビュ機能削除
        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    this.logic.Preview();
        //}

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

            Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
            Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
            Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
            Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

            Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

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
        /// 廃棄種類CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD))
            {
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
            //廃棄物区分が空白の場合、明細入力ができないようにする
            if ((this.HAIKI_KBN_CD.TextLength <= 0) ||
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
                bool catchErr = false;
                // テーブル固定値のデータかどうかを調べる
                if (!this.logic.CheckFixedRow(this.Ichiran.Rows[e.RowIndex], out catchErr) && !catchErr)
                {
                    this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
                }
            }
        }

        private void HAIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            // thongh 20150803 #11901 start
            bool catchErr = false;
            string cd = this.ChangeValue(this.HAIKI_KBN_CD.Text, out catchErr);
            if (catchErr || string.IsNullOrEmpty(cd))
            {
                return;
            }
            if (beforeValuesForHaiki_Kbn.ContainsKey(this.HAIKI_KBN_CD.Name) && !cd.Equals(this.beforeValuesForHaiki_Kbn[this.HAIKI_KBN_CD.Name]))
            {
                this.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;
                this.Ichiran.DataSource = null;
                this.logic.SearchResultAll = null;
                this.Ichiran.AllowUserToAddRows = false;//thongh 2015/12/28 #1979
            }

            var template = this.Ichiran.Template.Clone();
            if (Convert.ToInt16(this.HAIKI_KBN_CD.Text) == 1 || Convert.ToInt16(this.HAIKI_KBN_CD.Text) == 3)
            {
                template.Row.Cells["HAIKI_SHURUI_CD"].Tag = "0000～6999：普通産廃として扱われます。　7000～9999：特管産廃として扱われます。";
            }
            else if (Convert.ToInt16(this.HAIKI_KBN_CD.Text) == 2)
            {
                template.Row.Cells["HAIKI_SHURUI_CD"].Tag = "0000～1099：安定型品目として扱われます。　1100～2099：管理型品目として扱われます。　2100～9999：特管産廃として扱われます。";
            }
            this.Ichiran.Template.Clear();
            this.Ichiran.Template = template;
            this.Ichiran.Refresh();

            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            // 前回値チェック用データをセット
            if (beforeValuesForHaiki_Kbn.ContainsKey(this.HAIKI_KBN_CD.Name))
            {
                beforeValuesForHaiki_Kbn[this.HAIKI_KBN_CD.Name] = cd;
            }
            else
            {
                beforeValuesForHaiki_Kbn.Add(this.HAIKI_KBN_CD.Name, cd);
            }
            // thongh 20150803 #11901 end
        }

        ///// <summary>
        ///// CD表示変更イベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //internal virtual void CdFormatting(object sender, CellFormattingEventArgs e)
        //{
        //    if (e.CellName.Equals("HAIKI_SHURUI_CD"))
        //    {
        //        this.logic.CdFormatting(e);
        //    }
        //}

        /// <summary>
        /// 表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Form_Shown(object sender, EventArgs e)
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可に修正
            this.logic.SetFixedIchiran();

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HaikiShuruiHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        // thongh 20150803 #11901 start
        /// <summary> 指定Valueに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <returns>指定Valueに変換後文字列</returns>
        private string ChangeValue(string value,out bool catchErr)
        {
            try
            {
                catchErr = false;
                // フォーマット変換後文字列
                string ret = string.Empty;

                // 引数がブランクの場合はブランクを返す
                if (value.Trim() != string.Empty)
                {
                    ret = Convert.ToInt16(value).ToString();
                }

                return ret;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ChangeValue", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return "";
            }
        }
        // thongh 20150803 #11901 end

        public void BeforeRegist()
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.logic.SetFixedIchiran();
            this.logic.EditableToPrimaryKey();
            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }
    }
}

