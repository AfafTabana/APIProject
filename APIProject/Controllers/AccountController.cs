using APIProject.DTOs.Acount;
using APIProject.Models;
using APIProject.UnitofWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using APIProject.UnitofWork; 
using System.Text;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermManager;
        private readonly SignInManager<ApplicationUser> signInMAnager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UnitOfWork unitofWork;

        public AccountController(UserManager<ApplicationUser> _usermManager ,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole> _roleManager , UnitOfWork unit)
        {
            this.unitofWork = unit; 
            this.usermManager = _usermManager;
            this.signInMAnager = _signInManager;
            this.roleManager = _roleManager;
        }
        //Add method to generate token 
        #region
        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            // 1   define claims to pass it in jwtsecurity key
            var userclaims = new List<Claim>
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Id),
                  new Claim(ClaimTypes.Name, user.UserName),
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Add roles to every user with claims 
            foreach (var role in roles)
            {
                userclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            //2  define secret key 
            var key = "Welcome to our system Exam this task by ahmed ashraf";
            var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            //3 defin signingcreadintials
            var signingcer = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);



            // pass jwt components to token obhect 
            var tokenobject = new JwtSecurityToken(
                     claims: userclaims,
                     expires: DateTime.Now.AddDays(1),
                     signingCredentials: signingcer
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenobject);

        }
        #endregion


        #region register action 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //Check if data is valid 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Check the user name and email used befor 
            if (await usermManager.FindByNameAsync(registerDTO.UserName) is not null)
                return BadRequest("user name already Exsits");

            if (await usermManager.FindByEmailAsync(registerDTO.Email) is not null)
                return BadRequest("Email Already used Before ");
            //== create user 

            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email

            };

            var result = await usermManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //give  role to new use 
            //1== if the role doesn't exist creat it 
            //this line wil be executed only in the first user
            if (!await roleManager.RoleExistsAsync("User"))
                //creat role 
                await roleManager.CreateAsync(new IdentityRole("User"));
            //adding user to role
            await usermManager.AddToRoleAsync(user, "User");

            var createdUser = await usermManager.FindByNameAsync(user.UserName);


            //else we will crat tooken
            var student = new Student
            {
                Email = createdUser.Email,
                Username = createdUser.UserName,
                ApplicationUserId = createdUser.Id
            };

            unitofWork.StudentRepo.Add(student);
            await unitofWork.SaveAsync();


            return Ok();
        }

        #endregion

        ///
        #region  login Asction 
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // check validation 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //check that user is exsists 
            var user = await usermManager.FindByNameAsync(loginDTO.UserName);
            if (user == null)
                return BadRequest("Invalid username or password ");
            //check password

            var validpassword = await usermManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!validpassword)
                return BadRequest("Invalid username or password ");


            //return roles to add it in claims and passes it with user in token

            var roles = await usermManager.GetRolesAsync(user);
            //generate token 
            var token = GenerateJwtToken(user, roles);
            //return obj from token as jsin 

            return Ok(new { token });

        }

        #endregion



    }
}
