using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    [Table("Local")]
    public class Local
    {
        [ExplicitKey]
        public string cdlocal { get; set; }
        public string? dslocal { get; set; }
        public string? drlocal { get; set; }
        public string? tlflocal { get; set; }
        public string? tlflocal1 { get; set; }
        [ExplicitKey]
        public string cdzona { get; set; }
        public string? NRO_CENTRALIZACION { get; set; }
        public string? dislocal { get; set; }
        public string? provlocal { get; set; }
        public string? deplocal { get; set; }
        public string? cdsunat { get; set; }
        public string? cdDepartamento { get; set; }
        public string? cdProvincia { get; set; }
        public string? cdDistrito { get; set; }
    }
}
