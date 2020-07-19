using Grafica.Estructura;
using Grafica.MyGame.Objects;
using Grafica.ObjectController;
using OpenTK;
using OpenTK.Input;
using System.Collections;
using System.Collections.Generic;

namespace Grafica.UserController
{
    class UController
    {
        bool[] firstStateKey = new bool[4];
        bool[] secondStateKey = new bool[4];
        UPlanner uPlanner;
        OPlanner oPlanner;
        Hashtable objects;
        Labyrinth labyrinth;
        Tank firstTank;
        Tank secondTank;
        public UController()
        {
            uPlanner = new UPlanner();
            oPlanner = new OPlanner();
            objects = new Hashtable();
            for (int i = 0; i < firstStateKey.Length; i++)
                firstStateKey[i] = false;
            for (int i = 0; i < secondStateKey.Length; i++)
                secondStateKey[i] = false;
        }

        public void init(Hashtable objects)
        {
            this.objects = objects;
        }

        public void addKey(Key key)
        {
            switch (key)
            {
                case Key.Up://First Tank
                    firstStateKey[0] = true;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Down:
                    firstStateKey[1] = true;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Right:
                    firstStateKey[2] = true;
                    firstTank.rotation += new Vector3(0.0f, firstTank.movement.angle * (-1), 0.0f);
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Left:
                    firstStateKey[3] = true;
                    firstTank.rotation += new Vector3(0.0f, firstTank.movement.angle, 0.0f);
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Space:
                    oPlanner.addBullet(objects, firstTank);
                    break;
                case Key.W://Second Tank
                    secondStateKey[0] = true;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.S:
                    secondStateKey[1] = true;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.D:
                    secondStateKey[2] = true;
                    secondTank.rotation += new Vector3(0.0f, secondTank.movement.angle * (-1), 0.0f);
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.A:
                    secondStateKey[3] = true;
                    secondTank.rotation += new Vector3(0.0f, secondTank.movement.angle, 0.0f);
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.Q:
                    oPlanner.addBullet(objects, secondTank);
                    break;
            }
        }

        public void deleteKey(Key key)
        {
            switch (key)
            {
                case Key.Up://First Tank
                    firstStateKey[0] = false;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Down:
                    firstStateKey[1] = false;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Right:
                    firstStateKey[2] = false;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.Left:
                    firstStateKey[3] = false;
                    uPlanner.changeState(firstStateKey, firstTank);
                    break;
                case Key.W://Second Tank
                    secondStateKey[0] = false;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.S:
                    secondStateKey[1] = false;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.D:
                    secondStateKey[2] = false;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
                case Key.A:
                    secondStateKey[3] = false;
                    uPlanner.changeState(secondStateKey, secondTank);
                    break;
            }
        }

        public void run()
        {
            setObjects();
            setWalls();
            uPlanner.run(firstTank, secondTank);
            oPlanner.run(firstTank, secondTank);
        }

        public void setObjects()
        {
            labyrinth = (Labyrinth)objects["labyrinth"];
            firstTank = (Tank)objects["tank1"];
            secondTank = (Tank)objects["tank2"];
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
            uPlanner.setWalls(vertical, horizontal);
            oPlanner.setWalls(vertical, horizontal);
        }

        public void dispose()
        {
            uPlanner.dispose();
            oPlanner.dispose();
        }
    }
}
