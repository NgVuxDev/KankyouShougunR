using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Logic;
using r_framework.Utility;
namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    public partial class PopupForm : SuperPopupForm
    {

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        public PopupLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public string sql { get; set; }

        public PopupForm()
        {
            InitializeComponent();
            this.customDataGridView1.IsBrowsePurpose = true;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic = new PopupLogic(this);

            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            var displayColumnName = (this.PopupGetMasterField ?? "").Split(',').Select(s => s.Trim().ToUpper());
            if (this.table != null && displayColumnName.Count() > 0)
            {
                if (!string.IsNullOrWhiteSpace(this.table.TableName))
                {
                    this.Text = this.table.TableName;
                }
                var removeColumnsName = this.table.Columns.OfType<DataColumn>().Where(s => !displayColumnName.Contains(s.ColumnName)).Select(s => s.ColumnName).ToList();
                removeColumnsName.ForEach(s => this.table.Columns.Remove(s));

                this.logic.DataGridViewLoad(this.table, this.PopupDataHeaderTitle);
            }

            // 表示を調整
            catchErr = this.logic.CordinateGridSize();
            if (catchErr)
            {
                return;
            }
            this.bt_func12.Click += this.FormClose;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
