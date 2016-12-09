using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted
{
  public class SimpleCalculator
  {
    public int Add(int a, int b)
    {
      return a + b;
    }

		public int Multiply(int a, int b)
		{
			// Bug for demo purposes
			return a + b;
		}
  }
}
