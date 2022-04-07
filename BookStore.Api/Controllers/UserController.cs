using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Request;
using BookStore.Api.Response;
using BookStore.Sample.Function.Response;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStore.Api.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet(template: "GetUser/")]
        public async Task<IEnumerable<User?>> Get(string username)
        {
            var user = await _userRepository.Get(username);
            return user;
        }

        [HttpGet(template: "GetCard/")]
        public async Task<IEnumerable<CreditCard?>> GetCard(int user_id)
        {
            var card = await _userRepository.GetCard(user_id);
            return card;
        }

        [HttpPost(template: "Create/")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            await _userRepository.Create(request.username, request.password, request.email, request.name, request.home_address);
            return Ok();
        }

        [HttpPost(template: "UpdateUser/")]
        public async Task<IActionResult> Update([FromBody] UpdateUserInfo request)
        {
            await _userRepository.Update(request.username, request.password, request.email, request.name, request.home_address);
            return Ok();
        }

        [HttpPost(template: "CreateCard/")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest request)
        {
            await _userRepository.CreateCard(request.user_id, request.name, request.number, request.expire_date, request.pin);
            return Ok();
        }
    }
}