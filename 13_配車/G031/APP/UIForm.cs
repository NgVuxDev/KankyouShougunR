// $Id: UIForm.cs 54301 2015-07-02 02:02:07Z minhhoang@e-mall.co.jp $
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
using Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku;
using r_framework.CustomControl;
using Shougun.Core.Common.BusinessCommon.Utility;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        public UIForm(UIHeader headerForm)
            : base(WINDOW_ID.T_COURSE_HAISHA_IRAI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);
        }


        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }
        #endregion

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if ("".Equals(e.Value)) //空文字を入力された場合
            {
                e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true; //後続の解析不要
            }
        }

        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                this.logic.Ichiran_CellValidating(e);
            }
            catch
            {
                throw;
            }

        }

        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 start
        private void SAGYOU_DATE_BEGIN_Leave(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_END.IsInputErrorOccured = false;
            this.SAGYOU_DATE_END.BackColor = Constans.NOMAL_COLOR;
        }

        private void SAGYOU_DATE_END_Leave(object sender, EventArgs e)
        {
            this.SAGYOU_DATE_BEGIN.IsInputErrorOccured = false;
            this.SAGYOU_DATE_BEGIN.BackColor = Constans.NOMAL_COLOR;
        }
        // koukouei 20141023 「From　>　To」のアラート表示タイミング変更 end

        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            switch(this.Ichiran.Columns[e.ColumnIndex].Name)
            {
                case ("TEIKI_HAISHA_NUMBER"):
                    // 配車番号の前回値を取得
                    var haishaNo = this.Ichiran.Rows[e.RowIndex].Cells["TEIKI_HAISHA_NUMBER"].Value;
                    if (haishaNo != null)
                    {
                        this.logic.beforeHaishaNumber = haishaNo.ToString();
                    }
                    break;
                case ("COURSE_NAME_CD"):
                    // 配車番号の前回値を取得
                    var courseNameCd = Convert.ToString(this.Ichiran.Rows[e.RowIndex].Cells["COURSE_NAME_CD"].Value);
                    this.logic.beforeCd = courseNameCd;
                    break;
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
        /// 一覧編集ボックス表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.Ichiran_KeyDown;
            e.Control.KeyDown += this.Ichiran_KeyDown;
            e.Control.TextChanged -= this.Control_TextChanged;
            e.Control.TextChanged += this.Control_TextChanged;
        }

        /// <summary>
        /// 一覧編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
            this.logic.CheckKeyDown(e);
        }
        //////////////////////
        //明細チェックボックス
        //////////////////////
        #region 列ヘッダーにチェックボックスを表示
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                if (e.RowIndex == -1)
                {
                    using (Bitmap bmp = new Bitmap(25, 25))
                    {
                        // チェックボックスの描画領域を確保
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(Color.Transparent);
                        }

                        // 描画領域の中央に配置
                        Point pt1 = new Point((bmp.Width - checkBoxAll.Width) / 2, (bmp.Height - checkBoxAll.Height) / 2);
                        if (pt1.X < 0) pt1.X = 0;
                        if (pt1.Y < 0) pt1.Y = 0;

                        // Bitmapに描画
                        checkBoxAll.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                        // DataGridViewの現在描画中のセルの中央に描画
                        int x = (e.CellBounds.Width - bmp.Width);
                        int y = (e.CellBounds.Height - bmp.Height) / 2;

                        Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                        e.Paint(e.ClipBounds, e.PaintParts);
                        e.Graphics.DrawImage(bmp, pt2);
                        e.Handled = true;
                    }
                }
            }
        }
        #endregion
        #region 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //[臨時]OFF→[臨時]ON時のチェック
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {

                //スペースで、OFFの場合は抜ける
                if (((bool)this.logic.SpaceChk) && (!(bool)this.logic.SpaceON))
                {
                    return;
                } 
                this.logic.SpaceON = false;

                string CheckUketsukeNumber = this.Ichiran[int.Parse(this.Ichiran.Columns["UKETSUKE_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString();

                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.Ichiran[1, e.RowIndex];
                if (cell.Value != null)
                {
                    if ((!(bool)cell.Value) || this.logic.SpaceChk)
                    {
                        //受付伝票が、ﾓﾊﾞｲﾙ連携されているか 
                        if (this.logic.RenkeiCheck(2, CheckUketsukeNumber))
                        {
                            //this.IchiranLogic.SpaceChk(TRUE)⇒Value(FALSE)で戻す
                            //this.IchiranLogic.SpaceChk(FALSE)⇒Value(TRUE)で戻す
                            this.Ichiran[1, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、臨時に変更する事は出来ません。");
                            return;
                        }
                        //受付伝票が、ロジコン連携されているか
                        if (this.logic.RenkeiCheck(1, CheckUketsukeNumber))
                        {
                            this.Ichiran[1, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("ロジコンパス連携中の為、臨時に変更する事は出来ません。");
                            return;
                        }
                        //定期配車に組み込んだ受付の情報が、ﾓﾊﾞｲﾙ連携されているか
                        if (this.logic.RenkeiCheck(5, CheckUketsukeNumber))
                        {
                            this.Ichiran[1, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、臨時に変更する事は出来ません。");
                            return;
                        }
                        //[臨時]チェックON→[ﾓﾊﾞｲﾙ連携]チェックOFF
                        DataGridViewCheckBoxCell cell2 = (DataGridViewCheckBoxCell)this.Ichiran[1, e.RowIndex];
                        if (cell2.Value.ToString() == "False")
                        {
                            this.Ichiran[0, e.RowIndex].Value = false;
                            this.Ichiran.Refresh();
                        }

                        if (this.logic.SpaceChk)
                        {
                            this.Ichiran[1, e.RowIndex].Value = !(bool)this.Ichiran[1, e.RowIndex].Value;
                            this.logic.SpaceChk = false;
                        }
                        return;
                    }
                }
            }

            //[ﾓﾊﾞｲﾙ連携]OFF→[ﾓﾊﾞｲﾙ連携]ON時のチェック
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {

                //スペースで、OFFの場合は抜ける
                if (((bool)this.logic.SpaceChk) && (!(bool)this.logic.SpaceON))
                {
                    return;
                }
                this.logic.SpaceON = false;

                string CheckUketsukeNumber = this.Ichiran[int.Parse(this.Ichiran.Columns["UKETSUKE_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString();
                string CheckTeikiNumber = this.Ichiran[int.Parse(this.Ichiran.Columns["TEIKI_HAISHA_NUMBER"].Index.ToString()), e.RowIndex].Value.ToString();

                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.Ichiran[0, e.RowIndex];
                if (cell.Value != null) 
                {
                    if ((!(bool)cell.Value) || this.logic.SpaceChk)
                    {
                        //[臨時]チェックがON→[ﾓﾊﾞｲﾙ連携]OFF
                        DataGridViewCheckBoxCell cell2 = (DataGridViewCheckBoxCell)this.Ichiran[1, e.RowIndex];
                        if ((bool)cell2.Value)
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            return;
                        }
                        //受付伝票が、ﾓﾊﾞｲﾙ連携されているか →[ﾓﾊﾞｲﾙ連携]OFF
                        if (this.logic.RenkeiCheck(2, CheckUketsukeNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、連携出来ません。");
                            return;
                        }
                        //配車番号が未入力→[ﾓﾊﾞｲﾙ連携]OFF
                        if (string.IsNullOrEmpty(CheckTeikiNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("配車番号が指定されていない為、連携できません。");
                            return;
                        }
                        //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                        //[システム日付] != [作業日]の場合はチェックをつけない
                        if (string.IsNullOrEmpty(this.Ichiran[int.Parse(this.Ichiran.Columns["SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value.ToString()))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                            return;
                        }
                        else
                        {
                            if (!(DateTime.Parse(this.Ichiran[int.Parse(this.Ichiran.Columns["SAGYOU_DATE"].Index.ToString()), e.RowIndex].Value.ToString()).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                            {
                                this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                                this.logic.SpaceChk = false;
                                this.Ichiran.Refresh();
                                this.msgLogic.MessageBoxShowError("作業日が当日の場合のみ連携が可能です。");
                                return;
                            }
                        }
                        //定期配車伝票が、モバイル状況一覧に表示される条件か
                        //UNTENSHA_CD、SHARYOU_CD、SHASHU_CD、SHASHU_NAME_RYAKUのしずれかが無し→×。
                        if (this.logic.RenkeiCheck(6, CheckTeikiNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                            return;
                        }
                        //★振替先の定期配車の業者の取引先有無のが無じゃないかチェックを入れる
                        if (this.logic.RenkeiCheck(7, CheckTeikiNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                            return;
                        }
                        //★業者の取引先有無のが無じゃないかチェックを入れる
                        if (this.logic.RenkeiCheck(8,this.Ichiran[int.Parse(this.Ichiran.Columns["GYOUSHA_CD"].Index.ToString()), e.RowIndex].Value.ToString()))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("モバイル将軍へ連携する条件になっていません。");
                            return;
                        }

                        //定期配車に組み込んだ受付の情報が、ﾓﾊﾞｲﾙ連携されているか
                        if (this.logic.RenkeiCheck(5, CheckUketsukeNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("既にモバイル将軍へ連携されている為、変更する事は出来ません。");
                            return;
                        }
                        //配車番号が、ロジコン連携されているか
                        if (this.logic.RenkeiCheck(3, CheckTeikiNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("ロジこんぱす連携中の為、変更する事は出来ません。");
                            return;
                        }
                        //配車番号が、NAVITIME連携されているか
                        if (this.logic.RenkeiCheck(4, CheckTeikiNumber))
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.logic.SpaceChk;
                            this.logic.SpaceChk = false;
                            this.Ichiran.Refresh();
                            this.msgLogic.MessageBoxShowError("NAVITIME連携中の為、変更する事は出来ません。");
                            return;
                        }
                        if (this.logic.SpaceChk)
                        {
                            this.Ichiran[0, e.RowIndex].Value = !(bool)this.Ichiran[0, e.RowIndex].Value;
                            this.logic.SpaceChk = false;
                        }
                    }
                }
            }
            //初期化
            this.logic.SpaceChk = false;

            //[ﾓﾊﾞｲﾙ連携]のヘッダをクリック
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                checkBoxAll.Checked = !checkBoxAll.Checked;
                this.Ichiran.Refresh();
            }
        }
        #endregion

        /// <summary>
        /// TextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            this.logic.CheckTextChanged(sender);
        }

        /// <summary>
        /// [対象][モバイル連携]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.Ichiran.CurrentCell;
                //[モバイル連携]
                if (curCell.ColumnIndex == 0 && curCell.RowIndex >= 0)
                {
                    this.logic.SpaceChk = true;
                    this.logic.SpaceON = false;
                    //[モバイル連携]OFFにする場合は、何もしない。
                    //[モバイル連携]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                    if (!(bool)this.Ichiran[0, curCell.RowIndex].Value)
                    {
                        this.logic.SpaceON = true;
                        this.Ichiran[0, curCell.RowIndex].Value = !(bool)this.Ichiran[0, curCell.RowIndex].Value;
                        //this.Ichiran.Refresh();
                    }
                }

                //[臨時]
                if (curCell.ColumnIndex == 1 && curCell.RowIndex >= 0)
                {
                    this.logic.SpaceChk = true;
                    this.logic.SpaceON = false;
                    //[臨時]OFFにする場合は、何もしない。
                    //[臨時]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                    if (!(bool)this.Ichiran[1, curCell.RowIndex].Value)
                    {
                        this.logic.SpaceON = true;
                        this.Ichiran[1, curCell.RowIndex].Value = !(bool)this.Ichiran[1, curCell.RowIndex].Value;
                        //this.Ichiran.Refresh();
                    }
                }
            }
        }

        #region 明細全チェック
        /// <summary>
        /// [ﾓﾊﾞｲﾙ連携]全チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Ichiran.Rows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in this.Ichiran.Rows)
            {

                string CheckUketsukeNumber = row.Cells["UKETSUKE_NUMBER"].Value.ToString();
                string CheckTeikiNumber = row.Cells["TEIKI_HAISHA_NUMBER"].Value.ToString();

                //[ﾓﾊﾞｲﾙ連携]ON
                if (checkBoxAll.Checked)
                {
                    //[臨時]チェックがON→[ﾓﾊﾞｲﾙ連携]OFF
                    if ((bool)row.Cells["KUMIAI_SUMI"].Value)
                    {
                        continue;
                    }
                    //配車番号が未入力→[ﾓﾊﾞｲﾙ連携]OFF
                    if (string.IsNullOrEmpty(CheckTeikiNumber))
                    {
                        continue;
                    }
                    //作業日 != 当日→[ﾓﾊﾞｲﾙ連携]OFF
                    //[システム日付] != [作業日]の場合はチェックをつけない
                    if (string.IsNullOrEmpty(row.Cells["SAGYOU_DATE"].Value.ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        if (!(DateTime.Parse(row.Cells["SAGYOU_DATE"].Value.ToString()).ToString("yyyy/MM/dd")).Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                        {
                            continue;
                        }
                    }

                    //2:受付伝票が、ﾓﾊﾞｲﾙ連携されているか →[ﾓﾊﾞｲﾙ連携]OFF
                    //6:定期配車伝票が、モバイル状況一覧に表示される条件か
                    //5:定期配車に組み込んだ受付の情報が、ﾓﾊﾞｲﾙ連携されているか
                    //3:配車番号が、ロジコン連携されているか
                    //4:配車番号が、NAVITIME連携されているか
                    //7:振替先の定期配車の業者の取引先有無のが無じゃないかチェックを入れる
                    //8:業者の取引先有無のが無じゃないかチェックを入れる
                    if (this.logic.RenkeiCheck(2, CheckUketsukeNumber) || this.logic.RenkeiCheck(6, CheckTeikiNumber) || this.logic.RenkeiCheck(5, CheckUketsukeNumber)
                        || this.logic.RenkeiCheck(3, CheckTeikiNumber) || this.logic.RenkeiCheck(4, CheckTeikiNumber) || this.logic.RenkeiCheck(7, CheckTeikiNumber)
                        || this.logic.RenkeiCheck(8, row.Cells["GYOUSHA_CD"].Value.ToString()))
                    {
                        continue;
                    }
                    row.Cells[0].Value = checkBoxAll.Checked;
                }
                else
                {
                    row.Cells[0].Value = checkBoxAll.Checked;
                }
            }
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[0];
            this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];
        }
        #endregion



    }
}
