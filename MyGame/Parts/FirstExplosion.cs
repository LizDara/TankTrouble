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
    class FirstExplosion : Parte
    {
        float[] vertexExplosion = {
            -0.5f,  0.5f,  0.5f,    0.45f, 0.42f,
             0.5f,  0.5f,  0.5f,    0.57f, 0.42f,
             0.5f,  0.5f, -0.5f,    0.57f, 0.61f,
            -0.5f,  0.5f, -0.5f,    0.45f, 0.61f
        };
        uint[] index = {
            0, 1, 2,
            2, 3, 0
        };
        public FirstExplosion()
        {
            key = "firstExplosion";
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
