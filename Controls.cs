using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JointProject1
{
    internal class Control
    {
        private Form1 form; // Reference to the main form

        public Control(Form1 form)
        {
            this.form = form;
            form.KeyDown += Form1_KeyDown; // Subscribe to the KeyDown event of the form
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Your key handling code here
            if (e.KeyCode == Keys.W)
            {
                // Handle the W key press
                // ...
            }
            else if (e.KeyCode == Keys.S)
            {
                // Handle the S key press
                // ...
            }
            else if (e.KeyCode == Keys.A)
            {
                // Handle the A key press
                // ...
            }
            else if (e.KeyCode == Keys.D)
            {
                // Handle the D key press
                // ...
            }
            else if (e.KeyCode == Keys.Space)
            {
                // Handle the Space key press
                // ...
            }
        }
    }
}