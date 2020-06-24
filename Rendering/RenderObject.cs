using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Grafica.Rendering
{
    public class RenderObject : IDisposable
    {
        private bool initialized;
        private int VertexBufferObject;
        private int VertexArrayObject;
        private int ElementBufferObject;
        private int vertexCount;
        public RenderObject(float[] vertex, uint[] index)
        {
            createBuffer();

            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertex.Length * sizeof(float)), vertex, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);//Enlazar EBO
            GL.BufferData<uint>(BufferTarget.ElementArrayBuffer, (IntPtr)(index.Length * sizeof(uint)), index, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            
            initialized = true;
        }

        public RenderObject(float[] vertex)
        {
            createBuffer();

            GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)(vertex.Length * sizeof(float)), vertex, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            initialized = true;
        }

        public void createBuffer()
        {
            VertexBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();
            ElementBufferObject = GL.GenBuffer();
            GL.BindVertexArray(VertexArrayObject);//Enlazar VAO
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);//Enlazar VBO
        }

        public void bind()
        {
            GL.BindVertexArray(VertexArrayObject);
        }

        public void render(bool obj)
        {
            if (obj)
                GL.DrawArrays(PrimitiveType.Triangles, 0, vertexCount);
            else
                GL.DrawElements(PrimitiveType.Triangles, vertexCount, DrawElementsType.UnsignedInt, 0);
        }

        public void setVertexCount(int vertexCount)
        {
            this.vertexCount = vertexCount;
        }

        public void dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if (initialized)
            {
                GL.DeleteVertexArray(VertexArrayObject);
                GL.DeleteBuffer(VertexBufferObject);
                initialized = false;
            }
        }
    }
}
