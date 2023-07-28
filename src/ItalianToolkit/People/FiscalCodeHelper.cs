using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ItalianToolkit.People
{
    public static class FiscalCodeHelper
    {
        private static readonly Dictionary<int, char> _homocodyMap = new Dictionary<int, char>()
        {
            {0, 'L'},
            {1, 'M'},
            {2, 'N'},
            {3, 'P'},
            {4, 'Q'},
            {5, 'R'},
            {6, 'S'},
            {7, 'T'},
            {8, 'U'},
            {9, 'V'}
        };

        private static readonly Dictionary<int, char> _monthsMap = new Dictionary<int, char>()
        {
            {1, 'A'},
            {2, 'B'},
            {3, 'C'},
            {4, 'D'},
            {5, 'E'},
            {6, 'H'},
            {7, 'L'},
            {8, 'M'},
            {9, 'P'},
            {10, 'R'},
            {11, 'S'},
            {12, 'T'}
        };

        private static readonly Dictionary<char, int> _controlEvenCharacters = new Dictionary<char, int>()
        {
            {'A', 0 },
            {'B', 1 },
            {'C', 2 },
            {'D', 3 },
            {'E', 4 },
            {'F', 5 },
            {'G', 6 },
            {'H', 7 },
            {'I', 8 },
            {'J', 9 },
            {'K', 10 },
            {'L', 11 },
            {'M', 12 },
            {'N', 13 },
            {'O', 14 },
            {'P', 15 },
            {'Q', 16 },
            {'R', 17 },
            {'S', 18 },
            {'T', 19 },
            {'U', 20 },
            {'V', 21 },
            {'W', 22 },
            {'X', 23 },
            {'Y', 24 },
            {'Z', 25 },
            {'0', 0 },
            {'1', 1 },
            {'2', 2 },
            {'3', 3 },
            {'4', 4 },
            {'5', 5 },
            {'6', 6 },
            {'7', 7 },
            {'8', 8 },
            {'9', 9 }
        };

        private static readonly Dictionary<char, int> _controlOddCharacters = new Dictionary<char, int>()
        {
            {'A', 1 },
            {'B', 0 },
            {'C', 5 },
            {'D', 7 },
            {'E', 9 },
            {'F', 13 },
            {'G', 15 },
            {'H', 17 },
            {'I', 19 },
            {'J', 21 },
            {'K', 2 },
            {'L', 4 },
            {'M', 18 },
            {'N', 20 },
            {'O', 11 },
            {'P', 3 },
            {'Q', 6 },
            {'R', 8 },
            {'S', 12 },
            {'T', 14 },
            {'U', 16 },
            {'V', 10 },
            {'W', 22 },
            {'X', 25 },
            {'Y', 24 },
            {'Z', 23 },
            {'0', 1 },
            {'1', 0 },
            {'2', 5 },
            {'3', 7 },
            {'4', 9 },
            {'5', 13 },
            {'6', 15 },
            {'7', 17 },
            {'8', 19 },
            {'9', 21 }
        };

        private static readonly Dictionary<int, char> _controlDigit = new Dictionary<int, char>()
        {
            { 0, 'A' },
            { 1, 'B' },
            { 2, 'C' },
            { 3, 'D' },
            { 4, 'E' },
            { 5, 'F' },
            { 6, 'G' },
            { 7, 'H' },
            { 8, 'I' },
            { 9, 'J' },
            { 10, 'K' },
            { 11, 'L' },
            { 12, 'M' },
            { 13, 'N' },
            { 14, 'O' },
            { 15, 'P' },
            { 16, 'Q' },
            { 17, 'R' },
            { 18, 'S' },
            { 19, 'T' },
            { 20, 'U' },
            { 21, 'V' },
            { 22, 'W' },
            { 23, 'X' },
            { 24, 'Y' },
            { 25, 'Z' }
        };

        /// <summary>
        /// Check if the given fiscal code is formally valid
        /// </summary>
        /// <param name="fiscalCode">the fiscal code to check</param>
        /// <returns><code>true</code> if formally valid, <code>false</code> otherwise</returns>
        /// <exception cref="ArgumentException">if the given fiscal code is <code>null</code> or empty</exception>
        public static bool IsFormallyValid(string fiscalCode)
        {
            if (string.IsNullOrWhiteSpace(fiscalCode))
            {
                throw new ArgumentException(nameof(fiscalCode));
            }

            fiscalCode = fiscalCode.Trim();

            if (fiscalCode.Length != 16 && fiscalCode.Length != 11)
            {
                return false;
            }

            string pattern = "^([A-Z]{6}[0-9LMNPQRSTUV]{2}[ABCDEHLMPRST]{1}[0-9LMNPQRSTUV]{2}[A-Z]{1}[0-9LMNPQRSTUV]{3}[A-Z]{1})$|([0-9]{11})$";
            var rgx = new Regex(pattern);

            return rgx.IsMatch(fiscalCode.ToUpper());
        }

        /// <summary>
        /// Calculate a fiscal code with given data
        /// </summary>
        /// <param name="lastName">the last name</param>
        /// <param name="firstName">the first name</param>
        /// <param name="birthDate">the date of birth</param>
        /// <param name="gender">the gender (e.g. "M", "F")</param>
        /// <param name="cityCode">the Belfiore city code (e.g. "A271")</param>
        /// <returns>a string representing the fiscal code</returns>
        /// <exception cref="ArgumentException">if given parameters are empty or <code>null</code></exception>
        public static string CalculateFiscalCode(string lastName, string firstName, DateTime birthDate, string gender, string cityCode)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException(nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException(nameof(firstName));
            }

            var acceptedGenders = new string[] { "M", "F" };
            if (string.IsNullOrWhiteSpace(gender) || !acceptedGenders.Contains(gender.ToUpper(), StringComparer.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException(nameof(gender));
            }

            var codePattern = "^[A-Za-z][0-9][0-9][0-9]$";
            var rgx = new Regex(codePattern);
            if (string.IsNullOrWhiteSpace(cityCode) || !rgx.IsMatch(cityCode))
            {
                throw new ArgumentException(nameof(cityCode));
            }

            string fiscalCode;

            try
            {
                firstName = firstName.ToUpper();
                lastName = lastName.ToUpper();
                gender = gender.ToUpper();
                cityCode = cityCode.ToUpper();

                fiscalCode = $"{ParseLastName(lastName)}{ParseFirstName(firstName)}{ParseBirthDate(birthDate, gender)}{cityCode}";
                fiscalCode += CalculateControlDigit(fiscalCode);
            }
            catch (Exception)
            {
                fiscalCode = string.Empty;
            }

            return fiscalCode;
        }

        /// <summary>
        /// Checks if the given fiscal code is an homocody for the base one
        /// </summary>
        /// <param name="baseFiscalCode">the base fiscal code</param>
        /// <param name="homocodyFiscalCode">the fiscal code to check</param>
        /// <returns><code>true</code> if given fiscal code is an homocody for the base one, <code>false</code> otherwise</returns>
        public static bool IsHomocody(string baseFiscalCode, string homocodyFiscalCode)
        {
            var variations = GenerateHomocodies(baseFiscalCode.ToUpper());

            return variations.Contains(homocodyFiscalCode.ToUpper());
        }

        /// <summary>
        /// Generate all valid homocodies from a base fiscal code
        /// </summary>
        /// <param name="baseFiscalCode">the base fiscal code</param>
        /// <returns>a list with all the valid homocodies</returns>
        public static List<string> GenerateHomocodies(string baseFiscalCode)
        {
            if (string.IsNullOrWhiteSpace(baseFiscalCode))
            {
                throw new ArgumentException(nameof(baseFiscalCode));
            }

            var variations = new List<string>();
            var recalculatedVariations = new List<string>();

            // not a standard fiscal code
            if (baseFiscalCode.Length != 16)
            {
                return variations;
            }

            baseFiscalCode = baseFiscalCode.ToUpper();

            if (!IsFormallyValid(baseFiscalCode))
            {
                return variations;
            }

            // sequential - multiple
            for (int i = 1; i <= 7; i++)
            {
                var variation = string.Empty;

                var mappedChars = 0;

                for (int j = baseFiscalCode.Length - 1; j >= 0; j--)
                {
                    char computingChar = baseFiscalCode[j];

                    if (!char.IsDigit(computingChar) || mappedChars == i)
                    {
                        variation = variation.Insert(0, computingChar.ToString());
                    }
                    else
                    {
                        int lookup = Convert.ToInt32(computingChar.ToString());
                        var mappedChar = _homocodyMap[lookup];

                        variation = variation.Insert(0, mappedChar.ToString());

                        mappedChars++;
                    }
                }

                variations.Add(variation);
            }

            // single characters variation
            foreach (int pos in new[] { 14, 13, 12, 10, 9, 7, 6 })
            {
                int lookup = Convert.ToInt32(baseFiscalCode[pos].ToString());

                var variation = baseFiscalCode.Remove(pos, 1);
                variation = variation.Insert(pos, _homocodyMap[lookup].ToString());

                variations.Add(variation);
            }

            // recalculate control digits
            foreach (var v in variations)
            {
                char controlDigit = CalculateControlDigit(v);

                var recalculatedVariation = v.Substring(0, 15).Insert(15, controlDigit.ToString());

                recalculatedVariations.Add(recalculatedVariation);
            }

            return recalculatedVariations;
        }

        private static char CalculateControlDigit(string fiscalCode)
        {
            if (fiscalCode.Length >= 16)
            {
                fiscalCode = fiscalCode.Substring(0, 15);
            }

            int totalSum = 0;
            for (int i = 0; i < fiscalCode.Length; i++)
            {
                totalSum += (i + 1) % 2 == 0
                    ? _controlEvenCharacters[fiscalCode[i]]
                    : _controlOddCharacters[fiscalCode[i]];
            }

            int control = totalSum % 26;

            return _controlDigit[control];
        }

        private static string ParseLastName(string lastName)
        {
            StringBuilder result = new StringBuilder();
            int Cont = 0;

            try
            {
                // take consonants
                for (int i = 0; i <= lastName.Length - 1; i++)
                {
                    if ((IsVowel(lastName[i]) == false) & (lastName[i] != ' ') && (lastName[i] != '\'') && (lastName[i] != '-') && (lastName[i] != '_'))
                    {
                        result.Append(lastName[i]);
                        Cont += 1;
                        if (Cont == 3)
                            return result.ToString();
                    }
                }

                // take vowels
                if (Cont < 3)
                {
                    for (int i = 0; i <= lastName.Length - 1; i++)
                    {
                        if ((IsVowel(lastName[i]) == true) && (lastName[i] != ' ') && (lastName[i] != '\'') && (lastName[i] != '-') && (lastName[i] != '_'))
                        {
                            result.Append(lastName[i]);
                            Cont += 1;
                            if (Cont == 3)
                                return result.ToString();
                        }
                    }
                }

                // fill out missing chars
                for (int i = 1; i <= 3 - Cont; i++)
                    result.Append("X");
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return result.ToString();
        }

        private static string ParseFirstName(string firstName)
        {
            StringBuilder result = new StringBuilder();
            int Cont = 0;

            try
            {
                // take consonants 1 3 4
                for (int i = 0; i <= firstName.Length - 1; i++)
                {
                    if ((IsVowel(firstName[i]) == false) && (firstName[i] != ' ') && (firstName[i] != '\'') && (firstName[i] != '-') && (firstName[i] != '_'))
                    {
                        if (Cont != 1)
                            result.Append(firstName[i]);
                        Cont += 1;
                        if (Cont == 4)
                            return result.ToString();
                    }
                }

                if (result.Length < 3)
                {
                    result.Remove(0, result.Length);
                    Cont = 0;
                    // take all consonants
                    for (int i = 0; i <= firstName.Length - 1; i++)
                    {
                        if ((IsVowel(firstName[i]) == false) & (firstName[i] != ' ') & (firstName[i] != '\'') & (firstName[i] != '-') & (firstName[i] != '_'))
                        {
                            result.Append(firstName[i]);
                            Cont += 1;
                            if (Cont == 3)
                                return result.ToString();
                        }
                    }
                }

                // take vowels
                if (Cont < 3)
                {
                    for (int i = 0; i <= firstName.Length - 1; i++)
                    {
                        if ((IsVowel(firstName[i]) == true) & (firstName[i] != ' ') & (firstName[i] != '\'') & (firstName[i] != '-') & (firstName[i] != '_'))
                        {
                            result.Append(firstName[i]);
                            Cont += 1;
                            if (Cont == 3)
                                return result.ToString();
                        }
                    }
                }

                // fill out missing chars
                for (int i = 1; i <= 3 - Cont; i++)
                    result.Append("X");
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return result.ToString();
        }

        private static string ParseBirthDate(DateTime birthDate, string gender)
        {
            StringBuilder result = new StringBuilder();

            string year;
            string day;

            year = birthDate.ToString("yy");

            result.Append(year);
            result.Append(DecodeMonth(birthDate.Month));

            if (gender == "F")
            {
                day = (birthDate.Day + 40).ToString();
            }
            else
            {
                day = birthDate.Day.ToString("D2");
            }

            result.Append(day);
            return result.ToString();
        }

        private static string DecodeMonth(int month)
        {
            return _monthsMap[month].ToString();
        }

        private static bool IsVowel(char letter)
        {
            char[] vowels = new char[] { 'A', 'E', 'I', 'O', 'U' };

            return vowels.Contains(char.ToUpper(letter));
        }
    }
}
