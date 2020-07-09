using Grafica.Estructura;
using Grafica.Rendering;
using OpenTK;

namespace Grafica.MyGame.Parts
{
    class Wall : Parte
    {
        float heigh = 1.5f;
        float width = 0.4f;
        uint[] index = {
            0, 1, 2,
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20
        };
        public Wall()
        {
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

        public override void SetTexture(string path)
        {
            texture = new Texture(path);
        }

        public void setVertex(Vector2 begin, Vector2 end)
        {
            if (begin.Y.Equals(end.Y))
            {
                float[] vertex = {
                    begin.X,    0,      begin.Y - width,    0.0f, 0.0f,//back
                    end.X,      0,      end.Y - width,      1.0f, 0.0f,
                    end.X,      heigh,  end.Y - width,      1.0f, 1.0f,
                    begin.X,    heigh,  begin.Y - width,    0.0f, 1.0f,

                    begin.X,    0,      begin.Y,            0.0f, 0.0f,//front
                    end.X,      0,      end.Y,              1.0f, 0.0f,
                    end.X,      heigh,  end.Y,              1.0f, 1.0f,
                    begin.X,    heigh,  begin.Y,            0.0f, 1.0f,

                    begin.X,    0,      begin.Y,            0.0f, 0.0f,//left
                    begin.X,    0,      begin.Y - width,    1.0f, 0.0f,
                    begin.X,    heigh,  begin.Y - width,    1.0f, 1.0f,
                    begin.X,    heigh,  begin.Y,            0.0f, 1.0f,

                    end.X,      0,      end.Y,              0.0f, 0.0f,//right
                    end.X,      0,      end.Y - width,      1.0f, 0.0f,
                    end.X,      heigh,  end.Y - width,      1.0f, 1.0f,
                    end.X,      heigh,  end.Y,              0.0f, 1.0f,

                    begin.X,    0,      begin.Y,            0.0f, 0.0f,//bottom
                    end.X,      0,      end.Y,              1.0f, 0.0f,
                    end.X,      0,      end.Y - width,      1.0f, 1.0f,
                    begin.X,    0,      begin.Y - width,    0.0f, 1.0f,

                    begin.X,    heigh,  begin.Y,            0.0f, 0.0f,//top
                    end.X,      heigh,  end.Y,              1.0f, 0.0f,
                    end.X,      heigh,  end.Y - width,      1.0f, 1.0f,
                    begin.X,    heigh,  begin.Y - width,    0.0f, 1.0f
                };
                renderObject = new RenderObject(vertex, index);
            }
            else
            {
                float[] vertex = {
                    end.X,              0,      end.Y,      0.0f, 0.0f,//back
                    end.X + width,      0,      end.Y,      1.0f, 0.0f,
                    end.X + width,      heigh,  end.Y,      1.0f, 1.0f,
                    end.X,              heigh,  end.Y,      0.0f, 1.0f,

                    begin.X,            0,      begin.Y,    0.0f, 0.0f,//front
                    begin.X + width,    0,      begin.Y,    1.0f, 0.0f,
                    begin.X + width,    heigh,  begin.Y,    1.0f, 1.0f,
                    begin.X,            heigh,  begin.Y,    0.0f, 1.0f,

                    begin.X,            0,      begin.Y,    0.0f, 0.0f,//left
                    end.X,              0,      end.Y,      1.0f, 0.0f,
                    end.X,              heigh,  end.Y,      1.0f, 1.0f,
                    begin.X,            heigh,  begin.Y,    0.0f, 1.0f,

                    begin.X + width,    0,      begin.Y,    0.0f, 0.0f,//right
                    end.X + width,      0,      end.Y,      1.0f, 0.0f,
                    end.X + width,      heigh,  end.Y,      1.0f, 1.0f,
                    begin.X + width,    heigh,  begin.Y,    0.0f, 1.0f,

                    end.X,              0,      end.Y,      0.0f, 0.0f,//bottom
                    begin.X,            0,      begin.Y,    1.0f, 0.0f,
                    begin.X + width,    0,      begin.Y,    1.0f, 1.0f,
                    end.X + width,      0,      end.Y,      0.0f, 1.0f,

                    end.X,              heigh,  end.Y,      0.0f, 0.0f,//bottom
                    begin.X,            heigh,  begin.Y,    1.0f, 0.0f,
                    begin.X + width,    heigh,  begin.Y,    1.0f, 1.0f,
                    end.X + width,      heigh,  end.Y,      0.0f, 1.0f
                };
                renderObject = new RenderObject(vertex, index);
            }
            renderObject.setVertexCount(vertexCount);
        }
    }
}
