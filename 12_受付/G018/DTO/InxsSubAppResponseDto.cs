using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shougun.Core.Reception.UketsukeMochikomiNyuuryoku
{
    public class InxsSubAppResponseDto
    {
        public int RequestStatus { get; set; } 
        public bool IsUpdateSagyouDate { get; set; }
        public string SagyouDate { get; set; }
        public bool ExecuteDeleteShougunDenpyou { get; set; }
        public List<DetailResponseDto> Detail { get; set; }
    }

    public class DetailResponseDto
    {
        public int RowNo { get; set; }
        public string HinmeiCd { get; set; }
        public decimal Suuryou { get; set; }
    }
}
