using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica.MyGame
{
    class Movement
    {
        public enum Directions
        {
            PlusZ,
            MinusZ,
            PlusX,
            MinusX
        }

        public Directions direction;
        public bool forward;
        public bool right;
        public bool backward;
        public bool left;
        public float angle;
        public float radius;
        public float width;
        public Movement()
        {
            direction = Directions.PlusZ;
            forward = false;
            right = false;
            backward = false;
            left = false;
            angle = 90;
        }
    }
}
