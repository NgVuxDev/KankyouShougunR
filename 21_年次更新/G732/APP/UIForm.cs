using System;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        public UIForm()
            : base(WINDOW_ID.RIREKI_DEUTA_SAKUJO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //初期化、初期表示
            if (!this.Logic.WindowInit())
            {
                return;
            }

        }

        /// <summary>
        /// データ削除(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {

            //データ削除対象があるか確認
            if (this.EigyouRange_2.Checked && this.UkersukeRange_2.Checked && this.HaishaRange_2.Checked && this.KeiryouRange_2.Checked && 
                this.HankanRange_2.Checked && this.ManifestRange_2.Checked && this.UnchinRange_2.Checked && this.RenkeiRange_2.Checked)
            {
                this.Logic.errmessage.MessageBoxShowError("削除対象が1件も選択されていません。");
                return;
            }

            //日付チェック
            if (this.Logic.errmessage.MessageBoxShowConfirm("履歴データ削除をします。\nよろしいですか？") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //処理実行
            this.Logic.jikkou();

        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        #region 表示切替
        /// <summary>
        /// 営業データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_EigyouRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_EigyouRange.Text.Equals("1"))
            {
                this.EIGYOU_DAY.Enabled = true;
            }
            else
            {
                this.EIGYOU_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 受付データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_UketsukeRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_UketsukeRange.Text.Equals("1"))
            {
                this.UKETSUKE_DAY.Enabled = true;
            }
            else
            {
                this.UKETSUKE_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 配車データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_HaishaRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_HaishaRange.Text.Equals("1"))
            {
                this.HAISHA_DAY.Enabled = true;
            }
            else
            {
                this.HAISHA_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 計量データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_KeiryouRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_KeiryouRange.Text.Equals("1"))
            {
                this.KEIRYOU_DAY.Enabled = true;
            }
            else
            {
                this.KEIRYOU_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 販管データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_HankanRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_HankanRange.Text.Equals("1"))
            {
                this.HANKAN_DAY.Enabled = true;
            }
            else
            {
                this.HANKAN_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// マニフェストデータ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ManifestRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_ManifestRange.Text.Equals("1"))
            {
                this.MANIFEST_DAY.Enabled = true;
            }
            else
            {
                this.MANIFEST_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 運賃データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_UnchinRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_UnchinRange.Text.Equals("1"))
            {
                this.UNCHIN_DAY.Enabled = true;
            }
            else
            {
                this.UNCHIN_DAY.Enabled = false;
            }
        }

        /// <summary>
        /// 外部連携データ削除する/しないで、日付の使用可不可の切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_RenkeiRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_RenkeiRange.Text.Equals("1"))
            {
                this.RENKEI_DAY.Enabled = true;
            }
            else
            {
                this.RENKEI_DAY.Enabled = false;
            }
        }
        #endregion

    }
}
