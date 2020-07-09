using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica.Controllers
{
    class Planner
    {
        public Planner()
        {

        }

        public void changeState(bool[] stateKey, Movement movement)
        {
            if (stateKey[0])
            {
                movement.forward = true;
            }
            else
            {
                movement.forward = false;
            }
            if (stateKey[1])
            {
                movement.backward = true;
            }
            else
            {
                movement.backward = false;
            }
            if (stateKey[2])
            {
                //movement.right = true;
                switch (movement.direction)
                {
                    case Movement.Directions.PlusZ:
                        movement.direction = Movement.Directions.MinusX;
                        break;
                    case Movement.Directions.MinusX:
                        movement.direction = Movement.Directions.MinusZ;
                        break;
                    case Movement.Directions.MinusZ:
                        movement.direction = Movement.Directions.PlusX;
                        break;
                    case Movement.Directions.PlusX:
                        movement.direction = Movement.Directions.PlusZ;
                        break;
                }
            }
            else
            {
                movement.right = false;
            }
            if (stateKey[3])
            {
                //movement.left = true;
                switch (movement.direction)
                {
                    case Movement.Directions.PlusZ:
                        movement.direction = Movement.Directions.PlusX;
                        break;
                    case Movement.Directions.PlusX:
                        movement.direction = Movement.Directions.MinusZ;
                        break;
                    case Movement.Directions.MinusZ:
                        movement.direction = Movement.Directions.MinusX;
                        break;
                    case Movement.Directions.MinusX:
                        movement.direction = Movement.Directions.PlusZ;
                        break;
                }
            }
            else
            {
                movement.left = false;
            }
        }
    }
}
