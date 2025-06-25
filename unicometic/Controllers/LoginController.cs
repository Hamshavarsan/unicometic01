using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class LoginController
    {
        private readonly DatabaseManager dbManager = new DatabaseManager();

        public bool ValidateLogin(string username, string password, out string role)
        {
            return dbManager.ValidateUser(username, password, out role);
        }
    }
}
