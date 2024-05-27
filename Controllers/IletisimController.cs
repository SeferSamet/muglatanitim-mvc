using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using muglatanitim.Models;
using System.Data.SqlClient;

public class IletisimController : Controller
{
    private readonly IConfiguration _configuration;

    public IletisimController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
public IActionResult Post(IletisimModel model)
{
    if (ModelState.IsValid) // modeldeki degerlerin dogruluguna yani uygun çalışıp çalışmadıgına'q
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "INSERT INTO tb_iletisim (ad_soyad, telefon, email, mesaj) VALUES (@ad_soyad, @telefon, @email, @mesaj)";
            
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ad_soyad", model.AdSoyad);
                command.Parameters.AddWithValue("@telefon", model.Telefon);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@mesaj", model.Mesaj);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    TempData["success"] = true;
                    return RedirectToAction("Index");
                }
            }
        }
    }

    TempData["failed"] = true;
    return RedirectToAction("Index");
}

}
