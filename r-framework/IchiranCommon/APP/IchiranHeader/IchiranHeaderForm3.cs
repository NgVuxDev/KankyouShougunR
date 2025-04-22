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
    public partial class IchiranHeaderForm3 : Form
    {
        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 345;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        [Obsolete("ヘッダーフォームは各画面プロジェクトにて用意します。")]
        public IchiranHeaderForm3()
        {
            InitializeComponent();

            //FW内のフォームは、うまくプロパティが保存されないため、個々で自前記述

            this.dtFrom.FocusOutCheckMethod = new Collection<SelectCheckDto>();
            this.dtFrom.RegistCheckMethod = new Collection<SelectCheckDto>();
            this.dtTo.FocusOutCheckMethod = new Collection<SelectCheckDto>();
            this.dtTo.RegistCheckMethod = new Collection<SelectCheckDto>();


            var dto = new SelectCheckDto();
            dto.CheckMethodName = "日付整合性チェック(From用)";
            dto.SendParams = new[] { "dtTo" };
            this.dtFrom.FocusOutCheckMethod.Add(dto);
            this.dtFrom.RegistCheckMethod.Add(dto);

            dto = new SelectCheckDto();
            dto.CheckMethodName = "日付整合性チェック(To用)";
            dto.SendParams = new[] { "dtFrom" };
            this.dtTo.FocusOutCheckMethod.Add(dto);
            this.dtTo.RegistCheckMethod.Add(dto);


            this.HEADER_KYOTEN_CD.FocusOutCheckMethod = new Collection<SelectCheckDto>();
            dto = new SelectCheckDto();
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
