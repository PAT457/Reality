using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reality.Content.Utils
{
    class FrameSleep
    {

        private int curFrame = 0;
        private int goalFrame;
        private bool inSession = false;

        public FrameSleep()
        {
            //No Variables need to be set, nothing.
        }

        public bool wait(int sleepAmmount)
        {
            if (inSession)
            {
                curFrame = curFrame + 1;
            }

            else
            {
                inSession = true;
                goalFrame = sleepAmmount;
                curFrame = 0;
            }

            if (curFrame >= goalFrame)
            {
                inSession = false;
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
