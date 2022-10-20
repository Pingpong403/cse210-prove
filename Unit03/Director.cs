using System;


namespace Unit03
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private Jumper _jumper = new Jumper();
        private Word _word = new Word();
        private TerminalService _terminalService = new TerminalService();
        private bool _isPlaying = true;
        private string _guess;

        /// <summary>
        /// Constructs a new instance of Director.
        /// </summary>
        public Director()
        {
        }

        /// <summary>
        /// Starts the game by running the main game loop.
        /// </summary>
        public void StartGame()
        {
            // Console.Clear();
            while (_isPlaying)
            {
                GetInputs();
                DoUpdates();
                DoOutputs();
            }
        }

        /// <summary>
        /// Displays the jumper and hint then asks for a letter.
        /// </summary>
        private void GetInputs()
        {
            _terminalService.WriteText(_word.getHint());
            Console.WriteLine();
            _terminalService.WriteText(_jumper.getJumper());
            Console.WriteLine();
            _terminalService.WriteText("^^^^^^^");

            _guess = _terminalService.ReadText("Guess a letter [a-z]: ");
            _guess = _guess.ToUpper();
            // Console.Clear();
        }

        /// <summary>
        /// Checks if the letter was right then updates hint and jumper accordingly.
        /// If the jumper dies, updates accordingly.
        /// </summary>
        private void DoUpdates()
        {
            if (_word.letterInWord(_guess))
            {
                _word.revealLetter(_guess);
            }
            else
            {
                _jumper.cutLine();
            }
            if (_jumper.jumperLength() == 3)
            {
                _jumper.makeDead();
            }
        }

        /// <summary>
        /// If the jumper dies, displays dead jumper and ends the game.
        /// If the player wins, congratulates them and ends the game.
        /// </summary>
        private void DoOutputs()
        {
            if (_jumper.isDead)
            {
                _terminalService.WriteText(_jumper.getJumper());
                _terminalService.WriteText("^^^^^^^");
                _isPlaying = false;
            }
            if (_word.won())
            {
                Console.WriteLine();
                _terminalService.WriteText(_jumper.getWinner());
                Console.WriteLine("\nWow! You got it!");
                _isPlaying = false;
            }
        }
    }
}