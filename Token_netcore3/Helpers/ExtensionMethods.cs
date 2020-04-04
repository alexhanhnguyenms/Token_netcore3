using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Token_netcore3.Helpers
{
    // <summary>
    /// Lớp mơ rộng của user (phải là static)
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Lấy danh sách user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }
        /// <summary>
        /// Lấy user nhưng bo mật khẩu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}
