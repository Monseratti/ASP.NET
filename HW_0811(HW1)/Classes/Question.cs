namespace HW_0811_HW1_.Classes
{
	public enum QType
	{
		OneA = 0,
		TwoA = 1,
		InputA = 2
	}
	public class Question
	{
		public string QQ { get; set; }
		public QType QType { get; set; }
		public Dictionary<string,int> Answers { get; set; }

		public Question(string qq, QType qType, Dictionary<string, int> ansvers) { 
			QQ = qq; 
			QType = qType;
			Answers = ansvers;
		}
	}
}
