using SpSim.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Util
{
    public class ActorUtil
    {

        public static Room GetRoomById(List<Room> rooms, long targetId){
            foreach(Room r in rooms){
                if(r.Id == targetId){
                    return r;
                }
            }

            return new Room();
        }

        public static Implement GetImplementById(List<Implement> implements, long targetId)
        {
            foreach (Implement i in implements)
            {
                if (i.Id == targetId)
                {
                    return i;
                }
            }

            return new Implement();
        }

        public static List<Implement> GetImplementsByRoom(List<Implement> implements, long roomId){
            List<Implement> output = new List<Implement>();

            foreach (Implement i in implements)
            {
                if (i.CurrentRoom == roomId)
                {
                    output.Add(i);
                }
            }

            return output;
        }

    }
}
