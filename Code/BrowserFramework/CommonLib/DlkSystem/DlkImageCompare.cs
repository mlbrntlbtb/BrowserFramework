using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace CommonLib.DlkSystem
{
    /// <summary>
    /// This class is used to compare images
    /// </summary>
    public class DlkImageCompare
    {
        /// <summary>
        /// A record holding image comparison data
        /// </summary>
        public struct ImageCompareRecord
        {
            public int iTotalPixels;
            public int iMismatchPixels;
            public double PctOfMismatch;
        }

        /// <summary>
        /// result definitions
        /// </summary>
        public enum CompareResult
        {
            ciCompareOk,
            ciPixelMismatch,
            ciSizeMismatch
        };

        /// <summary>
        /// Compares the image using a fast hash technique and then if failure is found by pixel
        /// </summary>
        /// <param name="mMasterFile"></param>
        /// <param name="mCompareFile"></param>
        /// <param name="mOutputFileOfPixelDiffs"></param>
        /// <returns></returns>
        public static CompareResult Compare(String mMasterFile, String mCompareFile, String mOutputFileOfPixelDiffs)
        {
            CompareResult cr = CompareImagesByHash(mMasterFile, mCompareFile);
            if (cr == CompareResult.ciPixelMismatch)
            {
                ImageCompareRecord icr = CompareImagesByPixel(mMasterFile, mCompareFile, mOutputFileOfPixelDiffs);
                DlkLogger.LogData(
                    "Pixel Comparison Results : Acceptable Percentage of Error from Config: " + 
                    DlkEnvironment.mImageCapturePixelMismatchThreshold.ToString() + 
                    ", Actual Percentage of Error: " + icr.PctOfMismatch.ToString()
                    );
                if (DlkEnvironment.mImageCapturePixelMismatchThreshold > icr.PctOfMismatch)
                {
                    cr = CompareResult.ciCompareOk;
                }
            }
            return cr;
        }

        /// <summary>
        /// compares images using a hash technique
        /// </summary>
        /// <param name="mMasterFile"></param>
        /// <param name="mCompareFile"></param>
        /// <returns></returns>
        private static CompareResult CompareImagesByHash(String mMasterFile, String mCompareFile)
        {
            // a fast way to compare 2 images

            Bitmap bmp1 = new Bitmap(mMasterFile);
            Bitmap bmp2 = new Bitmap(mCompareFile);

            CompareResult cr = CompareResult.ciCompareOk;

            //Test to see if we have the same size of image
            if (bmp1.Size != bmp2.Size)
            {
                cr = CompareResult.ciSizeMismatch;
            }
            else
            {
                //Convert each image to a byte array
                System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
                byte[] btImage1 = new byte[1];
                btImage1 = (byte[])ic.ConvertTo(bmp1, btImage1.GetType());
                byte[] btImage2 = new byte[1];
                btImage2 = (byte[])ic.ConvertTo(bmp2, btImage2.GetType());

                //Compute a hash for each image
                SHA256Managed shaM = new SHA256Managed();
                byte[] hash1 = shaM.ComputeHash(btImage1);
                byte[] hash2 = shaM.ComputeHash(btImage2);

                //Compare the hash values
                for (int i = 0; i < hash1.Length && i < hash2.Length && cr == CompareResult.ciCompareOk; i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        cr = CompareResult.ciPixelMismatch;
                    }
                }
            }
            bmp1.Dispose();
            bmp2.Dispose();
            return cr;
        }

        /// <summary>
        /// compares images using a pixel techqique
        /// </summary>
        /// <param name="mMasterFile"></param>
        /// <param name="mCompareFile"></param>
        /// <param name="OutputFilePng"></param>
        /// <returns></returns>
        private static ImageCompareRecord CompareImagesByPixel(String mMasterFile, String mCompareFile, String OutputFilePng)
        {
            ImageCompareRecord icr = new ImageCompareRecord();
            icr.iTotalPixels = 0;
            icr.iMismatchPixels = 0;
            icr.PctOfMismatch = 0.0;

            Bitmap bmp1 = new Bitmap(mMasterFile);
            Bitmap bmp2 = new Bitmap(mCompareFile);
            Bitmap bmpResult = new Bitmap(bmp1.Width, bmp1.Height, bmp1.PixelFormat);
            try
            {
                for (int i = 0; i < bmp1.Width; i++)
                {
                    for (int j = 0; j < bmp1.Height; j++)
                    {
                        icr.iTotalPixels++;
                        Color pixColor1 = bmp1.GetPixel(i, j);
                        Color pixColor2 = bmp2.GetPixel(i, j);
                        if (pixColor1 == pixColor2)
                        {
                            bmpResult.SetPixel(i, j, pixColor1);
                        }
                        else
                        {
                            icr.iMismatchPixels++;
                            bmpResult.SetPixel(i, j, Color.HotPink);
                        }
                    }
                }
                bmpResult.Save(OutputFilePng, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                // do nothing
            }
            bmp1.Dispose();
            bmp2.Dispose();
            bmpResult.Dispose();

            if (icr.iMismatchPixels > 0)
            {
                icr.PctOfMismatch = Math.Round(((double)icr.iMismatchPixels / (double)icr.iTotalPixels),4) *100;
            }
            return icr;
        }
    }
}
