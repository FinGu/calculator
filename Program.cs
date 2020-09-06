using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace calculator {
    class Program {
        static double FiveHeadMove(double input, double second_input, char _operator) {
            switch (_operator) {
                case '+':
                    return input + second_input;

                case '/':
                    return input / second_input;

                case 'x':
                    return input * second_input;

                case '*':
                    return input * second_input;

                case '^':
                    return Math.Pow(input, second_input);

                case '~': // int xor'ing
                    int one = Convert.ToInt32(input);
                    int two = Convert.ToInt32(second_input);

                    return one ^ two;

                default:
                    return 0;
            }
        }
        static double idx { get; set; } = 0;
        static void Main(string[] args) {
            try {
                Console.WriteLine("write your expression : ");

                Regex operator_rgx = new Regex("([+/x*^~])");

                string raw_input = Console.ReadLine();

                string[] input = raw_input.Split(' ');

                var matches_more_than_one_time = operator_rgx.Matches(raw_input).Count > 1;

                for (int i = 0; i < input.Length; i++) {
                    var match = operator_rgx.Match(input[i]);

                    if (match.Success) {
                        var _operator = Convert.ToChar(match.Value);

                        var normal_operation = FiveHeadMove(
                            Convert.ToDouble(input[i - 1]), Convert.ToDouble(input[i + 1]), _operator);

                        var different_operation = FiveHeadMove(
                            idx, Convert.ToDouble(input[i + 1]), _operator);

                        idx = (matches_more_than_one_time && idx != 0) 
                            ? different_operation : normal_operation;
                    }
                }

                Console.WriteLine($"result: {idx}");
            }
            catch (System.IndexOutOfRangeException) {
                Console.WriteLine("the arguments need spaces to be considered valid");
            }
            catch (System.FormatException) {
                Console.WriteLine("the arguments weren't valid as ints");
            }

            Console.ReadLine();
        }
    }
}
