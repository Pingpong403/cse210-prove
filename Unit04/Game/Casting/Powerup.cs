using System;
using System.Collections.Generic;

namespace Unit04.Game.Casting
{
    /// <summary>
    /// <para>A gemstone with special powers</para>
    /// <para
    /// The responsibility of a powerup is to know its powers and give them when
    /// prompted.
    /// </para>
    /// </summary>
    class Powerup : Actor{
        private string _trait;
        private int _duration;
        private string _colorDirection = "down";
        private List<string> _traits = new List<string>{
            "invincibility",
            "fastMove"
        };
        private List<int> _durations = new List<int>{
            336,
            288
        };
        private List<string> _symbols = new List<string>{
            "!",
            ">"
        };
        private List<Color> _colors = new List<Color>{
            new Color(255, 255, 0),
            new Color(0, 255, 0)
        };

        public Powerup() : base(){
            Random random = new Random();
            int powerupChoiceIndex = random.Next(_traits.Count);
            _trait = _traits[powerupChoiceIndex];
            _duration = _durations[_traits.IndexOf(_trait)];
            this._text = _symbols[_traits.IndexOf(_trait)];
            this._color = _colors[_traits.IndexOf(_trait)];
            this._velocity = new Point(0, 2);
        }

        /// <summary>
        /// Get the powerup type.
        /// </summary>
        public string GetTrait(){
            return _trait;
        }

        /// <summary>
        /// Get the powerup duration.
        /// </summary>
        public int GetDuration(){
            return _duration;
        }

        /// <summary>
        /// Moves the powerup without wrapping.
        /// </summary>
        public void Move(){
            int x = (_position.GetX() + _velocity.GetX());
            int y = (_position.GetY() + _velocity.GetY());
            this._position = new Point(x, y);
        }

        /// <summary>
        /// Increases or decreses the color based on cycle position.
        /// </summary>
        public void IncrementColor(){
            // code for incrementing down or up
            if (_colorDirection == "down"){
                // code for different powerups
                if (_trait == "invincibility"){
                    // checks if color is too low
                    if (this.GetColor().GetRed() + this.GetColor().GetGreen() > 100){
                        this.SetColor(new Color(this.GetColor().GetRed() - 25, this.GetColor().GetGreen() - 25, 0));
                        // prevents value overflow
                        if (this.GetColor().GetRed() < 50){
                            this.SetColor(new Color(50, 50, 0));
                        }
                    }
                    else{
                        this.SetColor(new Color(75, 75, 0));
                        _colorDirection = "up";
                    }
                }
                else if (_trait == "fastMove"){
                    if (this.GetColor().GetGreen() > 50){
                        this.SetColor(new Color(0, this.GetColor().GetGreen() - 25, 0));
                        if (this.GetColor().GetGreen() < 50){
                            this.SetColor(new Color(0, 50, 0));
                        }
                    }
                    else{
                        this.SetColor(new Color(0, 75, 0));
                        _colorDirection = "up";
                    }
                }
            }
            else{
                if (_trait == "invincibility"){
                    if (this.GetColor().GetRed() + this.GetColor().GetGreen() < 510){
                        this.SetColor(new Color(this.GetColor().GetRed() + 25, this.GetColor().GetGreen() + 25, 0));
                        if (this.GetColor().GetRed() > 255){
                            this.SetColor(new Color(255, 255, 0));
                        }
                    }
                    else{
                        this.SetColor(new Color(230, 230, 0));
                        _colorDirection = "down";
                    }
                }
                else if (_trait == "fastMove"){
                    if (this.GetColor().GetGreen() < 255){
                        this.SetColor(new Color(0, this.GetColor().GetGreen() + 25, 0));
                        if (this.GetColor().GetGreen() > 255){
                            this.SetColor(new Color(0, 255, 0));
                        }
                    }
                    else{
                        this.SetColor(new Color(0, 230, 0));
                        _colorDirection = "down";
                    }
                }
            }
        }
    }
}