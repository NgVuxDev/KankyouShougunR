using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.DTO
{
    /// <summary>
    /// 拡張テーブル用DTOクラス
    /// </summary>
    internal class DTODTExClass
    {
        /// <summary>
        /// DT_R18_EX
        /// </summary>
        internal DT_R18_EX dtR18ExEntity { get; set; }

        /// <summary>
        /// DT_R19_EX
        /// </summary>
        internal DT_R19_EX[] dtR19ExEntityList { get; set; }

        /// <summary>
        /// DT_R04_EX
        /// </summary>
        internal DT_R04_EX[] dtR04ExEntityList { get; set; }

        /// <summary>
        /// DT_R08_EX
        /// </summary>
        internal DT_R08_EX[] dtR08ExEntityList { get; set; }

        /// <summary>
        /// DT_R13_EX
        /// </summary>
        internal DT_R13_EX[] dtR13ExEntityList { get; set; }
    }
}
