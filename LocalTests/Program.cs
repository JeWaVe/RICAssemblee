using RICAssemblee.DataImport.RawData;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");

            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");

            var allDeputes = Directory.GetFiles("acteurs/organe").Select(f =>
            {
                string content = File.ReadAllText(f);
                try
                {
                    return RawOrgane.FromJson(content).Organe;
                }
                catch (Exception e)
                {
                    return null;
                }
            }).ToList();
        }
    }
}
