using System;
using System.Collections.Generic;

namespace Unit04.Game.Casting{
        /// <summary>
        /// <para>A chunk of earth.</para>
        /// <para>
        /// The responsibility of a mineral is to know its value.
        /// </para>
        /// </summary>
    class Mineral : Actor{
        private int _value;
        public bool special;
        private List<Color> _rainbowSequence = new List<Color>{
            {new Color(255, 0, 0)},
            {new Color(255, 127, 0)},
            {new Color(255, 255, 0)},
            {new Color(127, 255, 0)},
            {new Color(0, 255, 0)},
            {new Color(0, 255, 127)},
            {new Color(0, 255, 255)},
            {new Color(0, 127, 255)},
            {new Color(0, 0, 255)},
            {new Color(127, 0, 255)},
            {new Color(255, 0, 255)},
            {new Color(255, 0, 127)}
        };

        /// <summary>
        /// Constructs a new instance of Mineral.
        /// </summary>
        public Mineral(){
            _value = 0;
        }
        /// <summary>
        /// Constructs a new instance of Mineral given its type.
        /// </summary>
        /// <param name="type">The type of mineral. Must be "rock" or "gem".</param>
        public Mineral(string type, bool special){
            this.special = special;
            if (!special){
                if (type == "rock" || type == "gem"){
                    _value = (type == "rock") ? -1 : 1;

                    Random random = new Random();
                    int yVel = random.Next(2, 6);
                    _velocity = new Point(0, yVel);
                }
                else{
                    throw new ArgumentException("mineral must be of type\"rock\" or type \"gem\"");
                }
            }
            else{
                if (type == "rock" || type == "gem"){
                    _value = (type == "rock") ? -6 : 6;

                    Random random = new Random();
                    int yVel = random.Next(6, 10);
                    _velocity = new Point(0, yVel);
                }
                else{
                    throw new ArgumentException("mineral must be of type\"rock\" or type \"gem\"");
                }
                Random random2 = new Random();
                int randomIndex = random2.Next(_rainbowSequence.Count);
                for (int i=0; i<randomIndex; i++){
                    this.CycleColor();
                }
            }
        }

        /// <summary>
        /// Get the value of the mineral.
        /// </summary>
        public int GetValue(){
            return _value;
        }

        /// <summary>
        /// Fetches the next color to display
        /// </summary>
        /// <returns>The top color</returns>
        public Color GetTopColor(){
            return _rainbowSequence[0];
        }

        /// <summary>
        /// Cycles the rainbow sequence.
        /// </summary>
        public void CycleColor(){
            Color firstElement = _rainbowSequence[0];
            _rainbowSequence.RemoveAt(0);
            _rainbowSequence.Add(firstElement);
        }
    }
}