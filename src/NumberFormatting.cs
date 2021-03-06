using System.Text.RegularExpressions;
using System;

namespace HallLibrary.Extensions
{
	/// <summary>
	/// Contains methods useful for formatting numbers in <see cref="String" />s.
	/// </summary>
	public static class NumberFormatting
	{
		private static readonly Regex _number = new Regex(@"^-?\d+(?:" + _defaultDecimalSeparatorForRegex + @"\d+)?$"); // TODO: replace dot with decimal separator from current culture
		private static readonly Regex _thousands = new Regex(@"(?<=\d)(?<!" + _defaultDecimalSeparatorForRegex + @"\d*)(?=(?:\d{3})+($|" + _defaultDecimalSeparatorForRegex + @"))"); // TODO: replace dot with decimal separator from current culture
		private const char _defaultThousandsSeparator = ',';
		private const string _defaultDecimalSeparatorForRegex = @"\.";
		
		/// <summary>
		/// Determine if the specified <see cref="String" /> <paramref name="value" /> is a number.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns>True if <paramref name="value" /> is a valid number, False if not.</returns>
		public static bool IsValidNumber (string value)
		{
			return _number.IsMatch(value);
		}
		
		/// <summary>
		/// Format the number in the <see cref="String" /> <paramref name="number" /> by adding thousand separators.
		/// </summary>
		/// <param name="number">The string to format.</param>
		/// <param name="thousandsSeparator">The separator to use.</param>
		/// <returns><paramref name="number" /> formatted with thousand separators.</returns>
		/// <exception cref="ArgumentException"><paramref name="number" /> is not a valid number.</exception>
		public static string AddThousandsSeparators (string number, string thousandsSeparator = null)
		{
			if (!IsValidNumber(number))
				throw new ArgumentException(nameof(number), "String does not contain a valid number");
			return _thousands.Replace(number, thousandsSeparator ?? _defaultThousandsSeparator.ToString()); // TODO: replace comma with thousands separator from current culture
		
			/*
			// alternative implementation, without regex
			if (thousandsSeparator == null)
				thousandsSeparator = _defaultThousandsSeparator.ToString();
			var digits = number.IndexOf(".", StringComparison.InvariantCultureIgnoreCase);
			if (digits == -1)
				digits = number.Length;

			for (var pos = digits - 3; pos > 0; pos -= 3)
			{
				number = number.Substring(0, pos) + thousandsSeparator + number.Substring(pos);
			}
			*/
		}
		/*
		// Store integer 182
		int decValue = 182;
		// Convert integer 182 as a hex in a string variable
		string hexValue = decValue.ToString("X"); // doesn't add 0x prefix
		// Convert the hex string back to the number
		int decAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber); // doesn't cope with 0x prefix
		*/
	}
}
