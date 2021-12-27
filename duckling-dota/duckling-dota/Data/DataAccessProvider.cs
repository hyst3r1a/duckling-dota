using System.Collections.Generic;
using System.Linq;
using duckling_dota.Data.Interfaces;
using duckling_dota.Data.Models;

namespace duckling_dota.Data
{
    public class DataAccessProvider : Interfaces.IDataAccessProvider
    {
       
        private readonly ApplicationDbContext _context;

        public DataAccessProvider(ApplicationDbContext context)
        {
            _context = context;   
        }

        public void AddDoterRecord(DucklingDoter doter)
        {
            _context.Add(doter);
            _context.SaveChanges();
        }

        public void DeleteDoterRecord(string id)
        {
            var entity = _context.DucklingDoters.First(t => t.Id == id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public DucklingDoter GetDoterSingleRecord(string id)
        {
            return _context.DucklingDoters.First(t => t.Id == id);
        }

        public List<DucklingDoter> GetDotersRecords()
        {
            return _context.DucklingDoters.ToList();
        }

        public void UpdateDoterRecord(DucklingDoter doter)
        {
            _context.DucklingDoters.Update(doter);
            _context.SaveChanges();
        }
    }
}
