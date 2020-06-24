using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Grafica.Rendering;
using OpenTK.Input;
using Grafica.MyGame;

namespace Grafica.Window
{
    class Game : GameWindow
    {
        GameScene scene;
        RenderFrame render;
        float speed = 1.5f;
        public Game(int width, int height, string title) : base(width, height, default, title)
        {
        }

        public void Init()
        {
            scene = new GameScene();
            render = new RenderFrame();
            scene.SetMatrixProjection(
                Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Width / Height, 0.1f, 50.0f));
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

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            scene.shader.Dispose();

            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
                return;

            KeyboardState input = OpenTK.Input.Keyboard.GetState();

            if (input.IsKeyDown(Key.W))
            {
                scene.moveTank(new Vector3(0.0f, 0.0f, 0.1f), "t");
                //scene.position += scene.front * speed * (float)e.Time; //Forward
            }

            if (input.IsKeyDown(Key.S))
            {
                scene.moveTank(new Vector3(0.0f, 0.0f, -0.1f), "t");
                //scene.position -= scene.front * speed * (float)e.Time; //Backwards
            }

            if (input.IsKeyDown(Key.A))
            {
                scene.moveTank(new Vector3(0.0f, 5.0f, 0.0f), "r");
                //scene.position -= Vector3.Normalize(Vector3.Cross(scene.front, scene.up)) * speed * (float)e.Time; //Left
            }

            if (input.IsKeyDown(Key.D))
            {
                scene.moveTank(new Vector3(0.0f, -5.0f, 0.0f), "r");
                //scene.position += Vector3.Normalize(Vector3.Cross(scene.front, scene.up)) * speed * (float)e.Time; //Right
            }

            if (input.IsKeyDown(Key.Space))
            {
                scene.position += scene.up * speed * (float)e.Time; //Up
            }

            if (input.IsKeyDown(Key.LShift))
            {
                scene.position -= scene.up * speed * (float)e.Time; //Down
            }

            base.OnUpdateFrame(e);
        }
    }
}
