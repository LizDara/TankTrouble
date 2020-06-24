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
    class TankPart : Parte
    {
        public TankPart()
        {
        }

        public override void CalculateMatrix()
        {
            model = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public override void SetTexture(string path)
        {
            texture = new Texture(path);
        }

        public void setVertex(float[] vertex, int count)
        {
            vertexCount = count;
            renderObject = new RenderObject(vertex);
            renderObject.setVertexCount(vertexCount);
        }
    }
}
