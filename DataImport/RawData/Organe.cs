using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace RICAssemblee.DataImport.RawData
{
    internal partial class RawOrgane
    {
        [JsonProperty("organe", NullValueHandling = NullValueHandling.Ignore)]
        public Organe Organe { get; set; }

        public static RawOrgane FromJson(string json) => JsonConvert.DeserializeObject<RawOrgane>(json, Converter.Settings);

        public static IEnumerable<Organe> FromDirectory(string path)
        {
            return Directory.GetFiles(path).Select(f => FromJson(File.ReadAllText(f)).Organe);
        }
    }

    public partial class Organe
    {
        [JsonProperty("@xmlns:xsi", NullValueHandling = NullValueHandling.Ignore)]
        public Uri XmlnsXsi { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("codeType", NullValueHandling = NullValueHandling.Ignore)]
        public TypeOrgane CodeType { get; set; }

        [JsonProperty("libelle")]
        public string Libelle { get; set; }

        [JsonProperty("libelleEdition", NullValueHandling = NullValueHandling.Ignore)]
        public string LibelleEdition { get; set; }

        [JsonProperty("libelleAbrege", NullValueHandling = NullValueHandling.Ignore)]
        public string LibelleAbrege { get; set; }

        [JsonProperty("libelleAbrev", NullValueHandling = NullValueHandling.Ignore)]
        public string LibelleAbrev { get; set; }

        [JsonProperty("viMoDe", NullValueHandling = NullValueHandling.Ignore)]
        public ViMoDe ViMoDe { get; set; }

        [JsonProperty("organeParent")]
        public string OrganeParent { get; set; }

        [JsonProperty("chambre")]
        public string Chambre { get; set; }

        [JsonProperty("regime", NullValueHandling = NullValueHandling.Ignore)]
        public string Regime { get; set; }

        [JsonProperty("legislature", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Legislature { get; set; }

        [JsonProperty("secretariat", NullValueHandling = NullValueHandling.Ignore)]
        public Secretariat Secretariat { get; set; }
    }

    public partial class Secretariat
    {
        [JsonProperty("secretaire01")]
        public string Secretaire01 { get; set; }

        [JsonProperty("secretaire02")]
        public string Secretaire02 { get; set; }
    }

    public partial class ViMoDe
    {
        [JsonProperty("dateDebut", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateDebut { get; set; }

        [JsonProperty("dateAgrement")]
        public object DateAgrement { get; set; }

        [JsonProperty("dateFin", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateFin { get; set; }
    }
}
