using OpenGL.Vertices;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OpenGL.Meshes;

namespace OpenGL.ModelLoaders
{
    public static class ObjLoader
    {
        public static Mesh[] Load(string fileName, GLVertexAttribute[] attributes)
        {
            string[] content = File.ReadAllLines(fileName);
            string[] parts = null;
            VertexAttributeData[] vertexAttributeData = attributes.Select(x => x.Data).ToArray();

            List<float[]> positions = new List<float[]>();
            List<float[]> textureCoordinates = new List<float[]>();
            List<float[]> normals = new List<float[]>();

            string[] positionLines = content.Where(x => x.StartsWith("v ")).ToArray();
            string[] textureCoordinateLines = content.Where(x => x.StartsWith("vt")).ToArray();
            string[] normalLines = content.Where(x => x.StartsWith("vn")).ToArray();

            for (int i = 0; i < positionLines.Length; i++)
            {
                parts = positionLines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                positions.Add(new float[] { float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]) });
            }

            for (int i = 0; i < textureCoordinateLines.Length; i++)
            {
                parts = textureCoordinateLines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                textureCoordinates.Add(new float[] { float.Parse(parts[1]), float.Parse(parts[2]) });
            }

            for (int i = 0; i < normalLines.Length; i++)
            {
                parts = normalLines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                normals.Add(new float[] { float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]) });
            }

            List<Mesh> meshes = new List<Mesh>();
            List<Vertex> vertices;
            int counter2 = 0;
            content = content.Where(x => !(x.StartsWith("s") || x.StartsWith("#"))).ToArray();

            for (int i = 0; i < content.Length; i++)
            {
                string[] previousParts = i > 0 ? content[i - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries) : new string[] { "" };
                string[] currentParts = content[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if(previousParts.Length == 0)
                {
                    previousParts = new string[] { "" };
                }

                if(currentParts.Length == 0)
                {
                    currentParts = new string[] { "" };
                }

                if (i == 0 && currentParts[0].StartsWith("f") || !previousParts[0].StartsWith("f") && currentParts[0].StartsWith("f"))
                {
                    vertices = new List<Vertex>();

                    int counter = i;
                    while (content[counter].StartsWith("f"))
                    {
                        parts = content[counter].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        for(int j = 0; j < parts.Length - 1; j++)
                        {
                            Vertex v = new Vertex(vertexAttributeData);
                            string[] indices = parts[j + 1].Split("/", StringSplitOptions.RemoveEmptyEntries);
                            v.Data["position"] = positions[int.Parse(indices[0]) - 1];
                            if(indices.Length > 1)
                                v.Data["texCoord"] = textureCoordinates[int.Parse(indices[1]) - 1];
                            
                            vertices.Add(v);
                        }

                        if(++counter == content.Length)
                        {
                            break;
                        }
                    }

                    counter2 += vertices.Count;
                    meshes.Add(new Mesh(vertices.ToArray(), attributes, (parts.Length - 1 == 3) ? PrimitiveType.Triangles : PrimitiveType.Quads));
                }
            }

            Console.WriteLine(counter2);
            return meshes.ToArray();
        }
    }
}