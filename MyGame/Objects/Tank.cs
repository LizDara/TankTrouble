using Grafica.Estructura;
using Grafica.LoadFiles;
using Grafica.MyGame.Parts;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Tank : Objeto
    {
        ObjLoader objLoader;
        public Tank()
        {
            key = "tank";
            obj = true;
            movement = new Movement();
            objLoader = new ObjLoader("Recursos/T34.obj");
            TankPart tankPart = new TankPart();
            addPart("tankPart", tankPart);
            init();
        }

        public void init()
        {
            center = new Vector3(-3.7f, 1.3638055f, 3.9f);//-3.7f  3.9f
            movement.radius = 0.63f;
            movement.direction = Movement.Directions.PlusX;
            rotation = new Vector3(0.0f, 90.0f, 0.0f);
            translation = new Vector3(-3.7f, 0.0f, 3.9f);
            scale = new Vector3(0.15f, 0.15f, 0.15f);
        }

        public override void CalculateMatrix()
        {
            /*Quaternion quaternionX = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(rotation.X));
            Quaternion quaternionY = Quaternion.FromAxisAngle(new Vector3(2.0f, 1.0f, 2.5f), MathHelper.DegreesToRadians(rotation.Y));
            Quaternion quaternionZ = Quaternion.FromAxisAngle(Vector3.UnitZ, MathHelper.DegreesToRadians(rotation.Z));

            Quaternion quaternion = Quaternion.Multiply(Quaternion.Multiply(quaternionZ, quaternionY), quaternionX);
            quaternion.Normalize();

            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateTranslation(translation) * Matrix4.CreateFromQuaternion(quaternion);*/

            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void addPart(string key, TankPart part)
        {
            objLoader.objLoad();
            part.SetTexture("Recursos/camuflajeverde.jpg");
            part.setVertex(objLoader.vertex, objLoader.vertexIndex.Count);
            parts.Add(key, part);
            partCount++;
        }
    }
}
