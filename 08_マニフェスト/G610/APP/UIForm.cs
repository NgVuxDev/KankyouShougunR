using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using Seasar.Quill;
using Shougun.Core.PaperManifest.JissekiHokokuUnpan;
using r_framework.Utility;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        internal SearchDto searchDto;

        public UIForm(SearchDto dto)
            : base(WINDOW_ID.T_JISSEKIHOKOKU_UNPAN_CSV, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);
            this.searchDto = dto;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            foreach (Control cotr in this.allControl)
            {
                if (cotr is CustomCheckBox)
                {
                    ((CustomCheckBox)cotr).Enter += new EventHandler(this.CustomCheckBoxEnter);
                }
            }
            if (!this.Logic.WindowInit())
            {
                BasePopForm parentForm = (BasePopForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
        }

        #region ファンクションボタン
        /// <summary>
        /// (F6)CSV出力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Logic.CSVOutput();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// (F12)閉じるイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            BasePopForm parentForm = (BasePopForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 全選択ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnZenOn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Logic.allControl)
            {
                if (this.searchDto.CSV_SHUKEI_KBN == 1)
                {
                    if (c.Name.Equals("cbx_KoufuNo")) { continue; }
                    if (c.Name.Equals("cbx_KoufuDate")) { continue; }
                }
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = true;
                }
            }
        }

        /// <summary>
        /// 全解除ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnZenOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Logic.allControl)
            {
                if (this.searchDto.CSV_SHUKEI_KBN == 1)
                {
                    if (c.Name.Equals("cbx_KoufuNo")) { continue; }
                    if (c.Name.Equals("cbx_KoufuDate")) { continue; }
                }
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = false;
                }
            }
        }

        /// <summary>
        /// 排出事業者全選択ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnHaishutuZenOn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbHstJigyousha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = true;
                }
            }
        }

        /// <summary>
        /// 排出事業者全解除ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnHaishutuZenOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbHstJigyousha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = false;
                }
            }
        }

        /// <summary>
        /// 運搬受託者全選択ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnUnpanZenOn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbUpJutakusha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = true;
                }
            }
        }

        /// <summary>
        /// 運搬受託者全解除ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnUnpanZenOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbUpJutakusha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = false;
                }
            }
        }

        /// <summary>
        /// 積替保管全選択ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnTumikaeZenOn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbTumikaeHokan.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = true;
                }
            }
        }

        /// <summary>
        /// 積替保管全解除ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnTumikaeZenOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbTumikaeHokan.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = false;
                }
            }
        }

        /// <summary>
        /// 処分受託者全選択ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnShobunZenOn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbShobunJutakusha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = true;
                }
            }
        }

        /// <summary>
        /// 処分受託者全解除ボタンClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnShobunZenOff_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.gbShobunJutakusha.Controls)
            {
                if (c is CustomCheckBox)
                {
                    (c as CustomCheckBox).Checked = false;
                }
            }
        }

        internal void CustomCheckBoxEnter(object sender, EventArgs e)
        {
            ((CustomCheckBox)sender).BackColor = ((CustomCheckBox)sender).Parent.BackColor;
        }
        #endregion
    }
}
