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
        public ObjLoader(string path)
        {
            file = new StreamReader(path);
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
                            //if (comps.Length > 1 && comps[1].Length != 0)
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
            System.Console.WriteLine("vertex: " + vertex.Length);

            int v = 0;
            for (int i = 0; i < vertexIndex.Count; i++)
            {
                int vi = (int)vertexIndex[i];
                int ti = (int)textureIndex[i];
                vertex[v] = vertexList[vi].X;
                vertex[v + 1] = vertexList[vi].Y;
                vertex[v + 2] = vertexList[vi].Z;
                vertex[v + 3] = textureList[ti].X;
                vertex[v + 4] = textureList[ti].Y;
                v += 5;
            }
            System.Console.WriteLine(vertex[0]);
            System.Console.WriteLine(vertex[1]);
            System.Console.WriteLine(vertex[2]);
            System.Console.WriteLine(vertex[3]);
            System.Console.WriteLine(vertex[4]);
            System.Console.WriteLine(vertex[5]);
            System.Console.WriteLine(vertex[6]);
            System.Console.WriteLine(vertex[7]);
            System.Console.WriteLine(vertex[8]);
            System.Console.WriteLine(vertex[9]);
        }
    }
}
