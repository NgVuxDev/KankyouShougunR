using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.SampaiManifestoThumiKae.DTO
{
    internal class Parameters
    {
        public string SystemId { get; set; }
        public string RenkeiDenshuKbnCd { get; set; }
        public string RenkeiSystemId { get; set; }
        public string RenkeiMeisaiSystemId { get; set; }
        public int Mode { get; set; }
        public string PtSystemId { get; set; }
        public string Seq { get; set; }
        public string PtSeq { get; set; }
        public string SeqRD { get; set; }
        public string ManifestID { get; set; }
        public string KongoCd { get; set; }

        public void Save()
        {
            Properties.Settings.Default.SystemId = this.SystemId;
            Properties.Settings.Default.RenkeiDenshuKbnCd = this.RenkeiDenshuKbnCd;
            Properties.Settings.Default.RenkeiSystemId = this.RenkeiSystemId;
            Properties.Settings.Default.RenkeiMeisaiSystemId = this.RenkeiMeisaiSystemId;
            Properties.Settings.Default.Mode = this.Mode;
            Properties.Settings.Default.PtSystemId = this.PtSystemId;
            Properties.Settings.Default.Seq = this.Seq;
            Properties.Settings.Default.PtSeq = this.PtSeq;
            Properties.Settings.Default.SeqRD = this.SeqRD;
            Properties.Settings.Default.PtSeq = this.PtSeq;
            Properties.Settings.Default.ManifestID = this.ManifestID;
            Properties.Settings.Default.KongoCd = this.KongoCd;

            Properties.Settings.Default.Save();
        }

        public void Load()
        {
            this.SystemId = Properties.Settings.Default.SystemId;
            this.RenkeiDenshuKbnCd = Properties.Settings.Default.RenkeiDenshuKbnCd;
            this.RenkeiSystemId = Properties.Settings.Default.RenkeiSystemId;
            this.RenkeiMeisaiSystemId = Properties.Settings.Default.RenkeiMeisaiSystemId;
            this.Mode = Properties.Settings.Default.Mode;
            this.PtSystemId = Properties.Settings.Default.PtSystemId;
            this.Seq = Properties.Settings.Default.Seq;
            this.PtSeq = Properties.Settings.Default.PtSeq;
            this.SeqRD = Properties.Settings.Default.SeqRD;
            this.PtSeq = Properties.Settings.Default.PtSeq;
            this.ManifestID = Properties.Settings.Default.ManifestID;
            this.KongoCd = Properties.Settings.Default.KongoCd;
        }

    }

}
