using Grafica.Estructura;
using Grafica.MyGame.Parts;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica.MyGame.Objects
{
    class Floor : Objeto
    {
        Parte cement;
        public Floor()
        {
            key = "floor";
            obj = false;
            cement = new Cement();
            addPart(cement.key, cement, "Recursos/cemento2.jpg");
        }

        public override void CalculateMatrix()
        {
            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void addPart(string key, Parte part, string path)
        {
            part.SetTexture(path);
            parts.Add(key, part);
            partCount++;
        }
    }
}
