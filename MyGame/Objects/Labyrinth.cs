using Grafica.Estructura;
using Grafica.MyGame.Parts;
using Grafica.LoadFiles;
using OpenTK;
using System.Collections.Generic;
using Grafica.Rendering;

namespace Grafica.MyGame.Objects
{
    class Labyrinth : Objeto
    {
        Parser parser;
        public List<Vector2[]> listVertex;
        public float sizeX;
        public float sizeZ;
        public float firstPositionX;
        public float firstPositionZ;
        public float secondPositionX;
        public float secondPositionZ;
        public Labyrinth()
        {
            key = "labyrinth";
            obj = false;
            texture = new Texture("Recursos/plomo4.jpg");
            parser = new Parser("Recursos/laberinto.txt");
            parser.loadVertex();
            sizeX = parser.sizeX;
            sizeZ = parser.sizeZ;
            firstPositionX = parser.firstPositionX;
            firstPositionZ = parser.firstPositionZ;
            secondPositionX = parser.secondPositionX;
            secondPositionZ = parser.secondPositionZ;
            listVertex = parser.listVertex;
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
            foreach (Vector2[] pairVertex in parser.listVertex)
            {
                Wall wall = new Wall();
                wall.setVertex(pairVertex[0], pairVertex[1]);
                addPart("wall" + partCount, wall);
            }
        }

        public void addPart(string key, Wall part)
        {
            parts.Add(key, part);
            partCount++;
        }
    }
}
