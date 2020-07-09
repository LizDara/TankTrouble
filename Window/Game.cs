using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Grafica.Rendering;
using OpenTK.Input;
using Grafica.MyGame;
using Grafica.Controllers;
using Grafica.MyGame.Objects;
using Grafica.Estructura;

namespace Grafica.Window
{
    class Game : GameWindow
    {
        GameScene scene;
        RenderFrame render;
        Controller controller;
        float speed = 1.5f;
        public Game(int width, int height, string title) : base(width, height, default, title)
        {
        }

        public void Init()
        {
            scene = new GameScene();
            render = new RenderFrame();
            controller = new Controller();
            scene.SetMatrixProjection(
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Width / Height, 0.1f, 50.0f));
            controller.addObject((Objeto)scene.objects["labyrinth"]);
            controller.addObject((Objeto)scene.objects["tank"]);
            controller.moveObject();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            Init();

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            render.Draw(scene);
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            controller.dispose();
            scene.shader.Dispose();

            base.OnUnload(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key.Equals(Key.Up))//0
            {
                System.Console.WriteLine("Down");
                controller.addKey(Key.Up);
            }

            if (e.Key.Equals(Key.Down))//1
            {
                controller.addKey(Key.Down);
            }

            if (e.Key.Equals(Key.Left))//2
            {
                controller.addKey(Key.Left);
            }

            if (e.Key.Equals(Key.Right))//3
            {
                controller.addKey(Key.Right);
            }

            if (e.Key.Equals(Key.Space))
            {
                //Shoot
            }
            
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (e.Key.Equals(Key.Up))//0
            {
                System.Console.WriteLine("Up");
                controller.deleteKey(Key.Up);
            }

            if (e.Key.Equals(Key.Down))//1
            {
                controller.deleteKey(Key.Down);
            }

            if (e.Key.Equals(Key.Right))//2
            {
                controller.deleteKey(Key.Right);
            }

            if (e.Key.Equals(Key.Left))//3
            {
                controller.deleteKey(Key.Left);
            }

            base.OnKeyUp(e);
        }
    }
}
