using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Utility;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using System.Drawing;
using r_framework.CustomControl;

namespace Shougun.Core.PaperManifest.JissekiHokokuCsv
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        private DialogResult m_Result = DialogResult.Cancel;

        public UIForm(T_JISSEKI_HOUKOKU_ENTRY entry, bool SyukeiariFlg)
            : base(WINDOW_ID.T_JISSEKIHOKOKU_CSV, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this, entry, SyukeiariFlg);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

               

                if (!this.Logic.WindowInit())
                {
                    var parentForm = (BasePopForm)this.Parent;
                    parentForm.Visible = false;
                    //画面クローズ
                    parentForm.DialogResult = m_Result;
                    this.Close();
                    parentForm.Close();
                };

                base.OnLoad(e);

                foreach (Control cotr in this.allControl)
                {
                    if (cotr is CustomCheckBox)
                    {
                        ((CustomCheckBox)cotr).Enter += new EventHandler(this.CustomCheckBoxEnter);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("OnLoad", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (BasePopForm)this.Parent;

                //画面クローズ
                parentForm.DialogResult = m_Result;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 排出事業者全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHstOn_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetHstJigyoushaVal(true);

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 排出事業者全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHstOff_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetHstJigyoushaVal(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者（一次）全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnpanOn1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetUpJutakushaVal1(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者（一次）全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnpanOff1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetUpJutakushaVal1(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 積替保管（一次）全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTkHokanOn1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetTkHokanVal1(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 積替保管（一次）全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTkHokanOff1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetTkHokanVal1(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分受託者全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShobunJutakushaOn_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetShobunJutakushaVal(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処分受託者全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShobunJutakushaOff_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetShobunJutakushaVal(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者（二次）全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpJutakushaOn2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetUpJutakushaVal2(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬受託者（二次）全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpJutakushaOff2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetUpJutakushaVal2(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 委託先全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItakuOn_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetItakuVal(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 委託先全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItakuOff_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetItakuVal(false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 積替保管（二次）全選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTkHokanOn2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetTkHokanVal2(true);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 積替保管（二次）全解除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTkHokanOff2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SetTkHokanVal2(false);

            LogUtility.DebugMethodEnd();
        }

        internal void CustomCheckBoxEnter(object sender, EventArgs e)
        {
            ((CustomCheckBox)sender).BackColor = ((CustomCheckBox)sender).Parent.BackColor;
        }
    }
}
