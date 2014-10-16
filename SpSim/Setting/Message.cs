using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    public class Message
    {
        /// <summary>
        /// What will be told
        /// </summary>
        public string MessageText = "";

        /// <summary>
        /// What kind off message is this?
        /// </summary>
        public MessageType Type;

        /// <summary>
        /// Additional Parameters like Pain or Affcetion needed
        /// to show this Message
        /// </summary>
        public List<Object> Params = new List<Object>();

        public Message(string text, MessageType type)
        {
            MessageText = text;
            Type = type;
        }

        public Message(Message mess)
        {
            MessageText = mess.MessageText;
            Type = mess.Type;
            Params = mess.Params;
        }
    }

    /// <summary>
    /// Defines when the message is shown and how its handeled
    /// Parameters by Type:
    /// Default:                    -/-
    /// LookAtGirl:                 int reqAffection, int reqPain, int reqrepnt
    /// PainDown:                   int reqAffection, int reqPain, int reqrepnt
    /// Pick up Impl:               int reqAffection, int reqPain, int reqrepnt
    /// Enter with Impl:            int reqAffection, int reqPain, int reqrepnt
    /// Swap Impl:                  int reqAffection, int reqPain, int reqrepnt
    /// Swap worse Impl:            int reqAffection, int reqPain, int reqrepnt
    /// Drop Impl:                  int reqAffection, int reqPain, int reqrepnt
    /// Holding announce:           int reqAffection, int reqPain, int reqrepnt
    /// Holding react announce:     int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Holding watch announce:     int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Holding start:              int reqby
    /// Holding react start:        int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Holding watch start:        int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Drag:                       int reqby
    /// Drag react:                 int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Drag watch:                 int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Holding stop:               int reqby
    /// Holding stop react:         int reqAffection, int reqPain, int reqrepnt, int reqby
    /// Holding stop without drag:  int reqAffection, int reqPain, int reqrepnt, int reqby
    /// </summary>
    public enum MessageType
    {
        DEFAULT, LOOK_AT_GIRL, PAIN_DOWN,
        PICKUP_IMPL, ENTER_IMPL, SWAP_IMPL, SWAP_WORSE_IMPL, DROP_IMPL,
        HOLDING_ANNOUNCE, HOLDING_REACT_ANNOUNCE, HOLDING_WATCH_ANNOUNCE, HOLDING_START, HOLDING_START_REACT, HOLDING_START_WATCH,
        DRAG, DRAG_REACT, DRAG_WATCH, HOLDING_STOP, HOLDING_STOP_REACT, HOLDING_STOP_WITHOUT_DRAG
    }
}
