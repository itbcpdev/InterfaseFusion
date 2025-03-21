using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace InterfaceFusion
{
    public class LocalRepository : ILocalRepository
    {
        public LocalDto GetLocal()
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.QuerySingle<LocalDto>("SELECT EmpresaId = 1, LocalId = CONVERT(INT,cdlocal), DrLocal = drlocal, DsLocal = dslocal FROM Local WITH (NOLOCK)", commandType: CommandType.Text);

        }
    }
}
