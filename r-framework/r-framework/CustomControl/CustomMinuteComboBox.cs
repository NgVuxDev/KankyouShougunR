using System;
using System.ComponentModel;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 「分」コンボボックス
    /// </summary>
    public sealed partial class CustomMinuteComboBox : CustomComboBox
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomMinuteComboBox()
        {
            InitializeComponent();
        }

        #region Property

        [Category("EDISONプロパティ_チェック設定")]
        [Description("「時間」入力コンボボックスが存在する場合は対象のName属性を設定してください。")]
        public string LinkedHourComboBox { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// Itemsに「分」リストをセットします（1分刻みで0分～59分）
        /// </summary>
        public void SetItems()
        {
            this.SetItems(1);
        }

        /// <summary>
        /// Itemsに「分」リストをセットします（interval分刻みで0分～59分）
        /// </summary>
        /// <param name="interval">間隔</param>
        public void SetItems(int interval)
        {
            this.Items.Clear();
            this.Items.Add(String.Empty);
            for (int i = 0; i < 60; i = i + interval)
            {
                this.Items.Add(i.ToString());
            }
        }

        #endregion
    }
}
