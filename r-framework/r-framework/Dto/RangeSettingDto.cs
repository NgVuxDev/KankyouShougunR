using System.ComponentModel;

namespace r_framework.Dto
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RangeSettingDto
    {
        [Description("入力可能な最大値を指定してください。")]
        public decimal Max { get; set; }
        private bool ShouldSerializeMax()
        {
            return this.Max != decimal.MaxValue;
        }
        internal void ResetMax()
        {
            this.Max = decimal.MaxValue;
        }

        [Description("入力可能な最小値を指定してください。")]
        public decimal Min { get; set; }
        private bool ShouldSerializeMin()
        {
            return this.Min != decimal.Zero;
        }
        internal void ResetMin()
        {
            this.Min = decimal.Zero;
        }

        public RangeSettingDto()
        {
            this.Max = decimal.MaxValue;
            this.Min = decimal.Zero;
        }
    }
}