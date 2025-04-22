using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Entity;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.CourseNameHoshu.Logic;
using Shougun.Core.Master.CourseNameHoshu.DataGridCustomControl;

namespace Shougun.Core.Master.CourseNameHoshu.APP
{
    /// <summary>
    /// コース名画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 画面メイン処理

        /// <summary>
        /// コース名画面ロジック
        /// </summary>
        private LogicCls logic;

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        bool IsCdFlg = false;
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public UIForm()
            : base(WINDOW_ID.M_COURSE_NAME, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                //LogUtility.DebugMethodStart();

                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                this.Search(null, e);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
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

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            base.OnShown(e);
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);
                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("C001");
                    this.Ichiran.CellValidating -= Ichiran_CellValidating;
                    this.Ichiran.DataSource = this.logic.SearchResult;
                    this.Ichiran.CellValidating += Ichiran_CellValidating;
                    this.logic.ColumnAllowDBNull();

                    //LogUtility.DebugMethodEnd();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.Ichiran.CellValidating -= Ichiran_CellValidating;
                this.Ichiran.DataSource = table;
                this.Ichiran.CellValidating += Ichiran_CellValidating;

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
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
                //LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    if (this.logic.ActionBeforeCheck())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E061");

                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    Boolean isCheckOK = this.logic.CheckBeforeUpdate();

                    if (!isCheckOK)
                    {
                        //LogUtility.DebugMethodEnd();
                        return;
                    }

                    bool catchErr = true;
                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOK)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E061");
                        //LogUtility.DebugMethodEnd();
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
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
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    if (this.logic.CheckDelete())
                    {
                        bool catchErr = true;
                        if (!this.logic.CreateEntity(true, out catchErr))
                        {
                        return;
                        }
                        this.logic.LogicalDelete();
                        //this.logic.Search();
                        if (this.logic.isRegist)
                        {
                            this.Search(sender, e);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.Cancel())
                {
                    return;
                }
                Search(sender, e);
                this.CONDITION_ITEM.Focus();

            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");

                    //LogUtility.DebugMethodEnd();
                    return;
                }
                this.logic.CSVOutput();

            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                this.logic.InitCondition();
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                //LogUtility.DebugMethodStart(sender, e);

                var parentForm = (MasterBaseForm)this.Parent;

                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                //this.Close();
                parentForm.Close();

            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region コントロールのイベント

        ///// <summary>
        ///// 新追加行のセル既定値処理
        ///// </summary>
        //private void Ichiran_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    //LogUtility.DebugMethodStart(sender, e);

        //    if (Ichiran.Rows[e.RowIndex].IsNewRow)
        //    {
        //        // セルの既定値処理
        //        Ichiran.Rows[e.RowIndex].Cells["UPDATE_DATE"].Value = DateTime.Now;
        //        Ichiran.Rows[e.RowIndex].Cells["CREATE_DATE"].Value = DateTime.Now;
        //        Ichiran.Rows[e.RowIndex].Cells["DELETE_FLG"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["MONDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["TUESDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["WEDNESDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["THURSDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["FRIDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["SATURDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["SUNDAY"].Value = false;
        //        Ichiran.Rows[e.RowIndex].Cells["CREATE_PC"].Value = "";
        //        Ichiran.Rows[e.RowIndex].Cells["UPDATE_PC"].Value = "";
        //    }

        //    //LogUtility.DebugMethodEnd();
        //}

        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    //Ichiran.Rows[e.Row.Index].Cells["UPDATE_DATE"].Value = DateTime.Now;
                    //Ichiran.Rows[e.Row.Index].Cells["CREATE_DATE"].Value = DateTime.Now;
                    Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["MONDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["TUESDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["WEDNESDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["THURSDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["FRIDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["SATURDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["SUNDAY"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DefaultValuesNeeded", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コース名CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (null != this.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value)
                {
                    this.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value = this.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value.ToString().ToUpper();
                }

                // コース名CDの重複チェック
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value) &&
                    !"".Equals(Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value) &&
                    Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals("COURSE_NAME_CD"))
                {
                    string bupanZaiko_HokanBasyo_Cd = (string)Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value;
                    bupanZaiko_HokanBasyo_Cd = bupanZaiko_HokanBasyo_Cd.PadLeft(6, '0');
                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(bupanZaiko_HokanBasyo_Cd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E022", "入力されたコース名CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);

                        //LogUtility.DebugMethodEnd();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);
                var cName = this.Ichiran.Columns[e.ColumnIndex].Name;

                switch (cName)
                {
                    case "COURSE_NAME_CD":
                    case "KYOTEN_CD":
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case "COURSE_NAME":
                    case "COURSE_NAME_RYAKU":
                    case "COURSE_NAME_BIKOU":
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.On;
                        break;
                    case "COURSE_NAME_FURIGANA":
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                        break;

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コースCD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["COURSE_NAME_CD"].Index)
                {
                    IsCdFlg = true;
                    TextBox itemID = e.Control as TextBox;
                    if (itemID != null)
                    {
                        itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                    }
                }
                else
                {
                    IsCdFlg = false;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_EditingControlShowing", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if (IsCdFlg && !char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_EditingControlShowing", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 明細一覧のcellを結合する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            ////ヘッダセルの結合処理開始
            //if (e.RowIndex > -1)
            //{
            //    // ヘッダー以外は処理なし
            //    return;
            //}

            //// 10～16列目を結合する処理
            //if (e.ColumnIndex == 9 || e.ColumnIndex == 15) //★★★
            //{
            //    // セルの矩形を取得
            //    Rectangle rect = e.CellBounds;

            //    DataGridView dgv = (DataGridView)sender;

            //    //★★★11～16列目の場合
            //    if (e.ColumnIndex == 9)
            //    {
            //        // 2列目の幅を取得して、1列目の幅に足す
            //        rect.Width = rect.Width + dgv.Columns[10].Width + dgv.Columns[11].Width + dgv.Columns[12].Width + dgv.Columns[13].Width + dgv.Columns[14].Width + dgv.Columns[15].Width;
            //        rect.Y = e.CellBounds.Y + 1;
            //    }
            //    else if (e.ColumnIndex == 15)
            //    {
            //        // 1列目の幅を取得して、2列目の幅に足す
            //        rect.Width = rect.Width + dgv.Columns[9].Width + dgv.Columns[10].Width + dgv.Columns[11].Width + dgv.Columns[12].Width + dgv.Columns[13].Width + dgv.Columns[14].Width;
            //        rect.Y = e.CellBounds.Y + 1;

            //        //★★★Leftを1列目に合わせる
            //        rect.X = rect.X - dgv.Columns[9].Width - dgv.Columns[10].Width - dgv.Columns[11].Width - dgv.Columns[12].Width - dgv.Columns[13].Width - dgv.Columns[14].Width;

            //    }

            //    // 背景、枠線、セルの値を描画
            //    using (SolidBrush brush = new SolidBrush(this.Ichiran.ColumnHeadersDefaultCellStyle.BackColor))
            //    {
            //        // 背景の描画
            //        e.Graphics.FillRectangle(brush, rect);

            //        using (Pen pen = new Pen(dgv.GridColor))
            //        {
            //            // 枠線の描画
            //            e.Graphics.DrawRectangle(pen, rect);
            //        }
            //    }

            //    // セルに表示するテキストを描画
            //    TextRenderer.DrawText(e.Graphics,
            //                    "曜日※",
            //                    e.CellStyle.Font,
            //                    rect,
            //                    e.CellStyle.ForeColor,
            //                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);

            //    //================================
            //    // DataGridViewヘッダー結合セルの枠線の設定
            //    // Graphics オブジェクトを取得
            //    Graphics g = e.Graphics;

            //    // グレー，太さ 2 のペンを定義
            //    // 直線を描画(ヘッダ上部)
            //    int startX = dgv.Columns[0].Width + dgv.Columns[1].Width + dgv.Columns[2].Width + dgv.Columns[3].Width + dgv.Columns[4].Width + dgv.Columns[5].Width + dgv.Columns[6].Width + dgv.Columns[7].Width + dgv.Columns[8].Width;
            //    int endX = startX + dgv.Columns[9].Width + dgv.Columns[10].Width + dgv.Columns[11].Width + dgv.Columns[12].Width + dgv.Columns[13].Width + dgv.Columns[14].Width + dgv.Columns[15].Width;
            //    g.DrawLine(new Pen(Color.DarkGray, 1), startX, rect.Y - 1, endX, rect.Y - 1);
            //    //// 直線を描画(ヘッダ下部)
            //    g.DrawLine(new Pen(Color.DarkGray, 2), startX + 1, rect.Y + rect.Height, endX + 1, rect.Y + rect.Height);
            //    //================================
            //}

            //// 結合セル以外は既定の描画を行う
            //if (!(e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11 ||
            //      e.ColumnIndex == 12 || e.ColumnIndex == 13 || e.ColumnIndex == 14 ||
            //      e.ColumnIndex == 15))
            //{
            //    e.Paint(e.ClipBounds, e.PaintParts);
            //}

            //// イベントハンドラ内で処理を行ったことを通知
            //e.Handled = true;

            try
            {
                //■■■■■■■■■■■■
                //ヘッダセルの結合処理開始
                //■■■■■■■■■■■■
                if (e.RowIndex > -1)
                {
                    // ヘッダー以外は処理なし
                    return;
                }

                // 9列から結合
                //int colIndex = 9;
                int colIndex = 7;

                // 9～15列目を結合する処理
                if (e.ColumnIndex == colIndex ||
                    e.ColumnIndex == colIndex + 1 ||
                    e.ColumnIndex == colIndex + 2 ||
                    e.ColumnIndex == colIndex + 3 ||
                    e.ColumnIndex == colIndex + 4 ||
                    e.ColumnIndex == colIndex + 5 ||
                    e.ColumnIndex == colIndex + 6)
                {
                    // セルの矩形を取得
                    Rectangle rect = e.CellBounds;

                    DataGridView dgv = (DataGridView)sender;

                    // 1列目の場合
                    if (e.ColumnIndex == colIndex)
                    {
                        // 2列目の幅を取得して、1列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width + dgv.Columns[colIndex + 6].Width;
                        rect.Y = e.CellBounds.Y + 1;
                    }
                    else if (e.ColumnIndex == colIndex + 1)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width);
                    }
                    else if (e.ColumnIndex == colIndex + 2)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width);
                    }
                    else if (e.ColumnIndex == colIndex + 3)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width);
                    }
                    else if (e.ColumnIndex == colIndex + 4)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width);
                    }
                    else if (e.ColumnIndex == colIndex + 5)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width);
                    }
                    else if (e.ColumnIndex == colIndex + 6)
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width = rect.Width + dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X = rect.X - (dgv.Columns[colIndex].Width + dgv.Columns[colIndex + 1].Width + dgv.Columns[colIndex + 2].Width + dgv.Columns[colIndex + 3].Width + dgv.Columns[colIndex + 4].Width + dgv.Columns[colIndex + 5].Width);
                    }

                    // 背景、枠線、セルの値を描画
                    using (SolidBrush brush = new SolidBrush(this.Ichiran.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);

                        using (Pen pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }

                        using (Pen pen1 = new Pen(Color.DarkGray))
                        {
                            // 直線を描画(ヘッダ上部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                            // 直線を描画(ヘッダ下部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                        }
                    }

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                    "曜日※",
                                    e.CellStyle.Font,
                                    rect,
                                    e.CellStyle.ForeColor,
                                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);

                    //================================
                    //// DataGridViewヘッダー結合セルの枠線の設定
                    //// Graphics オブジェクトを取得
                    //Graphics g = e.Graphics;

                    //// グレー，太さ 2 のペンを定義
                    //// 直線を描画(ヘッダ上部)
                    //int startX = dgv.Columns[0].Width + dgv.Columns[1].Width + dgv.Columns[2].Width + dgv.Columns[3].Width + dgv.Columns[4].Width;
                    //int endX = startX + dgv.Columns[5].Width + dgv.Columns[6].Width;
                    //g.DrawLine(new Pen(Color.Red, 1), startX, rect.Y - 1, endX, rect.Y - 1);
                    ////// 直線を描画(ヘッダ下部)
                    //g.DrawLine(new Pen(Color.DarkGray, 2), startX + 1, rect.Y + rect.Height, endX + 1, rect.Y + rect.Height);
                    //================================
                }

                // 結合セル以外は既定の描画を行う
                if (!(e.ColumnIndex == colIndex ||
                    e.ColumnIndex == colIndex + 1 ||
                    e.ColumnIndex == colIndex + 2 ||
                    e.ColumnIndex == colIndex + 3 ||
                    e.ColumnIndex == colIndex + 4 ||
                    e.ColumnIndex == colIndex + 5 ||
                    e.ColumnIndex == colIndex + 6))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Ichiran_CellPainting", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void Ichiran_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.Ichiran.Refresh();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Ichiran_Scroll", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                this.Ichiran.Refresh();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Ichiran_ColumnWidthChanged", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(sender, e);

                if ("".Equals(e.Value)) //空文字を入力された場合
                {
                    e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                    e.ParsingApplied = true; //後続の解析不要
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("Ichiran_CellParsing", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索値クリア
        /// </summary>
        public void clearConditionValue()
        {
            try
            {
                this.CONDITION_VALUE.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("clearConditionValue", ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if ("COURSE_NAME_CD".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "KYOTEN_CD".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "MONDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "TUESDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "WEDNESDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "THURSDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "FRIDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "SATURDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "SUNDAY".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "CREATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "UPDATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            }
            else if ("COURSE_NAME_FURIGANA".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            }
            else
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.On;
            }
        }

        #endregion

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
