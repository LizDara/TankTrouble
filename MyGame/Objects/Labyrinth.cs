using Grafica.Estructura;
using Grafica.MyGame.Parts;
using Grafica.LoadFiles;
using OpenTK;

namespace Grafica.MyGame.Objects
{
    class Labyrinth : Objeto
    {
        Parser parser;
        public Labyrinth()
        {
            key = "labyrinth";
            obj = false;
            parser = new Parser("Recursos/laberinto.txt");
            parser.findVertex();
            createPart();
        }

        public override void CalculateMatrix()
        {
            modelObject = Matrix4.CreateScale(scale) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.X)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Y)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z)) *
                Matrix4.CreateTranslation(translation);
        }

        public void createPart()
        {
            foreach (Vector2[] listVertex in parser.listVertex)
            {
                Wall wall = new Wall();
                wall.setVertex(listVertex[0], listVertex[1]);
                addPart("wall" + partCount, wall);
            }
        }

        public void addPart(string key, Wall part)
        {
            part.SetTexture("Recursos/pared8.jpg");
            parts.Add(key, part);
            partCount++;
        }
    }
}
