using Grafica.Estructura;
using Grafica.Rendering;
using OpenTK;

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

        public void setVertex(float[] vertex, int count)
        {
            this.vertex = vertex;
            vertexCount = count;
            renderObject = new RenderObject(vertex);
            renderObject.setVertexCount(vertexCount);
        }
    }
}
