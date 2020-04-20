namespace OpenGL.Shaders
{
    public class ShaderData
    {
        public string FileName { get; }
        public ShaderType Type { get; }

        public ShaderData(string fileName, ShaderType type)
        {
            FileName = fileName;
            Type = type;
        }
    }
}