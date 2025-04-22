using System;
using System.ComponentModel;
using r_framework.CustomControl;
using r_framework.Utility;

namespace Shougun.Core.Common.BusinessCommon.CustomControl
{
    public class CustomRadioCheckBox : r_framework.CustomControl.CustomCheckBox
    {
        [Category("EDISONプロパティ_チェック設定")]
        [Description("チェックが行われたときに連動し、値を反映させるテキストボックス名を入力してください。")]
        public string LinkedTextBox { get; set; }
        private bool ShouldSerializeLinkedTextBox()
        {
            return this.LinkedTextBox != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("ラジオボタンがチェックされている場合に設定返却する値を設定してください。")]
        public string Value { get; set; }
        private bool ShouldSerializeValue()
        {
            return this.Value != null;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (!string.IsNullOrEmpty(LinkedTextBox))
            {
                var ctrlUtil = new ControlUtility();
                ctrlUtil.ControlCollection = this.FindForm().Controls;
                var numTextBox = (CustomNumericTextBox2)ctrlUtil.GetSettingField(LinkedTextBox);
                numTextBox.Focus();

                if (this.Checked)
                {
                    // 他のチェックをOFFにする
                    foreach (var radioButtonName in numTextBox.LinkedRadioButtonArray)
                    {
                        CustomRadioCheckBox radioButton = (CustomRadioCheckBox)ctrlUtil.GetSettingField(radioButtonName);
                        if (radioButton != this)
                        {
                            radioButton.Checked = false;
                        }
                    }

                    // チェックされた場合は、必ず値をセット（先に他のチェックを外してから値セットすること！）
                    numTextBox.Text = this.Value;
                    numTextBox.SelectAll();
                }
                else
                {
                    // 1つでもチェックがある場合は trueになったコントロールのイベントで値はセットされる（falseのコントロールではセット不要）
                    // 無い場合は 空文字をセット
                    bool exist = false;
                    foreach (var radioButtonName in numTextBox.LinkedRadioButtonArray)
                    {
                        CustomRadioCheckBox radioButton = (CustomRadioCheckBox)ctrlUtil.GetSettingField(radioButtonName);
                        if (radioButton.Checked)
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                    {
                        numTextBox.Text = string.Empty;
                    }
                }

                // イベント等は 他のチェックを切り替えてから動かす！（trueが複数いる状態を作らないようにする）
                base.OnCheckedChanged(e);
            }
        }
    }
}