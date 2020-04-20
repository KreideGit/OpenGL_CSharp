using OpenGL.Buffers;
using OpenGL.Vertices;

using System;
using System.Collections.Generic;

namespace OpenGL.Meshes
{
    public class Mesh
    {
        public GLVertexArray VertexArray { get; }
        public PrimitiveType RenderMode { get; set; }

        private readonly int drawCount;
        private readonly Action drawAction;
        private List<GLBuffer> buffers;

        public Mesh(Vertex[] vertices, GLVertexAttribute[] attributes, PrimitiveType renderMode)
        {
            drawCount = vertices.Length;
            RenderMode = renderMode;

            VertexArray = new GLVertexArray();
            VertexArray.Bind();

            InitalizeModel(vertices, attributes);

            drawAction = DrawArrays;
            VertexArray.Unbind();
        }

        public Mesh(Vertex[] vertices, uint[] indices, GLVertexAttribute[] attributes)
        {
            drawCount = indices.Length;
            RenderMode = PrimitiveType.Triangles;

            VertexArray = new GLVertexArray();
            VertexArray.Bind();

            InitalizeModel(vertices, attributes);

            GLBuffer indexBuffer = new GLBuffer();
            indexBuffer.Bind(BufferTarget.ElementArrayBuffer);
            indexBuffer.Buffer(indices, indices.Length * sizeof(uint), BufferUsage.StaticDraw);

            buffers.Add(indexBuffer);

            drawAction = DrawElements;
            VertexArray.Unbind();
        }

        ~Mesh()
        {
            VertexArray.Unbind();
            Gl.DeleteVertexArrays(VertexArray.ID);

            for (int i = 0; i < buffers.Count; i++)
            {
                buffers[i].Unbind();
                Gl.DeleteBuffers(buffers[i].ID);
            }
        }

        private void InitalizeModel(Vertex[] vertices, GLVertexAttribute[] attributes)
        {
            buffers = new List<GLBuffer>();

            for (int i = 0; i < attributes.Length; i++)
            {
                int dataLength = vertices[i].Data[attributes[i].Data.Name].Length;
                float[] totalData = new float[dataLength * vertices.Length];

                for (int j = 0; j < vertices.Length; j++)
                {
                    float[] vertexData = vertices[j].Data[attributes[i].Data.Name];
                    for (int k = 0; k < vertexData.Length; k++)
                    {
                        totalData[j * vertexData.Length + k] = vertexData[k];
                    }
                }

                GLBuffer vertexBuffer = new GLBuffer();
                vertexBuffer.Bind(BufferTarget.ArrayBuffer);
                vertexBuffer.Buffer(totalData, totalData.Length * sizeof(float), BufferUsage.StaticDraw);

                attributes[i].Bind();
                attributes[i].SetData(dataLength, attributes[i].Data.DataType, false, 0, IntPtr.Zero);

                buffers.Add(vertexBuffer);
            }
        }

        public void Draw()
        {
            VertexArray.Bind();
            drawAction();
            VertexArray.Unbind();
        }

        private void DrawArrays()
        {
            Gl.DrawArrays(RenderMode, 0, drawCount);
        }

        private void DrawElements()
        {
            Gl.DrawElements(RenderMode, drawCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
    }
}