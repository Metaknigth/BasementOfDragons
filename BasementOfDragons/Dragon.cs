using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementOfDragons
{
    class Dragon
    {
        private string sDragonAt = "D";
        private int iXPos;
        private int iYPos;
        private int iHP;


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
        public int getHP()
        {
            return iHP;
        }

        public void setHP(int piHP)
        {
            iHP = piHP;
        }
        //get set methods for playerAt
        public string getDragonAt()
        {
            return sDragonAt;
        }

        public void setDragonAt(string psDragonAt)
        {
            sDragonAt = psDragonAt;
        }
    }
}
