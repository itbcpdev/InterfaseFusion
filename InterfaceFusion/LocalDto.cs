using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    public class LocalDto
    {
        public int EmpresaId { get; set; }
        public int LocalId { get; set; }
        public string DsLocal { get; set; }
        public string DrLocal { get; set; }
    }
}
