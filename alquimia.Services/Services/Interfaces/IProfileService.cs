using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Data.Entities;

namespace alquimia.Services.Services.Interfaces
{
    public interface IProfileService
    {
        Task <List<Product>> BringMyProducts();
        Task<List<Formula>> BringMyFormulas();
        Task<List<string>> BringMyData();
    }
}
