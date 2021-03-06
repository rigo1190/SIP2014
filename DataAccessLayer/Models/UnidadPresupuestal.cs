﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UnidadPresupuestal:Generica
    {
        public UnidadPresupuestal()
        {
            this.DetalleSubUnidadesPresupuestales = new HashSet<UnidadPresupuestal>();
            this.DetalleTechoFinanciero = new HashSet<TechoFinancieroUnidadPresupuestal>();
            this.DetalleTechoFinancieroBitacoraMovimientos = new HashSet<TechoFinancieroBitacoraMovimientos>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [Index(IsUnique = true)]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Abreviatura { get; set; }
        public string Nombre { get; set; }

        [Index("IX_Orden_ParentId", 1, IsUnique = true)]
        public int Orden { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Titular { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Cargo { get; set; }

        [Index("IX_Orden_ParentId", 2)]
        public int? ParentId { get; set; }

        public int? SectorId { get; set; }

        public virtual UnidadPresupuestal Parent { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual ICollection<UnidadPresupuestal> DetalleSubUnidadesPresupuestales { get; set; }

        public virtual ICollection<TechoFinancieroUnidadPresupuestal> DetalleTechoFinanciero { get; set; }

        public virtual ICollection<TechoFinancieroBitacoraMovimientos> DetalleTechoFinancieroBitacoraMovimientos { get; set; }
     
    }
}
