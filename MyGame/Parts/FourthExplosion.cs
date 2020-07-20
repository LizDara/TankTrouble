using Grafica.Estructura;
using Grafica.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafica.MyGame.Parts
{
    class FourthExplosion : Parte
    {
        float[] vertexExplosion = {
            -0.69f, 0.5f,  0.65f,    0.04f, 0.08f,
             0.69f, 0.5f,  0.65f,    0.2f, 0.08f,
             0.69f, 0.5f, -0.65f,    0.2f, 0.33f,
            -0.69f, 0.5f, -0.65f,    0.04f, 0.33f
        };
        uint[] index = {
            0, 1, 2,
            2, 3, 0
        };
        public FourthExplosion()
        {
            key = "fourthExplosion";
            draw = false;
            vertexCount = index.Length;
            vertex = vertexExplosion;
            renderObject = new RenderObject(vertex, index);
            renderObject.setVertexCount(vertexCount);
        }
        public override void CalculateMatrix()
        {
            model = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }
    }
}
