using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Water_Drinking_Log.Models;

namespace Water_Drinking_Log.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();

        }

        [BindProperty]
        public DrinkingWaterLog DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                   @$"INSERT INTO drinking_water(date, quantity)
                      VALUES('{DrinkingWater.Date}', {DrinkingWater.Quantity})";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }


    }
}
