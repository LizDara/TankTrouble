using OpenTK;
using System.IO;
using System.Collections.Generic;

namespace Grafica.LoadFiles
{
    public class Parser
    {
        string text;
        StreamReader file;
        public List<Vector2[]> listVertex;
        public int count;
        public Parser(string path)
        {
            text = "Nothing";
            file = new StreamReader(path);
            text = file.ReadLine();
            file.Close();
            count = 0;
            listVertex = new List<Vector2[]>();
        }

        public void findVertex()
        {
            int indexBegin = 0;
            int indexEnd;
            while (text.IndexOf('(', indexBegin) != -1)
            {
                indexBegin = text.IndexOf('(', indexBegin);
                indexEnd = text.IndexOf(')', indexBegin);
                setVertex(text.Substring(indexBegin + 1, indexEnd - indexBegin - 1));
                indexBegin = indexEnd;
                count++;
            }
        }

        public void setVertex(string vertexText)
        {
            Vector2 begin = Vector2.Zero;
            Vector2 end = Vector2.Zero;
            int indexBegin = 0;
            int indexEnd;
            int countPoint = 0;
            float x = 0;
            float z = 0;
            while (countPoint < 4)
            {
                if (countPoint == 3)
                    indexEnd = vertexText.Length;
                else
                    indexEnd = vertexText.IndexOf(',', indexBegin);

                if (countPoint == 0 || countPoint == 2)
                    x = float.Parse(vertexText.Substring(indexBegin, indexEnd - indexBegin), System.Globalization.CultureInfo.InvariantCulture);
                if (countPoint == 1 || countPoint == 3)
                    z = float.Parse(vertexText.Substring(indexBegin, indexEnd - indexBegin), System.Globalization.CultureInfo.InvariantCulture);

                countPoint++;
                indexBegin = indexEnd + 1;

                if (countPoint == 2)
                {
                    begin = new Vector2(x, z);
                }
                if (countPoint == 4)
                {
                    end = new Vector2(x, z);
                }
            }
            listVertex.Add(new Vector2[] { begin, end });
        }
    }
}
