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

            float mayY = 0;
            float menY = 0;
            int v = 0;
            for (int i = 0; i < vertexIndex.Count; i++)
            {
                int vi = (int)vertexIndex[i];
                int ti = (int)textureIndex[i];
                vertex[v] = vertexList[vi].X - 0.8138175f;
                vertex[v + 1] = vertexList[vi].Y;
                vertex[v + 2] = vertexList[vi].Z - 0.1832345f; //radio = 3.9849515
                vertex[v + 3] = textureList[ti].X;
                vertex[v + 4] = textureList[ti].Y;
                if (mayY < vertex[v])
                    mayY = vertex[v];
                if (menY > vertex[v])
                    menY = vertex[v];
                v += 5;
            }
            System.Console.WriteLine("Mayor: " + mayY);
            System.Console.WriteLine("Menor: " + menY);
            /*vertex[v + 0] = -6.0f;
            vertex[v + 1] = 0.0f;
            vertex[v + 2] = 6.0f;
            vertex[v + 3] = 0.0f;
            vertex[v + 4] = 1.0f;

            vertex[v + 5] = 6.0f;
            vertex[v + 6] = 0.0f;
            vertex[v + 7] = 6.0f;
            vertex[v + 8] = 1.0f;
            vertex[v + 9] = 1.0f;

            vertex[v + 10] = 6.0f;
            vertex[v + 11] = 0.0f;
            vertex[v + 12] = -6.0f;
            vertex[v + 13] = 1.0f;
            vertex[v + 14] = 0.0f;
            
            vertex[v + 15] = 6.0f;
            vertex[v + 16] = 0.0f;
            vertex[v + 17] = -6.0f;
            vertex[v + 18] = 1.0f;
            vertex[v + 19] = 0.0f;

            vertex[v + 20] = -6.0f;
            vertex[v + 21] = 0.0f;
            vertex[v + 22] = -6.0f;
            vertex[v + 23] = 0.0f;
            vertex[v + 24] = 0.0f;

            vertex[v + 25] = -6.0f;
            vertex[v + 26] = 0.0f;
            vertex[v + 27] = 6.0f;
            vertex[v + 28] = 0.0f;
            vertex[v + 29] = 1.0f;*/
        }
    }
}
