using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealApp.Pages
{
    public class PersonalInfoModel : PageModel
    {
        [BindProperty]
        public double Weight { get; set; }

        [BindProperty]
        public string WeightUnit { get; set; }

        [BindProperty]
        public double Height { get; set; }

        [BindProperty]
        public string HeightUnit { get; set; }

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public string Sex { get; set; }

        [BindProperty]
        public double ActivityLevel { get; set; }

        public double Bmr { get; private set; }

        public void OnPost()
        {
            // Convert weight to kilograms if necessary
            if (WeightUnit == "lbs")
            {
                Weight = Weight * 0.453592; // Convert pounds to kilograms
            }

            // Convert height to centimeters if necessary
            if (HeightUnit == "ft")
            {
                double feet = Math.Floor(Height); // Whole feet
                double inches = (Height - feet) * 12; // Remaining inches
                Height = (feet * 30.48) + (inches * 2.54); // Convert feet and inches to centimeters
            }

            // Calculate BMR based on sex
            if (Sex == "M")
            {
                // Male BMR formula
                Bmr = 10 * Weight + 6.25 * Height - 5 * Age + 5;
            }
            else if (Sex == "F")
            {
                // Female BMR formula
                Bmr = 10 * Weight + 6.25 * Height - 5 * Age - 161;
            }

            // Multiply BMR by activity level to get daily calorie needs
            ActivityLevel = ActivityLevel / 1000;
            double dailyCalories = Bmr * ActivityLevel-500;

            // Calculate macros in grams
            double proteinCalories = dailyCalories * 0.30; // 30% from protein
            double fatCalories = dailyCalories * 0.30;     // 30% from fat
            double carbCalories = dailyCalories * 0.40;    // 40% from carbs

            int proteinGrams = (int)Math.Round(proteinCalories / 4); // 1g protein = 4 calories
            int fatGrams = (int)Math.Round(fatCalories / 9);         // 1g fat = 9 calories
            int carbGrams = (int)Math.Round(carbCalories / 4);       // 1g carbohydrate = 4 calories
            int calories = (int)Math.Round(dailyCalories);
            // Store macro values in session
            HttpContext.Session.SetInt32("ProteinGrams", proteinGrams);
            HttpContext.Session.SetInt32("CarbGrams", carbGrams);
            HttpContext.Session.SetInt32("FatGrams", fatGrams);
            HttpContext.Session.SetInt32("CaloriGoal", calories);

        }
    }
}
