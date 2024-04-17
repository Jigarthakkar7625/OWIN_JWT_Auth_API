using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace OWIN_JWT_Auth_API.Controllers
{
    // Authentixcation >> User is valid or not
    // Authorization :: User has rights or nor (roles)
    [RoutePrefix("api/Customer")]
    [Authorize(Roles = "User,Admin")]
    public class CustomerController : ApiController
    {

        [HttpGet]
        //[Route("GetData/{userid:int?}")] // Attribute routing
        [Route("GetData")] // Attribute routing (Dynamic)
        //[ActionName("GetData123")]
        //GetData/3/JIgar
        public IHttpActionResult GetData()
        {
            try
            {

                // How to get username or UserId from the token ?
                // how to get data from Claim
                var abc = new string[] { "value1", "value2" }; // Get data DB
                return Ok(abc);

                //return BadRequest();
                //return Unauthorized();
                //return Ok(abc);
                //return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
                //throw;
            }


            //return BadRequest();
            //return Ok(abc);
        }


        [AllowAnonymous] // Public api
        [HttpGet]
        [Route("Login")] // Attribute routing (Dynamic)
        public IHttpActionResult lOGIN()
        {
            try
            {
                var getToken = GetToken();

                //var abc = new string[] { "value1", "value2" }; // Get data DB
                //return Ok(abc);

                //return BadRequest();
                //return Unauthorized();
                return Ok(getToken);
                //return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
                //throw;
            }


            //return BadRequest();
            //return Ok(abc);
        }

        public object GetToken()
        {
            // check in  DB 
            // 
            string key = "E9DB7E89123F52A9F2DB04EF04C7FE88"; //Secret key which will be used later during validation    
            var issuer = "https://localhost:44321/";  //normally this will be your site URL    

            // Encrypt
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    

            // Database mathi

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));
            permClaims.Add(new Claim(ClaimTypes.Role, "User"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };

        }

        // GET: api/Customer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
