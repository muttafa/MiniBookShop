using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using omerd.Server.Models;

namespace omerd.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public UserController(ApplicationDBContext context)
        {
            _dbContext = context;
        }


        [HttpPost]
        [Route("login")]
        public ActionResult LogIn([FromBody] UserLoginViewModel user)
        {
            try
            {
                if (user != null)
                {
                    var currentUser = _dbContext.Users.Where(x => x.Username == user.UserName && x.Password == user.Password).FirstOrDefault();

                    if (currentUser != null)
                    {
                        return Ok(new { userID= currentUser.UserID,success = true });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Kullanıcı Bulunamadı" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Kullanıcı bilgileri alınamadı" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("createUser")]
        public ActionResult Register([FromBody] UserRegisterViewModel user)
        {

            var userList = _dbContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            try
            {
                if (user != null && userList == null)
                {
                    var newUser = new User
                    {
                        Address = user.Adress,
                        Email = user.Email,
                        Password = user.Password,
                        Username = user.UserName,
                    };
                    if (newUser != null)
                    {
                        _dbContext.Users.Add(newUser);
                        _dbContext.SaveChanges();
                        return Ok(new { success = true , userID = newUser.UserID});
                    }
                    else
                    {
                        return BadRequest(new {success = false, message = "Kullanıcı kayıt edilemedi"});
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Bu email zaten kayıtlı veya kullanıcı eklenemedi" });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });

                throw;
            }
        }

    }
}