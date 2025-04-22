// $Id: KyotenHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KyotenHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.Entity;
using System.Linq;
using System.Reflection;
using r_framework.APP.PopUp.Base;
using System.Collections.Generic;
using r_framework.Dto;

namespace KyotenHoshu.APP
{
    /// <summary>
    /// 拠点入力画面
    /// </summary>
    [Implementation]
    public partial class KyotenHoshuForm : SuperForm
    {

        /// <summary>
        /// 拠点入力画面ロジック
        /// </summary>
        private KyotenHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KyotenHoshuForm()
            : base(WINDOW_ID.M_KYOTEN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new KyotenHoshuLogic(this);

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

            this.Search(null, e);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
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

            // 検索条件入力チェック
            if (!this.logic.CheckSearchString())
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E084", CONDITION_ITEM.Text);
                CONDITION_VALUE.Focus();
                return;
            }

            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
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

            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);

            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();

            if (this.Ichiran.Rows.Count > 0)
            {
                string cd = Convert.ToString(this.Ichiran.Rows[0].Cells["KYOTEN_CD"].Value);
                if (cd != "" && cd.PadLeft(2, '0') == "00")
                {
                    this.Ichiran.Rows[0].Cells["KYOTEN_NAME"].Selected = true;
                }
            }

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
            if (!base.RegistErrorFlag && this.logic.CheckDelete())
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
            bool catchErr = this.logic.Cancel();
            if (catchErr)
            {
                return;
            }
            Search(sender, e);
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 拠点CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.CellName.Equals(Const.KyotenHoshuConstans.KYOTEN_CD))
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
        /// CD表示変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //internal virtual void CdFormatting(object sender, CellFormattingEventArgs e)
        //{
        //    if (e.CellName.Equals("KYOTEN_CD"))
        //    {
        //        this.logic.CdFormatting(e);
        //    }
        //}

        private void Ichiran_CellEnter(object sender, CellEventArgs e)
        {

            // 新規行の場合には削除チェックさせない
            if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
            {
                this.Ichiran.Rows[e.RowIndex][0].Selectable = false;
            }
            else
            {
                // テーブル固定値のデータかどうかを調べる
                bool catchErr = false;
                if (!this.logic.CheckFixedRow(this.Ichiran.Rows[e.RowIndex],out catchErr) && !catchErr)
                {
                    this.Ichiran.Rows[e.RowIndex][0].Selectable = true;
                }
            }
        }

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
        private void KyotenHoshuForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        private void KyotenHoshuForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.ActiveControl is GcCustomTextBoxEditingControl)
            {
                if (this.Ichiran.CurrentCell != null)
                {
                    if (this.Ichiran.CurrentCell.Name.Equals("KYOTEN_POST") || this.Ichiran.CurrentCell.Name.Equals("KYOTEN_TEL") || this.Ichiran.CurrentCell.Name.Equals("KYOTEN_FAX"))
                    {
                        if ((e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z) 
                            || (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) 
                            || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                            || e.KeyCode == Keys.Delete
                            || e.KeyCode == Keys.Back
                            || e.KeyCode == Keys.Subtract
                            || e.KeyCode == Keys.Left
                            || e.KeyCode == Keys.Right
                            || e.KeyCode.ToString().Equals("OemMinus"))
                        {
                        }
                        else
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                }
            }
        }

        private void Ichiran_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            foreach (Row row in this.Ichiran.Rows)
            {
                if (!row.IsNewRow)
                {
                    var objValue = row[Const.KyotenHoshuConstans.KYOTEN_CD].Value;
                    bool catchErr = false;
                    if (this.logic.CheckFixedRow(row, out catchErr))
                    {
                        row["KYOTEN_POST"].ReadOnly = false;
                        row["KYOTEN_POST"].Selectable = true;
                    }
                    else if (catchErr)
                    {
                        throw new Exception("");
                    }
                }
            }
        }

        public void BeforeRegist()
        {
            this.Ichiran.CellValidating -= new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.logic.SetFixedIchiran();
            this.logic.EditableToPrimaryKey();
            this.Ichiran.CellValidating += new System.EventHandler<CellValidatingEventArgs>(this.Ichiran_CellValidating);
        }

        private static readonly string AssemblyName = "JushoKensakuPopup2";

        private static readonly string CalassNameSpace = "APP.JushoKensakuPopupForm2";

        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            var currentRow = this.Ichiran.CurrentRow;

            if (currentRow == null)
            {
                return;
            }

            string PostalCode = Convert.ToString(currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_POST].Value);

            if (string.IsNullOrEmpty(PostalCode))
            {
                return;
            }

            var post7 = PostalCode.Replace("-", string.Empty);
            if (string.IsNullOrEmpty(post7) || post7.Length < 3)
            {
                return;
            }

            post7 = post7.Insert(3, "-");

            S_ZIP_CODE[] zipCodeArray = this.logic.GetDataByPost7LikeSearch(post7);

            if (zipCodeArray.Length == 0)
            {
                return;
            }

            string todofuken = string.Empty;
            string jusho = string.Empty;

            if (zipCodeArray.Length == 1)
            {
                S_ZIP_CODE entity = zipCodeArray.First();

                post7 = entity.POST7;
                todofuken = entity.TODOUFUKEN;
                jusho = entity.SIKUCHOUSON + entity.OTHER1;

                currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Value = todofuken + jusho;
            }
            else if (1 < zipCodeArray.Length)
            {
                // 住所検索ポップアップ表示
                var assembltyName = AssemblyName + ".dll";

                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, AssemblyName + "." + CalassNameSpace);
                var classinfo = objectHandler.Unwrap() as SuperPopupForm;

                if (classinfo != null)
                {
                    // 検索結果を設定
                    classinfo.Params = new object[1] { zipCodeArray };

                    classinfo.ShowDialog();

                    if (classinfo.ReturnParams != null)
                    {
                        for (int i = 0; i < classinfo.ReturnParams.Count; i++)
                        {
                            List<PopupReturnParam> returnParamList = classinfo.ReturnParams[i];

                            post7 = returnParamList[0].Value.ToString();
                            todofuken = returnParamList[1].Value.ToString();
                            jusho = returnParamList[2].Value.ToString()
                                            + returnParamList[3].Value.ToString();
                        }

                        currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Value = todofuken + jusho;
                    }
                    classinfo.Dispose();
                }
            }

            if (!string.IsNullOrEmpty(post7))
            {
                currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_POST].Value = post7;
                this.Ichiran.Focus();
                currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Selected = true;
            }

        }

        public virtual void bt_func2_Click(object sender, EventArgs e)
        {
            var currentRow = this.Ichiran.CurrentRow;

            if (currentRow == null)
            {
                return;
            }

            string Address1 = Convert.ToString(currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Value);

            if (string.IsNullOrEmpty(Address1))
            {
                return;
            }

            if (Address1.Length >= 3)
            {
                string s = Address1.Substring(0, 3);

                if (s == "東京都" || s == "大阪府" || s == "京都府" || s == "北海道" || s.Substring(2, 1) == "県")
                {
                    Address1 = Address1.Substring(3);
                }
                else if (s == "神奈川" || s == "和歌山" || s == "鹿児島")
                {
                    if (Address1.Length >= 4)
                    {
                        Address1 = Address1.Substring(4);
                    }
                }
            }

            if (string.IsNullOrEmpty(Address1))
            {
                return;
            }

            S_ZIP_CODE[] zipCodeArray = null;
            zipCodeArray = logic.GetDataByJushoLikeSearch(Address1);

            while ((zipCodeArray.Length == 0) && (Address1.Length > 1))
            {
                Address1 = Address1.Substring(0, Address1.Length - 1);
                zipCodeArray = logic.GetDataByJushoLikeSearch(Address1);
            }
            
            if (zipCodeArray.Length == 0)
            {
                return;
            }

            string post7 = string.Empty;
            string todofuken = string.Empty;
            
            if (zipCodeArray.Length == 1)
            {
                S_ZIP_CODE entity = zipCodeArray.First();

                post7 = entity.POST7;
                todofuken = entity.TODOUFUKEN;
                Address1 = entity.SIKUCHOUSON + entity.OTHER1;

                if (!string.IsNullOrEmpty(todofuken) || !string.IsNullOrEmpty(Address1))
                {
                    //currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Value = todofuken + Address1;
                }
            }
            else if (1 < zipCodeArray.Length)
            {
                // 住所検索ポップアップ表示
                var assembltyName = AssemblyName + ".dll";

                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, AssemblyName + "." + CalassNameSpace);
                var classinfo = objectHandler.Unwrap() as SuperPopupForm;

                if (classinfo != null)
                {
                    // 検索結果を設定
                    classinfo.Params = new object[1] { zipCodeArray };

                    classinfo.ShowDialog();

                    if (classinfo.ReturnParams != null)
                    {
                        for (int i = 0; i < classinfo.ReturnParams.Count; i++)
                        {
                            List<PopupReturnParam> returnParamList = classinfo.ReturnParams[i];

                            post7 = returnParamList[0].Value.ToString();
                            todofuken = returnParamList[1].Value.ToString();
                            Address1 = returnParamList[2].Value.ToString() + returnParamList[3].Value.ToString();

                            if (!string.IsNullOrEmpty(todofuken) || !string.IsNullOrEmpty(Address1))
                            {
                                //currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Value = todofuken + Address1;
                            }
                        }
                    }
                    classinfo.Dispose();
                }
            }

            if (!string.IsNullOrEmpty(post7))
            {
                currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_POST].Value = post7;
                this.Ichiran.Focus();
                currentRow.Cells[Const.KyotenHoshuConstans.KYOTEN_ADDRESS1].Selected = true;
            }

        }
    }
}