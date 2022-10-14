using System;
using System.Collections.Generic;

namespace Unit03
{
    /// <summary>
    /// Constructs a new instance of Jumper
    /// 
    /// The purpose of Jumper is to keep track of its current form and
    /// manipulate itself.
    /// </summary>
    public class Jumper
    {
        private List<string> _jumper;
        public bool isDead;

        public Jumper()
        {
            _jumper = new List<string>{
                "  ___  ",
                " /___\\ ",
                " \\   / ",
                "  \\ /  ",
                "   O   ",
                "  /|\\  ",
                "  / \\  "
            };
            isDead = false;
        }

        /// <summary>
        /// Returns the jumper.
        /// </summary>
        public string getJumper()
        {
            string stringJumper = "";
            foreach (string jumperLine in _jumper)
            {
                stringJumper += $"{jumperLine}\n";
            }
            return stringJumper;
        }

        /// <summary>
        /// Removes the top line of the jumper's parachute.
        /// </summary>
        public void cutLine()
        {
            _jumper.RemoveAt(0);
        }

        /// <summary>
        /// Checks the jumper's length.
        /// </summary>
        public int jumperLength()
        {
            return _jumper.Count;
        }

        /// <summary>
        /// Turns the state of the jumper to dead and changes his head
        /// to reflect that.
        /// </summary>
        public void makeDead()
        {
            isDead = true;
            _jumper[0] = "   x   ";
        }
    }
}