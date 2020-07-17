using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections;

namespace Grafica.Controllers
{
    class Planner
    {
        public int bulletCount = 0;
        public Planner()
        {

        }

        public void changeState(bool[] stateKey, Tank tank)
        {
            if (stateKey[0])
            {
                tank.movement.forward = true;
            }
            else
            {
                tank.movement.forward = false;
            }
            if (stateKey[1])
            {
                tank.movement.backward = true;
            }
            else
            {
                tank.movement.backward = false;
            }
            if (stateKey[2])
            {
                //movement.right = true;
                switch (tank.movement.direction)
                {
                    case Movement.Directions.PlusZ:
                        tank.movement.direction = Movement.Directions.MinusX;
                        break;
                    case Movement.Directions.MinusX:
                        tank.movement.direction = Movement.Directions.MinusZ;
                        break;
                    case Movement.Directions.MinusZ:
                        tank.movement.direction = Movement.Directions.PlusX;
                        break;
                    case Movement.Directions.PlusX:
                        tank.movement.direction = Movement.Directions.PlusZ;
                        break;
                }
            }
            else
            {
                tank.movement.right = false;
            }
            if (stateKey[3])
            {
                //movement.left = true;
                switch (tank.movement.direction)
                {
                    case Movement.Directions.PlusZ:
                        tank.movement.direction = Movement.Directions.PlusX;
                        break;
                    case Movement.Directions.PlusX:
                        tank.movement.direction = Movement.Directions.MinusZ;
                        break;
                    case Movement.Directions.MinusZ:
                        tank.movement.direction = Movement.Directions.MinusX;
                        break;
                    case Movement.Directions.MinusX:
                        tank.movement.direction = Movement.Directions.PlusZ;
                        break;
                }
            }
            else
            {
                tank.movement.left = false;
            }
        }

        public void addBullet(Hashtable objects, Tank tank)
        {
            Bullet bullet = new Bullet();
            switch (tank.movement.direction)
            {
                case Movement.Directions.PlusZ:
                    bullet.center = tank.center + new Vector3(0.0f, 0.0f, tank.movement.radius);
                    bullet.translation = tank.translation + new Vector3(0.0f, 0.0f, tank.movement.radius);
                    break;
                case Movement.Directions.MinusX:
                    bullet.center = tank.center + new Vector3(-tank.movement.radius, 0.0f, 0.0f);
                    bullet.translation = tank.translation + new Vector3(-tank.movement.radius, 0.0f, 0.0f);
                    break;
                case Movement.Directions.MinusZ:
                    bullet.center = tank.center + new Vector3(0.0f, 0.0f, -tank.movement.radius);
                    bullet.translation = tank.translation + new Vector3(0.0f, 0.0f, -tank.movement.radius);
                    break;
                case Movement.Directions.PlusX:
                    bullet.center = tank.center + new Vector3(tank.movement.radius, 0.0f, 0.1f);
                    bullet.translation = tank.translation + new Vector3(tank.movement.radius, 0.0f, 0.1f);
                    break;
            }
            bullet.movement.direction = tank.movement.direction;
            bulletCount++;
            objects.Add("bullet" + bulletCount, bullet);
        }
    }
}
