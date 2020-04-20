using System;
using System.Text;

namespace OpenGL
{
    public static class Errors
    {
        public static void CheckProgramError(uint programID, ProgramProperty property)
        {
            Gl.GetProgram(programID, property, out int success);

            if (success == 0)
            {
                StringBuilder error = new StringBuilder(1024);
                Gl.GetProgramInfoLog(programID, 1024, out _, error);
                Console.WriteLine(error);
            }
        }

        public static void CheckShaderError(uint shaderID, ShaderParameterName parameter)
        {
            Gl.GetShader(shaderID, parameter, out int success);

            if (success == 0)
            {
                StringBuilder error = new StringBuilder(1024);
                Gl.GetShaderInfoLog(shaderID, 1024, out _, error);
                Console.WriteLine(error);
            }
        }
    }
}