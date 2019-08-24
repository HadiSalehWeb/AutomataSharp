using System;
using AutomataSharp;

namespace Demo.Turnstile
{
    class Program
    {
        enum TsState
        {
            Open,
            Closed
        }
        enum TsAction
        {
            AddCoin = 0,
            Push = 1
        }

        static void Main()
        {
            var turnstile = new EnumMachine<TsState, TsAction>(
                TsState.Closed,
                new TransitionRuleset<TsState, TsAction>
                {
                    (TsAction.AddCoin, TsState.Open),
                    (TsAction.Push, TsState.Closed),
                }
            );

            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Current turnstile state: {turnstile.CurrentState}");
                    Console.WriteLine("Available actions:");
                    Console.WriteLine("    0: Add coin.");
                    Console.WriteLine("    1: Push.");
                    turnstile.Feed((TsAction)int.Parse(Console.ReadLine()));
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        //Examples:
        // Acceptor for a regular language that describes how a certain word is built in english
        // One where the number of actions is infinite and thus a dictionary/ruleset cannot be used
        // "A classifier is a generalization of a finite state machine that, similar to an acceptor, produces a single output on termination but has more than two terminal states." <- do this shit
        // Take a look at transducers
        // Text based adventure game
        // computational linguistics transducer
    }
}
