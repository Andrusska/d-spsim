using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpSim.Setting
{
    /// <summary>
    /// The ways of holding a girl
    /// </summary>
    public class Holding
    {
        public long Id;

        public string Name;

        /// <summary>
        /// List of parts of clothing of which one is needed
        /// to hold the girl
        /// </summary>
        public List<string> RequieredClothing = new List<string>();
    }
}
