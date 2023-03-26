using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEvents.Model
{
	public static class Captcha
	{

		private static string captcha { get; set; }

		public static string GeneratedCaptcha()
		{
			captcha = null;
			Random random = new Random();
			int rndNum = random.Next(6, 10);
			string enAlph = "qwertyuioplkjhgfdsazxcvbnm";
			for(int i = 0; i < rndNum; i++)
			{
				int w = random.Next(1, 10);
				if (w == 1)
					captcha += random.Next(0, 9).ToString();
				else
					captcha += enAlph[random.Next(0, enAlph.Length)];
			}
			return captcha;
 		}

	}
}
