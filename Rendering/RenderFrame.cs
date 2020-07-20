using Grafica.Estructura;
using Grafica.MyGame;
using OpenTK;
using System.Collections.Generic;

namespace Grafica.Rendering
{
    class RenderFrame
    {
        List<string> deleteKey;
        public RenderFrame()
        {

        }
        public void Draw(GameScene scene)
        {
            if(!scene.finish)
            {
                scene.shader.Use();
                scene.CalculateViewProjection();
                scene.CalculateMatrix();
                deleteKey = new List<string>();
                foreach (Objeto objectScene in scene.objects.Values)
                {
                    if (objectScene.draw)
                    {
                        objectScene.CalculateMatrix();
                        objectScene.texture.Use();
                        foreach (Parte partObject in objectScene.parts.Values)
                        {
                            if (partObject.draw)
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
                    else
                    {
                        deleteKey.Add(objectScene.key);
                    }
                }
                foreach (string key in deleteKey)
                {
                    scene.objects.Remove(key);
                }
            }
        }
    }
}
