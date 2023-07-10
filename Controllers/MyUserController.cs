using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CrudAPI.Models;
using CrudAPI.DTOs;
using CrudAPI.Repositories;
using AutoMapper;

namespace CrudAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class MyUsersController : ControllerBase
    {
        private readonly IMyUserRepository _userRepository;
        private readonly IMapper Mapper;

        public MyUsersController(IMyUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var userDTOs = Mapper.Map<List<MyUserDTO>>(users);
            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = Mapper.Map<MyUserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost]
        public IActionResult CreateUser(MyUserDTO userDTO)
        {
            var user = Mapper.Map<MyUser>(userDTO);
            _userRepository.AddUser(user);
            var createdUserDTO = Mapper.Map<MyUserDTO>(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, createdUserDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, MyUserDTO userDTO)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            Mapper.Map(userDTO, existingUser);
            _userRepository.UpdateUser(existingUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return NoContent();
        }

        // Add other actions as needed for additional functionality

    }
}
