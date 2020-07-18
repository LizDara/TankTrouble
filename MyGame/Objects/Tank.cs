using Grafica.Estructura;
using Grafica.LoadFiles;
using Grafica.MyGame.Parts;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Tank : Objeto
    {
        ObjLoader objLoader;
        public float x;
        public float z;
        public Tank(float x, float z)
        {
            obj = true;
            this.x = x;
            this.z = z;
            movement = new Movement();
            objLoader = new ObjLoader("Recursos/T-34.obj");
            TankPart tankPart = new TankPart();
            tankPart.key = "tankPart";
            init();
            addPart(tankPart);
        }

        public void init()
        {
            //objLoader.center(0.8138175f, 0.0f, 0.1832345f);//T34.obj
            //movement.radius = 0.63f;//T34.obj
            //scale = new Vector3(0.15f, 0.15f, 0.15f);//T34.obj
            objLoader.center(-0.095f, -3.03708f, -1.0f);
            center = new Vector3(x, 1.3638055f, z);
            movement.radius = 0.57f;
            movement.width = 0.25f;
            translation = new Vector3(x, 0.0f, z);
            scale = new Vector3(0.0015f, 0.0015f, 0.0015f);
        }

        public void setTexture(string path)
        {
            texture = new Texture(path);
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

        public void addPart(TankPart part)
        {
            objLoader.objLoad();
            part.setVertex(objLoader.vertex, objLoader.vertexIndex.Count);
            parts.Add(part.key, part);
            partCount++;
        }
    }
}
