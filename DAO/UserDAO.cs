using Newtonsoft.Json;
using System;
using WebApplication2.Entity;

namespace WebApplication2.DAO
{
	public class UserDAO
	{
		private static string filePath = System.IO.Directory.GetCurrentDirectory() + "\\DATA\\User.txt";
        public static void createUser(User user)
		{
			string json = JsonConvert.SerializeObject(user);

			StreamWriter writer = new StreamWriter(filePath,append: true);
			writer.WriteLine(json);
			writer.Close();
        }


		public static List<User> getAllUser()
		{
			List<User> users = new List<User>();
			StreamReader reader = new StreamReader(filePath);
			string line = null;
			while ((line = reader.ReadLine()) != null)
			{
				User user = JsonConvert.DeserializeObject<User>(line);
				users.Add(user);
			}

			reader.Close();

			return users;
		}
    }
}
