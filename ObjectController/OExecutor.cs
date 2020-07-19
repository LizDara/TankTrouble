using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Grafica.ObjectController
{
    class OExecutor
    {
        Bullet bullet;
        List<Vector2[]> listVertical;
        List<Vector2[]> listHorizontal;
        public bool running;
        float width = 0.4f;
        public OExecutor()
        {
            running = true;
        }

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            listVertical = vertical;
            listHorizontal = horizontal;
        }

        public void run(Bullet[] bullets, Tank firstTank, Tank secondTank)
        {
            Thread thread = new Thread(() =>
            {
                while (running)
                {
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i] != null)
                        {
                            bullet = bullets[i];
                            Movement movement = bullet.movement;
                            if (movement.forward)
                            {
                                List<Vector2[]> listCloserWall = new List<Vector2[]>();
                                bool move = true;
                                Tank tank;
                                if (bullet.parentKey.Equals("tank1"))
                                    tank = secondTank;
                                else
                                    tank = firstTank;
                                switch (movement.direction)
                                {
                                    case Movement.Directions.PlusZ:
                                        //Impacto de la bala con algun tanque
                                        Console.WriteLine("PlusZ");
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < bullet.center.X &&
                                            (tank.center.X + tank.movement.width) > bullet.center.X)
                                            {
                                                if ((bullet.center.Z + bullet.movement.radius) >= (tank.center.Z - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < bullet.center.X &&
                                            (tank.center.X + tank.movement.radius) > bullet.center.X)
                                            {
                                                if ((bullet.center.Z + bullet.movement.radius) >= (tank.center.Z - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//Impacto de la bala con alguna pared
                                            foreach (Vector2[] pairVector in listHorizontal)
                                            {
                                                if ((pairVector[0].X < (bullet.center.X - bullet.movement.width) && pairVector[1].X > (bullet.center.X - bullet.movement.width)) ||
                                                (pairVector[0].X < (bullet.center.X + bullet.movement.width) && pairVector[1].X > (bullet.center.X + bullet.movement.width)))
                                                    listCloserWall.Add(pairVector);
                                            }
                                            foreach (Vector2[] pairVector in listCloserWall)
                                            {
                                                if (pairVector[0].Y > bullet.center.Z &&
                                                (bullet.center.Z + bullet.movement.radius) >= (pairVector[0].Y - width))
                                                {
                                                    move = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//No hay impacto
                                            bullet.center += new Vector3(0.0f, 0.0f, 0.005f);
                                            bullet.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                        }
                                        break;
                                    case Movement.Directions.MinusX:
                                        //Impacto de la bala con algun tanque
                                        Console.WriteLine("MinusX");
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                                tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < bullet.center.Z &&
                                            (tank.center.Z + tank.movement.width) > bullet.center.Z)
                                            {
                                                if ((bullet.center.X - bullet.movement.radius) >= (tank.center.X + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < bullet.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > bullet.center.Z)
                                            {
                                                if ((bullet.center.X + bullet.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//Impacto de algun objeto con alguna pared
                                            foreach (Vector2[] pairVector in listVertical)
                                            {
                                                if ((pairVector[0].Y > (bullet.center.Z - bullet.movement.width) && pairVector[1].Y < (bullet.center.Z - bullet.movement.width)) ||
                                                (pairVector[0].Y > (bullet.center.Z + bullet.movement.width) && pairVector[1].Y < (bullet.center.Z + bullet.movement.width)))
                                                    listCloserWall.Add(pairVector);
                                            }
                                            foreach (Vector2[] pairVector in listCloserWall)
                                            {
                                                if (pairVector[0].X < bullet.center.X &&
                                                (bullet.center.X - bullet.movement.radius) <= (pairVector[0].X + width))
                                                {
                                                    move = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//No hay impacto
                                            bullet.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                            bullet.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                        }
                                        break;
                                    case Movement.Directions.MinusZ:
                                        //Impacto de la bala con algun tanque
                                        Console.WriteLine("MinusZ");
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                                tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < bullet.center.X &&
                                            (tank.center.X + tank.movement.width) > bullet.center.X)
                                            {
                                                if ((bullet.center.Z - bullet.movement.radius) >= (tank.center.Z + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < bullet.center.X &&
                                            (tank.center.X + tank.movement.radius) > bullet.center.X)
                                            {
                                                if ((bullet.center.Z - bullet.movement.radius) >= (tank.center.Z + tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//Impacto de algun objeto con alguna pared
                                            foreach (Vector2[] pairVector in listHorizontal)
                                            {
                                                if ((pairVector[0].X < (bullet.center.X - bullet.movement.width) && pairVector[1].X > (bullet.center.X - bullet.movement.width)) ||
                                                (pairVector[0].X < (bullet.center.X + bullet.movement.width) && pairVector[1].X > (bullet.center.X + bullet.movement.width)))
                                                    listCloserWall.Add(pairVector);
                                            }
                                            foreach (Vector2[] pairVector in listCloserWall)
                                            {
                                                if (pairVector[0].Y < bullet.center.Z &&
                                                (bullet.center.Z - bullet.movement.radius) <= (pairVector[0].Y))
                                                {
                                                    move = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {//No hay impacto
                                            bullet.center += new Vector3(0.0f, 0.0f, -0.005f);
                                            bullet.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                        }
                                        break;
                                    case Movement.Directions.PlusX:
                                        //Impacto de la bala con algun tanque
                                        Console.WriteLine("PlusX");
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                                tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < bullet.center.Z &&
                                            (tank.center.Z + tank.movement.width) > bullet.center.Z)
                                            {
                                                if ((bullet.center.X + bullet.movement.radius) >= (tank.center.X - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < bullet.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > bullet.center.Z)
                                            {
                                                if ((bullet.center.X + bullet.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {
                                            foreach (Vector2[] pairVector in listVertical)
                                            {
                                                if ((pairVector[0].Y > (bullet.center.Z - bullet.movement.width) && pairVector[1].Y < (bullet.center.Z - bullet.movement.width)) ||
                                                (pairVector[0].Y > (bullet.center.Z + bullet.movement.width) && pairVector[1].Y < (bullet.center.Z + bullet.movement.width)))
                                                    listCloserWall.Add(pairVector);
                                            }
                                            foreach (Vector2[] pairVector in listCloserWall)
                                            {
                                                if (pairVector[0].X > bullet.center.X &&
                                                (bullet.center.X + bullet.movement.radius) >= pairVector[0].X)
                                                {
                                                    move = false;
                                                    bullet.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (move)
                                        {
                                            bullet.center += new Vector3(0.005f, 0.0f, 0.0f);
                                            bullet.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            });
            thread.Start();
        }
    }
}
