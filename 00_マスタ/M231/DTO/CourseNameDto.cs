using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.CourseNameHoshu.DTO
{
    public class CourseNameDto
    {
        /// <summary>
        /// 削除フラグ    :   DELETE_FLG
        /// </summary>
        public SqlBoolean DELETE_FLG { get; set; }
        /// <summary>
        /// コース名称CD    :   COURSE_NAME_CD
        /// </summary>
        public String COURSE_NAME_CD { get; set; }
        /// <summary>
        /// コース名称    :   COURSE_NAME
        /// </summary>
        public String COURSE_NAME { get; set; }
        /// <summary>
        /// コース名称略称名    :   COURSE_NAME_RYAKU
        /// </summary>
        public String COURSE_NAME_RYAKU { get; set; }
        /// <summary>
        /// コース名称ふりがな    :   COURSE_NAME_FURIGANA
        /// </summary>
        public String COURSE_NAME_FURIGANA { get; set; }
        /// <summary>
        /// 拠点CD    :   KYOTEN_CD
        /// </summary>
        public String KYOTEN_CD { get; set; }
        /// <summary>
        /// 拠点略称名    :   KYOTEN_NAME_RYAKU
        /// </summary>
        public String KYOTEN_NAME_RYAKU { get; set; }
        /// <summary>
        /// 月曜日    :   MONDAY
        /// </summary>
        public SqlBoolean MONDAY { get; set; }
        /// <summary>
        /// 火曜日    :   TUESDAY
        /// </summary>
        public SqlBoolean TUESDAY { get; set; }
        /// <summary>
        /// 水曜日    :   WEDNESDAY
        /// </summary>
        public SqlBoolean WEDNESDAY { get; set; }
        /// <summary>
        /// 木曜日    :   THURSDAY
        /// </summary>
        public SqlBoolean THURSDAY { get; set; }
        /// <summary>
        /// 金曜日    :   FRIDAY
        /// </summary>
        public SqlBoolean FRIDAY { get; set; }
        /// <summary>
        /// 土曜日    :   SATURDAY
        /// </summary>
        public SqlBoolean SATURDAY { get; set; }
        /// <summary>
        /// 日曜日    :   SUNDAY
        /// </summary>
        public SqlBoolean SUNDAY { get; set; }
        /// <summary>
        /// コース名備考    :   COURSE_NAME_BIKOU
        /// </summary>
        public String COURSE_NAME_BIKOU { get; set; }
        /// <summary>
        /// 作成者    :   CREATE_USER
        /// </summary>
        public String CREATE_USER { get; set; }
        /// <summary>
        /// 作成日時    :   CREATE_DATE
        /// </summary>
        public String CREATE_DATE { get; set; }
        /// <summary>
        /// 最終更新者    :   UPDATE_USER
        /// </summary>
        public String UPDATE_USER { get; set; }
        /// <summary>
        /// 最終更新日時    :   UPDATE_DATE
        /// </summary>
        public String UPDATE_DATE { get; set; }
    }
}
