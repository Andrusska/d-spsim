using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// Represents an action, the protagonist is able to do
    /// </summary>
    public class Action
    {
        /// <summary>
        /// The text, that's displayed to describe this action
        /// </summary>
        public string DisplayText;

        /// <summary>
        /// Categorizes the Action and sets how the location
        /// handels the data
        /// </summary>
        public ActionType Type;

        /// <summary>
        /// Defines which kind of 
        /// </summary>
        public ActionFollowedStatus FollowedStatus = ActionFollowedStatus.DEFAULT;

        /// <summary>
        /// The parameters needed for this action,
        /// e.g.: What is picked up? Where does this lead?
        /// 
        /// List of Params by ActionType
        /// LookAround:                 -/-
        /// Think about yourself:       -/-
        /// Move to room x:             long RoomId
        /// Pick up Implement:          long ImplementId
        /// Drop Implement:             -/-
        /// Pick up Clothing:           long clothingId
        /// Look at carried Clothing:   -/-   
        /// Drop carried Clothing:       long clothingId
        /// Return to default:          -/-
        /// Look at girl:               long girlId
        /// Think about girl:           long girlId
        /// select girl:                long girlId
        /// Take off:                   long currentGirl, long clothingId
        /// Strip:                      long girlId
        /// Wear:                       long girlId, long clothingId
        /// Send to Room:               long girlId, long roomId
        /// </summary>
        public List<Object> Params = new List<Object>();

        /// <summary>
        /// The text thats displayed after you take this action
        /// </summary>
        public string ActionText;

        public Action(string displayText, ActionType type)
        {
            DisplayText = displayText;
            Type = type;
        }

        public Action(string displayText, ActionType type, string actionText)
        {
            DisplayText = displayText;
            Type = type;
            ActionText = actionText;
        }
    }

    /// <summary>
    /// Collection of the differen ActionTypes
    /// </summary>
    public enum ActionType
    {
        //RoomActions
        LOOK_AT_ACTOR, MOVE_TO_ROOM, LOOK_AROUND_ROOM, THINK_ABOUT_YOURSELF,
        //ImplementActions
        PICK_UP_IMPLEMENT, DROP_IMPLEMENT,
        //ClothingActions
        PICK_UP_CLOTHING, LOOK_AT_CARRIED_CLOTHING, DROP_CARRIED_CLOTHING, RETURN_TO_DEFAULT,
        //GirlActions
        LOOK_AT_GIRL, THINK_ABOUT_GIRL, SELECT_GIRL, TAKE_OFF, STRIP, WEAR, SEND_TO_ROOM
    }

    public enum ActionFollowedStatus
    {
        DEFAULT,            //Default-,Roomactions
        CLOTHING_IVENTORY   //ClothingInventoryActions
    }
}
