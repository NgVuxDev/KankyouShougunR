
namespace r_framework.Dto
{
    /// <summary>
    /// ポップアップ返却値
    /// </summary>
    public class PopupReturnParam
    {
        /// <summary>
        /// キー
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 値
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// クローン作成
        /// </summary>
        /// <returns></returns>
        public PopupReturnParam Clone()
        {
            return (PopupReturnParam)MemberwiseClone();
        }
    }
}
