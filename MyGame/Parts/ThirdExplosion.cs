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
    class ThirdExplosion : Parte
    {
        float[] vertexExplosion = {
            -0.68f, 0.5f,  0.52f,    0.8f, 0.41f,
             0.68f, 0.5f,  0.52f,    0.97f, 0.41f,
             0.68f, 0.5f, -0.52f,    0.97f, 0.62f,
            -0.68f, 0.5f, -0.52f,    0.8f, 0.62f
        };
        uint[] index = {
            0, 1, 2,
            2, 3, 0
        };
        public ThirdExplosion()
        {
            key = "thirdExplosion";
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
