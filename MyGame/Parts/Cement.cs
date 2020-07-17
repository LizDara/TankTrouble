using Grafica.Estructura;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Parts
{
    class Cement : Parte
    {
        /*public float[] vertexCement = {
            -5.1f,  0.0f,  5.1f,    0.0f, 1.0f,
             5.5f,  0.0f,  5.1f,    1.0f, 1.0f,
             5.5f,  0.0f, -5.5f,    1.0f, 0.0f,
            -5.1f,  0.0f, -5.5f,    0.0f, 0.0f
        };
        public float[] vertexCement = {
            -10.1f,  0.0f,  10.1f,    0.0f, 1.0f,
             10.5f,  0.0f,  10.1f,    1.0f, 1.0f,
             10.5f,  0.0f, -10.5f,    1.0f, 0.0f,
            -10.1f,  0.0f, -10.5f,    0.0f, 0.0f
        };*/
        uint[] index = {
            0, 1, 2,
            2, 3, 0
        };
        public Cement()
        {
            key = "cement";
            vertexCount = index.Length;
        }

        public override void CalculateMatrix()
        {
            model = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void setVertex(float x, float z)
        {
            float[] vertexCement = {
            -x - 0.1f,  0.0f,  z + 0.1f,    0.0f, 1.0f,
             x + 0.5f,  0.0f,  z + 0.1f,    1.0f, 1.0f,
             x + 0.5f,  0.0f, -z - 0.5f,    1.0f, 0.0f,
            -x - 0.1f,  0.0f, -z - 0.5f,    0.0f, 0.0f
            };
            vertex = vertexCement;
            renderObject = new RenderObject(vertex, index);
            renderObject.setVertexCount(vertexCount);
        }
    }
}
