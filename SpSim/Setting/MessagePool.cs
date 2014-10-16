using SpSim.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    public class MessagePool
    {
        //Dictionary containing the Messages
        private Dictionary<MessageType, List<Message>> Pool = new Dictionary<MessageType, List<Message>>();

        //Initializes all the MessageLists
        public MessagePool()
        {
            foreach (MessageType type in Enum.GetValues(typeof(MessageType)))
            {
                Pool.Add(type, new List<Message>());
            }
        }

        /// <summary>
        /// Adds a Message to the Pool
        /// </summary>
        public void AddMessage(Message message)
        {
            Pool[message.Type].Add(message);
        }

        /// <summary>
        /// Adds some Messages to the Pool
        /// </summary>
        public void AddMessages(List<Message> messages)
        {
            if (messages.Count > 0)
            {
                Pool[messages[0].Type].AddRange(messages);
            }
        }

        /// <summary>
        /// Gets a message by MessageType.
        /// For the needed parameter, look at the MessageType enum
        /// </summary>
        /// <param name="type"></param>
        public string GetMessage(MessageType type, List<Object> neededParams)
        {
            bool[] filter = GetFilterByMessageType(type);
            string output = "";
            int counter = 0;

            //get Possible Messages
            List<Message> possibleMessages = new List<Message>();
            foreach (Message mess in Pool[type])
            {
                possibleMessages.Add(new Message(mess));
            }

            //Filter
            List<string> possibleValues;
            string girlValue;

            for (int h = 0; h < filter.Length; h++)
            {
                if (filter[h])
                {

                    for (int i = possibleMessages.Count - 1; i > -1; i--)
                    {
                        possibleValues = ((string)possibleMessages[i].Params[counter]).Split(',').ToList<string>();
                        for (int j = 0; j < possibleValues.Count; j++)
                        {
                            possibleValues[j] = possibleValues[j].Trim();
                        }
                        girlValue = ((int)neededParams[counter] + "").Trim();

                        if (!possibleValues.Contains(girlValue))
                        {
                            possibleMessages.RemoveAt(i);
                            continue;
                        }
                    }

                    counter++;
                }
            }

            //Id filter
            for (int i = possibleMessages.Count - 1; i > -1; i--)
            {
                long id = Convert.ToInt64(possibleMessages[i].Params[possibleMessages[i].Params.Count -1]);
                long girlId = (int)neededParams[neededParams.Count -1];

                if ((id != 0) && (id != girlId))
                {
                    possibleMessages.RemoveAt(i);
                    continue;
                }
            }

            //Select one
            if (possibleMessages.Count > 0)
            {
                string messageString = possibleMessages[RandomNumber.Between(0, possibleMessages.Count - 1)].MessageText;

                return messageString;
            }

            return "No siutable Message found, you need to add some!";
        }

        /// <summary>
        /// Gets the filter for the Messagetype. Values are:
        /// reqlike | reqpain | reqrepnt | reqpos | reqby
        /// </summary>
        public bool[] GetFilterByMessageType(MessageType type)
        {
            switch(type){
                case MessageType.LOOK_AT_GIRL: return new bool[] { true, true, true, false, false };
                case MessageType.PAIN_DOWN: return new bool[] { true, true, true, false, false };
                case MessageType.PICKUP_IMPL: return new bool[] { true, true, true, false, false };
                case MessageType.ENTER_IMPL: return new bool[] { true, true, true, false, false };
                case MessageType.SWAP_IMPL: return new bool[] { true, true, true, false, false };
                case MessageType.SWAP_WORSE_IMPL: return new bool[] { true, true, true, false, false };
                case MessageType.DROP_IMPL: return new bool[] { true, true, true, false, false };
                case MessageType.HOLDING_ANNOUNCE: return new bool[] { false, false, false, false, false };
                case MessageType.HOLDING_REACT_ANNOUNCE: return new bool[] { true, true, true, false, false };
                case MessageType.HOLDING_WATCH_ANNOUNCE: return new bool[] { true, true, true, false, false };
                case MessageType.HOLDING_START: return new bool[] { false, false, false, false, true };
                case MessageType.HOLDING_START_REACT: return new bool[] { true, true, true, false, true };
                case MessageType.HOLDING_START_WATCH: return new bool[] { true, true, true, false, true };
                case MessageType.DRAG: return new bool[] { false, false, false, false, true };
                case MessageType.DRAG_REACT: return new bool[] { true, true, true, false, true };
                case MessageType.DRAG_WATCH: return new bool[] { true, true, true, false, true };
                case MessageType.HOLDING_STOP: return new bool[] { false, false, false, false, true };
                case MessageType.HOLDING_STOP_REACT: return new bool[] { true, true, true, false, true };
                case MessageType.HOLDING_STOP_WITHOUT_DRAG: return new bool[] { true, true, true, false, true };
                default : return new bool[4];
            }
        }

    }
}
