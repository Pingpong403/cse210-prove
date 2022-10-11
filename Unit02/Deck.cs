using System;
using System.Collections.Generic;

namespace Unit02
{
    /// <summary>
    /// A deck of 52 cards.
    /// 
    /// The responsibility of Deck is to keep track of the current draw pile
    /// and discard pile and manipulate them as needed.
    /// </summary>
    public class Deck
    {
        public List<string> drawPile;
        public List<string> discardPile;

        /// <summary>
        /// Constructs a new instance of Die.
        /// </summary>
        public Deck()
        {
            drawPile = new List<string>{
                "1", "1", "1", "1", 
                "2", "2", "2", "2", 
                "3", "3", "3", "3", 
                "4", "4", "4", "4", 
                "5", "5", "5", "5", 
                "6", "6", "6", "6", 
                "7", "7", "7", "7", 
                "8", "8", "8", "8", 
                "9", "9", "9", "9", 
                "10", "10", "10", "10", 
                "11", "11", "11", "11", 
                "12", "12", "12", "12", 
                "13", "13", "13", "13"
            };
            discardPile = new List<string>();
        }

        /// <summary>
        /// Removes and returns the top card of the current drawPile.
        /// </summary>
        public string Draw()
        {
            string card = drawPile[0];
            drawPile.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Adds the given card to the discard pile.
        /// </summary>
        public void Discard(string card)
        {
            discardPile.Add(card);
        }

        /// <summary>
        /// Puts the given card at a random spot in the drawPile.
        /// </summary>
        public void ReturnCard(string card)
        {
            Random rnd = new Random();
            int randomIndex = rnd.Next(1, drawPile.Count);
            drawPile.Insert(randomIndex, card);
        }
        
        /// <summary>
        /// Shuffles the current drawPile.
        /// </summary>
        public void Shuffle()
        {
            List<string> newDeck = new List<string>();
            int drawPileLength = drawPile.Count;
            for (int i = 0; i < drawPileLength; i++)
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(drawPile.Count);
                newDeck.Add(drawPile[randomIndex]);
                drawPile.RemoveAt(randomIndex);
            }
            drawPile = newDeck;
        }
    }
}