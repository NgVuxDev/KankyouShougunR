using System;
using System.ComponentModel;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 「時」コンボボックス
    /// </summary>
    public sealed partial class CustomHourComboBox : CustomComboBox
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomHourComboBox()
        {
            InitializeComponent();
        }

        #region Property

        [Category("EDISONプロパティ_チェック設定")]
        [Description("「分」入力コンボボックスが存在する場合は対象のName属性を設定してください。")]
        public string LinkedMinuteComboBox { get; set; }

        #endregion

        #region Method

        /// <summary>
        /// Itemsに「時」リストをセットします（1時間刻みで0時～23時）
        /// </summary>
        public void SetItems()
        {
            this.SetItems(1);
        }

        /// <summary>
        /// Itemsに「時」リストをセットします（interval時間刻みで0時～23時）
        /// </summary>
        /// <param name="interval">間隔</param>
        public void SetItems(int interval)
        {
            this.Items.Clear();
            this.Items.Add(String.Empty);
            for (int i = 0; i < 24; i = i + interval)
            {
                this.Items.Add(i.ToString());
            }
        }

        #endregion
    }
}
