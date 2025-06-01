using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;

namespace alquimia.Services.Services
{
    public class ProfileService : IProfileService
    {
        public Task<List<string>> BringMyData()
        {
            throw new NotImplementedException();
        }

        public Task<List<Formula>> BringMyFormulas()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> BringMyProducts()
        {
            throw new NotImplementedException();
        }
    }
}
