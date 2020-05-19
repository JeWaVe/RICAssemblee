﻿using RICAssemblee.DataImport.Models;
using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LocalTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //new WebClient().DownloadFile("http://data.assemblee-nationale.fr/static/openData/repository/15/amo/deputes_actifs_mandats_actifs_organes_divises/AMO40_deputes_actifs_mandats_actifs_organes_divises_XV.json.zip", "acteurs.zip");

            //ZipFile.ExtractToDirectory("acteurs.zip", "acteurs");
            var deputes = DeputeModel.FromDirectory("acteurs").OrderByDescending(d => d.Mandats.Length).ToArray();
            
            //var tmp = allOrganes.Where(d => d.Parent != null).ToList();

        }
    }
}
