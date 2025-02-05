using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MealApp.Pages
{
    public class MyProgressModel : PageModel
    {
        public List<WeightEntry> WeightProgress { get; set; } = new();

        public void OnGet()
        {
            // Retrieve weight progress data from session
            var weightData = HttpContext.Session.GetString("WeightProgress");
            if (!string.IsNullOrEmpty(weightData))
            {
                WeightProgress = JsonConvert.DeserializeObject<List<WeightEntry>>(weightData);
            }
        }

        public IActionResult OnPostSubmitWeight(string date, double weight)
        {
            // Retrieve existing weight progress data
            var weightData = HttpContext.Session.GetString("WeightProgress");
            if (!string.IsNullOrEmpty(weightData))
            {
                WeightProgress = JsonConvert.DeserializeObject<List<WeightEntry>>(weightData);
            }

            // Add today's weight entry
            WeightProgress.Add(new WeightEntry { Date = date, Weight = weight });

            // Save updated weight data back to session
            HttpContext.Session.SetString("WeightProgress", JsonConvert.SerializeObject(WeightProgress));

            return RedirectToPage();
        }
    }

    public class WeightEntry
    {
        public string Date { get; set; }
        public double Weight { get; set; }
    }
}
