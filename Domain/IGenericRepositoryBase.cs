using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IGenericRepositoryBase
    {
        public Task<List<Entrie>> GetNodoHTTPS(bool boleano, string apiUrl);
        public Task<List<string>> GetCategory(string apiUrl);
    }
}
