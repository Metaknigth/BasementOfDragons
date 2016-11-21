using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementOfDragons
{
    class Trigger
    {
        private string sTriggerAt = "t";
        private int iXPos;
        private int iYPos;
        private bool bTrigOn;

        // get set methods for XPos
        public int getXPos()
        {
            return iXPos;
        }

        public void setXPos(int piXPos)
        {
            iXPos = piXPos;
        }
        //get set methods for YPos
        public int getYPos()
        {
            return iYPos;
        }

        public void setYPos(int piYPos)
        {
            iYPos = piYPos;
        }
        //get set methods for HP
        public bool getTrigOn()
        {
            return bTrigOn;
        }

        public void setTrigOn(bool pbTrigOn)
        {
            bTrigOn = pbTrigOn;
        }
        //get set methods for playerAt
        public string getTriggerAt()
        {
            return sTriggerAt;
        }

        public void setTriggerAt(string psTriggerAt)
        {
            sTriggerAt = psTriggerAt;
        }

    }
}
