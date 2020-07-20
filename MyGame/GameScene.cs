using Grafica.Estructura;
using Grafica.MyGame.Objects;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame
{
    class GameScene : Escenario
    {
        Labyrinth labyrinth;
        Tank firstTank;
        Tank secondTank;
        Floor floor;
        public bool finish = false;
        public GameScene()
        {
            shader = new Shader("Recursos/shader.vert", "Recursos/shader.frag");
            position = new Vector3(0.0f, 18.0f, 0.0f);//16.0f Y
            front = new Vector3(0.0f, 0.0f, 0.0f);
            up = new Vector3(0.0f, 0.0f, -1.0f);//-1.0f Z
            init("Recursos/laberinto2.txt");
        }

        public void init(string path)
        {
            labyrinth = new Labyrinth(path);
            floor = new Floor(labyrinth.sizeX, labyrinth.sizeZ);
            firstTank = new Tank(labyrinth.firstPositionX, labyrinth.firstPositionZ);
            firstTank.setTexture("Recursos/camuflaje.jpg");
            firstTank.movement.direction = Movement.Directions.PlusX;
            firstTank.rotation = new Vector3(0.0f, 90.0f, 0.0f);
            firstTank.key = "tank1";
            secondTank = new Tank(labyrinth.secondPositionX, labyrinth.secondPositionZ);
            secondTank.setTexture("Recursos/camuflaje2.jpg");
            secondTank.movement.direction = Movement.Directions.MinusX;
            secondTank.rotation = new Vector3(0.0f, -90.0f, 0.0f);
            secondTank.key = "tank2";
            addObject(labyrinth);
            addObject(floor);
            addObject(firstTank);
            addObject(secondTank);
        }

        public void addObject(Objeto obj)
        {
            objects.Add(obj.key, obj);
            objectCount++;
        }

        public void nextLevel()
        {
            objects.Clear();
            init("Recursos/laberinto.txt");
        }

        public override void CalculateMatrix()
        {
            modelScene = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public override void CalculateViewProjection()
        {
            CalculateView();
            viewProjection = view * projection;
        }

        public void CalculateView()
        {
            view = Matrix4.LookAt(position, front, up);
        }

        public void SetMatrixProjection(Matrix4 projection)
        {
            this.projection = projection;
        }
    }
}
