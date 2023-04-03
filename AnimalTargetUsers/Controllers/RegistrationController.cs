using AnimalTargetUsers.DBWork;
using AnimalTargetUsers.UserData;
using DBDatas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AnimalTargetUsers.Controllers;

[ApiController]
[Route("registration")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly UsersDbContext _usersDbContext;

    public RegistrationController(ILogger<RegistrationController> logger, UsersDbContext usersDbContext)
    {
        _logger = logger;
        _usersDbContext = usersDbContext;
    }

    /*[HttpGet("user{id:long} {password}")]
    public async Task<ActionResult<User>> GetById(long id, string password)
    {
        User user = _usersDbContext.Users.FirstOrDefault(user => user.Id == id);
        if (user != null)
        {
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password,
                    password) == PasswordVerificationResult.Success)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        return NotFound();
    }*/
    
    [HttpGet]
    public async Task<ActionResult<User>> GetAll()
    {
        return Ok(_usersDbContext.Users.ToList());
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> Registrate(UserRequest userRequest)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            if (await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Email == userRequest.Email) != null)
            {
                return Conflict();
            }

            EntityEntry<User> user = await _usersDbContext.Users.AddAsync(new User()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email
            });
            // в целом тут это делать бессмысленно. Выше HashPassword(new User(), userRequest.Password) будет ок
            user.Entity.Password = new PasswordHasher<User>().HashPassword(user.Entity, userRequest.Password); 
            
            await _usersDbContext.SaveChangesAsync();
            
            return Created(user.Entity.Id.ToString(), new UserResponse()
            {
                Id = user.Entity.Id,
                Email = user.Entity.Email,
                FirstName = user.Entity.FirstName,
                LastName = user.Entity.LastName
            });
        }
        else
        {
            return BadRequest();
        }
    }


}