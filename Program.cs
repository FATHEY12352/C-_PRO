using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JointProject1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime currentUpdateTime;
            DateTime lastUpdateTime;
            TimeSpan frameTime;
            currentUpdateTime = DateTime.Now;
            lastUpdateTime = DateTime.Now;

            Form1 form = new Form1();
            form.Show();
            while (form.Created == true)
            {
                currentUpdateTime = DateTime.Now;
                frameTime = currentUpdateTime - lastUpdateTime;
                if (frameTime.TotalMilliseconds > 20)
                {
                    Application.DoEvents();
                    form.UpdateWorld();
                    form.Refresh();
                    lastUpdateTime = DateTime.Now;
                }
            }
        }
    }
}
