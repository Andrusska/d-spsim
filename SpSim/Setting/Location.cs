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
        /// List of all clothes
        /// </summary>
        public List<Clothing> Clothes = new List<Clothing>();

        /// <summary>
        /// The protagonist
        /// </summary>
        public Protagonist Protagonist = new Protagonist();

        /// <summary>
        /// List of Girls in this setting
        /// </summary>
        public List<Girl> Girls = new List<Girl>();

        /// <summary>
        /// Contains all the possible options for the protagonist
        /// It´s cleared after you take an action and refilled
        /// through the evaluation after the status has been printed
        /// </summary>
        public List<Action> PossibleActions = new List<Action>();

        #endregion

        #region Startup Methods

        /// <summary>
        /// Prepares the Location for the game
        /// </summary>
        public void Prepare()
        {
            //Place the protagonist in his room
            Protagonist.CurrentRoom = Protagonist.OwnRoom;
            //Apply hand to protagonist
            Protagonist.LockedImplement = 0;
            //Dress up the girls
            DressUpGirls();
            //Randomly place the in the house
            ScatterImplements();
            //Place some clothes
            ScatterClothes();
            //Places the rest of the clothes
            DumpClothes();

            //Display the status
            PrintDefaultStatus();
            //Evaluate Actions
            EvaluateDefaultActions();
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

        /// <summary>
        /// Initially dresses up the girls
        /// </summary>
        private void DressUpGirls()
        {
            bool wearsAOnepiece = false;

            foreach (Girl girl in Girls)
            {
                wearsAOnepiece = DressUpTopOrOnepiece(girl);
                if (!wearsAOnepiece)
                {
                    DressUpPart(girl, ClothingType.BOTTOM);
                }
                DressUpPart(girl, ClothingType.BRA);
                DressUpPart(girl, ClothingType.PANTIES);
                DressUpPart(girl, ClothingType.SOCKS);
                DressUpPart(girl, ClothingType.SHOES);
            }
        }

        /// <summary>
        /// Dresses up the girls "topclothing"
        /// </summary>
        /// <returns>
        /// true if the girl wears a onepiece (or is naked)
        /// and false if she wears a top
        /// </returns>
        private bool DressUpTopOrOnepiece(Girl girl)
        {
            Random rnd = new Random();

            List<long> pool;
            Clothing cloth;
            int index;
            int counter;

            List<ClothPrefItem> items = ClothPrefItem.GetItemsByType(girl.ClothPref, ClothingType.TOP);
            items.AddRange(ClothPrefItem.GetItemsByType(girl.ClothPref, ClothingType.ONEPIECE));

            //If theres no Preference
            if (items.Count == 0)
            {
                //Add a random fitting clothing
                //Get all available & fitting clothes
                pool = ActorUtil.GetClothesByTypeAndRoom(Clothes, ClothingType.TOP, 0);
                pool.AddRange(ActorUtil.GetClothesByTypeAndRoom(Clothes, ClothingType.ONEPIECE, 0));
                //Add the nude Element
                pool.Add(0);
                //Put one on
                index = rnd.Next(pool.Count);
                cloth = ActorUtil.GetClothingById(Clothes, pool[index]);
                cloth.CurrentRoom = (girl.Id + 1) * -1;
                girl.WornClothes[(int)cloth.Type - 1] = cloth.Id;
                if (cloth.Id == 0 || cloth.Type == ClothingType.ONEPIECE)
                {
                    return true;
                }

                return false;
            }
            else
            {
                //Pick one by random
                counter = rnd.Next(20) + 1;
                //Iterate over all Preferences
                foreach (ClothPrefItem item in items)
                {
                    counter -= item.Chance;
                    if (counter <= 0)
                    {
                        //Put Clothing on
                        cloth = ActorUtil.GetClothingById(Clothes, item.Id);
                        cloth.CurrentRoom = (girl.Id + 1) * -1;
                        girl.WornClothes[(int)cloth.Type - 1] = cloth.Id;
                        //Exit this method
                        if (cloth.Id == 0 || cloth.Type == ClothingType.ONEPIECE)
                        {
                            return true;
                        }

                        return false;
                    }
                }
                //If no one was selected

                //Add a random fitting clothing
                //Get all available & fitting clothes
                pool = ActorUtil.GetClothesByTypeAndRoom(Clothes, ClothingType.TOP, 0);
                pool.AddRange(ActorUtil.GetClothesByTypeAndRoom(Clothes, ClothingType.ONEPIECE, 0));
                //Add the nude Element
                pool.Add(0);
                //Put one on
                index = rnd.Next(pool.Count);
                cloth = ActorUtil.GetClothingById(Clothes, pool[index]);
                cloth.CurrentRoom = (girl.Id + 1) * -1;
                girl.WornClothes[(int)cloth.Type - 1] = pool[index];
                if (cloth.Id == 0 || cloth.Type == ClothingType.ONEPIECE)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Adds one piece of fitting clothing (or not)
        /// </summary>
        private void DressUpPart(Girl girl, ClothingType type)
        {
            Random rnd = new Random();

            List<ClothPrefItem> items = ClothPrefItem.GetItemsByType(girl.ClothPref, type);
            List<long> pool;
            Clothing cloth;
            int index;
            int counter;

            //If theres no Preference
            if (items.Count == 0)
            {
                //Add a random fitting clothing
                //Get all available & fitting clothes
                pool = ActorUtil.GetClothesByTypeAndRoom(Clothes, type, 0);
                //Add the nude Element
                pool.Add(0);
                //Put one on
                index = rnd.Next(pool.Count);
                cloth = ActorUtil.GetClothingById(Clothes, pool[index]);
                cloth.CurrentRoom = (girl.Id + 1) * -1;
                girl.WornClothes[(int)cloth.Type - 1] = pool[index];
            }
            else
            {
                //Pick one by random
                counter = rnd.Next(10) + 1;
                //Iterate over all Preferences
                foreach (ClothPrefItem item in items)
                {
                    counter -= item.Chance;
                    if (counter <= 0)
                    {
                        //Put Clothing on
                        cloth = ActorUtil.GetClothingById(Clothes, item.Id);
                        cloth.CurrentRoom = (girl.Id + 1) * -1;
                        girl.WornClothes[(int)type - 1] = cloth.Id;
                        //Exit this method
                        return;
                    }
                }
                //If none was selected,
                //put on a random clothing

                //Get all available & fitting clothes
                pool = ActorUtil.GetClothesByTypeAndRoom(Clothes, type, 0);
                //Add the nude Element
                pool.Add(0);
                //Put one on
                index = rnd.Next(pool.Count);
                ActorUtil.GetClothingById(Clothes, pool[index]).CurrentRoom = (girl.Id + 1) * -1;
                girl.WornClothes[(int)type - 1] = pool[index];
            }
        }

        /// <summary>
        /// Places some clothes in the assigned rooms
        /// </summary>
        private void ScatterClothes()
        {
            Random rnd = new Random();
            Clothing cloth;
            int counter;
            int breakCounter;

            foreach (Room r in Rooms)
            {

                if (r.ScatteredTypes.Count > 0)
                {
                    counter = 0;
                    breakCounter = 0;
                    int limit = rnd.Next(r.ClothCount + 1);

                    while (counter < limit - 1 && breakCounter < 200)
                    {
                        cloth = Clothes[rnd.Next(Clothes.Count)];
                        if (cloth.CurrentRoom == 0 && r.ScatteredTypes.Contains(cloth.Type))
                        {
                            cloth.CurrentRoom = r.Id;
                            counter++;
                        }

                        breakCounter++;
                    }
                }
            }
        }

        /// <summary>
        /// Dumps the leftover clothes in the first dump it finds
        /// </summary>
        private void DumpClothes()
        {
            foreach (Room r in Rooms)
            {
                if (r.ClothingDump)
                {
                    foreach (Clothing cloth in Clothes)
                    {
                        if (cloth.CurrentRoom == 0)
                        {
                            cloth.CurrentRoom = r.Id;
                        }
                    }
                }
            }
        }

        #endregion

        #region General ActionMethods

        /// <summary>
        /// Prints the available options of the protagonist
        /// for the player to choose from
        /// </summary>
        public void PrintAvailableActions()
        {
            string output = "";

            for (int i = 0; i < PossibleActions.Count; i++)
            {
                output += String.Format("[{0}] {1}\t", i, PossibleActions[i].DisplayText);
            }

            Display.AppendText(Environment.NewLine + output + Environment.NewLine);
        }

        #endregion

        #region DefaultActions

        /// <summary>
        /// Generally informs the player about the room
        /// and the actors after taking an action
        /// </summary>
        public void PrintDefaultStatus()
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

            //Are there girls here?
            List<Girl> localGirls = ActorUtil.GetGirlsByRoom(Girls, Protagonist.CurrentRoom);

            foreach (Girl g in localGirls)
            {
                output += Environment.NewLine + String.Format("{0} is here", g.Name);
            }

            //Some clothes Lieing around?
            if (ActorUtil.GetClothesByRoom(Clothes, Protagonist.CurrentRoom).Count > 0)
            {
                output += Environment.NewLine + "There are some clothes scattered around the room.";
            }

            Display.AppendText(Environment.NewLine + output + Environment.NewLine);
        }

        /// <summary>
        /// Refills the ActionList with all defaultActions
        /// DefaultActions are the things you can do while walking around the
        /// location like picking stuff up.
        /// </summary>
        public void EvaluateDefaultActions()
        {
            //Clear the ActionList
            PossibleActions.Clear();

            AddDefaultActions();
            AddOpenClothingInventoryAction();
            AddMoveActions();
            AddImplementActions();
            AddDefaultGirlActions();
            AddSelectGirlAction();
            AddClothingPickUpActions();
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
            PossibleActions.Add(new Action("Think about yourself", ActionType.THINK_ABOUT_YOURSELF, StringHelper.UnbreakLines(Protagonist.Lore)));
        }

        /// <summary>
        /// Lets you open your ClothingInventory
        /// </summary>
        private void AddOpenClothingInventoryAction()
        {
            PossibleActions.Add(new Action("Look at carried Clothes", ActionType.LOOK_AT_CARRIED_CLOTHING, ""));
        }

        /// <summary>
        /// Lets you select a girl
        /// </summary>
        private void AddSelectGirlAction()
        {
            Action action;

            foreach (Girl girl in Girls)
            {
                if (girl.CurrentRoom == Protagonist.CurrentRoom)
                {
                    action = new Action(String.Format("Tell {0} to...", girl.Name), ActionType.SELECT_GIRL);
                    action.Params.Add(girl.Id);
                    action.ActionText = String.Format("I talk to {0}", girl.Name);
                    PossibleActions.Add(action);
                }
            }
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
                action = new Action(String.Format("Move to {0}", room.Name), ActionType.MOVE_TO_ROOM);
                action.Params.Add(room.Id);
                action.ActionText = String.Format("I go to {0}", room.Name);
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
        /// Adds the Implement PickUp/Drop Actions
        /// </summary>
        private void AddClothingPickUpActions()
        {
            Action action;

            //Pick up Actions
            List<Clothing> locCloth = ActorUtil.GetClothesByRoom(Clothes, Protagonist.CurrentRoom);
            foreach (Clothing cloth in locCloth)
            {
                action = new Action(String.Format("Pick up the {0}", cloth.Description), ActionType.PICK_UP_CLOTHING, String.Format("I pick up the {0}", cloth.Description));
                action.Params.Add(cloth.Id);
                PossibleActions.Add(action);
            }
        }

        /// <summary>
        /// Adds the default Girl interactions
        /// </summary>
        private void AddDefaultGirlActions()
        {
            Action action;

            List<Girl> localGirls = ActorUtil.GetGirlsByRoom(Girls, Protagonist.CurrentRoom);

            foreach(Girl g in localGirls){

                action = new Action(String.Format("Look at {0}", g.Name), ActionType.LOOK_AT_GIRL);
                action.Params.Add(g.Id);
                PossibleActions.Add(action);

                action = new Action(String.Format("Think about {0}", g.Name), ActionType.THINK_ABOUT_GIRL);
                action.Params.Add(g.Id);
                PossibleActions.Add(action);
            }
        }

        #endregion

        #region ClothingInventoryActions

        /// <summary>
        /// Informs the Player about the carried Clothes
        /// </summary>
        public void PrintClothingIventoryStatus()
        {
            string output = "";

            //Are there any clothes?
            List<Clothing> carriedClothes = ActorUtil.GetClothesByRoom(Clothes, -1);
            if (carriedClothes.Count > 0)
            {
                string clothingStatus = "";

                if (carriedClothes.Count == 1)
                {
                    clothingStatus = String.Format("I'm currently holding {0}.", carriedClothes[0].GetInventroyDescription());
                }
                else if (carriedClothes.Count == 2)
                {
                    clothingStatus = String.Format("I'm currently holding {0} and {1}.", carriedClothes[0].GetInventroyDescription(), carriedClothes[1].GetInventroyDescription());
                }
                else
                {

                    clothingStatus = "I'm currently holding ";
                    for (int i = 0; i < carriedClothes.Count - 1; i++)
                    {
                        if (i == carriedClothes.Count - 2)
                        {
                            clothingStatus += String.Format("{0} and {1}.", carriedClothes[i].GetInventroyDescription(), carriedClothes[i + 1].GetInventroyDescription());
                        }
                        else
                        {
                            clothingStatus += String.Format("{0}, ", carriedClothes[i].GetInventroyDescription());
                        }
                    }
                }

                output += Environment.NewLine + clothingStatus;
            }
            else
            {
                output = "I'm not holding any clothes.";
            }

            Display.AppendText(Environment.NewLine + output + Environment.NewLine);
        }

        /// <summary>
        /// Refills the ActionList with all defaultActions
        /// Adds the stuff you can do with your carried clothes
        /// </summary>
        public void EvaluateClothingInventoryActions()
        {
            //Clear the ActionList
            PossibleActions.Clear();

            AddDropActions();
            AddReturnAction();
        }

        /// <summary>
        /// Adds all the dropActions for the carried clothes.
        /// </summary>
        private void AddDropActions()
        {
            Action action;

            List<Clothing> carriedClothes = ActorUtil.GetClothesByRoom(Clothes, -1);
            foreach (Clothing cloth in carriedClothes)
            {
                action = new Action(String.Format("Drop the {0}", cloth.Description), ActionType.DROP_CARRIED_CLOTHING);
                action.Params.Add(cloth.Id);
                action.ActionText = String.Format(String.Format("I put down the {0}", cloth.Description));
                PossibleActions.Add(action);
            }
        }

        /// <summary>
        /// Adds the action to return to the default view.
        /// </summary>
        private void AddReturnAction()
        {
            Action action = new Action("Go back", ActionType.RETURN_TO_DEFAULT, "");
            PossibleActions.Add(action);
        }

        #endregion

        #region GirlActions

        /// <summary>
        /// Informs about the girl hes talking to
        /// </summary>
        public void PrintSelectedGirlStatus(long currentGirl)
        {
            string output = "";

            Girl g = ActorUtil.GetGirlById(Girls, currentGirl);

            output += g.GetClothesDescription(Clothes) + Environment.NewLine;
            output += "Tell her to..." + Environment.NewLine;

            Display.AppendText(Environment.NewLine + output);
        }

        /// <summary>
        /// Refills the ActionList with all interactions
        /// </summary>
        public void EvaluateGirlActions(long currentGirl)
        {
            //Clear the ActionList
            PossibleActions.Clear();

            AddStripActions(currentGirl);
            AddWearActions(currentGirl);
            AddSendToRoomAction(currentGirl);

            AddReturnAction();
        }

        /// <summary>
        /// Adds the actions to strip the selected girl
        /// </summary>
        private void AddStripActions(long currentGirl)
        {
            Action action;

            Girl g = ActorUtil.GetGirlById(Girls, currentGirl);

            //Take everything off at once
            action = new Action("... strip", ActionType.STRIP);
            action.Params.Add(g.Id);
            PossibleActions.Add(action);

            //Individual actions
            for (int i = 0; i < g.WornClothes.Length; i++)
            {
                if (g.WornClothes[i] != 0)
                {
                    action = new Action(String.Format("... take off her {0}", Clothing.GetReadableTypeName((ClothingType)(i + 1))),ActionType.TAKE_OFF);
                    action.Params.Add(currentGirl);
                    action.Params.Add(g.WornClothes[i]);
                    PossibleActions.Add(action);
                }
            }
        }

        /// <summary>
        /// Adds the option to dress up the girl
        /// </summary>
        private void AddWearActions(long currentGirl)
        {
            Action action;

            List<Clothing> carriedClothes = ActorUtil.GetClothesByRoom(Clothes, -1);
            foreach (Clothing cloth in carriedClothes)
            {
                action = new Action(String.Format("... to wear the {0}", cloth.Description), ActionType.WEAR);
                action.Params.Add(currentGirl);
                action.Params.Add(cloth.Id);
                PossibleActions.Add(action);
            }
        }

        /// <summary>
        /// Adds the option to dress up the girl
        /// </summary>
        private void AddSendToRoomAction(long currentGirl)
        {
            Action action;

            foreach (Room r in Rooms)
            {
                if (r.Id != Protagonist.CurrentRoom)
                {
                    action = new Action(String.Format("... to go to {0}", r.Name), ActionType.SEND_TO_ROOM);
                    action.Params.Add(currentGirl);
                    action.Params.Add(r.Id);
                    PossibleActions.Add(action);
                }
            }
        }

        #endregion

        #region ActionHandling

        /// <summary>
        /// Reacts to a players index selection
        /// </summary>
        public void HandleSelection(int input)
        {
            //Sort out illegal inputs
            if (input < 0 || input >= PossibleActions.Count)
            {
                throw new Exception();
            }

            Action selectedAction = PossibleActions[input];

            switch (selectedAction.Type)
            {
                //Default Actions
                case ActionType.LOOK_AROUND_ROOM:
                    ReactLookAround(selectedAction);
                    break;
                case ActionType.THINK_ABOUT_YOURSELF:
                    ReactThinkAboutYourself(selectedAction);
                    break;
                case ActionType.MOVE_TO_ROOM:
                    ReactMoveToRoom(selectedAction);
                    break;
                case ActionType.PICK_UP_IMPLEMENT:
                    ReactPickUpImplement(selectedAction);
                    break;
                case ActionType.DROP_IMPLEMENT:
                    ReactDropImplement(selectedAction);
                    break;
                case ActionType.PICK_UP_CLOTHING:
                    ReactPickUpClothing(selectedAction);
                    break;

                //ClothingInventory Actions
                case ActionType.LOOK_AT_CARRIED_CLOTHING:
                    ReactOpenClothingInventory(selectedAction);
                    break;
                case ActionType.DROP_CARRIED_CLOTHING:
                    ReactDropClothing(selectedAction);
                    break;
                case ActionType.RETURN_TO_DEFAULT:
                    ReactReturnToDefault();
                    break;

                //Girl Actions
                case ActionType.LOOK_AT_GIRL:
                    ReactLookAtGirl(selectedAction);
                    break;
                case ActionType.THINK_ABOUT_GIRL:
                    ReactThinkAboutGirl(selectedAction);
                    break;
                case ActionType.SELECT_GIRL:
                    ReactSelectGirl(selectedAction);
                    break;
                case ActionType.TAKE_OFF:
                    ReactToTakeOff(selectedAction);
                    break;
                case ActionType.STRIP:
                    ReactStrip(selectedAction);
                    break;
                case ActionType.WEAR:
                    ReactWear(selectedAction);
                    break;
                case ActionType.SEND_TO_ROOM:
                    ReactSendToRoom(selectedAction);
                    break;

                //Break
                default: break;
            }

            PrintAvailableActions();
        }

        #region DefaultActions

        /// <summary>
        /// Finishes a DefaultAction
        /// </summary>
        private void FinishDefaultAction()
        {
            PrintDefaultStatus();
            EvaluateDefaultActions();
        }

        /// <summary>
        /// Reaction to the LookAround Action
        /// </summary>
        private void ReactLookAround(Action action)
        {
            string output = Environment.NewLine;
            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the ThinkAboutYourself Action
        /// </summary>
        private void ReactThinkAboutYourself(Action action)
        {
            string output = Environment.NewLine;
            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the MoveToRoom Action
        /// </summary>
        private void ReactMoveToRoom(Action action)
        {
            string output = Environment.NewLine;
            Protagonist.CurrentRoom = (long)action.Params[0];
            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the PickUpImplement Action
        /// </summary>
        private void ReactPickUpImplement(Action action)
        {
            string output = Environment.NewLine;
            Implement newImplement = ActorUtil.GetImplementById(Implements, (long)action.Params[0]);

            //If the protagonist already holds an implement
            if (Protagonist.VariableImplement != -1)
            {
                //Drop the current Implement
                ActorUtil.GetImplementById(Implements, Protagonist.VariableImplement).CurrentRoom = Protagonist.CurrentRoom;
                Protagonist.VariableImplement = -1;
            }
            Protagonist.VariableImplement = newImplement.Id;
            newImplement.CurrentRoom = 0;

            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the DropImplement Action
        /// </summary>
        private void ReactDropImplement(Action action)
        {
            string output = Environment.NewLine;

            ActorUtil.GetImplementById(Implements, Protagonist.VariableImplement).CurrentRoom = Protagonist.CurrentRoom;
            Protagonist.VariableImplement = -1;

            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the PickUpClothing Action
        /// </summary>
        private void ReactPickUpClothing(Action action)
        {
            string output = Environment.NewLine;
            Clothing pickUp = ActorUtil.GetClothingById(Clothes, (long)action.Params[0]);

            // A value of -1 "means in the protagonist's pocket"
            pickUp.CurrentRoom = -1;

            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the LookAtGirl Action
        /// </summary>
        private void ReactLookAtGirl(Action action)
        {
            string output = Environment.NewLine;
            Girl girl = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);

            output += girl.Description + Environment.NewLine;
            output += girl.GetClothesDescription(Clothes) + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Reaction to the ThinkAboutGirl Action
        /// </summary>
        private void ReactThinkAboutGirl(Action action)
        {
            string output = Environment.NewLine;
            Girl girl = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);

            output += girl.Lore + Environment.NewLine;
            Display.AppendText(output);

            FinishDefaultAction();
        }

        /// <summary>
        /// Selects a girl
        /// </summary>
        private void ReactSelectGirl(Action action)
        {
            FinishGirlAction((long)action.Params[0]);
        }

        #endregion

        #region ClothingInventoryActions

        /// <summary>
        /// Finishes an CLothingInventroyAction
        /// </summary>
        private void FinishClothingInventoryAction()
        {
            PrintClothingIventoryStatus();
            EvaluateClothingInventoryActions();
        }

        /// <summary>
        /// Opens the ClothingInventory
        /// </summary>
        private void ReactOpenClothingInventory(Action action)
        {
            FinishClothingInventoryAction();
        }

        /// <summary>
        /// Drops a clothing in the current room
        /// </summary>
        private void ReactDropClothing(Action action)
        {
            string output = Environment.NewLine;

            ActorUtil.GetClothingById(Clothes, (long)action.Params[0]).CurrentRoom = Protagonist.CurrentRoom;

            output += action.ActionText + Environment.NewLine;
            Display.AppendText(output);

            FinishClothingInventoryAction();
        }

        /// <summary>
        /// Goes back to the default actions
        /// </summary>
        private void ReactReturnToDefault()
        {
            FinishDefaultAction();
        }

        #endregion

        #region GirlActions

        /// <summary>
        /// Finishes a GirlAction
        /// </summary>
        private void FinishGirlAction(long currentGirl)
        {
            PrintSelectedGirlStatus(currentGirl);
            EvaluateGirlActions(currentGirl);
        }

        /// <summary>
        /// Takes off the selected clothing
        /// </summary>
        private void ReactToTakeOff(Action action)
        {
            string output;

            Girl g = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);
            Clothing cloth = ActorUtil.GetClothingById(Clothes, (long)action.Params[1]);

            g.WornClothes[(int)cloth.Type - 1] = 0;
            cloth.CurrentRoom = -1;

            if (cloth.Article)
            {
                output = String.Format("{0} takes off her {1} and hands them to me.", g.Name, cloth.Description);
            }
            else
            {
                output = String.Format("{0} takes off her {1} and hands it to me.", g.Name, cloth.Description);
            }
            
            Display.AppendText(Environment.NewLine + output + Environment.NewLine);

            FinishGirlAction(g.Id);
        }

        /// <summary>
        /// Strips the selected girl naked
        /// </summary>
        private void ReactStrip(Action action)
        {
            string output;

            Girl g = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);

            for (int i = 0; i < g.WornClothes.Length; i++)
            {
                if (g.WornClothes[i] != 0)
                {
                    Clothing cloth = ActorUtil.GetClothingById(Clothes, g.WornClothes[i]);

                    g.WornClothes[i] = 0;
                    cloth.CurrentRoom = -1;
                }
            }
            
            output = String.Format("{0} takes off all her clothes and hands them to me.", g.Name);
            Display.AppendText(Environment.NewLine + output + Environment.NewLine);

            FinishGirlAction(g.Id);
        }

        /// <summary>
        /// Dresses up the girl
        /// </summary>
        private void ReactWear(Action action)
        {
            string output;
            Clothing removedPiece;

            Girl g = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);
            Clothing cloth = ActorUtil.GetClothingById(Clothes, (long)action.Params[1]);

            if (cloth.Type == ClothingType.ONEPIECE)
            {
                //Remove shirt
                if (g.WornClothes[(int)ClothingType.TOP - 1] != 0)
                {
                    ActorUtil.GetClothingById(Clothes, g.WornClothes[0]).CurrentRoom = -1;
                    g.WornClothes[0] = 0;
                }
                //Remove pants
                if (g.WornClothes[(int)ClothingType.BOTTOM - 1] != 0)
                {
                    ActorUtil.GetClothingById(Clothes, g.WornClothes[1]).CurrentRoom = -1;
                    g.WornClothes[1] = 0;
                }
                //Remove Onepiece
                if (g.WornClothes[(int)ClothingType.ONEPIECE - 1] != 0)
                {
                    ActorUtil.GetClothingById(Clothes, g.WornClothes[2]).CurrentRoom = -1;
                    g.WornClothes[2] = 0;
                }

                cloth.CurrentRoom = (g.Id + 1) * -1;
                g.WornClothes[2] = cloth.Id;
                output = string.Format("{0} takes off her other clothes and puts on the {1}", g.Name, cloth.Description);
                Display.AppendText(Environment.NewLine + output + Environment.NewLine);

            } else if ((cloth.Type == ClothingType.TOP || cloth.Type == ClothingType.BOTTOM) && g.WornClothes[(int)ClothingType.ONEPIECE - 1] != 0){
                
                //Remove Onepiece
                removedPiece = ActorUtil.GetClothingById(Clothes, g.WornClothes[2]);
                removedPiece.CurrentRoom = -1;
                g.WornClothes[2] = 0;
                cloth.CurrentRoom = (g.Id + 1) * -1;
                output = string.Format("{0} takes off her {1} and puts on the {2}", g.Name, removedPiece.Description, cloth.Description);
                Display.AppendText(Environment.NewLine + output + Environment.NewLine);
            }
            else
            {
                if (g.WornClothes[(int)cloth.Type - 1] != 0)
                {
                    removedPiece = ActorUtil.GetClothingById(Clothes, g.WornClothes[(int)cloth.Type - 1]);
                    removedPiece.CurrentRoom = -1;
                    g.WornClothes[(int)cloth.Type - 1] = cloth.Id;
                    cloth.CurrentRoom = (g.Id + 1) * -1;
                    output = string.Format("{0} takes off her {1} and puts on the {2}", g.Name, removedPiece.Description, cloth.Description);
                    Display.AppendText(Environment.NewLine + output + Environment.NewLine);
                }

                g.WornClothes[(int)cloth.Type - 1] = cloth.Id;
                cloth.CurrentRoom = (g.Id + 1) * -1;
                output = string.Format("{0} puts on the {1}", g.Name, cloth.Description);
                Display.AppendText(Environment.NewLine + output + Environment.NewLine);
            }

            FinishGirlAction(g.Id);
        }

        /// <summary>
        /// Sends the selected girl to a room
        /// </summary>
        private void ReactSendToRoom(Action action)
        {
            string output;

            Girl g = ActorUtil.GetGirlById(Girls, (long)action.Params[0]);

            g.CurrentRoom = (long)action.Params[1];

            output = String.Format("{0} leaves the room.", g.Name);
            Display.AppendText(Environment.NewLine + output + Environment.NewLine);

            FinishDefaultAction();
        }

        #endregion

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

        /// <summary>
        /// Prints all the clothes in the Location (dor Testing)
        /// </summary>
        public void PrintClothes()
        {
            foreach (Clothing c in Clothes)
            {
                Display.AppendText(Environment.NewLine + StringHelper.UnbreakLines(c.ToString()) + Environment.NewLine);
            }
        }

        #endregion
    }
    
}
