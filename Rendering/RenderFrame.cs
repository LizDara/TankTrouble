using Grafica.Estructura;
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
                objectScene.CalculateMatrix();
                foreach (Parte partObject in objectScene.parts.Values)
                {
                    partObject.CalculateMatrix();
                    partObject.renderObject.bind();
                    Matrix4 matrix = 
                        partObject.model * objectScene.modelObject * scene.modelScene * scene.viewProjection;
                    partObject.texture.Use();
                    scene.shader.SetMatrix4("projection", matrix);
                    partObject.renderObject.render(objectScene.obj);
                }
            }
        }
    }
}
