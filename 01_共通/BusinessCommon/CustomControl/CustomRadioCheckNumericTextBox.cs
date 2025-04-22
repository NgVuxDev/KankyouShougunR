using System;
using r_framework.Utility;

namespace Shougun.Core.Common.BusinessCommon.CustomControl
{
    public class CustomRadioCheckNumericTextBox : r_framework.CustomControl.CustomNumericTextBox2
    {
        /// <summary>
        /// ラジオボタン風チェックボックス用テキストボックス
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            // フォームにaddされるまでは、動かない。
            if (this.FindForm() == null)
            {
                return;
            }

            // フォーマットは先にしないと、リンクに支障が出る。範囲チェックは後でも良い
            if (!this.Focused)
            {
                var textLogic = new r_framework.Logic.CustomTextBoxLogic(this);

                // 自動フォーマット処理
                textLogic.Format(this);
            }

            // イベントが動くより前にリンクする
            if (LinkedRadioButtonArray.Length != 0)
            {
                // ラジオボタンリンク処理
                ControlUtility controlUtil = new ControlUtility();
                controlUtil.ControlCollection = this.FindForm().Controls;

                foreach (var radioButtonName in LinkedRadioButtonArray)
                {
                    CustomRadioCheckBox radioButton = (CustomRadioCheckBox)controlUtil.GetSettingField(radioButtonName);
                    if (radioButton.Value == this.Text)
                    {
                        radioButton.Checked = true; // Trueにだけする、falseはしない（他のfalseは、チェックボックス側で行う）
                    }
                }
            }

            var bk = LinkedRadioButtonArray;
            try
            {
                this.LinkedRadioButtonArray = new string[] { }; // 0個の配列にして内部処理させない
                // FWの処理を動かす フォーマット等はこっち
                base.OnTextChanged(e);
            }
            finally
            {
                this.LinkedRadioButtonArray = bk; // 必ず戻す
            }
        }
    }
}