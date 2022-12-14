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
        private int _score = 6;
        private Point _lastInput = new Point(0, 0);
        private bool _gameOver = false;
        private bool _winConditionMet = false;
        private bool _loadRobot;
        private bool _firstMove = false;
        private bool _invincibleFrames = true;
        private bool _invincible = false;
        private bool _isPowerupAlive = false;
        private bool _isPowerupActive = false;
        private int _activePowerupTimer = 0;
        private int _powerupCooldown = 480;
        private string _activePowerupTrait = "none";
        private int _columns;
        private int _cellSize;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService, bool loadRobot, int columns, int cellSize)
        {
            this._keyboardService = keyboardService;
            this._videoService = videoService;
            this._loadRobot = loadRobot;
            this._columns = columns;
            this._cellSize = cellSize;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast. Only
        /// quits when the window is closed or the player wins or loses.
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
            if (_loadRobot && (_winConditionMet || _gameOver)){
                while (_videoService.IsWindowOpen()){
                    DoOutputs(cast);
                }
            }
            _videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            if (_loadRobot){
                Actor robot = cast.GetFirstActor("robot");
                Point velocity = _keyboardService.GetDirection();
                if (!_firstMove && !velocity.Equals(new Point(0, 0))){
                    _firstMove = true;
                }
                // prevents too much movement at high fps
                if ((velocity.Equals(_lastInput) && !velocity.Equals(new Point(0, 0))) && _activePowerupTrait != "fastMove"){
                    robot.SetVelocity(new Point(0, 0));
                }
                else{
                    robot.SetVelocity(velocity);
                }
                _lastInput = velocity;
            }
        }

        /// <summary>
        /// <para>
        /// Updates the robot's position and resolves any collisions with minerals.
        /// </para>
        /// <para>
        /// Checks for win or loss, updating score color to look more alarming
        /// if player is about to lose.
        /// </para>
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            // turn off invincibility when needed
            if (_firstMove && _invincibleFrames){
                _invincibleFrames = false;
            }
            if (_activePowerupTrait != "invincibility"){
                _invincible = false;
            }

            List<Actor> minerals = cast.GetActors("minerals");
            int maxX = _videoService.GetWidth();
            int maxY = _videoService.GetHeight();
            if (_loadRobot){
                int gemsInPlay = 0;
                Actor banner = cast.GetFirstActor("banner");
                Actor robot = cast.GetFirstActor("robot");

                robot.MoveNext(maxX, maxY);

                foreach (Actor actor in minerals)
                {
                    Mineral mineral = (Mineral) actor;
                    if (mineral.special){
                        mineral.SetColor(mineral.GetTopColor());
                        mineral.CycleColor();
                    }
                    if (mineral.GetValue() > 0){
                        gemsInPlay += 1;
                    }
                    actor.MoveNext(maxX, maxY);
                    // collision detection, which doesn't happen if player is
                    // invincible
                    if (robot.GetPosition().EqualsRange(actor.GetPosition(), 7) && !_invincibleFrames)
                    {
                        if (!_invincible){
                            _score += mineral.GetValue();
                            cast.RemoveActor("minerals", mineral);
                        }
                        else{
                            if (mineral.GetValue() > 0){
                                _score += mineral.GetValue();
                                cast.RemoveActor("minerals", mineral);
                            }
                        }
                    }
                }

                // blink player if they are invincible
                if (_invincibleFrames || _invincible){
                    if (robot.GetColor().IsSameColor(new Color(255, 255, 255))){
                        robot.SetColor(_invincibleFrames? new Color(0, 0, 0) : new Color(150, 150, 0));
                    }
                    else{
                        robot.SetColor(new Color(255, 255, 255));
                    }
                }
                if (_firstMove && robot.GetColor().IsSameColor(new Color(0, 0, 0)) && !_invincible){
                    robot.SetColor(new Color(255, 255, 255));
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

                // POWERUPS SECTION

                // do cooldown timer
                if (!_isPowerupActive && !_isPowerupAlive){
                    _powerupCooldown -= 1;
                }

                // move current powerup & change color if alive
                if (_isPowerupAlive){
                    Actor actor = cast.GetFirstActor("powerup");
                    Powerup powerup = (Powerup) actor;
                    if (powerup.GetPosition().GetY() > maxY){
                        cast.RemoveActor("powerup", powerup);
                        _powerupCooldown = 240;
                        _isPowerupAlive = false;
                    }
                    else{
                        powerup.Move();
                        powerup.IncrementColor();
                    }
                }

                // else spawn powerup if cooldown is done
                else if (_powerupCooldown == 0){
                    Powerup powerup = new Powerup();
                    Random random = new Random();
                    int randX = random.Next(1, _columns);
                    Point spawnPosition = new Point(randX, 0);
                    spawnPosition = spawnPosition.Scale(_cellSize);
                    powerup.SetPosition(spawnPosition);
                    cast.AddActor("powerup", powerup);
                    _isPowerupAlive = true;
                }

                // check collision
                if (_isPowerupAlive){
                    Actor actor = cast.GetFirstActor("powerup");
                    Powerup powerup = (Powerup) actor;
                    if (robot.GetPosition().EqualsRange(powerup.GetPosition(), 7)){
                        _activePowerupTrait = powerup.GetTrait();
                        _activePowerupTimer = powerup.GetDuration();
                        if (_activePowerupTrait == "invincibility"){
                            _invincible = true;
                        }
                        cast.RemoveActor("powerup", powerup);
                        _powerupCooldown = 240;
                        _isPowerupActive = true;
                        _isPowerupAlive = false;
                    }
                }
                // do powerup timer
                if (_isPowerupActive){
                    if (_activePowerupTimer > 0){
                        _activePowerupTimer -= 1;
                    }
                    else{
                        _activePowerupTrait = "none";
                        Random random = new Random();
                        _isPowerupActive = false;
                        robot.SetColor(new Color(255, 255, 255));
                    }
                }
            }
            if (!_loadRobot){
                foreach (Actor actor in minerals){
                    Mineral mineral = (Mineral) actor;
                    actor.MoveNext(maxX, maxY);
                }
            }
        }

        /// <summary>
        /// <para>
        /// Draws the actors on the screen.
        /// </para>
        /// <para>
        /// Updates the message banners if needed.
        /// </para>
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