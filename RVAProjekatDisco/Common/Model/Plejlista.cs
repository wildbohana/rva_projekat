using Common.Enumeracije;
using Common.ObjektiDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common.Model
{
	[DataContract]
    public class Plejlista
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
		public int IdPlejliste { get; set; }
		[DataMember]
        public string Naziv { get; set; }
		[DataMember]
		public string Autor { get; set; }
		[DataMember]
		public List<Pesma> ListaPesama {  get; set; }

		// Konstruktor(i)
		public Plejlista() { }

		public Plejlista(PlejlistaDTO pl)
		{
			Naziv = pl.Naziv;
			Autor = pl.Autor;
			ListaPesama = new List<Pesma>();
		}

		// Dodavanje pesme
		public Pesma DodajPesmu(Pesma pesma)
		{
			// Napravi fabriku
			Factory fabrika = new PesmaFactory();

			Pesma novaPesma;
			switch (pesma.Format)
			{
				case (FormatZapisa.MP3):
					novaPesma = fabrika.NapraviPesmu(FormatZapisa.MP3);
					break;
                case (FormatZapisa.WAV):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.WAV);
                    break;
                case (FormatZapisa.OGG):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.OGG);
                    break;
                case (FormatZapisa.FLAC):
                    novaPesma = fabrika.NapraviPesmu(FormatZapisa.FLAC);
                    break;
                default:
					novaPesma = fabrika.NapraviPesmu(FormatZapisa.MP3);	// promeni ?
					break;
			}

			// Popuni polja
			novaPesma.Naziv = pesma.Naziv;
			novaPesma.Autor = pesma.Autor;
			novaPesma.DuzinaMinute = pesma.DuzinaMinute;
			novaPesma.DuzinaSekunde = pesma.DuzinaSekunde;

			// Dodaj novu pesmu u plejlistu
			ListaPesama.Add(novaPesma);
			return novaPesma;
		}

		// Kloniranje plejliste sa svim njenim pesmama
		public Plejlista KlonirajPlejlistu()
		{
			Plejlista kopija = new Plejlista()
			{
				IdPlejliste = this.IdPlejliste,
				Autor = this.Autor,
				Naziv = this.Naziv + "_kopija"			
			};

			kopija.ListaPesama = new List<Pesma>(this.ListaPesama.Count);
			foreach (var item in this.ListaPesama)
			{
				kopija.ListaPesama.Add(item.KlonirajPesmu());
			}

			return kopija;
		}
	}
}
