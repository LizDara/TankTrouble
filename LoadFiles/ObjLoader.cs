using System.Collections.Generic;
using System.IO;
using OpenTK;

namespace Grafica.LoadFiles
{
    class ObjLoader
    {
        StreamReader file;
        List<Vector3> vertexList = new List<Vector3>();
        List<Vector2> textureList = new List<Vector2>();
        public List<uint> vertexIndex = new List<uint>();
        List<uint> textureIndex = new List<uint>();
        public float[] vertex;
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;
        public ObjLoader(string path)
        {
            file = new StreamReader(path);
        }

        public void center(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void objLoad()
        {
            while (!file.EndOfStream)
            {
                List<string> words = new List<string>(file.ReadLine().ToLower().Split(' '));
                words.RemoveAll(s => s == string.Empty);

                if (words.Count == 0)
                    continue;

                string type = words[0];
                words.RemoveAt(0);

                switch (type)
                {
                    case "v":
                        vertexList.Add(
                            new Vector3(
                                float.Parse(words[0], System.Globalization.CultureInfo.InvariantCulture),
                                float.Parse(words[1], System.Globalization.CultureInfo.InvariantCulture),
                                float.Parse(words[2], System.Globalization.CultureInfo.InvariantCulture)));
                        break;
                    case "vt":
                        textureList.Add(
                            new Vector2(
                                float.Parse(words[0], System.Globalization.CultureInfo.InvariantCulture),
                                float.Parse(words[1], System.Globalization.CultureInfo.InvariantCulture)));
                        break;
                    case "f":
                        foreach (string w in words)
                        {
                            if (w.Length == 0)
                                continue;
                            string[] comps = w.Split('/');

                            vertexIndex.Add(uint.Parse(comps[0]) - 1);

                            textureIndex.Add(uint.Parse(comps[1]) - 1);
                        }
                        break;
                    default:
                        break;
                }
            }
            processData();
        }

        public void processData()
        {
            vertex = new float[(vertexIndex.Count * 3) + (textureIndex.Count * 2)];

            float mayX = 0;
            float menX = 0;
            float mayY = 0;
            float menY = 0;
            float mayZ = 0;
            float menZ = 0;
            int v = 0;
            for (int i = 0; i < vertexIndex.Count; i++)
            {
                int vi = (int)vertexIndex[i];
                int ti = (int)textureIndex[i];
                vertex[v] = vertexList[vi].X - x;
                vertex[v + 1] = vertexList[vi].Y - y;
                vertex[v + 2] = vertexList[vi].Z - z;
                vertex[v + 3] = textureList[ti].X;
                vertex[v + 4] = textureList[ti].Y;

                if (mayX < vertex[v])
                    mayX = vertex[v];
                if (menX > vertex[v])
                    menX = vertex[v];

                if (mayY < vertex[v + 1])
                    mayY = vertex[v + 1];
                if (menY > vertex[v + 1])
                    menY = vertex[v + 1];
                
                if (mayZ < vertex[v + 2])
                    mayZ = vertex[v + 2];
                if (menZ > vertex[v + 2])
                    menZ = vertex[v + 2];
                v += 5;
            }
            System.Console.WriteLine("MayX: " + mayX);
            System.Console.WriteLine("MenX: " + menX);
            System.Console.WriteLine("MayY: " + mayY);
            System.Console.WriteLine("MenY: " + menY);
            System.Console.WriteLine("MayZ: " + mayZ);
            System.Console.WriteLine("MenZ: " + menZ);
        }
    }
}
