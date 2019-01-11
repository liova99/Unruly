using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unruly
{
    class SmartAssignmentStrategy : IAssignmentStragey
    {
        public AssignmentResult GetNextAssignment(CreateGame game)
        {
            foreach (Tuple<int, int> position in game.unassignedPositions)
            {
                if (position.Item2 > 2)
                {
                    if (game._myArray[position.Item1, position.Item2] == null)
                    {
                        if (game._myArray[position.Item1, position.Item2 - 2] == game._myArray[position.Item1, position.Item2 - 1])
                        {
                           
                           // return new AssignmentResult() { i = position.Item1, j = position.Item2, color = game._myArray[position.Item1, position.Item2 - 1] == false };
                        }

                    }
                }

                if (position.Item1 > 2)
                {
                    if (game._myArray[position.Item1, position.Item2] == null)
                    {
                        if (game._myArray[position.Item1 - 2, position.Item2] == game._myArray[position.Item1 - 1, position.Item2])
                        {
                            return new AssignmentResult() { i = position.Item1, j = position.Item2, color = game._myArray[position.Item1 -1, position.Item2] == true ? false : true };
                            //return new AssignmentResult() { i = position.Item1, j = position.Item2, color = game._myArray[position.Item1 - 1, position.Item2] == false };
                        }

                    }
                }
            }

            Tuple<int, int> rposition = game.unassignedPositions.FirstOrDefault();
            if (rposition == null)
            {
                return null;
            }

            return new AssignmentResult() { i = rposition.Item1, j = rposition.Item2, color = true };

        }
    }
}
