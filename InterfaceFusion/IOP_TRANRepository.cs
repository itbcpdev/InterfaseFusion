using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceFusion
{
    public interface IOP_TRANRepository
    {
        IEnumerable<OP_TRAN_Dto> GetAllOP_TRAN();

        int GetLastOP_TRAN();

        long Insert(OP_TRAN op_tran);

        bool Update(OP_TRAN op_tran);

        bool Delete(string NUMERO);

        InterfaceData GetInterfaceData();

        bool UpdateShift();

    }
}
