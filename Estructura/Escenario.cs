using System.Collections;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.Estructura
{
    abstract class Escenario
    {
        public int objectCount = 0;

        public Hashtable objects = new Hashtable();
        public Shader shader;

        public Matrix4 modelScene = Matrix4.Identity;

        public Vector3 translation = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;
        public Vector3 scale = Vector3.One;

        public Matrix4 view;
        public Matrix4 projection;
        public Matrix4 viewProjection;

        public Vector3 position;// (0.0f, 2.0f, 20.0f)// (0.0f, 25.0f, 0.0f);
        public Vector3 front;// (0.0f, 0.0f, -1.0f)// (0.0f, 0.0f, 0.0f);
        public Vector3 up;// (0.0f, 1.0f, 0.0f)// (-1.0f, 0.0f, 0.0f);

        public abstract void CalculateViewProjection();
        public abstract void CalculateMatrix();
    }
}
