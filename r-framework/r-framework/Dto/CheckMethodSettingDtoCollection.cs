
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace r_framework.Dto
{
    public class CheckMethodSettingDtoCollection : Collection<SelectCheckDto>
    {
        //[TypeConverter(typeof(CheckMethodConverter))]
        //public string CheckMethodName { get; set; }

        public CheckMethodSettingDtoCollection()
            : base()
        {
        }

        public CheckMethodSettingDtoCollection(IList<SelectCheckDto> list)
            : base(list)
        {
        }
    }
}
