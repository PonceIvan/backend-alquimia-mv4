using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IProfileService
    {
        Task <List<Product>> BringMyProducts();
        Task<List<Formula>> BringMyFormulas();
        Task<UserProfileDto?> BringMyData();
        Task<List<Product>> BringMyWishlist();
        Task<UserProfileDto?> UpdateMyData(UserProfileDto updatedData);
    }
}
