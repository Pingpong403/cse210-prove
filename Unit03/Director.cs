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
        /// Display jumper and hint, ask for letter.
        /// </summary>
        private void GetInputs()
        {
            _terminalService.WriteText(_word.getHint());
            Console.WriteLine();
            _terminalService.WriteText(_jumper.getJumper());
            _terminalService.WriteText("^^^^^^^");

            _guess = _terminalService.ReadText("Guess a letter [a-z]: ");
            _guess = _guess.ToUpper();
            // Console.Clear();
        }

        /// <summary>
        /// Check if letter was right, update hint and jumper accordingly.
        /// If jumper dies, update accordingly.
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
        /// If jumper dies, display dead jumper and end game.
        /// If player wins, congratulate them and end game.
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