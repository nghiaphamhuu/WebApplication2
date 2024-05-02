using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebApplication2.DAO;
using WebApplication2.Entity;

namespace WebApplication2.Service
{
    public class UserService
    {
        public static List<User> getAllUser()
        {
            return UserDAO.getAllUser();
        }

        public static string createUser(User user)
        {
            List<User> users = getAllUser();   
            
            foreach(User item in users)
            {
                if (item.email.Equals(user.email))
                {
                    return "The Email was registered ";
                }
            }

            UserDAO.createUser(user);

            return "Create User Successfully!";
        }

        public static bool logIn(User user)
        {
            List<User> users = getAllUser();

            foreach(User item in users)
            {
                if(item.email.Equals(user.email))
                {
                    if (item.password.Equals(user.password)) 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
