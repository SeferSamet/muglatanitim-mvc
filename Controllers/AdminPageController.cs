using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using muglatanitim.Models;

namespace muglatanitim.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly IConfiguration _configuration;

        public AdminPageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("login_user") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }

            List<IletisimModel> iletisimList = new List<IletisimModel>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_iletisim";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    iletisimList.Add(new IletisimModel
                    {
                        Id = (int)reader["id"],
                        AdSoyad = reader["ad_soyad"].ToString(),
                        Telefon = reader["telefon"].ToString(),
                        Email = reader["email"].ToString(),
                        Mesaj = reader["mesaj"].ToString()
                    });
                }
            }

            return View(iletisimList);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}
