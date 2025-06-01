using alquimia.Services.Services.Interfaces;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IFormulaService _formulaService;
        private readonly IProductService _productService;

        public ProfileController(IProfileService profileService, IFormulaService formulaService, IProductService productService)
        {
            _profileService = profileService;
            _formulaService = formulaService;
            _productService = productService;
        }



    }
}
