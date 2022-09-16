using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    [Table("OP_TRAN")]
    public class OP_TRAN
    {
        [ExplicitKey]
        public int C_INTERNO { get; set; }
        public string? CONTROLADOR { get; set; }        
        public string? NUMERO { get; set; }
        public decimal SOLES { get; set; }
        public string? PRODUCTO { get; set; }
        public decimal PRECIO { get; set; }
        public decimal GALONES { get; set; }      
        public string? CARA { get; set; }
        public DateTime? FECHA { get; set; }
        public DateTime? HORA { get; set; }
        public string? TURNO { get; set; }
        public string? ESTADO { get; set; }
        public string? DOCUMENTO { get; set; }
        public DateTime? DATEPROCE { get; set; }
        public string? CDTIPODOC { get; set; }
        public int MANGUERA { get; set; }
        public DateTime? FECSISTEMA { get; set; }
        public decimal VolumenFinal { get; set; }
        public decimal MontoFinal { get; set; }
        public int? IdTran { get; set; }
    }
}
