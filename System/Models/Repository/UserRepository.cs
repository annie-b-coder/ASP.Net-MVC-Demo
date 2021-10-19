using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.DTO;
using System.Models.Identity;
using System.Security.Cryptography;
using System.Web;

namespace System.Models.Repository
{
    public class UserRepository
    {
        private AppContext db = null;

        public UserRepository()
        {
            db = new AppContext();
        }

        public List<DTORole> GetRoles()
        {
              List<DTORole> roles = db.Roles.Select(s => new DTORole
                {
                    Id = s.Id,
                    Name = s.Name

                }).ToList();

                return roles;

        }

        public List<DTOUser> GetUsers()
        {
                List<DTOUser> reports = db.Users.Select(s => new DTOUser
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    RedirectPath = s.RedirectPath,
                    Roles = s.Roles.Select(d => new DTORole
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToList(),

                }).ToList();

                return reports;           
        }

        public DTOUser GetUser(string name)
        {          
                User user = db.Users.FirstOrDefault(z => z.UserName == name);

                if (user != null)
                {
                    DTOUser result = new DTOUser()
                    {

                        Id = user.Id,
                        UserName = user.UserName,
                        PasswordHash = user.PasswordHash,
                        RedirectPath = user.RedirectPath,
                        Roles = user.Roles.Select(d => new DTORole
                        {
                            Id = d.Id,
                            Name = d.Name
                        }).ToList(),
                    };

                    return result;
                }
                else return null;
        }
        public DTOUser GetUserById(int id)
        {
            User user = db.Users.FirstOrDefault(z => z.Id == id);

            if (user != null)
            {
                DTOUser result = new DTOUser()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    PasswordHash = user.PasswordHash,
                    RedirectPath = user.RedirectPath,
                    Roles = user.Roles.Select(d => new DTORole
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToList(),
                };

                return result;
            }
            else return null;
        }
        public DTORole CreateRole(string role)
        {
           
                var find = db.Roles.Any(a => a.Name == role);
                if (find)
                {
                    throw new Exception("Пользователь с таким именем уже существует");
                }

                Role n = new Role
                {
                    Name = role
                };

                try
                {
                    db.Roles.Add(n);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                DTORole result = new DTORole()
                {

                    Name = n.Name

                };

                return result;
          

        }

        public DTORole GetRole(string rolename)
        {
       
                Role role = db.Roles.FirstOrDefault(z => z.Name == rolename);

                if (role != null)
                {
                    DTORole result = new DTORole()
                    {

                        Id = role.Id,
                        Name = role.Name,
                    };

                    return result;
                }
                else return null;
            
        }
        

        public DTOUser CreateUser(CreateUser cr, List<DTORole> roles)
        {
          
                var find = db.Users.Any(a => a.UserName == cr.UserName);
                if (find)
                {
                    throw new Exception("Пользователь с таким именем уже существует");
                }

                string passhash = HashPassword(cr.Password);

                List<Role> _roles = new List<Role>();
                if (roles != null & roles.Count != 0)
                {
                    var roleIds = roles.Select(s => s.Id).ToList();
                    _roles = db.Roles.Where(w => roleIds.Contains(w.Id)).ToList();
                }

                User n = new User
                {
                    UserName = cr.UserName,
                    RedirectPath = cr.RedirectPath,
                    PasswordHash = passhash,
                    Roles = _roles
                };

                db.Users.Add(n);
                db.SaveChanges();

                DTOUser result = new DTOUser()
                {

                    UserName = n.UserName,
                    RedirectPath = n.RedirectPath,
                    PasswordHash = n.PasswordHash

                };

                return result;
            
        }

        public bool PasswordSignIn(string username, string password)
        {
            User user = db.Users.FirstOrDefault(z => z.UserName == username);
            if (user == null)
            {
                return false;
            }
            else
            {
                bool isPassTrue = VerifyHashedPassword(user.PasswordHash, password);
                if (!isPassTrue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

      

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
    }
}