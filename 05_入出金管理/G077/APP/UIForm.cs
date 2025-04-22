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
using Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku;
using Seasar.Quill;

namespace Shougun.Core.ReceiptPayManagement.Nyukinnyuryoku
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        HeaderSample  Header_now;

        internal String Nyuukin_CD;

        internal WINDOW_TYPE Window_type;

        public UIForm(WINDOW_TYPE Gamen_Type, String Nyukin_Number, HeaderSample header)
            : base(WINDOW_ID.T_NYUKIN, Gamen_Type)
        {
            this.InitializeComponent();

            this.Header_now = header;
           
            this.NYUKIN_NO.Text = Nyukin_Number;

            this.Nyuukin_CD = Nyukin_Number;
            　　
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            Logic.SetHeader(Header_now);

            this.Window_type = Gamen_Type;
            if (Gamen_Type == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.Logic.SetGamenHeader(1);
                //this.Window_type = "new";
            }
            if (Gamen_Type == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.Logic.SetGamenHeader(2);
                //this.Window_type = "upd";
            }
            if (Gamen_Type == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                this.Logic.SetGamenHeader(3);
                //this.Window_type = "del";
            }

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        public void CustomDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.Logic.ChangeCell(sender,e);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Logic.WindowInit();
            NYUKIN_NO.LostFocus += new EventHandler(NYUKIN_NO_LostFocus);
        }

        /// <summary>
        /// 新規画面に遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NewAdd(object sender, EventArgs e)
        {
            this.Logic.ChangeGamenMode();
            this.Logic.Clear();           
        }

        /// <summary>
        /// 再検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ReSearch(object sender, EventArgs e)
        {
            this.Logic.ReSearch();
        }
        
        /// <summary>
        /// 登録/更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RegistData(object sender, EventArgs e)
        {           
            //各チェックと更新処理を行う
            this.Logic.ChkBefUpdOrDel();
        }

        /// <summary>
        /// 一覧画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OpenIchiran(object sender, EventArgs e)
        {
            this.Logic.Ichiran();
        }
        
        /// <summary>
        /// 消込抽出ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SearchKeshikomi(object sender, EventArgs e)
        {
            this.Logic.SearchKeshikomi();
        }

        /// <summary>
        /// 消込抽出ボタン処理(2次開発)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Tekata(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            parentForm.Close();
        }

        private void KesikomiIchiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            //変更された行がIsChangedの場合は除く.
            if (this.KesikomiIchiran.Columns[e.ColumnIndex].HeaderText == "変更マーク" )
            {
                return;
            }
            if (this.KesikomiIchiran.Columns[e.ColumnIndex].HeaderText == "入金額")
            {
                this.Logic.ChangeKesikomiGaku(e);
            }
        }

        private void MeisaiIchiran_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //変更された行はマークを掛ける
            if (this.MeisaiIchiran.Rows.Count < 2)
            {
                return;
            }

            //今回合計額、今回入金額と調整額のセット
            this.Logic.DoChangeKingaku();
        }

        private void NYUKIN_NO_LostFocus(object sender, EventArgs e)
        {
            if (this.NYUKIN_NO.Text != "" && this.NYUKIN_NO.ReadOnly != true)
            {
                //消込初期表示Flgを初期化する
                this.Logic.First_Kesikomi_Flg = true;
                this.Logic.FirstSetInit();
            }
        }

        private void UIForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                this.Close();
            }
        }

        public virtual void TORIHIKISAKI_NO_LostFocus(object sender, EventArgs e)
        {
            //TORIHIKISAKI_NO_Validatingを発生するため、イベント追加
        }

        private void TORIHIKISAKI_NO_Validating(object sender, CancelEventArgs e)
        {
            if (this.Logic.IsTorihikiChanged)
            {
                if (!this.Logic.ChangeTorihikisaki(this.TORIHIKISAKI_NO.Text))
                {
                    return;
                }
                this.Logic.IsTorihikiChanged = false;
            }
        }

        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            this.BIKOU_TXT.Text = "";
            if (this.Logic.IsDateChanged)
            {
                if (this.DENPYOU_DATE.Text != " ")
                {
                    if (!this.Logic.PopupInit())
                    {
                        return;
                    }
                    if (!this.Logic.ChangeTorihikisaki(this.TORIHIKISAKI_NO.Text))
                    {
                        return;
                    }
                    this.Logic.IsDateChanged = false;
                }
            }
        }

        public virtual void DENPYOU_DATE_LostFocus(object sender, EventArgs e)
        {
            //DENPYOU_DATE_Validatingを発生するため、イベント追加
            this.BIKOU_TXT.Text  = "";
        }

        private void DENPYOU_DATE_ValueChanged(object sender, EventArgs e)
        {
            this.Logic.IsDateChanged = true;
        }

        private void TORIHIKISAKI_NO_TextChanged(object sender, EventArgs e)
        {
            this.Logic.IsTorihikiChanged = true;
        }

        private void KesikomiIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Logic.ChangeMoneyValue_NoGanma_Kesikomi(e);
        }

        private void KesikomiIchiran_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.Logic.ChangeMoneyValue_HaveGanma_Kesikomi(e);
        }

        private void MeisaiIchiran_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.Logic.ChangeMoneyValue_NoGanma_Meisai(e);
        }

        private void MeisaiIchiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.Logic.ChangeMoneyValue_HaveGanma_Meisai(e);
        }

        private void TORIHIKISAKI_NAME_TextChanged(object sender, EventArgs e)
        {
            this.Logic.ChangeTorihikisakiRyakusyo(this.TORIHIKISAKI_NO.Text);
        }

        private void MeisaiIchiran_KeyDown(object sender, KeyEventArgs e)
        {
            //Enterキーが押されているか確認
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;　　　　　　
　　　　　      SendKeys.Send("{TAB}")　;　　　
            }
        }

    }
}
