using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementOfDragons
{
    class EndGoal
    {
        private string sEndGoalAt = "Ω";
        private int iXPos;
        private int iYPos;
        private bool bGoalComplete;

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
        //get set methods for 
        public bool getGoalComplete()
        {
            return bGoalComplete;
        }

        public void setGoalComplete(bool pbGoalComplete)
        {
            bGoalComplete = pbGoalComplete;
        }
        //get set methods for playerAt
        public string getEndGoalAt()
        {
            return sEndGoalAt;
        }

        public void setEndGoalAt(string psEndGoalAt)
        {
            sEndGoalAt = psEndGoalAt;
        }



    }
}
