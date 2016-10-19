using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliTest
{
    public class MyCalculator
    {
        public int Run(int x, int y)
        {
            if (x > 0)
            {
                return x * y;
            }
            else
            {
                try
                {
                    return x / y;
                }
                catch (NullReferenceException)
                {
                    return 0;
                }
            }
        }
    }
}
