using OpenTK;
using Grafica.Rendering;

namespace Grafica.Estructura
{
    abstract class Parte
    {
        public Vector3 center = Vector3.Zero;

        public Vector3 translation = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public Vector3 scale = Vector3.One;

        public Matrix4 model = Matrix4.Identity;

        public RenderObject renderObject;
        public Texture texture;

        public float[] vertex;
        public int vertexCount;

        public string key;

        public abstract void CalculateMatrix();
        public abstract void SetTexture(string path);
    }
}
