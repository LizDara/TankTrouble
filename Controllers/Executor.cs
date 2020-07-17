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
                                    foreach (Vector2[] pairVector in listHorizontal)
                                    {
                                        if (pairVector[0].X < obj.center.X && pairVector[1].X > obj.center.X)
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
                                    if (move)
                                    {
                                        obj.center += new Vector3(0.0f, 0.0f, 0.005f);
                                        obj.translation += new Vector3(0.0f, 0.0f, 0.005f);
                                    }
                                    break;
                                case Movement.Directions.MinusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if (pairVector[0].Y > obj.center.Z && pairVector[1].Y < obj.center.Z)
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
                                    if (move)
                                    {
                                        obj.center += new Vector3(-0.005f, 0.0f, 0.0f);
                                        obj.translation += new Vector3(-0.005f, 0.0f, 0.0f);
                                    }
                                    break;
                                case Movement.Directions.MinusZ:
                                    foreach (Vector2[] pairVector in listHorizontal)
                                    {
                                        if (pairVector[0].X < obj.center.X && pairVector[1].X > obj.center.X)
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
                                    if (move)
                                    {
                                        obj.center += new Vector3(0.0f, 0.0f, -0.005f);
                                        obj.translation += new Vector3(0.0f, 0.0f, -0.005f);
                                    }
                                    break;
                                case Movement.Directions.PlusX:
                                    foreach (Vector2[] pairVector in listVertical)
                                    {
                                        if (pairVector[0].Y > obj.center.Z && pairVector[1].Y < obj.center.Z)
                                            listCloserWall.Add(pairVector);
                                    }
                                    foreach (Vector2[] pairVector in listCloserWall)
                                    {
                                        if (pairVector[0].X > obj.center.X &&
                                        (obj.center.X + obj.movement.radius) >= pairVector[0].X)
                                        {
                                            move = false;
                                            if (i > 1)
                                            {//bool draw en cada objeto para saber si se dibujara y si no, se lo elimina
                                                obj.draw = false;
                                                movement.forward = false;
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
                                        if (pairVector[0].X < obj.center.X && pairVector[1].X > obj.center.X)
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
                                        if (pairVector[0].Y > obj.center.Z && pairVector[1].Y < obj.center.Z)
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
                                        if (pairVector[0].X < obj.center.X && pairVector[1].X > obj.center.X)
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
                                        if (pairVector[0].Y > obj.center.Z && pairVector[1].Y < obj.center.Z)
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
                            //tank.translation += new Vector3(0.0f, 0.0f, -0.005f);
                        }
                    }
                }
            });
            thread.Start();
        }
    }
}
