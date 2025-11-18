using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Extend
{
    public static class StreamExtend
    {
        public static void CopyToFile(this Stream fs, string dest, int bufferSize = 8388608)
        {
            using FileStream fsWrite = new(dest, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] buf = new byte[bufferSize];
            int len;
            while ((len = fs.Read(buf, 0, buf.Length)) != 0)
            {
                fsWrite.Write(buf, 0, len);
            }
        }

        public static void SaveFile(this MemoryStream ms, string filename)
        {
            using FileStream fs = new(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] buffer = ms.ToArray();
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
        }

        public static string GetFileMD5(this FileStream fs)
        {
            return HashFile(fs, "md5");
        }

        public static string GetFileSha1(this Stream fs)
        {
            return HashFile(fs, "sha1");
        }

        private static string HashFile(Stream fs, string algo)
        {
            byte[] hash;
            using (HashAlgorithm hashAlgorithm = algo.ToLower() == "sha1" ? SHA1.Create() : MD5.Create())
            {
                hash = hashAlgorithm.ComputeHash(fs);
            }

            StringBuilder sb = new();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
