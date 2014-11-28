using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Firmas:Generica
    {
        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string SecretarioSEFIPLAN { get; set; }


        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string SubsecretarioDeEgresos { get; set; }


    }
}
