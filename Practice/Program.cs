using System;
using System.Linq;
using HoldemHand;

namespace Practice
{
    class Program
    {
        static void Main1(string[] args)
        {
            var board = "5d 4s 6c 3c 2d";
            var player1 = new Hand("Ac As", board);
            var player2 = new Hand("Ad Ks", board);

            Console.WriteLine($"Player1 Hand: {player1.Description}");
            Console.WriteLine($"Player2 Hand: {player2.Description}");

            if (player1 == player2)
            {
                Console.WriteLine("It's a tie!");
            }
            else if (player1 > player2)
            {
                Console.WriteLine("Player1 wins!");
            }
            else
            {
                Console.WriteLine("Player2 wins!");
            }
        }

        static void Main2(string[] args)
        {
            ulong handMask = Hand.ParseHand("Ac As 4d 5d 6c 7c 8d");
            uint handval = Hand.Evaluate(handMask, 7);

            Console.WriteLine($"Hand: {Hand.DescriptionFromHandValue(handval)}");
        }

        static void Main3(string[] args)
        {
            var partialHandmask = Hand.ParseHand("Ac As 4d 5d 6c");
            var count = Hand.Hands(partialHandmask, 0UL, 7).LongCount();

            Console.WriteLine($"Total hands to check: {count}");
        }

        static void Main(string[] args)
        {
            int     _i1, _i2;
            ulong   _card1, _card2, dead, handmask;
            long    count = 0;

            /// Parse hand and create a mask
            ulong partialHandmask = Hand.ParseHand("ac as 4d 5d 6c");

            // We should not repeat cards we already hold.
            dead = partialHandmask;

            // Loop through the all remaining two card combinations.
            for (_i1 = Hand.NumberOfCards - 1; _i1 >= 0; _i1--)
            {
                _card1 = (1UL << _i1);
                if ((dead & _card1) != 0) continue; // Ignore dead cards
                for (_i2 = _i1 - 1; _i2 >= 0; _i2--)
                {
                    _card2 = (1UL << _i2);
                    if ((dead & _card2) != 0) continue; // Ignore dead cards
                    // OR back our 5 card partial hand mask to make a seven card hand.
                    handmask = _card1 | _card2 | partialHandmask;
                    count++;
                }
            }

            Console.WriteLine("Total hands to check: {0}", count);
        }
    }
}