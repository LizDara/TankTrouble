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
            scale = new Vector3(0.3f, 0.3f, 0.3f);
            objLoader = new ObjLoader("Recursos/T34.obj");
            TankPart tankPart = new TankPart();
            addPart("tankPart", tankPart);
        }

        public override void CalculateMatrix()
        {
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
