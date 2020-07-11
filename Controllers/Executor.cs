using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Grafica.Controllers
{
    class Executor
    {
        Tank tank;
        List<Vector2[]> listVertical;
        List<Vector2[]> listHorizontal;
        public bool running;
        public bool move;
        float width = 0.4f;
        public Executor()
        {
            running = true;
            move = true;
        }

        public void setWalls(List<Vector2[]> vertical, List<Vector2[]> horizontal)
        {
            listVertical = vertical;
            listHorizontal = horizontal;
        }

        public void run(Objeto obj)
        {
            Thread thread = new Thread(() =>
            {
                tank = (Tank)obj;
                while (running)
                {
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
                                foreach (Vector2[] pairVector in listHorizontal)
                                {
                                    if (pairVector[0].X < tank.center.X && pairVector[1].X > tank.center.X)
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
                            case Movement.Directions.MinusX:
                                foreach (Vector2[] pairVector in listVertical)
                                {
                                    if (pairVector[0].Y > tank.center.Z && pairVector[1].Y < tank.center.Z)
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
                            case Movement.Directions.MinusZ:
                                foreach (Vector2[] pairVector in listHorizontal)
                                {
                                    if (pairVector[0].X < tank.center.X && pairVector[1].X > tank.center.X)
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
                            case Movement.Directions.PlusX:
                                foreach (Vector2[] pairVector in listVertical)
                                {
                                    if (pairVector[0].Y > tank.center.Z && pairVector[1].Y < tank.center.Z)
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
                        }
                        //tank.translation += new Vector3(0.0f, 0.0f, 0.005f);
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
                                    if (pairVector[0].X < tank.center.X && pairVector[1].X > tank.center.X)
                                        listCloserWall.Add(pairVector);
                                }
                                foreach (Vector2[] pairVector in listCloserWall)
                                {
                                    if (pairVector[0].Y < tank.center.Z &&
                                    (tank.center.Z - tank.movement.radius) <= (pairVector[0].Y + width))
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
                                    if (pairVector[0].Y > tank.center.Z && pairVector[1].Y < tank.center.Z)
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
                                    if (pairVector[0].X < tank.center.X && pairVector[1].X > tank.center.X)
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
                                    if (pairVector[0].Y > tank.center.Z && pairVector[1].Y < tank.center.Z)
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
                        //tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                    }
                }
            });
            thread.Start();
        }
    }
}
