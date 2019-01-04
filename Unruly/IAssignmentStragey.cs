using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unruly
{
    interface IAssignmentStragey
    {
        /// <summary>
        /// returns null if there is no assignment possible
        /// (if there isn't any null field)
        /// </summary>
        /// <returns></returns>

        AssignmentResult GetNextAssignment(CreateGame game);

    }
}
