using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostgresCRUD.Models;
using System.Collections.Generic;

namespace PostgresCRUD.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddPatientRecord(Patients patient);
        void UpdatePatientRecord(Patients patient);
        void DeletePatientRecord(string id);
        Patients GetPatientSingleRecord(string id);
        List<Patients> GetPatientRecords();
    }
}
