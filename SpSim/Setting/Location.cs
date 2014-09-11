using SpSim.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpSim.Setting
{
    /// <summary>
    /// The general location, our little game takes place in
    /// </summary>
    public class Location
    {

        #region Fields and Properties

        /// <summary>
        /// Reference at the linearray of the displaybox to add some text
        /// </summary>
        public TextBox Display;

        /// <summary>
        /// List of all rooms
        /// </summary>
        public List<Room> Rooms = new List<Room>();

        /// <summary>
        /// List of all implements
        /// </summary>
        public List<Implement> Implements = new List<Implement>();

        /// <summary>
        /// The protagonist
        /// </summary>
        public Protagonist Protagonist = new Protagonist();

        /// <summary>
        /// Contains all the possible options for the protagonist
        /// It´s cleared after you take an action and refilled
        /// through the evaluation after the status has been printed
        /// </summary>
        public List<Action> PossibleActions = new List<Action>();

        #endregion

        #region Startup Methods

        public void Prepare()
        {
            //Place the protagonist in his room
            Protagonist.CurrentRoom = Protagonist.OwnRoom;
            //Apply hand to protagonist
            Protagonist.LockedImplement = 0;
            //Randomly place the in the house
            ScatterImplements();

            //Display the status
            PrintStatus();
            //Evaluate Actions
            EvaluatePossibleAction();
            //Display possible Actions
            PrintAvailableActions();
        }

        /// <summary>
        /// Places all the variable implements in random rooms
        /// </summary>
        private void ScatterImplements()
        {
            Random rnd = new Random();

            foreach (Implement i in Implements)
            {
                //Everything is places except for the locked item
                if (i.Id != 0)
                {
                    i.CurrentRoom = Rooms[rnd.Next(Rooms.Count - 1) + 1].Id;
                }
            }
        }

        #endregion

        #region Status/Options Methods

        /// <summary>
        /// Generally informs the player about the room
        /// and the actors after taking an action
        /// </summary>
        private void PrintStatus()
        {
            string output = "";

            //Where am I?
            string roomStatus = ActorUtil.GetRoomById(Rooms, Protagonist.CurrentRoom).GetStatus();
            output = String.Format("I'm {0}.", roomStatus);

            //Am I carrying any implement other then my hand?
            if (Protagonist.VariableImplement != -1)
            {
                string holdingStatus = Environment.NewLine + String.Format("I'm holding a {0}.", ActorUtil.GetImplementById(Implements, Protagonist.VariableImplement).Name);
                output += holdingStatus;
            }

            //Are there any implements?
            //locImpl = local Implements = Implements in your room
            List<Implement> locImpl = ActorUtil.GetImplementsByRoom(Implements, Protagonist.CurrentRoom);
            if (locImpl.Count > 0)
            {
                string implementStatus = "";

                if (locImpl.Count == 1)
                {
                    implementStatus = String.Format("I see a {0} here.", locImpl[0].Name);
                }
                else if (locImpl.Count == 2)
                {
                    implementStatus = String.Format("I see a {0} and a {1} here.", locImpl[0].Name, locImpl[1].Name);
                } else {

                    implementStatus = "I see ";
                    for (int i = 0; i < locImpl.Count - 1; i++)
                    {
                        if (i == locImpl.Count - 2)
                        {
                            implementStatus += String.Format("a {0} and a {1} here.", locImpl[i].Name, locImpl[i+1].Name);
                        }
                        else
                        {
                            implementStatus += String.Format("a {0}, ", locImpl[i].Name);
                        }
                    }
                }

                output += Environment.NewLine + implementStatus;
            }

            Display.AppendText(Environment.NewLine + output + Environment.NewLine);
        }

        /// <summary>
        /// Refills the ActionList with all possible Actions
        /// </summary>
        private void EvaluatePossibleAction()
        {
            //Clear the ActionList
            PossibleActions.Clear();

            AddDefaultActions();
            AddMoveActions();
            AddImplementActions();
        }

        /// <summary>
        /// Adds the default actions, which are possible in every room.
        /// </summary>
        private void AddDefaultActions()
        {
            //Look around the Room
            Action lookAround = new Action("Look around", ActionType.LOOK_AROUND_ROOM);
            lookAround.ActionText = StringHelper.UnbreakLines(ActorUtil.GetRoomById(Rooms, Protagonist.CurrentRoom).Description);
            PossibleActions.Add(lookAround);

            //Think about yourself
            PossibleActions.Add(new Action("Think about yourself", ActionType.THINK_ABOUT_YOURSELF, Protagonist.Lore));
        }

        /// <summary>
        /// Adds the room movement actions available to the protagonist
        /// </summary>
        private void AddMoveActions()
        {
            Room room;
            Action action;

            foreach (long l in ActorUtil.GetRoomById(Rooms, Protagonist.CurrentRoom).Links)
            {
                room = ActorUtil.GetRoomById(Rooms, l);
                action = new Action(String.Format("Move to the {0}", room.Name), ActionType.MOVE_TO_ROOM);
                action.Params.Add(room.Id);
                action.ActionText = String.Format("I go to the {0}", room.Name);
                PossibleActions.Add(action);
            }
        }

        /// <summary>
        /// Adds the Implement PickUp/Drop Actions
        /// </summary>
        private void AddImplementActions()
        {
            Action action;

            //Pick up Actions
            List<Implement> locImpl = ActorUtil.GetImplementsByRoom(Implements, Protagonist.CurrentRoom);
            foreach (Implement impl in locImpl)
            {
                action = new Action(String.Format("Pick up the {0}", impl.Name), ActionType.PICK_UP_IMPLEMENT, String.Format("I pick up the {0}", impl.Name));
                action.Params.Add(impl.Id);
                PossibleActions.Add(action);
            }

            //Drop Action
            if (Protagonist.VariableImplement != -1)
            {
                Implement currentImpl = ActorUtil.GetImplementById(Implements, Protagonist.VariableImplement);
                PossibleActions.Add(new Action(String.Format("Drop the {0}", currentImpl.Name), ActionType.DROP_IMPLEMENT, String.Format("I drop the {0}", currentImpl.Name)));
            }
        }

        /// <summary>
        /// Prints the available options of the protagonist
        /// for the player to choose from
        /// </summary>
        private void PrintAvailableActions()
        {
            string output = "";

            for (int i = 0; i < PossibleActions.Count; i++)
            {
                output += String.Format("[{0}] {1}\t", i, PossibleActions[i].DisplayText);
            }

            Display.AppendText(Environment.NewLine + output + Environment.NewLine);
        }

        #endregion

        #region Testingmethods

        /// <summary>
        /// Prints all the Rooms of the Location (for Testing)
        /// </summary>
        public void PrintRooms()
        {
            foreach (Room r in Rooms)
            {
                Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(r.ToString()) + Environment.NewLine);
            }
        }

        /// <summary>
        /// Prints information about the protagonist (for Testing)
        /// </summary>
        public void PrintProtagonist()
        {
            Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(Protagonist.ToString()) + Environment.NewLine);
        }

        /// <summary>
        /// Prints all the implements in the Location (for Testing)
        /// </summary>
        public void PrintImplements()
        {
            foreach (Implement i in Implements)
            {
                Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(i.ToString()) + Environment.NewLine);
            }
        }

        #endregion
    }
    
}
