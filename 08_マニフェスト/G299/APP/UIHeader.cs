using System;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.ManifestPattern
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;
        
        //アラート件数
        public Int32 NumberAlert = 0;

        // 20140604 syunrei No.730 マニフェストパターン一覧 start
        //廃棄区分        
        public String haikiKbnHeader = String.Empty;
        public string[] useInfo = new string[] {string.Empty};
        // 20140604 syunrei No.730 マニフェストパターン一覧 end
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader(String paramIn_hakikubun,  String[] paramIn_useInfo)
            : base()
        {
            InitializeComponent();
            // 20140604 syunrei No.730 マニフェストパターン一覧 start
            if (!String.IsNullOrEmpty(paramIn_hakikubun))
            {
                //廃棄区分
                this.haikiKbnHeader = paramIn_hakikubun; 
            }
            //拠点

            if (paramIn_useInfo.Length > 0)
            {
                this.useInfo = paramIn_useInfo;
            }

            // 20140604 syunrei No.730 マニフェストパターン一覧 end       

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false ;
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);   
       
            //拠点
            this.KYOTEN_CD.LostFocus += new EventHandler(KYOTEN_CD_LostFocus);


            //アラート件数
            this.AlertNumber.LostFocus += new EventHandler(NUMBER_ALERT_LostFocus);

            // 20140604 syunrei No.730 マニフェストパターン一覧 start
            //廃棄区分より拠点の使用可否制御
            if (this.haikiKbnHeader.Equals("4"))
            {
                this.label2.Visible = false;
                this.KYOTEN_CD.Visible = false;
                this.KYOTEN_NAME.Visible = false;
            }
            else
            {
                if (useInfo.Length > 0)
                {
                    this.KYOTEN_CD.Text = this.useInfo[0];
                    this.KYOTEN_NAME.Text = this.useInfo[1];
                }
               
            }
            // 20140604 syunrei No.730 マニフェストパターン一覧 end
        
        }

        #region 画面コントロールイベント
        
        /// <summary>
        /// 拠点
        /// </summary>
        private void KYOTEN_CD_LostFocus(object sender, EventArgs e)
        {
        }

        private void KYOTEN_CD_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.KYOTEN_CD = this.KYOTEN_CD.Text;
        }

        private void KYOTEN_CD_Validated(object sender, EventArgs e)
        {
            Properties.Settings.Default.KYOTEN_NAME = this.KYOTEN_NAME.Text;
        }

        /// <summary>
        /// アラート件数
        /// </summary>
        private void NUMBER_ALERT_LostFocus(object sender, EventArgs e)
        {
            this.AlertNumber.Text = this.NumberAlert.ToString();
        }

        private void NUMBER_ALERT_TextChanged(object sender, EventArgs e)
        {
            if (this.AlertNumber.Text == "")
            {
                this.NumberAlert = this.InitialNumberAlert;
            }
            else
            {
                this.NumberAlert = Int32.Parse(this.AlertNumber.Text.ToString());
            }
        }

        #endregion
    }
}
