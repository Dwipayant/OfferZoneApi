using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfferZoneAPI.Models;
using OfferZoneAPI.Services.AdminDbServices;

namespace OfferZoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly AdminDbService _adminContext;
        public AdminController(AdminDbService context)
        {
            _adminContext = context;
        }

        [HttpPost]
        [ActionName("CreateAdmin")]
        public string CreateAdmin(AdminRegistration adminData)
        {
            return _adminContext.CreateAdmin(adminData);
        }

        [HttpGet]
        [Route("GetAdmins")]
        public List<AdminRegistration> GetAdmins()
        {

            return _adminContext.GetAdmins();
        }
    }
}