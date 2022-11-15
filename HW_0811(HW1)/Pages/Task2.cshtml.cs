using HW_0811_HW1_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HW_0811_HW1_.Pages
{
	public class Task2Model : PageModel
	{
		public List<Question> Questions { get; set; }

		public Task2Model()
		{
			Questions = new List<Question>();

			for (int i = 0; i < 10; i++)
			{

				Questions.Add(new Question($"Question{i+1}", QType.OneA, new Dictionary<string, int>
					{
						{ "Answ1", 1 },
						{ "Answ2", 0 },
						{ "Answ3", 0 },
						{ "Answ4", 0 }
					}));

			}

		}

		public void OnGet()
		{
		}
		public IActionResult OnPost()
		{
			int count = 0;
			foreach(var question in Questions)
			{
				count += int.Parse(Request.Form[question.QQ]);
			}

			return RedirectToPage("Task2_1", new{score = count });
		}

	}
}
