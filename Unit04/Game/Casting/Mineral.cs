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

        public Mineral(string type){
            if (type == "rock" || type == "gem"){
                _value = (type == "rock") ? -1 : 1;

                List<int> velocities = new List<int>{
                    2,
                    3,
                    4,
                    5
                };
                Random random = new Random();
                int yVel = velocities[random.Next(velocities.Count)];
                _velocity = new Point(0, yVel);
            }
            else{
                _value = 0;
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
