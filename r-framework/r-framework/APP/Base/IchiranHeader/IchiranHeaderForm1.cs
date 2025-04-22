using System;
using System.Windows.Forms;
using r_framework.Utility;

namespace r_framework.APP.Base.IchiranHeader
{
    /// <summary>
    /// 一覧用ヘッダーForm
    /// </summary>
    [Obsolete("ヘッダーフォームは各画面プロジェクトにて用意します。")]
    public partial class IchiranHeaderForm1 : Form
    {
        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranHeaderForm1()
        {
            InitializeComponent();

            this.HEADER_KYOTEN_CD.FocusOutCheckMethod = new System.Collections.ObjectModel.Collection<Dto.SelectCheckDto>();
            var dto = new Dto.SelectCheckDto();
            dto.CheckMethodName = "拠点マスタコードチェックandセッティング";
            this.HEADER_KYOTEN_CD.FocusOutCheckMethod.Add(dto);

            this.HEADER_KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.HEADER_KYOTEN_NAME.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.HEADER_KYOTEN_CD.SetFormField = this.HEADER_KYOTEN_CD.Name + "," + this.HEADER_KYOTEN_NAME.Name;
            this.HEADER_KYOTEN_CD.GetCodeMasterField = this.HEADER_KYOTEN_CD.DBFieldsName + "," + this.HEADER_KYOTEN_NAME.DBFieldsName;

            dto = null;
        }

        /// <summary>
        /// ラベルタイトルテキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }
    }
}
