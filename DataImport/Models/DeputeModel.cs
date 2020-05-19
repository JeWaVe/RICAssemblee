using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class DeputeModel : BaseModel
    {
        public BaseAdresseModel[] Adresses { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public Uri UriHatvp { get; set; }

        public string Profession { get; set; }

        public MandatModel[] Mandats { get; set; }

        public static IEnumerable<DeputeModel> FromDirectory(string parentDir)
        {
            OrganeModel.FromDirectory(Path.Combine(parentDir, "organe"));
            return Directory.GetFiles(Path.Combine(parentDir, "acteur")).Select(f => new DeputeModel(RawActeur.FromJson(File.ReadAllText(f)).Acteur));
        }

        /// <summary>
        /// Organes must be parsed first
        /// </summary>
        /// <param name="rawActeur"></param>
        private DeputeModel(Acteur rawActeur)
        {
            this.Uid = rawActeur.Uid.Text;
            ParseAddresses(rawActeur);

            Prenom = rawActeur.EtatCivil.Ident.Prenom;
            Nom = rawActeur.EtatCivil.Ident.Nom;
            UriHatvp = rawActeur.UriHatvp;
            Profession = rawActeur.Profession.LibelleCourant;

            Mandats = new MandatModel[rawActeur.Mandats.Mandat.Length];
            for(int i = 0; i < rawActeur.Mandats.Mandat.Length; ++i)
            {
                var m = rawActeur.Mandats.Mandat[i];
                if(m.ActeurRef != rawActeur.Uid.Text)
                {
                    throw new InvalidDataException("le mandat ne correspond pas à l'acteur");
                }
                Mandats[i] = new MandatModel
                {
                    Debut = m.DateDebut.Value,
                    Fin = m.DateFin,
                    Libelle = m.Libelle,
                    Qualite = m.InfosQualite.CodeQualite,
                    Organes = m.Organes.OrganeRef.Select(o => OrganeModel.FromId(o)).ToArray()
                };
            }
        }

        private void ParseAddresses(Acteur rawActeur)
        {
            this.Adresses = new BaseAdresseModel[rawActeur.Adresses.Adresse.Length];
            Dictionary<string, int> addressesIndex = new Dictionary<string, int>();

            for (int i = 0; i < rawActeur.Adresses.Adresse.Length; ++i)
            {
                var add = rawActeur.Adresses.Adresse[i];
                addressesIndex.Add(add.Uid, i);
                switch (add.Type)
                {
                    case AdresseType.Circonscription:
                    case AdresseType.Officielle:
                        this.Adresses[i] = new AdressePostaleModel
                        {
                            CodePostal = add.CodePostal,
                            Complement = add.ComplementAdresse,
                            NomRue = add.NomRue,
                            NumeroRue = add.NumeroRue,
                            Type = add.Type,
                            Uid = add.Uid,
                            Ville = add.Ville
                        };
                        break;
                    case AdresseType.Facebook:
                    case AdresseType.Fax:
                    case AdresseType.Mail:
                    case AdresseType.SiteWeb:
                    case AdresseType.Twitter:
                    case AdresseType.UrlSenateur:
                    case AdresseType.Telephone:
                        this.Adresses[i] = new AdresseReseauModel
                        {
                            Type = add.Type,
                            Uid = add.Uid,
                            Valeur = add.ValElec
                        };
                        break;
                    default:
                        throw new NotImplementedException("unsupported address type");
                }
            }

            for (int i = 0; i < this.Adresses.Length; ++i)
            {
                var add = rawActeur.Adresses.Adresse[i];
                if (add.AdresseDeRattachement != null)
                {
                    if (addressesIndex.ContainsKey(add.AdresseDeRattachement))
                    {
                        var addReseau = this.Adresses[i] as AdresseReseauModel;
                        if (addReseau != null)
                        {
                            var rattachement = this.Adresses[addressesIndex[add.AdresseDeRattachement]] as AdressePostaleModel;
                            if (rattachement != null)
                            {
                                addReseau.AdresseDeRattachement = this.Adresses[addressesIndex[add.AdresseDeRattachement]] as AdressePostaleModel;
                            }
                            else
                            {
                                throw new NotImplementedException("l'adresse de rattachement n'est pas postale");
                            }
                        }
                        else
                        {
                            throw new NotImplementedException("adresse de rattachement pour une adress postale n'est pas supporté");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("adresse de rattachement inconnue");
                    }
                }
            }
        }
    }
}
