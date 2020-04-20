using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenGL.Textures
{
    public class GLTexture2D
    {
        public uint ID { get; }

        public GLTexture2D(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            ID = Gl.GenTexture();
            Bind(0);

            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMagFilter.Linear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);

            Gl.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, imageData.Scan0);

            Unbind();
            image.Dispose();
        }

        ~GLTexture2D()
        {
            Unbind();
            Gl.DeleteTextures(ID);
        }

        public void Bind(int unit)
        {
            if(unit < 0 || unit > 31)
            {
                throw new Exception("Unit is out of range.");
            }
            Gl.ActiveTexture(TextureUnit.Texture0 + unit);
            Gl.BindTexture(TextureTarget.Texture2d, ID);
        }

        public void Unbind()
        {
            Gl.BindTexture(TextureTarget.Texture2d, 0);
        }
    }
}