using System;
using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;


namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService _keyboardService = null;
        private VideoService _videoService = null;
        private int _score;
        private Point _lastInput;
        private bool _gameOver;
        private bool _winConditionMet;

        /// <summary>
        /// <para>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </para>
        /// <para>Sets score to 6.</para>
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this._keyboardService = keyboardService;
            this._videoService = videoService;
            _score = 6;
            _lastInput = new Point(0, 0);
            _gameOver = false;
            _winConditionMet = false;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            _videoService.OpenWindow();
            while (_videoService.IsWindowOpen() && !_winConditionMet && !_gameOver)
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            while (_videoService.IsWindowOpen()){
                DoOutputs(cast);
            }
            _videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = _keyboardService.GetDirection();
            // prevents too much movement at high fps
            if (velocity.Equals(_lastInput) && !velocity.Equals(new Point(0, 0))){
                robot.SetVelocity(new Point(0, 0));
            }
            else{
                robot.SetVelocity(velocity);
            }
            _lastInput = velocity;
        }

        /// <summary>
        /// <para>
        /// Updates the robot's position and resolves any collisions with minerals.
        /// </para>
        /// <para>Moves all artifacts according to their velocities.</para>
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            int gemsInPlay = 0;
            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            List<Actor> minerals = cast.GetActors("minerals");

            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();
            robot.MoveNext(maxX, maxY);

            foreach (Actor actor in minerals)
            {
                Mineral mineral = (Mineral) actor;
                if (mineral.GetValue() == 1){
                    gemsInPlay += 1;
                }
                actor.MoveNext(maxX, maxY);
                if (robot.GetPosition().EqualsRange(actor.GetPosition(), 7))
                {
                    _score += mineral.GetValue();
                    cast.RemoveActor("minerals", mineral);
                }
            }

            // warn player if score gets too low
            if (_score <= 3){
                banner.SetColor(new Color(255, 0, 0));
            }
            if (_score > 3){
                banner.SetColor(new Color(255, 255, 255));
            }
            banner.SetText($"Score: {_score}");

            // check for win or loss
            if (gemsInPlay == 0){
                _winConditionMet = true;
            }
            else if (_score <= 0){
                _gameOver = true;
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            if (_winConditionMet){
                Actor winMessage = cast.GetFirstActor("winMessage");
                winMessage.SetText("WINNER!");
            }
            if (_gameOver){
                Actor gameOverMessage = cast.GetFirstActor("gameOverMessage");
                gameOverMessage.SetText("GAME OVER");
            }
            List<Actor> actors = cast.GetAllActors();
            _videoService.ClearBuffer();
            _videoService.DrawActors(actors);
            _videoService.FlushBuffer();
        }
    }
}