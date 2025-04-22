using System;
using System.Windows.Forms;
using DenManiJigyoujouMihimodukeIchiran.Logic;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace DenManiJigyoujouMihimodukeIchiran.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class DenManiJigyoujouMihimodukeIchiranForm : SuperForm
    {
        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private DenManiJigyoujouMihimodukeIchiranLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private string gyoushaBef { get; set; }
        private string gyoushaAft { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenManiJigyoujouMihimodukeIchiranForm()
            : base(WINDOW_ID.M_DENSHI_JIGYOUJOU_MIHIMODUKE_ICHIRAN, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiJigyoujouMihimodukeIchiranLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
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
            
            // 業者区分名設定処理
            this.logic.SetGyoushaKbnName();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                return;
            }
            else if (count < 0)
            {
                return;
            }

            this.logic.SetIchiran();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.GYOUSHA_CD_TEXT = this.GYOUSHA_CD.Text;
            Properties.Settings.Default.GENBA_CD_TEXT = this.GENBA_CD.Text;
            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// コードから名称を表示する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals("JIGYOUSHA_KBN"))
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "排出事業者";
                        Ichiran.Rows[e.RowIndex].Cells["JIGYOUSHA_KBN_NAME"].Value = e.Value;
                        break;
                    case "2":
                        e.Value = "運搬業者";
                        Ichiran.Rows[e.RowIndex].Cells["JIGYOUSHA_KBN_NAME"].Value = e.Value;
                        break;
                    case "3":
                        e.Value = "処分業者";
                        Ichiran.Rows[e.RowIndex].Cells["JIGYOUSHA_KBN_NAME"].Value = e.Value;
                        break;
                }
            }
        }

        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
        /// <summary>
        /// 業者Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.befGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.SetGyousha();
        }

        /// <summary>
        /// 業者設定
        /// </summary>
        public void SetGyousha()
        {
            this.logic.GyoushaValidated();
        }

        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.GenbaValidated();
        }
        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

        /// <summary>
        /// 業者POPUP_BEFイベント
        /// </summary>
        public void GYOUSHA_POPUP_BEF()
        {
            gyoushaBef = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者POPUP_AFTイベント
        /// </summary>
        public void GYOUSHA_POPUP_AFT()
        {
            gyoushaAft = this.GYOUSHA_CD.Text;
            if (gyoushaBef != gyoushaAft)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
            }
        }
    }
    
}
