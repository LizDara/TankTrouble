using Grafica.Estructura;
using Grafica.MyGame;
using OpenTK;

namespace Grafica.Rendering
{
    class RenderFrame
    {
        public RenderFrame()
        {

        }
        public void Draw(Escenario scene)
        {
            scene.shader.Use();
            scene.CalculateViewProjection();
            scene.CalculateMatrix();
            foreach (Objeto objectScene in scene.objects.Values)
            {
                if (objectScene.draw)
                {
                    objectScene.CalculateMatrix();
                    objectScene.texture.Use();
                    foreach (Parte partObject in objectScene.parts.Values)
                    {
                        partObject.CalculateMatrix();
                        partObject.renderObject.bind();
                        Matrix4 matrix =
                            partObject.model * objectScene.modelObject * scene.modelScene * scene.viewProjection;
                        scene.shader.SetMatrix4("projection", matrix);
                        partObject.renderObject.render(objectScene.obj);
                    }
                }
            }
        }
    }
}
