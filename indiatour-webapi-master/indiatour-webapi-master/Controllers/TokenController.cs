//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using indiatour_webapi_master.Models;

//namespace indiatour_webapi_master.Controllers
//{
//    public class TokenController : ApiController
//    {
//        private ModelData db = new ModelData();
//        // get : /api/token
//        [HttpGet]
//        public static Object GetToken(login model)
//        {
//            string key = "my_secret_key_12345";           //Secret key which will be used later during validation    
//            var issuer = "https://localhost:44337/api/";  //normally this will be your site URL    

//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            //Create a List of Claims, Keep claims name short    
//            var permClaims = new List<Claim>();
//            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
//            permClaims.Add(new Claim("valid", "1"));
//            permClaims.Add(new Claim("email", model.email));
//            permClaims.Add(new Claim("password", model.password));

//            //Create Security Token object by giving required parameters    
//            var token = new JwtSecurityToken(issuer, //Issure    
//                            issuer,                  //Audience    
//                            permClaims,
//            expires: DateTime.Now.AddDays(1),
//            signingCredentials: credentials);

//            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

//            return new { data = jwt_token };
//        }


//        //[Route("api/findcost", Name = "cost")]
//        //[HttpGet]
//        //public IQueryable FindCost()
//        //{

//        //    return cos;
//        //}

//    }
//}
