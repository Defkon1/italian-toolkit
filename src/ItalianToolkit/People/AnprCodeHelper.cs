using System;
using System.Collections.Generic;
using System.Linq;

namespace ItalianToolkit.People
{
    public static class AnprCodeHelper
    {
        private static readonly Dictionary<char, int> _charsMap = new Dictionary<char, int>()
        {
            {'0', 0},
            {'1', 1},
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'A', 10},
            {'B', 11},
            {'C', 12},
            {'D', 13},
            {'E', 14},
            {'F', 15},
            {'G', 16},
            {'H', 17},
            {'I', 18},
            {'J', 19},
            {'K', 20},
            {'L', 21},
            {'M', 22},
            {'N', 23},
            {'O', 24},
            {'P', 25},
            {'Q', 26},
            {'R', 27},
            {'S', 28},
            {'T', 29},
            {'U', 30},
            {'V', 31},
            {'W', 32},
            {'X', 33},
            {'Y', 34},
            {'Z', 35}
        };

        private static readonly int _OddMultiplier = 2;

        private static readonly int _EvenMultiplier = 1;

        /// <summary>
        /// Determines whether the specified value is a formally valid ANPR identifier.
        /// </summary>
        /// <param name="anprCode">The identifier to validate</param>
        /// <returns><code>true</code> if the specified value is valid, <code>false</code> otherwise</returns>
        /// <exception cref="ArgumentException">The specified value is <code>null</code> or empty.</exception>
        public static bool IsFormallyValid(string anprCode)
        {
            if (string.IsNullOrWhiteSpace(anprCode))
            {
                throw new ArgumentException(nameof(anprCode));
            }

            anprCode = anprCode.Trim().ToUpper();
            
            if (anprCode.Length != 9)
            {
                return false;
            }

            var checkDigit = CalculateCheckCharacter(anprCode);

            return checkDigit == anprCode[anprCode.Length - 1];
        }

        /// <summary>
        /// Calculates the check character of the given ANPR identifier
        /// </summary>
        /// <param name="anprCode">The identifier for which to calculate the check character</param>
        /// <returns>a <code>char</code> representing the check character</returns>
        /// <exception cref="ArgumentException">The specified value is <code>null</code>, empty, longer than 9 characters, shorter than 8 characters or containing unvalid characters</exception>
        public static char CalculateCheckCharacter(string anprCode)
        {
            if (string.IsNullOrWhiteSpace(anprCode) || anprCode.Length > 9 || anprCode.Length < 8)
            {
                throw new ArgumentException(nameof(anprCode));
            }

            foreach (char c in anprCode)
            {
                if (!_charsMap.ContainsKey(c))
                {
                    throw new ArgumentException(nameof(anprCode));
                }
            }

            var testCode = anprCode.Length == 9 ? anprCode.Substring(0, 8) : anprCode;

            int sum = 0;
            int n = _charsMap.Count;

            for (int i = testCode.Length - 1; i >= 0; i--)
            {
                int factor = i % 2 == 0 ? _EvenMultiplier : _OddMultiplier;

                int codePoint = _charsMap[testCode[i]];
                int addend = factor * codePoint;

                // sum the digits of the "addend" as expressed in base "n"
                addend = Convert.ToInt32(addend / n) + (addend % n);
                sum += addend;
            }

            // calculate the number that must be added to the "sum" to make it divisible by N
            int remainder = sum % n;
            int checkCodePoint = (n - remainder) % n;

            return _charsMap.First(c => c.Value == checkCodePoint).Key;
        }
    }
}
