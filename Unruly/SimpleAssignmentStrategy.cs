using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unruly
{
    class SimpleAssignmentStrategy : IAssignmentStragey
    {
        public AssignmentResult GetNextAssignment(CreateGame game)
        {
            Tuple<int, int> position = game.unassignedPositions.FirstOrDefault();
            if (position == null)
            {
                return null;
            }


            return new AssignmentResult() { i = position.Item1, j = position.Item2, color = true };

        }
    }
}
