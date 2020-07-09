using Grafica.Estructura;
using Grafica.MyGame.Objects;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame
{
    class GameScene : Escenario
    {
        Objeto labyrinth;
        Objeto tank;
        Objeto floor;
        public GameScene()
        {
            shader = new Shader("Recursos/shader.vert", "Recursos/shader.frag");
            position = new Vector3(0.0f, 18.0f, 0.0f);
            front = new Vector3(0.0f, 0.0f, 0.0f);
            up = new Vector3(0.0f, 0.0f, -1.0f);
            labyrinth = new Labyrinth();
            tank = new Tank();
            floor = new Floor();
            addObject(labyrinth.key, labyrinth);
            addObject(tank.key, tank);
            addObject(floor.key, floor);
        }

        public void addObject(string key, Objeto obj)
        {
            objects.Add(key, obj);
            objectCount++;
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
