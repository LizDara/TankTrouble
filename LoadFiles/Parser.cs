using OpenTK;
using System.IO;
using System.Collections.Generic;

namespace Grafica.LoadFiles
{
    public class Parser
    {
        StreamReader file;
        public List<Vector2[]> listVertex;
        public float sizeX;
        public float sizeZ;
        public float firstPositionX;
        public float firstPositionZ;
        public float secondPositionX;
        public float secondPositionZ;
        public Parser(string path)
        {
            file = new StreamReader(path);
            listVertex = new List<Vector2[]>();
        }

        public void loadVertex()
        {
            while (!file.EndOfStream)
            {
                List<string> vertex = new List<string>(file.ReadLine().Split(','));
                if (vertex[0].Equals("s"))
                {
                    sizeX = float.Parse(vertex[1], System.Globalization.CultureInfo.InvariantCulture);
                    sizeZ = float.Parse(vertex[2], System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (vertex[0].Equals("p1"))
                {
                    firstPositionX = float.Parse(vertex[1], System.Globalization.CultureInfo.InvariantCulture);
                    firstPositionZ = float.Parse(vertex[2], System.Globalization.CultureInfo.InvariantCulture);
                }
                else if (vertex[0].Equals("p2"))
                {
                    secondPositionX = float.Parse(vertex[1], System.Globalization.CultureInfo.InvariantCulture);
                    secondPositionZ = float.Parse(vertex[2], System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    Vector2 begin = new Vector2(
                        float.Parse(vertex[0], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(vertex[1], System.Globalization.CultureInfo.InvariantCulture));
                    Vector2 end = new Vector2(
                        float.Parse(vertex[2], System.Globalization.CultureInfo.InvariantCulture),
                        float.Parse(vertex[3], System.Globalization.CultureInfo.InvariantCulture));
                    listVertex.Add(new Vector2[] { begin, end });
                }
            }
        }
    }
}
