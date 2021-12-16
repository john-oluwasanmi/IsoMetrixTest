using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace IsoMetrixTest
{
    public class StringCalculator : IStringCalculator
    {
        string _extraCharacterStart = string.Empty;
        string _newDelimeter = string.Empty;
        string _extraCharacterEnd = string.Empty;
        bool _isMultiDelimeter = false;
        string _groupValue = string.Empty;

        public int Add(string values)
        {
            List<string> delimiters = new List<string> { ",", "\n" };

            if (values.StartsWith("//"))
            {
                var newDelimeters = GetCustomDelimters(values);
                delimiters = new List<string> { };
                delimiters.AddRange(newDelimeters);
                delimiters.Add(@"\n");

                if (!_isMultiDelimeter)
                {
                    values = values.Replace($"//{_extraCharacterStart}{_newDelimeter}{_extraCharacterEnd}\\n", "");
                }
                else
                {
                    values = values.Replace($"//{_groupValue}\\n", "");
                }
            }

            var data = values.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var numbers = Validate(data);
            var result = numbers.Sum();

            return result;
        }

        private List<int> Validate(string[] data)
        {
            List<int> positiveNumbers = new List<int>();
            List<int> negativeNumbers = new List<int>();

            foreach (var stringNumber in data)
            {
                if (string.IsNullOrWhiteSpace(stringNumber))
                {
                    positiveNumbers.Add(0);
                }
                else
                {
                    bool isNumber = int.TryParse(stringNumber, out int numericValue);

                    if (isNumber && numericValue < 0)
                    {
                        negativeNumbers.Add(numericValue);
                    }
                    else if (isNumber && numericValue <= 1000)
                    {
                        positiveNumbers.Add(numericValue);
                    }
                }
            }

            if (negativeNumbers.Any())
            {
                var negativeValues = string.Join(",", negativeNumbers);
                throw new InvalidOperationException($"negatives not allowed {negativeValues}");
            }

            return positiveNumbers;
        }

        private List<string> GetCustomDelimters(string values)
        {
            // multiple custom delimeters
            Regex rxMultipleDelimeter = new Regex(@"^//([[]{1}[^A-Za-z0-9]+[]]{1})+\\n[0-9]+[^A-Za-z0-9]+[0-9]+[^A-Za-z0-9]?");
            MatchCollection rxMultipleDelimeterMatches = rxMultipleDelimeter.Matches(values);

            if (rxMultipleDelimeterMatches.Any())
            {
                _groupValue = rxMultipleDelimeterMatches[0].Groups[1].Value;
                var delimeters = _groupValue.Split("[", StringSplitOptions.RemoveEmptyEntries)
                                           .Select(r => r.Substring(0, 1))
                                           .ToList();

                _isMultiDelimeter = true;

                return delimeters;
            }

            // single  character
            Regex rx = new Regex(@"^//[^A-Za-z0-9]+\\n[0-9]+[^A-Za-z0-9][0-9]+");
            MatchCollection singleCharacterDelimeterMatches = rx.Matches(values);

            if (singleCharacterDelimeterMatches.Any())
            {
                return VerifyDelimeters(values);
            }

            // multiple characters
            Regex rxMultipleChar = new Regex(@"^//[[]{1}[^A-Za-z0-9]+[]]{1}\\n[0-9]+[^A-Za-z0-9]+[0-9]+[^A-Za-z0-9]?");
            MatchCollection multipleCharacterDelimeterMatches = rxMultipleChar.Matches(values);

            if (multipleCharacterDelimeterMatches.Any())
            {
                _extraCharacterEnd = "]";
                _extraCharacterStart = "[";
                return VerifyDelimeters(values, _extraCharacterStart, _extraCharacterEnd);
            }

            throw new Exception("Error in Matching");
        }

        private List<string> VerifyDelimeters(string values, string extraCharacterStart = "", string ExtraCharacterEnd = "")
        {
            var delimeters = DeriveDelimeters(values, extraCharacterStart, ExtraCharacterEnd);
            CheckInvalidDelimeter(delimeters);
            return delimeters;
        }

        private List<string> DeriveDelimeters(string values, string extraCharacterStart, string ExtraCharacterEnd)
        {
            List<string> multiDelimeters = new List<string>();
            var end = values.IndexOf($"{ExtraCharacterEnd}\\n");
            var start = values.IndexOf($"//{extraCharacterStart}");

            if (end < 0 || start < 0)
            {
                throw new InvalidOperationException("Patter not matched");
            }

            var length = 0;
            var diff = 0;

            if (!string.IsNullOrWhiteSpace(extraCharacterStart) && !string.IsNullOrWhiteSpace(ExtraCharacterEnd))
            {
                diff = 3;
                length = end - start - diff;
            }
            else if (string.IsNullOrWhiteSpace(extraCharacterStart) && string.IsNullOrWhiteSpace(ExtraCharacterEnd))
            {
                diff = 2;
                length = end - start - diff;
            }

            _newDelimeter = values.Substring(start + diff, length);
            multiDelimeters.Add(_newDelimeter);

            return multiDelimeters;
        }

        private void CheckInvalidDelimeter(List<string> delimeters)
        {
            if (delimeters.Any(r => r.Contains("[")) || delimeters.Any(r => r.Contains("]")))
            {
                throw new Exception("Invalid Delimeter");
            }
        }
    }

    public interface IStringCalculator
    {
        int Add(string numbers);
    }
}
