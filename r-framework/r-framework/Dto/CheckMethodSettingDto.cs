
namespace r_framework.Dto
{
    /// <summary>
    /// チェックメソッド情報を格納するDto
    /// </summary>
    public class CheckMethodSettingDto
    {
        /// <summary>
        /// チェックメソッド名(日本語)
        /// </summary>
        public string CheckMethodName { get; set; }

        /// <summary>
        /// 格納されているアセンブリ名
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 実行対象のクラスが格納されている名前空間
        /// </summary>
        public string ClassNameSpace { get; set; }

        /// <summary>
        /// チェック実行するメソッド
        /// </summary>
        public string MethodName { get; set; }
    }
}
