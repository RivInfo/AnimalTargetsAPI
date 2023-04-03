using AnimalTargetUsers.DBWork;
using AnimalTargetUsers.UserData;
using DBDatas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalTargetUsers.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController : Controller
{
    private readonly ILogger<AccountsController> _logger;
    private readonly UsersDbContext _usersDbContext;

    public AccountsController(ILogger<AccountsController> logger, UsersDbContext usersDbContext)
    {
        _logger = logger;
        _usersDbContext = usersDbContext;
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<UserResponse>> Delete(long id)
    {
        /*if (!HttpContext.User.Identity.IsAuthenticated)
    return Unauthorized();*/
        
        //Удаление не своего акка
        
        //аккаунт связан с животным
        if (id <= 0)
        {
            return BadRequest();
        }

        User? user = await _usersDbContext.Users.FindAsync(id);

        if (user != null)
        {
            _usersDbContext.Users.Remove(user);
            await _usersDbContext.SaveChangesAsync();
            return Ok();
        }
        else
        {
            return StatusCode(403);
        }
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<UserResponse>> Update(long id, UserRequest userRequest)
    {
        /*if (!HttpContext.User.Identity.IsAuthenticated)
            return Unauthorized();*/
        
        //обновление не своего акка

        if (ModelState.IsValid)
        {
            User? user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Email == userRequest.Email);
            if (user != null)
            {
                return StatusCode(409);
            }
            
            user = await _usersDbContext.Users.FindAsync(id);

            if (user != null)
            {
                user.Email = userRequest.Email;
                user.FirstName = userRequest.FirstName;
                user.LastName = userRequest.LastName;
                user.Password = new PasswordHasher<User>().HashPassword(user, userRequest.Password);
                
                await _usersDbContext.SaveChangesAsync();
                return Ok(new UserResponse()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }
            else
            {
                return StatusCode(403);
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserResponse>> GetById(long id)
    {
        /*if (!HttpContext.User.Identity.IsAuthenticated)
            return Unauthorized();*/
        
        if (id <= 0)
            return BadRequest();

        User? user = await _usersDbContext.Users.FindAsync(id);
        
        if (user!=null)
        {
            return Ok(new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<UserResponse>> Search(string? firstName, string? lastName,
        string? email,int from = 0,int size = 10)
    {
        /*if (!HttpContext.User.Identity.IsAuthenticated)
            return Unauthorized();*/

        if (from < 0 || size <= 0)
            return BadRequest();
        
        IQueryable<User> users = _usersDbContext.Users;
        if (email != null)
        {
            email = email.ToLower();
            users = users.Where(user => user.Email.ToLower().Contains(email));
        }
        if (firstName != null)
        {
            firstName = firstName.ToLower();
            users = users.Where(user => user.FirstName.ToLower().Contains(firstName));
        }
        if (lastName != null)
        {
            lastName = lastName.ToLower();
            users = users.Where(user => user.LastName.ToLower().Contains(lastName));
        }

        IQueryable<UserResponse> usersOut = users.Select(user => new UserResponse()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }).OrderBy(user => user.Id).Skip(from).Take(size);
        
        return Ok(usersOut);
    }
}