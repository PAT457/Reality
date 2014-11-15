using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

//As Of 3 Hours Of Working On A System that needed this function, It failed. I'm leaving this here for futrue use, or Maybe, I dont know.

namespace Reality.Content.Utils
{
    class getDecimal
    {
        public static float parse(float f)
        {
            String fl = f.ToString("R");
            String[] numbers = fl.Split(new char[] { '.' });
            if (numbers.Length == 1)
            {
                numbers = new String[2] {"0", "0"};
            }
            String newstr = numbers[1].Substring(0, 1);
            numbers[1] = newstr;
            Console.WriteLine("{0}", numbers[1]);
            return float.Parse("0." + numbers[1], CultureInfo.InvariantCulture.NumberFormat);
        }

        public static float parseWhole(float f)
        {
            String fl = f.ToString();
            String[] numbers = fl.Split(new char[] { '.' });
            return float.Parse(numbers[0], CultureInfo.InvariantCulture.NumberFormat);
        }

        public static float convertInt(int intf)
        {
            String fl = intf.ToString();
            fl = "0."+fl;
            //Console.WriteLine("{0}", fl);
            return float.Parse(fl, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}
