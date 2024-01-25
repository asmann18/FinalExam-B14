using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinalExam_B14.Utilities
{
    public static class IFormFileExtension
    {
        public async static Task<string> CreateFile(this IFormFile file, params string[] files)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf('.'));


            string path = "";
            foreach (string filename in files)
            {
                path = Path.Combine(path, filename);
            }
            path = Path.Combine(path, fileName);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public static bool ValidateType(this IFormFile file, string type = "image")
        {
            return file.ContentType.Contains(type);

        }

        public static bool ValidateSize(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1024;

        }
    }
}
