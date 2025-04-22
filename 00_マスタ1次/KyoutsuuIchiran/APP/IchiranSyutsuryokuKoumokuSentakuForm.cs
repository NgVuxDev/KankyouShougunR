using System;
using System.Windows.Forms;

namespace KyoutsuuIchiran.APP
{
    /// <summary>
    /// 一覧出力項目選択
    /// </summary>
    public partial class IchiranSyutsuryokuKoumokuSentakuForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranSyutsuryokuKoumokuSentakuForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 閉じるボタン押下時処理
        /// </summary>
        private void button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}