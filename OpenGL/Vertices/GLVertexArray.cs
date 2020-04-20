namespace OpenGL.Vertices
{
    public class GLVertexArray
    {
        public uint ID { get; }

        public GLVertexArray()
        {
            ID = Gl.GenVertexArray();
        }

        public void Bind()
        {
            Gl.BindVertexArray(ID);
        }

        public void Unbind()
        {
            Gl.BindVertexArray(0);
        }
    }
}