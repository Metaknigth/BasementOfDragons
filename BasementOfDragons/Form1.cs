using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using MySql.Data.MySqlClient;

namespace BasementOfDragons
{
    public partial class BasementOfDragons : Form
    {
        public BasementOfDragons()
        {
            InitializeComponent();
        }
        //ammount of Dragons
        public const int iMaxDragonCount = 4;

        //Instantiating player
        Player Player1 = new Player();

        EndGoal EndGoal1 = new EndGoal();

        //Making a list of of Class type Dragon and Trigger. 
        Dragon[] DragonList = new Dragon[iMaxDragonCount];
        Trigger[] TriggerList = new Trigger[iMaxDragonCount];


        Random rDamage = new Random();
        int iKillCount = 0;
        int iScore = 0;
        int iMoveCount = 0;
        
        //var used to calculate distance between a dragon and the player. Used in "AI"
        int iDragDistanceX = 0, iDragDistanceY = 0;

        // var used to go through the sGrid/Map array at various points during the program.
        int iRow;
        int iCol;


        //SQL stuff to connect to server
        MySqlConnection myConn;
        DataTable dTable = new DataTable();
		//The server this connects to has been retired and the account since no longer exists.
        string connStr = "server = bel.sunderland.ac.uk; " +
                                    "database =bg46ia; " +
                                    "uid =bg46ia; " +
                                    "pwd =; ";



        string sTextLog = "";

        //used to store players name
        string sName = "";

        //the array that represents the map. 
        string[,] sGrid = new string[14, 14] // [y,x] reminder. 
                {{"#","#","#","#","#","#","#","#","#","#","#","#","#","#"},
                 {"#"," "," "," "," "," "," ","#"," "," "," "," "," ","#"},
                 {"#"," "," "," "," ","#"," ","#"," ","#","#","#"," ","#"},
                 {"#","#","#","#","#","#"," "," "," ","#"," "," "," ","#"},
                 {"#"," "," "," ","#","#","#","#","#","#"," "," "," ","#"},
                 {"#"," ","#","#","#"," "," "," "," ","#"," ","#","#","#"},
                 {"#"," "," "," "," "," "," "," "," ","#"," ","#","#","#"},
                 {"#"," ","#","#","#"," "," "," "," ","#"," "," "," ","#"},
                 {"#"," "," "," ","#","#"," ","#","#","#","#","#"," ","#"},
                 {"#"," "," "," "," "," "," "," "," ","#"," "," "," ","#"},
                 {"#"," ","#","#","#","#","#","#","#","#"," "," "," ","#"},
                 {"#"," "," "," "," "," "," "," "," ","#"," "," "," ","#"},
                 {"#"," "," "," "," "," "," "," "," "," "," "," "," ","#"},
                 {"#","#","#","#","#","#","#","#","#","#","#","#","#","#"}};

        //an array used to store a copy of a clean/original version of the map for refreshing purposes
        string[,] sCurrGridCopy= new string[14,14];


        private void Form1_Load(object sender, EventArgs e)
        {
            StartScreen startScreenForm = new StartScreen();
            startScreenForm.ShowDialog();

            sName = StartScreen.sName;

            //int iDamamge = rDamage.Next(0, 6);
            //inisialisizing the instance of player
            Player1.setHP(40);
            Player1.setXPos(1);
            Player1.setYPos(1);
            Player1.setBaseDamage(5);

            EndGoal1.setXPos(3);
            EndGoal1.setYPos(4);
            EndGoal1.setGoalComplete(false);


            ////////////////
            //coping the Clean sGrid/Map without the player,dragon,etc to another array.
            // ref used: http://msdn.microsoft.com/en-us/library/system.array.copy(v=vs.110).aspx
            ///////////////
            Array.Copy(sGrid, sCurrGridCopy, sGrid.Length); 


            //instantiating the Dragon and Trigger classes; List of them. 
            //also setting all there instance variables/attributes.
            for (int i = 0; i < iMaxDragonCount; i++)
            {
                DragonList[i] = new Dragon(); // this is the instantiation.
                DragonList[i].setHP(10);

                switch (i)
                {
                    case 0:
                        DragonList[i].setXPos(11);
                        DragonList[i].setYPos(12);
                        break;
                    case 1:
                        DragonList[i].setXPos(3);
                        DragonList[i].setYPos(11);
                        break;
                    case 2:
                        DragonList[i].setXPos(8);
                        DragonList[i].setYPos(9);
                        break;
                    case 3:
                        DragonList[i].setXPos(6);
                        DragonList[i].setYPos(6);
                        break;
                    default:
                        break;
                }

            }

            //for the triggers. Used to "wake"/"aggro" the dragons. 
            for (int i = 0; i < iMaxDragonCount; i++)
            {
                TriggerList[i] = new Trigger();
                TriggerList[i].setTrigOn(false);
                switch (i)
                {
                    case 0:
                        TriggerList[i].setXPos(12);
                        TriggerList[i].setYPos(8);
                        break;
                    case 1:
                        TriggerList[i].setXPos(9);
                        TriggerList[i].setYPos(12);
                        break;
                    case 2:
                        TriggerList[i].setXPos(1);
                        TriggerList[i].setYPos(9);
                        break;
                    case 3:
                        TriggerList[i].setXPos(6);
                        TriggerList[i].setYPos(8);
                        break;
                    default:
                        break;
                }

            }


            //set font for TextBox here cause ugh.. i dunno. I did it here. Seems convenient. 
            rtxtMap.Font = new Font("Lucida console", 16, FontStyle.Bold);

            //displaying map for the first time
            drawMap();

        }

        private void drawMap()
        {
            ///////////////////  //////////  /////////////  ///////////// ///////////////////////
            //// overwriting the sGrid/Map with a clean copy from the previously filled array ///
            //////////////////   ////////// ////////////// ///////////// ////////////////////////
            Array.Copy(sCurrGridCopy, sGrid, sGrid.Length);


            // replacing elements in the sGrid/Map array with the same position as the Player Or a Dragon
            // Using foreach to go through all the dragons. 
            foreach (Dragon dragonItem in DragonList)
            {


                //displaying map
                for (iCol = 0; iCol < sGrid.GetLength(0); iCol++)
                {
                    for (iRow = 0; iRow < sGrid.GetLength(1); iRow++)
                    {
                        //replacing item in array with player.
                        if (Player1.getXPos() == iRow && Player1.getYPos() == iCol)
                        {
                            //sPreviousGridItem = sGrid[Player1.getYPos(), Player1.getXPos()];
                            sGrid[Player1.getYPos(), Player1.getXPos()] = Player1.getPlayerAt();
                        }
                        //placing dragon in the array 
                        else if (dragonItem.getXPos() == iRow && dragonItem.getYPos() == iCol)
                        {
                            if (dragonItem.getDragonAt() == "D")
                            {
                                sGrid[dragonItem.getYPos(), dragonItem.getXPos()] = dragonItem.getDragonAt();
                            }
                        }
                        else if (EndGoal1.getXPos() == iRow && EndGoal1.getYPos() == iCol)
                        {
                            sGrid[EndGoal1.getYPos(), EndGoal1.getXPos()] = EndGoal1.getEndGoalAt();
                        }
                        
                    }
                }
            }

            //drawing rest of map
            for (iCol = 0; iCol < sGrid.GetLength(0); iCol++)
            {
                for (iRow = 0; iRow < sGrid.GetLength(1); iRow++)
                {
                    //string to store the next tile.
                    string sNextTile = (sGrid[iCol, iRow]).ToString(); 
                    if (sNextTile == "#")
                    {
                        rtxtMap.AppendText("█"); /// used to just overwrite the # tile to a filled in rectangle
                                                      /// because drawing the map with that char in the array is 
                                                      /// annoying. Convenient to change it here
                    }
                    else if (sNextTile == "@")
                    {
                        rtxtMap.SelectionColor = Color.Green;
                        rtxtMap.AppendText(Player1.getPlayerAt()); //placing the player.
                        rtxtMap.SelectionColor = Color.DimGray; //reseting color. 
                    }
                    else if (sNextTile == "Ω")
                    {
                        if (EndGoal1.getGoalComplete() == true)
                        {
                            rtxtMap.SelectionColor = Color.Gold;
                            rtxtMap.AppendText(EndGoal1.getEndGoalAt()); //placing the player.
                            rtxtMap.SelectionColor = Color.DimGray; //reseting color. 
                        }
                        else
                        {
                            rtxtMap.SelectionColor = Color.Sienna;
                            rtxtMap.AppendText(EndGoal1.getEndGoalAt()); //placing the player.
                            rtxtMap.SelectionColor = Color.DimGray; //reseting color. 
                        }
                    }
                    else if (sNextTile =="D")
                    {
                        rtxtMap.SelectionColor = Color.Maroon;
                        rtxtMap.AppendText("D"); 
                        rtxtMap.SelectionColor = Color.DimGray;
                    }
                    else
                    {
                        rtxtMap.AppendText(sNextTile);
                    }
                    //new lines went at the end of line in the array
                    if (iRow == sGrid.GetLength(1) - 1)
                    {
                        rtxtMap.AppendText("\n"); 
                    }
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ///// -- this is pretty much the game update. It updates when a key is pressed -- ////
            //if player is dead.
            if (Player1.getHP() <= 0)
            {
                Player1.setPlayerAt("X");
                sTextLog = "You are dead.";
            }
            iKillCount = 0;

            //movign player to the key from E;
            movePlayer(e); //includes damaging monsters

            foreach (Dragon dragonItem in DragonList)
            {
                if (dragonItem.getHP() <=0)
                {
                    dragonItem.setDragonAt("x");
                    iKillCount++;
                }
            }

            //check if player is on a trigger
            checkTrigger();


            iScore = iKillCount * 10;
            if (iKillCount == iMaxDragonCount)
            {
                EndGoal1.setGoalComplete(true);
            }

            checkEndGoal();
            //move any triggered enemies (if inside method) 
            moveEnemy();

            checkPlayerDamage();

            //Clearing the texbox;
            rtxtMap.Text = "";
            //////////////////////////////!!!!:::::::.......  ..    .   ..  ....::::::////
            ////TO DO:                                                                ////
            //// *use LastPositions of player and other entities in combination with  ////
            ////     richTextbox1.Text.Replace(last char, new char);                  ////
            ////   to replace them. Might get rid of the tearing ??                   ////
            ////                                                                      ////
            //// * if not try and use this.refresh(); somehow maybe...                ////
            //////////////////////////////!!!!^^^^^^""""""""  ..    .   ..  ....::::::////


            lblPlayerHP.Text = "Heath: " + Player1.getHP().ToString();
            txtTextLog.AppendText(sTextLog + "\n");
            txtDragonsHP.Text = "";
            lblMoveCount.Text = "Moves: " + iMoveCount.ToString();
            lblScore.Text = "Score: " + (iScore).ToString();
            lblKillCount.Text = "Kills: " + iKillCount.ToString();
            for (int i = 0; i < iMaxDragonCount; i++)
            {
                if (DragonList[i].getHP() <= 0)
                {
                    txtDragonsHP.AppendText("Dragon " + (i + 1) + " is dead." + "\n");
                }
                else
                {
                    txtDragonsHP.AppendText("Dragon " + (i + 1) + " HP:" + DragonList[i].getHP().ToString() + "\n");              
                }
            }

            
            //txtDragonsHP.Text = sDragonsHP;
            //drawing map after all the movements, calculations and what-not.
            drawMap();
        }
        
        public void checkPlayerDamage()
        {
            //left
            if (sGrid[Player1.getYPos(), Player1.getXPos() - 1] == "D")
            {
                Player1.setHP(Player1.getHP() - 1);
            }
            //right
            if (sGrid[Player1.getYPos(), Player1.getXPos() + 1] == "D")
            {
                Player1.setHP(Player1.getHP() - 1);
            }
            //up
            if (sGrid[Player1.getYPos() - 1, Player1.getXPos()] == "D")
            {
                Player1.setHP(Player1.getHP() - 1);
            }
            //down
            if (sGrid[Player1.getYPos() + 1, Player1.getXPos()] == "D")
            {
                Player1.setHP(Player1.getHP() - 1);
            }

        }
        
        public void checkTrigger()
        {
            foreach (Trigger triggerItem in TriggerList)
            {
                if (Player1.getXPos() == triggerItem.getXPos() && Player1.getYPos() == triggerItem.getYPos())
                {
                    triggerItem.setTrigOn(true);
                }
            }
        }

        public void checkEndGoal()
        {
            if (Player1.getXPos() == EndGoal1.getXPos() & Player1.getYPos() == EndGoal1.getYPos())
            {
                if (EndGoal1.getGoalComplete() == true)
                {
                    sTextLog = "You have completed the level!!!" + "\n";

                    if (iMoveCount<=65)
                    {
                        iScore = iScore * 3;
                        sTextLog += "You get a score multiplier of 3!";
                    }
                    else if (iMoveCount >65 && iMoveCount <=90)
                    {
                        iScore = iScore * 2;
                        sTextLog += "You get a score multiplier of 2!";
                    }
                    else
                    {
                        iScore *= 1;
                        sTextLog += "Sorry! You didn't earn a score multiplier.";
                    }

                    btnScoreboard.Enabled = true;
                    btnScoreboard.Focus();
                
                }
            }

        }

        public void movePlayer(KeyEventArgs e)
        {
            int iDamage = rDamage.Next(1,6);
                ////////////////////
                ///new move stuff///
                ////////////////////
                switch (e.KeyData)
                {
                    case Keys.A:
                    case Keys.Left:
                        switch (sGrid[Player1.getYPos(), Player1.getXPos() - 1])
                        {
                            case "#":
                                sTextLog = "Can't go left.";
                                break;
                            case "D":
                                sTextLog = "You hit the dragon for " + iDamage.ToString() + " damage.";
                                for (int i = 0; i < iMaxDragonCount; i++)
                                {
                                    if (Player1.getYPos() == DragonList[i].getYPos() & Player1.getXPos() - 1 == DragonList[i].getXPos())
                                    {
                                        DragonList[i].setHP(DragonList[i].getHP() - iDamage);
                                        if (DragonList[i].getHP() <= 0)
                                        {
                                            sTextLog = "You killed Dragon " + (i+1) + " !";
                                        }
                                    }
                                }
                                break;
                            default:
                                Player1.setXPos(Player1.getXPos() - 1);
                                sTextLog = "You moved left.";
                                iMoveCount++;
                                break;
                        }
                        break;
                    case Keys.D:
                    case Keys.Right:

                        switch (sGrid[Player1.getYPos(), Player1.getXPos() + 1])
                        {
                            case "#":
                                sTextLog = "Can't go right.";
                                break;
                            case "D":
                                sTextLog = "You hit the dragon for" + iDamage.ToString() + " damage.";
                                for (int i = 0; i < iMaxDragonCount; i++)
                                {
                                    if (Player1.getYPos() == DragonList[i].getYPos() & Player1.getXPos() + 1 == DragonList[i].getXPos())
                                    {
                                        DragonList[i].setHP(DragonList[i].getHP() - iDamage);
                                        if (DragonList[i].getHP() <= 0)
                                        {
                                            sTextLog = "You killed Dragon " + (i + 1) + " !";
                                        }
                                    }
                                }
                                break;
                            default:
                                Player1.setXPos(Player1.getXPos() + 1);
                                sTextLog = "You moved right,";
                                iMoveCount++;
                                break;
                        }
                        break;
                        
                    case Keys.W:
                    case Keys.Up:
                        switch (sGrid[Player1.getYPos() - 1, Player1.getXPos()])
                        {
                            case "#":
                                sTextLog = "Can't go up.";
                                break;
                            case "D":
                                sTextLog = "You hit the dragon for " + iDamage.ToString() + " damage.";

                                for (int i = 0; i < iMaxDragonCount; i++)
                                {
                                    if (Player1.getYPos() - 1 == DragonList[i].getYPos() & Player1.getXPos() == DragonList[i].getXPos())
                                    {
                                        DragonList[i].setHP(DragonList[i].getHP() - iDamage);
                                        if (DragonList[i].getHP() <= 0)
                                        {
                                            sTextLog = "You killed Dragon " + (i + 1) + " !";
                                        }
                                    }
                                }

                                break;
                            default:
                                Player1.setYPos(Player1.getYPos() - 1);
                                sTextLog = "You moved up.";
                                iMoveCount++;
                                break;
                        }
                        break;
                    case Keys.S:
                    case Keys.Down:

                        switch (sGrid[Player1.getYPos() + 1, Player1.getXPos()])
                        {
                            case "#":
                                sTextLog = "Can't go down.";
                                break;
                            case "D":
                                sTextLog = "You hit the dragon for " + iDamage.ToString() + " damage.";

                                for (int i = 0; i < iMaxDragonCount; i++)
                                {
                                    if (Player1.getYPos() + 1 == DragonList[i].getYPos() & Player1.getXPos() == DragonList[i].getXPos())
                                    {
                                        DragonList[i].setHP(DragonList[i].getHP() - iDamage);
                                        if (DragonList[i].getHP() <= 0)
                                        {
                                            sTextLog = "You killed Dragon " + (i + 1) + " !";
                                        }

                                    }
                                }

                                break;
                            default:
                                Player1.setYPos(Player1.getYPos() + 1);
                                sTextLog = "You moved down.";
                                iMoveCount++;
                                break;
                        }
                        break;
                    case Keys.Escape:
                        Application.Exit();
                        break;
                    default:
                        break;
                }

        }

        public void moveEnemy()
        {
            for(int i = 0; i < iMaxDragonCount; i++)
            {
                if (TriggerList[i].getTrigOn() == true)
                {



                    //moving dragon since we have new player coordinates... ?
                    iDragDistanceX = Player1.getXPos() - DragonList[i].getXPos();
                    iDragDistanceY = Player1.getYPos() - DragonList[i].getYPos();


                    //my own weird little moving logic. Tis not the best but it works in very certain level designs..

                    if (Math.Abs(iDragDistanceX) > Math.Abs(iDragDistanceY)) // go the same why player is going kinda
                    {
                        if (iDragDistanceX > 1) //Player is to the RIGHT.
                        {
                            if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] == "#" | sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] == "D") //if RIGHT IS blocked 
                            {
                                if (iDragDistanceY >= 1) //if player is DOWN 
                                {
                                    if (sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "D") // if DOWN is NOT blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                    }   // if DOWN IS blocked, Check if UP is not                         
                                    else if (sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "D")
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); //go UP
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = DragonList[i].getYPos();
                                    }
                                }
                                else if (iDragDistanceY <= -1)//if player is UP (relative to D)
                                {
                                    if (sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "D") // if UP is NOT blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); //go UP
                                    }   // if UP IS blocked, Check if DOWN is not                         
                                    else if (sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "D")
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                            }// if RIGHT is NOT blocked
                            else
                            {
                                DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                            }

                        }//if player is to the LEFT 
                        else if (iDragDistanceX < -1)//going LEFT
                        {
                            if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] == "#" | sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] == "D") //if left is BLOCKED 
                            {
                                if (iDragDistanceY >= 1) //if player is DOWN 
                                {
                                    if (sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "D") // if DOWN is NOT blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                    }   // if DOWN IS blocked, Check if UP is not                         
                                    else if (sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "D")
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); // go UP
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                                else if (iDragDistanceY <= -1) //if player is UP
                                {
                                    if (sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] != "D") // if UP is NOT blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); // go UP
                                    }   // if UP IS blocked, Check if DOWN is not                         
                                    else if (sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "#" & sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] != "D")
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                    }
                                    else//if DOWN and Everything else is blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                            }// if LEFT is NOT blocked
                            else
                            {
                                DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                            }
                        }
                    }
                    else if (Math.Abs(iDragDistanceX) < Math.Abs(iDragDistanceY))
                    {
                        if (iDragDistanceY > 1)//if player is DOWN
                        {
                            if (sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] == "#" | sGrid[DragonList[i].getYPos() + 1, DragonList[i].getXPos()] == "D") //if DOWN IS blocked 
                            {
                                if (iDragDistanceX >= 1) //if player is to the RIGHT
                                {
                                    if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "D") // if RIGHT is NOT blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                    }   // if RIGHT IS blocked, Check if LEFT is not                         
                                    else if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "D")
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); // go UP

                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                                else if (iDragDistanceX <= -1)//if player is to the LEFT
                                {
                                    if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "D") // if LEFT is NOT blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                    }   // if LEFT IS blocked, Check if RIGHT is not                         
                                    else if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "D")
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() - 1); // go UP

                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                            }// if DOWN is NOT blocked
                            else
                            {
                                DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                            } // if LEFT is blocked
                        }
                        else if (iDragDistanceY < -1) // if player is UP
                        {
                            if (sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] == "#" | sGrid[DragonList[i].getYPos() - 1, DragonList[i].getXPos()] == "D") //if UP IS blocked 
                            {
                                if (iDragDistanceX >= 1) //if player is to the RIGHT 
                                {
                                    if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "D") // if RIGHT is NOT blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                    }   // if RIGHT IS blocked, Check if LEFT is not                         
                                    else if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "D")
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = iDragonX;
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                                else if (iDragDistanceX <= -1)//if player is to the LEFT
                                {
                                    if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() - 1] != "D") // if LEFT is NOT blocked
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() - 1); //go the only way LEFT
                                    }   // if LEFT IS blocked, Check if RIGHT is not                         
                                    else if (sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "#" & sGrid[DragonList[i].getYPos(), DragonList[i].getXPos() + 1] != "D")
                                    {
                                        DragonList[i].setXPos(DragonList[i].getXPos() + 1); //go RIGHT
                                    }
                                    else//if Everything else is blocked
                                    {
                                        DragonList[i].setYPos(DragonList[i].getYPos() + 1); //go DOWN
                                        //save the X and Y of this position so it doesn't go back here
                                        //iDragLastPosX = Dragon1.getXPos();
                                        //iDragLastPosY = iDragonY;
                                    }
                                }
                            }// if UP is NOT blocked
                            else
                            {
                                DragonList[i].setYPos(DragonList[i].getYPos() - 1); // go UP
                            } // if LEFT is blocked
                        }
                    }
                }


            }

        }

        private void btnScoreboard_Click(object sender, EventArgs e)
        {
            //Add player score to DataBase
            myConn = new MySqlConnection(connStr);
            MySqlDataAdapter dAdapter = new MySqlDataAdapter("SELECT * FROM Scoreboard", myConn);
            DataTable dTableAdd = new DataTable();

            dAdapter.Fill(dTableAdd);
            DataRow dr = dTableAdd.NewRow();
            dr["PlayerName"] = sName;
            dr["Score"] = iScore;
            dTableAdd.Rows.Add(dr);
            MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(dAdapter);
            dAdapter.Update(dTableAdd);


            //update DataGrid
            string sqlStr = "SELECT PlayerName, Score FROM Scoreboard ORDER BY Score DESC Limit 10";
            myConn = new MySqlConnection(connStr);
            dTable.Clear();
            dAdapter = new MySqlDataAdapter(sqlStr, myConn);
            dAdapter.Fill(dTable);
            dAdapter.Dispose();

            this.Close();


            ScoreBoard ScoreBoardForm = new ScoreBoard();
            ScoreBoardForm.PopulateScoreboard(dTable);
            ScoreBoardForm.ShowDialog();


        }

    }
}
