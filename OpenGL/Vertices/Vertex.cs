using System.Collections.Generic;

namespace OpenGL.Vertices
{
    public class Vertex
    {
        public Dictionary<string, float[]> Data { get; } 

        public Vertex(VertexAttributeData[] attributes)
        {
            Data = new Dictionary<string, float[]>();

            for(int i = 0; i < attributes.Length; i++)
            {
                Data.Add(attributes[i].Name, new float[attributes[i].DataLength]);
            }
        }
    }
}