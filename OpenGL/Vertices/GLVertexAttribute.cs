using System;

namespace OpenGL.Vertices
{
    public class GLVertexAttribute
    {
        public uint Index { get; }
        public VertexAttributeData Data { get; }

        public GLVertexAttribute(uint programID, uint index, VertexAttributeData data)
        {
            Index = index;
            Data = data;
            Gl.BindAttribLocation(programID, index, Data.Name);
        }

        public void Bind()
        {
            Gl.EnableVertexAttribArray(Index);
        }

        public void Unbind()
        {
            Gl.EnableVertexAttribArray(0);
        }

        public void SetData(int length, VertexAttribType dataType, bool normalize, int elementOffset, IntPtr initalOffset)
        {
            Gl.VertexAttribPointer(Index, length, dataType, normalize, elementOffset, initalOffset);
        }
    }
}