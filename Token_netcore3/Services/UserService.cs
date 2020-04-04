using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Token_netcore3.Helpers;

namespace Token_netcore3.Services
{

    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    /// <summary>
    /// Dịch vụ chứng thực máy chủ
    /// </summary>
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        /// <summary>
        /// Danh sách user trong hệ thống
        /// </summary>
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings">startup đã đăng ký giờ lấy sử dụng</param>
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //{{"alg":"HS256","typ":"JWT"}.{"unique_name":"1","nbf":1585969297,"exp":1586574076,"iat":1585969297}}
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1ODU5NjkyOTcsImV4cCI6MTU4NjU3N
            //DA3NiwiaWF0IjoxNTg1OTY5Mjk3fQ.yos0gPOaGQEOsvSZeW6VZGqcDw3UL_CVRVgFfsT2F94
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }
    }
}
