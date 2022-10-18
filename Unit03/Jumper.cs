using System;
using System.Collections.Generic;

namespace Unit03
{
    /// <summary>
    /// <para>Constructs a new instance of Jumper</para>
    /// <para>
    /// The purpose of Jumper is to keep track of its current form and
    /// change itself.
    /// </para>
    /// </summary>
    public class Jumper
    {
        private List<string> _jumper;
        public bool isDead;

        /// <summary>
        /// Constructs a new instance of Jumper
        /// </summary>
        public Jumper()
        {
            _jumper = new List<string>
            {
                @"  ___  ",
                @" /___\ ",
                @" \   / ",
                @"  \ /  ",
                @"   O   ",
                @"  /|\  ",
                @"  / \  "
            };
            isDead = false;
        }

        /// <summary>
        /// Returns the jumper.
        /// </summary>
        /// <returns>Current jumper as a string.</returns>
        public string getJumper()
        {
            string stringJumper = String.Join("\n", _jumper);
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
        /// <returns>Current length of the jumper.</returns>
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

        /// <summary>
        /// Returns a winning jumper!
        /// </summary>
        /// <returns>Winning jumper as a string.</returns>
        public string getWinner()
        {
            return " \\ O / \n   |   \n__/_\\__";
        }
    }
}