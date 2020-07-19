using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Grafica.UserController
{
    class UPlanner
    {
        UExecutor uExecutor;
        public UPlanner()
        {
            uExecutor = new UExecutor();
        }

        public void changeState(bool[] stateKey, Tank tank)
        {
            if (stateKey[0])
                tank.movement.forward = true;
            else
                tank.movement.forward = false;
            if (stateKey[1])
                tank.movement.backward = true;
            else
                tank.movement.backward = false;
            if (stateKey[2])
            {
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

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            uExecutor.setWalls(vertical, horizontal);
        }

        public void run(Tank firstTank, Tank secondTank)
        {
            uExecutor.run(firstTank, secondTank);//tank1 and tank2
        }

        public void dispose()
        {
            uExecutor.running = false;
        }

        /*public void addBullet(Hashtable objects, Tank tank)
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
                    bullet.center = tank.center + new Vector3(tank.movement.radius, 0.0f, 0.0f);
                    bullet.translation = tank.translation + new Vector3(tank.movement.radius, 0.0f, 0.0f);
                    break;
            }
            bullet.movement.direction = tank.movement.direction;
            bulletCount++;
            bullet.key = "bullet" + bulletCount;
            bullet.parentKey = tank.key;
            objects.Add(bullet.key, bullet);
        }*/
    }
}
