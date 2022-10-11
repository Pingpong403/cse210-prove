using System;
using System.Collections.Generic;


namespace Unit02
{
    /// <summary>
    /// A person who directs the game. 
    ///
    /// The responsibility of a Director is to control the sequence of play.
    /// </summary>
    public class Director
    {
        Deck _deck;
        string currentCard;
        string hiLo;
        string nextCard;
        bool correct;
        bool _isPlaying = true;
        int _score = 0;
        int _totalScore = 300;

        /// <summary>
        /// Constructs a new instance of Director.
        /// </summary>
        public Director()
        {
            _deck = new Deck();
            _deck.Shuffle();
        }

        /// <summary>
        /// Starts the game by running the main game loop.
        /// </summary>
        public void StartGame()
        {
            while (_isPlaying)
            {
                GetInputs();
                DoUpdates();
                DoOutputs();
            }
        }

        /// <summary>
        /// Asks the user if they think the next card will be higher or lower.
        /// Displays the top card first, next card last.
        /// </summary>
        public void GetInputs()
        {
            if (!_isPlaying)
            {
                return;
            }

            currentCard = _deck.Draw();
            Console.WriteLine($"The card is: {currentCard}");

            Console.Write("Higher or Lower? [h/l] ");
            hiLo = Console.ReadLine();

            _deck.Discard(currentCard);
            nextCard = _deck.drawPile[0];
            while (nextCard == currentCard)
            {
                nextCard = _deck.Draw();
                _deck.ReturnCard(nextCard);
            }
            Console.WriteLine($"Next card was: {nextCard}");
        }

        /// <summary>
        /// Determines if the player was correct and updates their score.
        /// </summary>
        public void DoUpdates()
        {
            if (hiLo == "h")
            {    
                correct = (int.Parse(nextCard) > int.Parse(currentCard));
            }
            else if (hiLo == "l")
            {
                correct = (int.Parse(nextCard) < int.Parse(currentCard));
            }

            if (correct)
            {
                _score = 100;
            }
            else
            {
                _score = -75;
            }
            _totalScore += _score;
            Console.WriteLine($"Your score is: {_totalScore}");
        }

        /// <summary>
        /// Determines if the player has lost and asks if they want to play
        /// again if they have not. Also determines if they have won based on
        /// number of available cards.
        /// </summary>
        public void DoOutputs()
        {
            if (!(_totalScore > 0))
            {
                Console.WriteLine("Your score dropped to 0. Better luck next time!");
                _isPlaying = false;
            }
            else if (_deck.drawPile.Count == 4)
            {
                Console.WriteLine("Congrats! You won! There are no more cards to draw.");
                _isPlaying = false;
            }
            else
            {
                Console.Write("Play again? [y/n] ");
                _isPlaying = (Console.ReadLine() == "y");
                Console.WriteLine();
            }
        }
    }
}