using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using muglatanitim.Models;
using System.Data.SqlClient;

namespace muglatanitim.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminLoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM tb_adminlogin WHERE username = @username AND password = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", model.Username);
                    command.Parameters.AddWithValue("@password", model.Password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        HttpContext.Session.SetString("login_user", model.Username);
                        return RedirectToAction("Index", "AdminPage");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
                        return View();
                    }
                }
            }
            return View();
        }
    }
}
