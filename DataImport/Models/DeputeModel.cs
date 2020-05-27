using RICAssemblee.DataImport.RawData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RICAssemblee.DataImport.Models
{
    public class DeputeModel : BaseModel
    {
        internal DeputeModel(Acteur rawActeur)
        {
            Uid = rawActeur.Uid;
            Prenom = rawActeur.EtatCivil.Ident.Prenom;
            Nom = rawActeur.EtatCivil.Ident.Nom;
            UriHatvp = rawActeur.UriHatvp;
            Profession = rawActeur.Profession.LibelleCourant;

            ParseAddresses(rawActeur);
            ParseMandats(rawActeur);

            _modelStorage.Register(this.Uid, this);
        }

        public BaseAdresseModel[] Adresses { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public Uri UriHatvp { get; set; }

        public string Profession { get; set; }

        public HashSet<MandatModel> Mandats { get; set; }

        public GroupeParlementaireModel GroupeParlementaire { get; set; }



        private void ParseAddresses(Acteur rawActeur)
        {
            Adresses = new BaseAdresseModel[rawActeur.Adresses.Adresse.Length];
            Dictionary<string, int> addressesIndex = new Dictionary<string, int>();

            for (int i = 0; i < rawActeur.Adresses.Adresse.Length; ++i)
            {
                var add = rawActeur.Adresses.Adresse[i];
                addressesIndex.Add(add.Uid, i);
                switch (add.Type)
                {
                    case AdresseType.Circonscription:
                    case AdresseType.Officielle:
                        Adresses[i] = new AdressePostaleModel
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
                        Adresses[i] = new AdresseReseauModel
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

            for (int i = 0; i < Adresses.Length; ++i)
            {
                var add = rawActeur.Adresses.Adresse[i];
                if (add.AdresseDeRattachement != null)
                {
                    if (addressesIndex.ContainsKey(add.AdresseDeRattachement))
                    {
                        var addReseau = Adresses[i] as AdresseReseauModel;
                        if (addReseau != null)
                        {
                            var rattachement = Adresses[addressesIndex[add.AdresseDeRattachement]] as AdressePostaleModel;
                            if (rattachement != null)
                            {
                                addReseau.AdresseDeRattachement = Adresses[addressesIndex[add.AdresseDeRattachement]] as AdressePostaleModel;
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
                if (m.ActeurRef != rawActeur.Uid)
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
                    Organes = m.Organes.OrganeRef.Select(o => _modelStorage.Get<OrganeModel>(o)).ToArray()
                };

                if (mandalModel.Libelle == null && m.Organes.OrganeRef.Count() == 1)
                {
                    mandalModel.Libelle = _modelStorage.Get<OrganeModel>(m.Organes.OrganeRef.First()).Libelle;
                }

                Mandats.Add(mandalModel);

                if (m.TypeOrgane == TypeOrgane.Gp)
                {
                    if (m.Organes.OrganeRef.Count() > 1)
                    {
                        throw new NotImplementedException("groupe parlementaire avec plus d'un organe associé");
                    }

                    // attention les groupes parlementaires perdent des membres et ne sont pas recréés... 
                    var groupeParlementaire = _modelStorage.Get<GroupeParlementaireModel>(m.Organes.OrganeRef.First());

                    if (m.DateFin != default && m.DateFin.HasValue && m.DateFin.Value < DateTimeOffset.Now)
                    {
                        continue;
                    }

                    groupeParlementaire.Deputes.Add(this);
                    GroupeParlementaire = groupeParlementaire;
                }
            }
        }
    }
}
