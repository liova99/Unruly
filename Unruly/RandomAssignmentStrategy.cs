using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unruly
{
    public class RandomAssignmentStrategy : IAssignmentStragey
    {
        Random r = new Random();

        public AssignmentResult GetNextAssignment(CreateGame game)
        {
            if (!game.unassignedPositions.Any())
            {
                return null;
            }

            Tuple<int, int> selectedValue = game.unassignedPositions.ElementAt(r.Next(game.unassignedPositions.Count));

            // try with random color too
            Console.WriteLine("Random Num :" + (r.Next(100) <= 50 ? true : false));
            return new AssignmentResult()
            {
               
            i = selectedValue.Item1,
            j = selectedValue.Item2,
            color = r.Next(100) <= 50 ? true : false
            };
        }
    }
}
