using System;
using System.Collections.Generic;
using duckling_dota.Data.Models;

namespace duckling_dota.Data.Interfaces
{
    public interface IDataAccessProvider
    {
        void AddDoterRecord(DucklingDoter patient);
        void UpdateDoterRecord(DucklingDoter patient);
        void DeleteDoterRecord(string id);
        DucklingDoter GetDoterSingleRecord(string id);
        List<DucklingDoter> GetDotersRecords();
    }
}
