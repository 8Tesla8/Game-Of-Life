using System;
using System.Collections.Generic;

namespace Logic
{
    public class Glider
    {

        public (int row, int column)[] GetGliderSpaceshipCoord(int gridLength)
        {
           var spaceshipGliderCoord = new Func<int, (int row, int column)>[6];

            //upperLine
            spaceshipGliderCoord[0] = (int middle) => (middle - 1, middle - 1);    //upLeft
            spaceshipGliderCoord[1] = (int middle) => (middle - 1, middle);        //upMiddle

            //middle line 
            spaceshipGliderCoord[2] = (int middle) => (middle, middle + 1);        //middleRight
            spaceshipGliderCoord[3] = (int middle) => (middle, middle);            //middleMiddle

            //bottom line 
            spaceshipGliderCoord[4] = (int middle) => (middle + 1, middle - 1);    //bottomLeft
            spaceshipGliderCoord[5] = (int middle) => (middle + 1, middle);        //bottomMiddle


            var coord = new (int row, int column)[spaceshipGliderCoord.Length];

            var middle = gridLength / 2;

            for (int i = 0; i < spaceshipGliderCoord.Length; i++)
                coord[i] = spaceshipGliderCoord[i].Invoke(middle);

            return coord;
        }


        public (int row, int column)[] GetRandomGliderCoord(int gridLength)
        {

            var gliderCoord = new Func<int, (int row, int column)>[9];

            //upperLine
            gliderCoord[0] = (int middle) => (middle - 1, middle - 1);    //upLeft
            gliderCoord[1] = (int middle) => (middle - 1, middle);        //upMiddle
            gliderCoord[2] = (int middle) => (middle - 1, middle + 1);    //upRight

            //middle line 
            gliderCoord[3] = (int middle) => (middle, middle -1);         //middleLeft
            gliderCoord[4] = (int middle) => (middle, middle);            //middleMiddle
            gliderCoord[5] = (int middle) => (middle, middle + 1);        //middleRight

            //bottom line 
            gliderCoord[6] = (int middle) => (middle + 1, middle - 1);    //bottomLeft
            gliderCoord[7] = (int middle) => (middle + 1, middle);        //bottomMiddle
            gliderCoord[8] = (int middle) => (middle + 1, middle + 1);    //bottomRight


            var coord = new List<(int row, int column)>();

            var middle = gridLength / 2;

            var random = new Random();


            for (int i = 0; i < gliderCoord.Length; i++)
            {
                var val = random.Next(2);

                if ((CellState)val == CellState.Dead)
                {
                    coord.Add(gliderCoord[i].Invoke(middle));
                }
            }

            return coord.ToArray();
        }
    }
}
