﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Fondo:Generica
    {
        public Fondo() 
        {
            this.DetalleSubFondos = new HashSet<Fondo>();
            this.DetalleLineamientos = new HashSet<FondoLineamientos>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }
        public int TipoFondoId { get; set; }

        [Index("IX_Orden_ParentId", 1, IsUnique = true)]
        public int Orden { get; set; }

        [Index("IX_Orden_ParentId",2)]
        public int? ParentId { get; set; }
        public virtual TipoFondo TipoFondo { get; set; }
        public virtual Fondo Parent { get; set; }
        public virtual ICollection<Fondo> DetalleSubFondos { get; set; }

        public virtual ICollection<FondoLineamientos> DetalleLineamientos { get; set; }

    }
}
