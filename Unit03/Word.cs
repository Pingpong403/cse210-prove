using System;
using System.Collections.Generic;

namespace Unit03
{
    /// <summary>
    /// Creates a new instance of Word
    /// 
    /// The responsobility of a Word is to know what word it holds and
    /// also hold the correct hint.
    /// </summary>
    public class Word
    {
        private List<string> _word;
        private List<string> _hint;
        private int _wordLength;

        public Word()
        {
            List<string> words = new List<string>{
                "H A M B U R G E R",
                "M U S T A R D",
                "K E T C H U P",
                "M A Y O",
                "P I C K L E S",
                "L E T T U C E",
                "T O M A T O",
                "B U N",
                "S E S A M E",
                "G R I L L",
                "A V O C A D O",
                "C H E E S E"
            };
            Random rnd = new Random();
            int randomWordIndex = rnd.Next(words.Count);
            _word = new List<string>(words[randomWordIndex].Split(" "));
            _wordLength = _word.Count;
            _hint = new List<string>();
            for (int i = 0; i < _wordLength; i++)
            {
                _hint.Add("_");
            }
        }

        /// <summary>
        /// Returns the current hint.
        /// </summary>
        public string getHint()
        {
            string combinedHint = "";
            foreach (string letter in _hint)
            {
                combinedHint += $"{letter} ";
            }
            return combinedHint;
        }

        /// <summary>
        /// Checks if the given letter in is the word.
        /// </summary>
        public bool letterInWord(string letter)
        {
            bool inWord = false;
            foreach (string character in _word)
            {
                if (letter == character)
                {
                    inWord = true;
                }
            }
            return inWord;
        }

        /// <summary>
        /// Puts all instances of the given letter in their correct spot 
        /// in the hint.
        /// </summary>
        public void revealLetter(string letter)
        {
            for (int i = 0; i < _wordLength; i++)
            {
                if (letter == _word[i])
                {
                    _hint[i] = letter;
                }
            }
        }

        /// <summary>
        /// Check if the word matches the hint
        /// </summary>
        public bool won()
        {
            bool win = false;
            int correctLetters = 0;
            for (int i = 0; i < _wordLength; i++)
            {
                if (_hint[i] == _word[i])
                {
                    correctLetters += 1;
                }
            }
            if (correctLetters == _wordLength)
            {
                win = true;
            }
            return win;
        }
    }
}