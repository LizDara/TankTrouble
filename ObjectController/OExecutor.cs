using Grafica.Estructura;
using Grafica.MyGame;
using Grafica.MyGame.Objects;
using Grafica.MyGame.Parts;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Grafica.ObjectController
{
    class OExecutor
    {
        Bullet bullet;
        Explosion explosion;
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

        public void run(Bullet[] bullets, Explosion[] explosions,Tank firstTank, Tank secondTank)
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
                            explosion = explosions[i];
                            Movement movement = bullet.movement;
                            Tank tank;
                            if (bullet.parentKey.Equals("tank1"))
                                tank = secondTank;
                            else
                                tank = firstTank;
                            if (movement.forward)
                            {
                                List<Vector2[]> listCloserWall = new List<Vector2[]>();
                                bool move = true;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                                    explosion.center = tank.center;
                                                    explosion.translation = tank.translation;
                                                    explosion.start = true;
                                                    move = false;
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
                                        {//No hay impacto
                                            bullet.center += new Vector3(0.005f, 0.0f, 0.0f);
                                            bullet.translation += new Vector3(0.005f, 0.0f, 0.0f);
                                        }
                                        break;
                                }
                            }
                            //Mostrar explosion
                            if (explosion != null && explosion.start)
                            {
                                Parte partExplosion;
                                Parte part;
                                Console.WriteLine("");
                                switch (explosion.duration)
                                {
                                    case 100:
                                        tank.draw = false;
                                        bullet.draw = false;
                                        partExplosion = (FirstExplosion)explosion.parts["firstExplosion"];
                                        partExplosion.draw = true;
                                        break;
                                    case 200:
                                        partExplosion = (SecondExplosion)explosion.parts["secondExplosion"];
                                        part = (FirstExplosion)explosion.parts["firstExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 300:
                                        partExplosion = (ThirdExplosion)explosion.parts["thirdExplosion"];
                                        part = (SecondExplosion)explosion.parts["secondExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 400:
                                        partExplosion = (FourthExplosion)explosion.parts["fourthExplosion"];
                                        part = (ThirdExplosion)explosion.parts["thirdExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 500:
                                        partExplosion = (FifthExplosion)explosion.parts["fifthExplosion"];
                                        part = (FourthExplosion)explosion.parts["fourthExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 600:
                                        partExplosion = (SixthExplosion)explosion.parts["sixthExplosion"];
                                        part = (FifthExplosion)explosion.parts["fifthExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 700:
                                        partExplosion = (SeventhExplosion)explosion.parts["seventhExplosion"];
                                        part = (SixthExplosion)explosion.parts["sixthExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 800:
                                        partExplosion = (EighthExplosion)explosion.parts["eighthExplosion"];
                                        part = (SeventhExplosion)explosion.parts["seventhExplosion"];
                                        part.draw = false;
                                        partExplosion.draw = true;
                                        break;
                                    case 900:
                                        part = (EighthExplosion)explosion.parts["eighthExplosion"];
                                        part.draw = false;
                                        explosion.draw = false;
                                        explosion.start = false;
                                        break;
                                }
                                explosion.duration++;
                            }
                        }
                    }
                }
            });
            thread.Start();
        }
    }
}
