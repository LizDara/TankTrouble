using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Grafica.Controllers
{
    class Executor
    {
        Objeto obj;
        List<Vector2[]> listVertical;
        List<Vector2[]> listHorizontal;
        public bool running;
        float width = 0.4f;
        public Executor()
        {
            running = true;
        }

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            listVertical = vertical;
            listHorizontal = horizontal;
        }

        public void run(Hashtable objects)
        {
            Thread thread = new Thread(() =>
            {
                while (running)
                {
                    for (int i = 0; i < objects.Count - 2; i++)
                    {
                        if (i > 1)
                            obj = (Bullet)objects["bullet" + (i - 1)];
                        else
                            obj = (Tank)objects["tank" + (i + 1)];
                        Movement movement = obj.movement;
                        if (movement.right)
                        {
                            obj.rotation += new Vector3(0.0f, movement.angle, 0.0f);
                        }
                        if (movement.left)
                        {
                            obj.rotation += new Vector3(0.0f, movement.angle * (-1), 0.0f);
                        }
                        if (movement.forward)
                        {
                            Console.WriteLine("Forward");
                            List<Vector2[]> listCloserWall = new List<Vector2[]>();
                            bool move = true;
                            switch (movement.direction)
                            {
                                case Movement.Directions.PlusZ:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (obj.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusZ || 
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < obj.center.X &&
                                            (tank.center.X + tank.movement.width) > obj.center.X)
                                            {
                                                if ((obj.center.Z + obj.movement.radius) >= (tank.center.Z - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < obj.center.X &&
                                            (tank.center.X + tank.movement.radius) > obj.center.X)
                                            {
                                                if ((obj.center.Z + obj.movement.radius) >= (tank.center.Z - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (obj.center.X - obj.movement.width) && pairVector[1].X > (obj.center.X - obj.movement.width)) ||
                                            (pairVector[0].X < (obj.center.X + obj.movement.width) && pairVector[1].X > (obj.center.X + obj.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y > obj.center.Z &&
                                            (obj.center.Z + obj.movement.radius) >= (pairVector[0].Y - width))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        obj.center += new Vector3(0.0f, 0.0f, 0.005f);
                                        obj.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (obj.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < obj.center.Z &&
                                            (tank.center.Z + tank.movement.width) > obj.center.Z)
                                            {
                                                if ((obj.center.X - obj.movement.radius) >= (tank.center.X + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < obj.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > obj.center.Z)
                                            {
                                                if ((obj.center.X + obj.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (obj.center.Z - obj.movement.width) && pairVector[1].Y < (obj.center.Z - obj.movement.width)) ||
                                            (pairVector[0].Y > (obj.center.Z + obj.movement.width) && pairVector[1].Y < (obj.center.Z + obj.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X < obj.center.X &&
                                            (obj.center.X - obj.movement.radius) <= (pairVector[0].X + width))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        obj.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                        obj.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (obj.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < obj.center.X &&
                                            (tank.center.X + tank.movement.width) > obj.center.X)
                                            {
                                                if ((obj.center.Z - obj.movement.radius) >= (tank.center.Z + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < obj.center.X &&
                                            (tank.center.X + tank.movement.radius) > obj.center.X)
                                            {
                                                if ((obj.center.Z - obj.movement.radius) >= (tank.center.Z + tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (obj.center.X - obj.movement.width) && pairVector[1].X > (obj.center.X - obj.movement.width)) ||
                                            (pairVector[0].X < (obj.center.X + obj.movement.width) && pairVector[1].X > (obj.center.X + obj.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y < obj.center.Z &&
                                            (obj.center.Z - obj.movement.radius) <= (pairVector[0].Y))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        obj.center += new Vector3(0.0f, 0.0f, -0.005f);
                                        obj.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (obj.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < obj.center.Z &&
                                            (tank.center.Z + tank.movement.width) > obj.center.Z)
                                            {
                                                if ((obj.center.X + obj.movement.radius) >= (tank.center.X - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < obj.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > obj.center.Z)
                                            {
                                                if ((obj.center.X + obj.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (obj.center.Z - obj.movement.width) && pairVector[1].Y < (obj.center.Z - obj.movement.width)) ||
                                            (pairVector[0].Y > (obj.center.Z + obj.movement.width) && pairVector[1].Y < (obj.center.Z + obj.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X > obj.center.X &&
                                            (obj.center.X + obj.movement.radius) >= pairVector[0].X)
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    obj.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {
                                        obj.center += new Vector3(0.005f, 0.0f, 0.0f);
                                        obj.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                            }
                        }
                        if (movement.backward)
                        {
                            Console.WriteLine("Backward");
                            List<Vector2[]> listCloserWall = new List<Vector2[]>();
                            bool move = true;
                            switch (movement.direction)
                            {
                                case Movement.Directions.PlusZ:
                                    foreach (Vector2[] pairVector in listHorizontal)
                                    {
                                        if ((pairVector[0].X < (obj.center.X - obj.movement.width) && pairVector[1].X > (obj.center.X - obj.movement.width)) ||
                                        (pairVector[0].X < (obj.center.X + obj.movement.width) && pairVector[1].X > (obj.center.X + obj.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].Y < obj.center.Z &&
                                        (obj.center.Z - obj.movement.radius) <= (pairVector[0].Y))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        obj.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                        obj.center += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if ((pairVector[0].Y > (obj.center.Z - obj.movement.width) && pairVector[1].Y < (obj.center.Z - obj.movement.width)) ||
                                        (pairVector[0].Y > (obj.center.Z + obj.movement.width) && pairVector[1].Y < (obj.center.Z + obj.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].X > obj.center.X &&
                                        (obj.center.X + obj.movement.radius) >= pairVector[0].X)
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        obj.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                        obj.center += new Vector3(0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    foreach (Vector2[] pairVector in listHorizontal)
                                    {
                                        if ((pairVector[0].X < (obj.center.X - obj.movement.width) && pairVector[1].X > (obj.center.X - obj.movement.width)) ||
                                        (pairVector[0].X < (obj.center.X + obj.movement.width) && pairVector[1].X > (obj.center.X + obj.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].Y > obj.center.Z &&
                                        (obj.center.Z + obj.movement.radius) >= (pairVector[0].Y - width))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        obj.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                        obj.center += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if ((pairVector[0].Y > (obj.center.Z - obj.movement.width) && pairVector[1].Y < (obj.center.Z - obj.movement.width)) ||
                                        (pairVector[0].Y > (obj.center.Z + obj.movement.width) && pairVector[1].Y < (obj.center.Z + obj.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].X < obj.center.X &&
                                        (obj.center.X - obj.movement.radius) <= (pairVector[0].X + width))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        obj.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                        obj.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                            }
                        }
                    }
                }
            });
            thread.Start();
        }
    }
}
