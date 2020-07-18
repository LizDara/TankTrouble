using Grafica.Estructura;
using Grafica.LoadFiles;
using Grafica.MyGame.Parts;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Bullet : Objeto
    {
        ObjLoader objLoader;
        public Bullet()
        {
            obj = true;
            movement = new Movement();
            texture = new Texture("Recursos/negro2.jpg");
            objLoader = new ObjLoader("Recursos/Bowling.obj");
            BulletPart bulletPart = new BulletPart();
            init();
            addPart("bulletPart", bulletPart);
        }

        public void init()
        {
            objLoader.center(0.0f, 0.0f, 11.07185f);//11.03585f
            movement.radius = 0.073f;
            movement.width = 0.073f;
            movement.forward = true;
            scale = new Vector3(0.006f, 0.006f, 0.006f);
        }

        public override void CalculateMatrix()
        {
            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void addPart(string key, BulletPart part)
        {
            objLoader.objLoad();
            part.setVertex(objLoader.vertex, objLoader.vertexIndex.Count);
            parts.Add(key, part);
            partCount++;
        }
    }
}