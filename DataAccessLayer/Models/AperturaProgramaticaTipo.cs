﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AperturaProgramaticaTipo:Generica
    {
        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }

        [Index(IsUnique = true)]
        public int Orden { get; set; }
        public bool EsObra { get; set; }

        [Index(IsUnique = true)]
        [StringLength(10, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Identificador { get; set; }
    }
}
