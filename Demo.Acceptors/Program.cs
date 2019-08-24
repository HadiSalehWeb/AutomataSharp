using System;
using System.Collections.Generic;
using System.Linq;
using AutomataSharp;

namespace Demo.Acceptors
{
    class Program
    {
        static void Main(string[] args)
        {
            var niceAcceptor = new FiniteStateMachine<string, char>(
                "",
                new List<string>
                {
                    "",
                    "n",
                    "ni",
                    "nic",
                    "nice",
                    "rejected"
                },
                new TransitionRuleset<string, char>(
                    (("", 'n'), "n"),
                    (("n", 'i'), "ni"),
                    (("ni", 'c'), "nic"),
                    (("nic", 'e'), "nice"),
                    "rejected"//last resort, default rule if nothing else applies transition to 5 (reject state)
                ),
                new List<string> { "nice" }
            );

            var endsWithZeroAcceptor = new FiniteStateMachine<int, int>(
                0,
                new List<int>
                {
                    0,// doesn't end with zero
                    1,// ends with zero
                },
                new TransitionRuleset<int, int>(
                    TransitionRule<int, int>.StateAgnostic(0, 1),// zero takes you to the accept state
                    TransitionRule<int, int>.StateAgnostic(1, 0)// one takes you to the reject state
                ),
                new List<int> { 1 }
            );

            var evenNumberOfZerosAcceptor = new FiniteStateMachine<int, int>(
                0,
                new List<int>
                {
                    0,// is even
                    1,// is odd
                },
                new TransitionRuleset<int, int>(
                    TransitionRule<int, int>.Regular(0, 0, 1),// Adding a zero while the number of zeros is even makes the number of zeros odd
                    TransitionRule<int, int>.Regular(1, 0, 0)// Adding a zero while the number of zeros is odd makes the number of zeros even
                ),
                new List<int> { 0 }
            );

            var oddNumberOfOnesAcceptor = new FiniteStateMachine<int, int>(
                0,
                new List<int>
                {
                    0,// is even
                    1,// is odd
                },
                new TransitionRuleset<int, int>(
                    TransitionRule<int, int>.Regular(0, 1, 1),// Same as above
                    TransitionRule<int, int>.Regular(1, 1, 0)
                ),
                new List<int> { 1 }
            );

            string input;

            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Which task would you like to execute? Use a finite state machine to");
                    Console.WriteLine("    0: test if an input is the string 'nice'.");
                    Console.WriteLine("    1: test if a binary input ends with 0.");
                    Console.WriteLine("    2: test if a binary input has an even number of 0s.");
                    Console.WriteLine("    3: test if a binary input has an odd number of 1s.");

                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 0:
                            Console.Clear();
                            Console.Write("Type a string of letters: ");
                            input = Console.ReadLine();
                            Console.WriteLine("The string you typed is" + (niceAcceptor.Accepts(input) ? "" : "n't") + " the word 'nice'. (press any key to continue...)");
                            Console.ReadKey();
                            break;
                        case 1:
                            Console.Clear();
                            Console.Write("Type a string of 0s and 1s: ");
                            input = Console.ReadLine();
                            Console.WriteLine("The string you typed " + (endsWithZeroAcceptor.Accepts(input.ToCharArray().Select(x => x - '0')) ? "ends" : "doesn't end") + " with a 0. (press any key to continue...)");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            Console.Write("Type a string of 0s and 1s: ");
                            input = Console.ReadLine();
                            Console.WriteLine("The string you typed has an " + (evenNumberOfZerosAcceptor.Accepts(input.ToCharArray().Select(x => x - '0')) ? "even" : "odd") + " number of 0s. (press any key to continue...)");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.Clear();
                            Console.Write("Type a string of 0s and 1s: ");
                            input = Console.ReadLine();
                            Console.WriteLine("The string you typed has an " + (oddNumberOfOnesAcceptor.Accepts(input.ToCharArray().Select(x => x - '0')) ? "odd" : "even") + " number of 1s. (press any key to continue...)");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Unrecognized input. (press any key to continue...)");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
