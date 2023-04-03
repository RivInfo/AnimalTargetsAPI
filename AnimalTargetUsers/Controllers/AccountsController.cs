using AnimalTargetUsers.DBWork;
using AnimalTargetUsers.UserData;
using DBDatas;
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

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserResponse>> GetById(long id)
    {
        /*if (!HttpContext.User.Identity.IsAuthenticated)
            return Unauthorized();*/
        
        if (id <= 0)
            return BadRequest();

        //User? user = await _usersDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        
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

        //IQueryable<UserResponse> usersOut = users.Cast<UserResponse>().Take(new Range(from, from + size));
        var usersOut = users/*.Take(new Range(from, from + size))*/
            .Select(user => new UserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            })/*.ToListAsync()*/;
        //usersOut = usersOut.Take(new Range(from, from + size)).ToList();
        return Ok(usersOut.ToList().Take(new Range(from, from + size)));
    }
}