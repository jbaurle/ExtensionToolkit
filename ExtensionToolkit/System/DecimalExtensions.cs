using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class DecimalExtensions
	{
		public static decimal TruncatePrecision(this decimal valueToTruncate, int precision)
		{
			if (precision < 0)
				throw new ArgumentException("must be >= 0", "precision");

			if (precision == 0)
				return decimal.Truncate(valueToTruncate);

			double precisionPower = Math.Pow(10, (double)precision);
			decimal precisionPowerDecimal = System.Convert.ToDecimal(precisionPower);

			return decimal.Truncate(valueToTruncate * precisionPowerDecimal) / precisionPowerDecimal;
		}
	}
}
