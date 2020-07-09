using Grafica.Estructura;
using Grafica.MyGame.Objects;
using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace Grafica.Controllers
{
    class Controller
    {
        bool[] stateKey = new bool[4];
        Planner planner;
        Executor executor;
        List<Objeto> objects;
        Tank tank;
        Labyrinth labyrinth;
        public Controller()
        {
            planner = new Planner();
            executor = new Executor();
            objects = new List<Objeto>();
            for (int i = 0; i < stateKey.Length; i++)
                stateKey[i] = false;
        }

        public void addObject(Objeto obj)
        {
            objects.Add(obj);
        }

        public void addKey(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    stateKey[0] = true;
                    break;
                case Key.Down:
                    stateKey[1] = true;
                    break;
                case Key.Right:
                    stateKey[2] = true;
                    tank.rotation += new Vector3(0.0f, tank.movement.angle * (-1), 0.0f);
                    break;
                case Key.Left:
                    stateKey[3] = true;
                    tank.rotation += new Vector3(0.0f, tank.movement.angle, 0.0f);
                    break;
            }
            planner.changeState(stateKey, tank.movement);
        }

        public void deleteKey(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    stateKey[0] = false;
                    break;
                case Key.Down:
                    stateKey[1] = false;
                    break;
                case Key.Right:
                    stateKey[2] = false;
                    break;
                case Key.Left:
                    stateKey[3] = false;
                    break;
            }
            planner.changeState(stateKey, tank.movement);
        }

        public void moveObject()
        {
            setObjects();
            setWalls();
            executor.run(tank);
        }

        public void setObjects()
        {
            labyrinth = (Labyrinth)objects[0];
            tank = (Tank)objects[1];
        }

        public void setWalls()
        {
            List<Vector2[]> vertical = new List<Vector2[]>();
            List<Vector2[]> horizontal = new List<Vector2[]>();
            foreach (Vector2[] pairVector in labyrinth.listVertex)
            {
                if (pairVector[0].X == pairVector[1].X)
                    vertical.Add(pairVector);
                else
                    horizontal.Add(pairVector);
            }
            executor.setWalls(vertical, horizontal);
        }

        public void dispose()
        {
            executor.running = false;
        }
    }
}
