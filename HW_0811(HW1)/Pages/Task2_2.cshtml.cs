using HW_0811_HW1_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW_0811_HW1_.Pages
{
    public class Task2_2Model : PageModel
    {
		public List<Question> Questions { get; set; }
		public int Score { get; set; }

		public Task2_2Model()
		{
			Questions = new List<Question>();

			for (int i = 0; i < 10; i++)
			{

				Questions.Add(new Question($"Question{i+21}", QType.InputA, new Dictionary<string, int>
					{
						{ "Correct answer", 5 }
					}));
			}

		}

		public void OnGet(string score)
		{
			Score = int.Parse(score);
		}

		public IActionResult OnPost()
		{
			Score = int.Parse(Request.Form["scoreNow"]);
			foreach (var question in Questions)
			{
				if (question.Answers.ContainsKey(Request.Form[question.QQ])) 
					Score += question.Answers[Request.Form[question.QQ]];
			}
			return RedirectToPage("Task2_3", new { score = Score });
		}
	}
}
