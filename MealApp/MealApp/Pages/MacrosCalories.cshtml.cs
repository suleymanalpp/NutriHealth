using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealApp.Pages
{
    public class MacrosCaloriesModel : PageModel
    {
        public int? ProteinGrams { get; private set; }
        public int? CarbGrams { get; private set; }
        public int? FatGrams { get; private set; }
        public int? CaloriGoal { get; private set; }
        public string ErrorMessage { get; private set; }

        public void OnGet()
        {
            // Retrieve values from the session
            ProteinGrams = HttpContext.Session.GetInt32("ProteinGrams");
            CarbGrams = HttpContext.Session.GetInt32("CarbGrams");
            FatGrams = HttpContext.Session.GetInt32("FatGrams");
            CaloriGoal = HttpContext.Session.GetInt32("CaloriGoal");

           
            // Check if any of the values are missing
            if (ProteinGrams == null || CarbGrams == null || FatGrams == null)
            {
                ErrorMessage = "Macronutrient values are not available. Please calculate them on the Personal Info page.";
            }
        }
    }
}
