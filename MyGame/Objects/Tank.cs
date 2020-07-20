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
        TankPart tankPart;
        public float x;
        public float z;
        public Tank(float x, float z)
        {
            obj = true;
            this.x = x;
            this.z = z;
            movement = new Movement();
            objLoader = new ObjLoader("Recursos/T-34.obj");
            tankPart = new TankPart();
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
            center = new Vector3(x, 0.2f, z);
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
