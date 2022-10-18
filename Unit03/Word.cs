using System;
using System.Collections.Generic;

namespace Unit03
{
    /// <summary>
    /// <para>Creates a new instance of Word</para>
    /// <para>
    /// The responsibility of a Word is to know what word it holds, hold
    /// the correct hint, and update the hint correctly.
    /// </para>
    /// </summary>
    public class Word
    {
        private List<string> _word;
        private List<string> _hint;
        private int _wordLength;

        /// <summary>
        /// Constructs a new instance of Word
        /// </summary>
        public Word()
        {
            List<string> words = new List<string>
            {
                "M A Y O",
                "B U N S",
                "C O R N",
                "G R I L L",
                "F R I E S",
                "T O N G S",
                "T O M A T O",
                "O N I O N S",
                "S E S A M E",
                "C H E E S E",
                "C O O L E R",
                "H O T D O G",
                "R E L I S H",
                "S P R I T E",
                "M U S T A R D",
                "K E T C H U P",
                "P I C K L E S",
                "L E T T U C E",
                "A V O C A D O",
                "F R I S B E E",
                "K O O L A I D",
                "C O C A C O L A",
                "B A R B E Q U E",
                "F O O T B A L L",
                "B A S E B A L L",
                "H A M B U R G E R",
                "L A W N C H A I R",
                "S U N S C R E E N",
                "W A T E R M E L O N",
                "S L I P N S L I D E",
                "T H O U S A N D I S L A N D"
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
        /// <returns>The current hint as a string.</returns>
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
        /// <param name="letter">The letter to check.</param>
        /// <returns>Whether the letter was in the word.</returns>
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
        /// <param name="letter">The letter to reveal.</param>
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
        /// <returns>Whether the word matches the hint.</returns>
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