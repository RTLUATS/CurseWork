using System;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;

namespace CurseWork
{
    internal static class WorkWithImage
    {
        internal static BitmapImage LoadImageFromPc()
        {
            var dialog = new OpenFileDialog();

            dialog.InitialDirectory = "C:\\";
            dialog.Filter = "Image File |*.jpg;*.jpeg;*.png";
            dialog.CheckFileExists = true;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == true)
            {
                if (dialog.OpenFile() != null)
                {
                    return new BitmapImage(new Uri(dialog.FileName));
                }
            }

            return null;
        }

        internal static byte[] ConverImageToArrayByte(ImageSource img)
        {
            var bmp = img as BitmapImage;
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(memStream);
            
            return memStream.ToArray();
        }

        internal static ImageSource ConvertArrayByteToImage(byte[] imageInByte)
        {
            using (var ms = new MemoryStream(imageInByte))
            {
                var image = new BitmapImage();

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;   
            }
        }

        
    }
}
