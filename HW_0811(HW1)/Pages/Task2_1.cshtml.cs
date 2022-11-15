using HW_0811_HW1_.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.HttpSys;

namespace HW_0811_HW1_.Pages
{
	public class Task2_1Model : PageModel
	{
		public List<Question> Questions { get; set; }
		public int Score { get; set; }
		public List<int> RandomsIndex { get; set; }

		public Task2_1Model()
		{
			Questions = new List<Question>();
			RandomsIndex = new List<int>();
			for (int i = 0; i < 10; i++)
			{

				Questions.Add(new Question($"Question{i + 11}", QType.TwoA, new Dictionary<string, int>
					{
						{ "Answ1", 1 },
						{ "Answ2", 1 },
						{ "Answ3", 0 },
						{ "Answ4", 1 }
					}));
			}
			FillRandom();
		}

		private void FillRandom()
		{
			int index = new Random().Next(0,Questions.Count);
			if (RandomsIndex.Contains(index)&&RandomsIndex.Count!=10) FillRandom();
			else if (RandomsIndex.Count == 10) return;
			else { RandomsIndex.Add(index); FillRandom(); }
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
				int answCount = 0;
				for (int i = 0; i < question.Answers.Count; i++)
				{
					try
					{
						answCount += int.Parse(Request.Form[$"{question.QQ}{i + 1}"]);
					}
					catch (Exception)
					{
					}
				}
				if (answCount == 3) Score += answCount;
			}
			return RedirectToPage("Task2_2", new { score = Score });
		}
	}
}
