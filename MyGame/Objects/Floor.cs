using Grafica.Estructura;
using Grafica.MyGame.Parts;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Floor : Objeto
    {
        Cement cement;
        public Floor(float x, float z)
        {
            key = "floor";
            obj = false;
            texture = new Texture("Recursos/cafe6.png");
            cement = new Cement();
            cement.setVertex(x, z);
            addPart(cement);
        }

        public override void CalculateMatrix()
        {
            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void addPart(Parte part)
        {
            parts.Add(part.key, part);
            partCount++;
        }
    }
}
