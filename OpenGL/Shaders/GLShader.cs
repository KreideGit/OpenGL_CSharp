using System;

namespace OpenGL.Shaders
{
    public class GLShader
    {
        public uint ID { get; }
        public ShaderData Data { get; }

        public GLShader(ShaderData data)
        {
            Data = data;

            string content = Misc.GetFileContentAsString(Data.FileName);
            ID = Gl.CreateShader(Data.Type);

            if (ID == 0)
            {
                throw new Exception("Shader creation failed.");
            }

            Gl.ShaderSource(ID, new string[] { content });

            Gl.CompileShader(ID);
            Errors.CheckShaderError(ID, ShaderParameterName.CompileStatus);
        }
    }
}