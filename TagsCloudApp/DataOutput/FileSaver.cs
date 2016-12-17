using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudApp
{
    public class FileSaver : IOutputStream
    {
        private readonly string path;

        public FileSaver(string path)
        {
            this.path = path;
        }

        public void SaveData(Image image)
        {
            image.Save(path, ImageFormat.Png);
        }
    }
}
