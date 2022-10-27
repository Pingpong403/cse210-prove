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
        public Mineral(string type){
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

        /// <summary>
        /// Get the value of the mineral.
        /// </summary>
        public int GetValue(){
            return _value;
        }
    }
}