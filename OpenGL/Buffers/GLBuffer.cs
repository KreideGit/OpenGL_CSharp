namespace OpenGL.Buffers
{
    public class GLBuffer
    { 
        public uint ID { get; }

        private BufferTarget target;

        public GLBuffer()
        {
            ID = Gl.GenBuffer();
        }

        public void Bind(BufferTarget target)
        {
            this.target = target;
            Gl.BindBuffer(target, ID);
        }

        public void Unbind()
        {
            Gl.BindBuffer(target, 0);
        }

        public void Buffer(object data, int size, BufferUsage usage)
        {
            Gl.BufferData(target, (uint)size, data, usage);
        }
    }
}