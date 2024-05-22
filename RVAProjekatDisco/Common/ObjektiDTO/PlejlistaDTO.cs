﻿using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class PlejlistaDTO
    {
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public string Autor { get; set; }
        [DataMember]
        public List<Pesma> ListaPesama { get; set; }    // trebalo bi samo get, ali necu da komplikujem
    }
}