using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SignalR.Hubs;
using SignalR;
using onetugdemo.Controllers;
using System;

namespace onetugdemo.Models
{
    public static class Demo
    {
        public static void Log(string format, params object[] values)
        {
            Log(string.Format(format, values));
        }

        public static void Log(string message)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<Controllers.Console>();
            context.Clients.addMessage(message);
        }

        public static void LogNumber(SmsRequest request)
        {
            if (request == null) return;

            string from = request.From;
            LogNumber(from);
        }

        public static void LogNumber(string from)
        {
            if (string.IsNullOrWhiteSpace(from)) return;

            MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["MONGOLAB_URI"]);
            var db = server.GetDatabase(Constants.MongolabDB);
            using (server.RequestStart(db))
            {
                User user = new Models.User { Number = from };

                var users = db.GetCollection<User>("users");

                var query = Query.EQ("Number", from);
                foreach (var existingUser in users.Find(query))
                {
                    //Demo.Log("log: found existing #{0}", existingUser.Number);
                    return;
                }

                //Demo.Log("log: storing new #{0}", user.Number);
                users.Insert(user);

            }
        }

        public static IEnumerable<User> GetAll()
        {
            MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["MONGOLAB_URI"]);
            var db = server.GetDatabase(Constants.MongolabDB);
            using (server.RequestStart(db))
            {
                var users = db.GetCollection<User>("users");

                foreach (var existingUser in users.FindAll())
                {
                    yield return existingUser;
                }
            }
        }

        private static Random rand = new Random();

        public static User GetRandom()
        {
            var users = GetAll();
            return users.ElementAt(rand.Next(users.Count()));
        }
    }
}