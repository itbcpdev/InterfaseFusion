using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace InterfaceFusion
{
    public class TANKS_INFORepository : ITANKS_INFORepository
    {
        public bool CheckTankTANK_INFO(int tank)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            bool output = false;

            var parameters = new { tanknr = tank };

            var sql = "select top 1 TankNr from TANKS_INFO WHERE TankNr = @tanknr";
            var lastTransaction = db.QuerySingleOrDefault(sql, parameters);

            if (lastTransaction == null)
            {
                output = false;
            }
            else
            {
                output = true;
            }

            return output;

        }

        public long Insert(TANKS_INFO tanks_info)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Insert(tanks_info);
        }

        public bool Update(TANKS_INFO tanks_info)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Update(tanks_info);
        }

        public long InsertHist(TANKS_INFO_HIST tanks_info_hist)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Insert(tanks_info_hist);
        }
    }
}
