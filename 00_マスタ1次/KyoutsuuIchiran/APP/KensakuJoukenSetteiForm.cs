using System;
using System.Windows.Forms;

namespace KyoutsuuIchiran.APP
{
    /// <summary>
    /// 検索条件設定ボタン
    /// </summary>
    public partial class KensakuJoukenSetteiForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KensakuJoukenSetteiForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 閉じるボタン押下処理
        /// </summary>
        private void button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}