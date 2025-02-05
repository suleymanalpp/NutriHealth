using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealApp.Pages
{
    public class WelcomeModel : PageModel
    {
        // This method is called when the "Start a New Plan" button is clicked (POST request)
        public IActionResult OnPost()
        {
            // Reset the session values to "0"
            HttpContext.Session.SetString("Calories", "0");
            HttpContext.Session.SetString("ProteinGrams", "0");
            HttpContext.Session.SetString("CarbGrams", "0");
            HttpContext.Session.SetString("FatGrams", "0");
            HttpContext.Session.SetString("Weight", "0");

            // Redirect to the PersonalInfo page after resetting the session
            return RedirectToPage("/PersonalInfo");
        }
    }
}
