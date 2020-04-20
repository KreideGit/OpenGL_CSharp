using OpenGL.Vertices;

namespace OpenGL.Shaders
{
    public class GLShaderProgram
    {
        public uint ID { get; }
        public GLShader[] Shaders { get; }
        public GLVertexAttribute[] Attributes { get; }

        public GLShaderProgram(ShaderData[] shaderData, VertexAttributeData[] vertexAttributeData)
        {
            ID = Gl.CreateProgram();

            Shaders = new GLShader[shaderData.Length];

            for(int i = 0; i < Shaders.Length; i++)
            {
                Shaders[i] = new GLShader(shaderData[i]);
                Gl.AttachShader(ID, Shaders[i].ID);
            }

            Attributes = new GLVertexAttribute[vertexAttributeData.Length];

            for(int i = 0; i < vertexAttributeData.Length; i++)
            {
                Attributes[i] = new GLVertexAttribute(ID, (uint)i, vertexAttributeData[i]);
            }

            Gl.LinkProgram(ID);
            Errors.CheckProgramError(ID, ProgramProperty.LinkStatus);
            
            Gl.ValidateProgram(ID);
            Errors.CheckProgramError(ID, ProgramProperty.ValidateStatus);
        }

        ~GLShaderProgram()
        {
            Unbind();

            for(int i = 0; i < Shaders.Length; i++)
            {
                Gl.DetachShader(ID, Shaders[i].ID);
                Gl.DeleteShader(Shaders[i].ID);
            }

            Gl.DeleteProgram(ID);
        }

        public void Bind()
        {
            Gl.UseProgram(ID);
        }

        public void Unbind()
        {
            Gl.UseProgram(0);
        }
    }
}