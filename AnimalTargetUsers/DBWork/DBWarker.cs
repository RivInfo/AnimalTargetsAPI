using AnimalTargetUsers.UserData;
using CupTest2023.Data;

namespace AnimalTargetUsers.DBWork;

public class DBWarker : IRepository<UserRequest, UserResponse>
{
    private UsersDbContext _db;
    
    public DBWarker()
    {
        _db = new UsersDbContext();
    }
    
    public UserResponse Register(UserRequest requestData)
    {

        return null; // if(TryFindUser(requestData))
        // _db.Users.
    }

    public bool TryFindUser(UserRequest requestData)
    {
        throw new NotImplementedException();
    }
}