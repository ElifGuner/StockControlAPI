using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Models;
using StockControlAPI.Models.Entities;
using StockControlAPI.Models.ViewModels;
using System.Security.Cryptography.Xml;

namespace StockControlAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public StockApiDBContext _stockApiDBContext;
        public UserController(StockApiDBContext stockApiDBContext)
        {
            _stockApiDBContext = stockApiDBContext;
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] ViewUser viewUser)
        {
            User user = new()
            {
                UserName = viewUser.UserName,
                Password = viewUser.Password
            };

            await _stockApiDBContext.Users.AddAsync(user);
            await _stockApiDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
