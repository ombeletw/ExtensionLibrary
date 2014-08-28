using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExtensionLibrary.ByteArray
{
    public static class ByteArrayExtensions
    {
        public static byte[] StreamToByteArray(Stream InputStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                InputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static bool SaveFiletoDisk(byte[] FileBuffer, string Path)
        {
            bool response = false;

            try
            {
                FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(FileBuffer);
                bw.Close();
                response = true;
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
            }

            return response;
        }
    }
}
