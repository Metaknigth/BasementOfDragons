using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasementOfDragons
{
    class Player
    {
        private string sPlayerAt = "@";
        private int iXPos;
        private int iYPos;
        private int iHP;
        private int iBaseDamage;


        // get set methods for iBaseDamage
        public int getBaseDamage()
        {
            return iBaseDamage;
        }

        public void setBaseDamage(int piBaseDamage)
        {
            iBaseDamage = piBaseDamage;
        }

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
        public string getPlayerAt()
        {
            return sPlayerAt;
        }

        public void setPlayerAt(string psPlayerAt)
        {
            sPlayerAt = psPlayerAt;
        }
    }
}
