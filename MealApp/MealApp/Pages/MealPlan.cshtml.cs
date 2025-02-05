using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MealApp.Pages
{
    public class MealPlanModel : PageModel
    {
        public Meal SelectedMeal { get; set; }
        public string SelectedMealType { get; set; }
        private readonly HttpClient _httpClient;

        public MealPlanModel()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> OnPostGetMealAsync(string mealType)
        {
            // Fetch calorie goal from session
            int? calorieGoal = HttpContext.Session.GetInt32("CaloriGoal");
            if (calorieGoal == null)
            {
                ViewData["Error"] = "Calorie goal not found in session.";
                return Page();
            }

            // Divide calorie goal by 3 and round to nearest integer
            int mealCalories = (int)Math.Round((double)calorieGoal / 3);
            

            // Define macro percentages
            
            string apiKey = "b111087a74f442fbaeee971787f13613"; // Replace with your Spoonacular API key
            string apiUrl = $"https://api.spoonacular.com/mealplanner/generate?apiKey={apiKey}&timeFrame=day&targetCalories={mealCalories}&carbsPercentage={40}&proteinPercentage={30}&fatPercentage={30}";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                var mealPlan = JsonConvert.DeserializeObject<MealPlan>(response);

                if (mealPlan?.Meals != null && mealPlan.Meals.Count > 0)
                {
                    SelectedMeal = mealPlan.Meals[0]; // Fetch the first meal
                    SelectedMealType = mealType; // Track the meal type (Breakfast, Lunch, or Dinner)
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle errors gracefully
                SelectedMeal = null;
                ViewData["Error"] = $"Error fetching meal: {ex.Message}";
            }

            return Page();
        }
    }

    public class MealPlan
    {
        [JsonProperty("meals")]
        public List<Meal> Meals { get; set; }

        [JsonProperty("nutrients")]
        public Nutrients Nutrients { get; set; }
    }

    public class Meal
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("readyInMinutes")]
        public int ReadyInMinutes { get; set; }

        [JsonProperty("servings")]
        public int Servings { get; set; }

        [JsonProperty("sourceUrl")]
        public string SourceUrl { get; set; }
    }

    public class Nutrients
    {
        [JsonProperty("calories")]
        public double Calories { get; set; }

        [JsonProperty("protein")]
        public double Protein { get; set; }

        [JsonProperty("fat")]
        public double Fat { get; set; }

        [JsonProperty("carbohydrates")]
        public double Carbohydrates { get; set; }
    }
}
