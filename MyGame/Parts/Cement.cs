using Grafica.Estructura;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Parts
{
    class Cement : Parte
    {
        public float[] vertexCement = {
            -5.5f,  0.0f,  5.5f,    0.0f, 1.0f,
             5.5f,  0.0f,  5.5f,    1.0f, 1.0f,
             5.5f,  0.0f, -5.5f,    1.0f, 0.0f,
            -5.5f,  0.0f, -5.5f,    0.0f, 0.0f
        };
        uint[] index = {
            0, 1, 2,
            2, 3, 0
        };
        public Cement()
        {
            key = "cement";
            vertex = vertexCement;
            renderObject = new RenderObject(vertex, index);
            vertexCount = index.Length;
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

        public override void SetTexture(string path)
        {
            texture = new Texture(path);
        }
    }
}
