using System;
using System.Collections.Generic;
using System.Linq;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestShukeihyo
{
    /// <summary>
    /// CSVのDTOクラス
    /// </summary>
    public class CsvColumnDto
    {
        // CSV表示用
        public string csvColumnCD { get; set; }
        public string csvColumnName { get; set; }
        // データ取得用
        public string tableColumnCD { get; set; }
        public string tableColumnName { get; set; }
    }
}
