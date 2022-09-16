using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace InterfaceFusion
{
    public class OP_TRANRepository : IOP_TRANRepository
    {
        public bool Delete(string NUMERO)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OP_TRAN_Dto> GetAllOP_TRAN()
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Query<OP_TRAN_Dto>("select NUMERO, SOLES, PRODUCTO, PRECIO, GALONES, CARA, HORA, DOCUMENTO, DATEPROCE, CDTIPODOC, MANGUERA, FECSISTEMA, VolumenFinal, MontoFinal from OP_TRAN", commandType: CommandType.Text);
            //return db.GetAll<OP_TRAN>();
        }

        public InterfaceData GetInterfaceData()
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.QuerySingle<InterfaceData>("[pa_Interface_Controller_Datos]", commandType: CommandType.StoredProcedure);
        }

        public int GetLastOP_TRAN()
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            var sql = "select top 1 C_INTERNO from OP_TRAN ORDER BY C_INTERNO DESC";
            var lastTransaction = db.QuerySingle(sql);

            return lastTransaction.C_INTERNO;
            
        }

        public long Insert(OP_TRAN op_tran)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Insert(op_tran);
        }

        public bool Update(OP_TRAN op_tran)
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Update(op_tran);
        }

        public bool UpdateShift()
        {
            using IDbConnection db = new SqlConnection(AppConnection.ConnectionString);
            if (db.State == ConnectionState.Closed)
                db.Open();

            db.Query("[pa_cierres_update_cero]", commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
