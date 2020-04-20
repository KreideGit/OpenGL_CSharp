using OpenGL.Meshes;
using OpenGL.Shaders;
using OpenGL.Textures;
using OpenGL.Vertices;
using OpenGL.ModelLoaders;

using GLFW;

namespace OpenGL
{
    class Program
    {
        public static void Main(string[] args)
        {
            Window window;

            Gl.Initialize();
            if(!Glfw.Init())
            {
                return;
            }

            window = Glfw.CreateWindow(1000, 1000, "Scene", Monitor.None, Window.None);
            
            if(window == null)
            {
                Glfw.Terminate();
                return;
            }

            Glfw.MakeContextCurrent(window);

            ShaderData[] shaderData = new ShaderData[]
            {
                new ShaderData(@"shaders/fragment.shader", ShaderType.FragmentShader),
                new ShaderData(@"shaders/vertex.shader", ShaderType.VertexShader)
            };

            VertexAttributeData[] vertexAttributeData = new VertexAttributeData[]
            {
                new VertexAttributeData("position", RepresentingType.Position, AttributeType.FloatVec3, VertexAttribType.Float),
                new VertexAttributeData("texCoord", RepresentingType.TextureCoordinate, AttributeType.FloatVec2, VertexAttribType.Float),
                new VertexAttributeData("normals", RepresentingType.Normal, AttributeType.FloatVec3, VertexAttribType.Float),
            };

            GLShaderProgram shaderProgram = new GLShaderProgram(shaderData, vertexAttributeData);

            GLTexture2D[] textures = new GLTexture2D[]
            {
                new GLTexture2D(@"textures/ak/texture.png"),
                new GLTexture2D(@"textures/ak/texture.png"),
                new GLTexture2D(@"textures/ak/texture.png"),
                new GLTexture2D(@"textures/ak/texture.png"),
            };

            Mesh[] meshes = ObjLoader.Load(@"models/akm.obj", shaderProgram.Attributes);

            float angleX = 0;
            float angleY = 0;
            float scale = 1.0f / 10.0f;

            Gl.Enable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Front);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Gl.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            Glfw.SetMouseButtonCallback(window, (w, button, action, mods) =>
            {
                if(action == InputState.Press)
                {
                    if(button == MouseButton.Left)
                    {
                        scale = scale + scale / 10;
                    }
                    else if(button == MouseButton.Right)
                    {
                        scale = scale - scale / 10;
                    }
                }
            });
            Glfw.SetKeyCallback(window, (w, key, scancode, action, mods) =>
            {
                if(action == InputState.Press)
                {
                    if(key == Keys.A)
                    {
                        angleY -= 15;
                    }
                    else if(key == Keys.D)
                    {
                        angleY += 15;
                    }
                    else if(key == Keys.S)
                    {
                        angleX += 15;
                    }
                    else if(key == Keys.W)
                    {
                        angleX -= 15;
                    }
                }
            });

            shaderProgram.Bind();

            while (!Glfw.WindowShouldClose(window))
            {
                Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                shaderProgram.Bind();
                for(int i = 0; i < meshes.Length; i++)
                {
                    textures[i].Bind(0);
                    meshes[i].Draw();
                }

                Gl.UniformMatrix4(0, false, (float[])(Matrix4x4f.RotatedY(angleY) * Matrix4x4f.RotatedX(angleX) * Matrix4x4f.Scaled(scale, scale, scale) * Matrix4x4f.Translated(0.0f, 0.0f, 0.0f)));

                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }
    }
}