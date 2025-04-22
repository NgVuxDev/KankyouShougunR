using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using r_framework.Dto;
using r_framework.Utility;

namespace Shougun.Core.Common.IchiranCommon.APP
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
        [Obsolete("ヘッダーフォームは各画面プロジェクトにて用意します。")]
        public IchiranHeaderForm1()
        {
            InitializeComponent();

            this.HEADER_KYOTEN_CD.FocusOutCheckMethod = new Collection<SelectCheckDto>();
            var dto = new SelectCheckDto();
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
