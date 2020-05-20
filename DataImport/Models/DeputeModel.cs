using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
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

        public HashSet<MandatModel> Mandats { get; set; }

        public GroupeParlementaireModel GroupeParlementaire { get; set; }

        /// <summary>
        /// Organes must be parsed first
        /// </summary>
        /// <param name="rawActeur"></param>
        internal DeputeModel(Acteur rawActeur)
        {
            Uid = rawActeur.Uid.Text;
            Prenom = rawActeur.EtatCivil.Ident.Prenom;
            Nom = rawActeur.EtatCivil.Ident.Nom;
            UriHatvp = rawActeur.UriHatvp;
            Profession = rawActeur.Profession.LibelleCourant;

            ParseAddresses(rawActeur);
            ParseMandats(rawActeur);
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

        private void ParseMandats(Acteur rawActeur)
        {
            Mandats = new HashSet<MandatModel>();
            for (int i = 0; i < rawActeur.Mandats.Mandat.Length; ++i)
            {
                var m = rawActeur.Mandats.Mandat[i];
                if (m.ActeurRef != rawActeur.Uid.Text)
                {
                    throw new InvalidDataException("le mandat ne correspond pas à l'acteur");
                }
                var mandalModel = new MandatModel
                {
                    Debut = m.DateDebut.Value,
                    Fin = m.DateFin,
                    Uid = m.Uid,
                    Libelle = m.Libelle,
                    Qualite = m.InfosQualite.CodeQualite,
                    Organes = m.Organes.OrganeRef.Select(o => OrganeModel.FromId(o)).ToArray()
                };

                if (mandalModel.Libelle == null && m.Organes.OrganeRef.Count() == 1)
                {
                    mandalModel.Libelle = OrganeModel.FromId(m.Organes.OrganeRef.First()).Libelle;
                }

                Mandats.Add(mandalModel);

                if(m.TypeOrgane == TypeOrgane.Gp)
                {
                    if(m.DateFin != default && m.DateFin.HasValue && m.DateFin.Value < DateTimeOffset.Now)
                    {
                        continue;
                    }

                    if(m.Organes.OrganeRef.Count() > 1)
                    {
                        throw new NotImplementedException("groupe parlementaire avec plus d'un organe associé");
                    }

                    string gpId = m.Organes.OrganeRef.First();
                    var organe = OrganeModel.FromId(gpId);

                    GroupeParlementaire = GroupeParlementaireModel.AddDepute(gpId, organe.Libelle, this);
                }
            }
        }
    }
}
