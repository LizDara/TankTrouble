using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Grafica.UserController
{
    class UExecutor
    {
        Tank tank;
        List<Vector2[]> listVertical;
        List<Vector2[]> listHorizontal;
        public bool running;
        float width = 0.4f;
        public UExecutor()
        {
            running = true;
        }

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            listVertical = vertical;
            listHorizontal = horizontal;
        }

        /*public void run(Hashtable objects)
        {
            Thread thread = new Thread(() =>
            {
                while (running)
                {
                    for (int i = 0; i < objects.Count - 2; i++)
                    {
                        if (i > 1)
                            tank = (Bullet)objects["bullet" + (i - 1)];
                        else
                            tank = (Tank)objects["tank" + (i + 1)];
                        Movement movement = tank.movement;
                        if (movement.right)
                        {
                            tank.rotation += new Vector3(0.0f, movement.angle, 0.0f);
                        }
                        if (movement.left)
                        {
                            tank.rotation += new Vector3(0.0f, movement.angle * (-1), 0.0f);
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
                                        if (this.tank.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusZ || 
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < this.tank.center.X &&
                                            (tank.center.X + tank.movement.width) > this.tank.center.X)
                                            {
                                                if ((this.tank.center.Z + this.tank.movement.radius) >= (tank.center.Z - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < this.tank.center.X &&
                                            (tank.center.X + tank.movement.radius) > this.tank.center.X)
                                            {
                                                if ((this.tank.center.Z + this.tank.movement.radius) >= (tank.center.Z - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (pairVector[0].Y - width))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(0.0f, 0.0f, 0.005f);
                                        tank.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (this.tank.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < this.tank.center.Z &&
                                            (tank.center.Z + tank.movement.width) > this.tank.center.Z)
                                            {
                                                if ((this.tank.center.X - this.tank.movement.radius) >= (tank.center.X + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < this.tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > this.tank.center.Z)
                                            {
                                                if ((this.tank.center.X + this.tank.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (pairVector[0].X + width))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                        tank.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (this.tank.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.X - tank.movement.width) < this.tank.center.X &&
                                            (tank.center.X + tank.movement.width) > this.tank.center.X)
                                            {
                                                if ((this.tank.center.Z - this.tank.movement.radius) >= (tank.center.Z + tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.X - tank.movement.radius) < this.tank.center.X &&
                                            (tank.center.X + tank.movement.radius) > this.tank.center.X)
                                            {
                                                if ((this.tank.center.Z - this.tank.movement.radius) >= (tank.center.Z + tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//Impacto de algun objeto con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (pairVector[0].Y))
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(0.0f, 0.0f, -0.005f);
                                        tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    if (i > 1)
                                    {//Impacto de la bala con algun tanque
                                        Tank tank;
                                        if (this.tank.parentKey.Equals("tank1"))
                                            tank = (Tank)objects["tank2"];
                                        else
                                            tank = (Tank)objects["tank1"];

                                        if (tank.movement.direction == Movement.Directions.PlusX ||
                                        tank.movement.direction == Movement.Directions.MinusX)
                                        {
                                            if ((tank.center.Z - tank.movement.width) < this.tank.center.Z &&
                                            (tank.center.Z + tank.movement.width) > this.tank.center.Z)
                                            {
                                                if ((this.tank.center.X + this.tank.movement.radius) >= (tank.center.X - tank.movement.radius))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                        if (tank.movement.direction == Movement.Directions.PlusZ ||
                                        tank.movement.direction == Movement.Directions.MinusZ)
                                        {
                                            if ((tank.center.Z - tank.movement.radius) < this.tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) > this.tank.center.Z)
                                            {
                                                if ((this.tank.center.X + this.tank.movement.radius) >= (tank.center.X - tank.movement.width))
                                                {
                                                    move = false;
                                                    tank.draw = false;
                                                    this.tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= pairVector[0].X)
                                            {
                                                move = false;
                                                if (i > 1)
                                                {
                                                    tank.draw = false;
                                                    movement.forward = false;
                                                }
                                            }
                                        }
                                    }
                                    if (move)
                                    {
                                        tank.center += new Vector3(0.005f, 0.0f, 0.0f);
                                        tank.translation += new Vector3(0.005f, 0.0f, 0.0f);
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
                                        if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                        (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].Y < tank.center.Z &&
                                        (tank.center.Z - tank.movement.radius) <= (pairVector[0].Y))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                        tank.center += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                        (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].X > tank.center.X &&
                                        (tank.center.X + tank.movement.radius) >= pairVector[0].X)
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        tank.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                        tank.center += new Vector3(0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    foreach (Vector2[] pairVector in listHorizontal)
                                    {
                                        if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                        (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].Y > tank.center.Z &&
                                        (tank.center.Z + tank.movement.radius) >= (pairVector[0].Y - width))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        tank.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                        tank.center += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                        (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].X < tank.center.X &&
                                        (tank.center.X - tank.movement.radius) <= (pairVector[0].X + width))
                                            move = false;
                                    }
                                    if (move)
                                    {
                                        tank.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                        tank.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                            }
                        }
                    }
                }
            });
            thread.Start();
        }*/

        public void run(Tank firstTank, Tank secondTank)
        {
            Thread thread = new Thread(() =>
            {
                while (running)
                {
                    Tank closerTank;
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            tank = firstTank;
                            closerTank = secondTank;
                        }
                        else
                        {
                            tank = secondTank;
                            closerTank = firstTank;
                        }
                        Movement movement = tank.movement;
                        if (movement.right)
                        {
                            tank.rotation += new Vector3(0.0f, movement.angle, 0.0f);
                        }
                        if (movement.left)
                        {
                            tank.rotation += new Vector3(0.0f, movement.angle * (-1), 0.0f);
                        }
                        if (movement.forward)
                        {
                            Console.WriteLine("Forward");
                            List<Vector2[]> listCloserWall = new List<Vector2[]>();
                            bool move = true;
                            switch (movement.direction)
                            {
                                case Movement.Directions.PlusZ:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.width) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.width) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z - closerTank.movement.radius) > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (closerTank.center.Z - closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX || 
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.radius) < (tank.center.X - tank.movement.width) && 
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X - tank.movement.width)) || 
                                        ((closerTank.center.X - closerTank.movement.radius) < (tank.center.X + tank.movement.width) && 
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z - closerTank.movement.width) > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (closerTank.center.Z - closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (pairVector[0].Y - width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(0.0f, 0.0f, 0.005f);
                                        tank.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X + closerTank.movement.width) < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (closerTank.center.X + closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X + closerTank.movement.radius) < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (closerTank.center.X + closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (pairVector[0].X + width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                        tank.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.width) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.width) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z + closerTank.movement.radius) < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (closerTank.center.Z + closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.radius) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.radius) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z + closerTank.movement.width) < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (closerTank.center.Z + closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (pairVector[0].Y))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.center += new Vector3(0.0f, 0.0f, -0.005f);
                                        tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X - closerTank.movement.width) > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= (closerTank.center.X - closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X - closerTank.movement.radius) > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= (closerTank.center.X - closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= pairVector[0].X)
                                                move = false;
                                        }
                                    }//No hay impacto
                                    if (move)
                                    {
                                        tank.center += new Vector3(0.005f, 0.0f, 0.0f);
                                        tank.translation += new Vector3(0.005f, 0.0f, 0.0f);
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
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.width) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.width) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z + closerTank.movement.radius) < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (closerTank.center.Z + closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.radius) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.radius) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z + closerTank.movement.width) < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (closerTank.center.Z + closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y < tank.center.Z &&
                                            (tank.center.Z - tank.movement.radius) <= (pairVector[0].Y))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                        tank.center += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X - closerTank.movement.width) > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= (closerTank.center.X - closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X - closerTank.movement.radius) > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= (closerTank.center.X - closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X > tank.center.X &&
                                            (tank.center.X + tank.movement.radius) >= pairVector[0].X)
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                        tank.center += new Vector3(0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.width) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.width) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.width) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z - closerTank.movement.radius) > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (closerTank.center.Z - closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.X - closerTank.movement.radius) < (tank.center.X - tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X - tank.movement.width)) ||
                                        ((closerTank.center.X - closerTank.movement.radius) < (tank.center.X + tank.movement.width) &&
                                        (closerTank.center.X + closerTank.movement.radius) > (tank.center.X + tank.movement.width)))
                                        {
                                            if ((closerTank.center.Z - closerTank.movement.width) > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (closerTank.center.Z - closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listHorizontal)
                                        {
                                            if ((pairVector[0].X < (tank.center.X - tank.movement.width) && pairVector[1].X > (tank.center.X - tank.movement.width)) ||
                                            (pairVector[0].X < (tank.center.X + tank.movement.width) && pairVector[1].X > (tank.center.X + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].Y > tank.center.Z &&
                                            (tank.center.Z + tank.movement.radius) >= (pairVector[0].Y - width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                        tank.center += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    //Impacto de un tanque con otro tanque
                                    if (closerTank.movement.direction == Movement.Directions.MinusZ ||
                                    closerTank.movement.direction == Movement.Directions.PlusZ)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.radius) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.radius) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X + closerTank.movement.width) < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (closerTank.center.X + closerTank.movement.width))
                                                move = false;
                                        }
                                    }
                                    if (closerTank.movement.direction == Movement.Directions.MinusX ||
                                    closerTank.movement.direction == Movement.Directions.PlusX)
                                    {
                                        if (((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z - tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z - tank.movement.width)) ||
                                        ((closerTank.center.Z - closerTank.movement.width) < (tank.center.Z + tank.movement.width) &&
                                        (closerTank.center.Z + closerTank.movement.width) > (tank.center.Z + tank.movement.width)))
                                        {
                                            if ((closerTank.center.X + closerTank.movement.radius) < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (closerTank.center.X + closerTank.movement.radius))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//Impacto de un tanque con alguna pared
                                        foreach (Vector2[] pairVector in listVertical)
                                        {
                                            if ((pairVector[0].Y > (tank.center.Z - tank.movement.width) && pairVector[1].Y < (tank.center.Z - tank.movement.width)) ||
                                            (pairVector[0].Y > (tank.center.Z + tank.movement.width) && pairVector[1].Y < (tank.center.Z + tank.movement.width)))
                                                listCloserWall.Add(pairVector);
                                        }
                                        foreach (Vector2[] pairVector in listCloserWall)
                                        {
                                            if (pairVector[0].X < tank.center.X &&
                                            (tank.center.X - tank.movement.radius) <= (pairVector[0].X + width))
                                                move = false;
                                        }
                                    }
                                    if (move)
                                    {//No hay impacto
                                        tank.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                        tank.center += new Vector3(-0.005f, 0.0f, 0.0f);
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
