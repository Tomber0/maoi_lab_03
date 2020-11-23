using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace maoi_lab_03
{
    public class ImageMatrix
    {
        public ImageMatrix(Image image)
        {
            LocalImage = image;
            this.Width = image.Width;
            this.Height = image.Height;
            DarkLimit = 125;
        }
        private void Setup() 
        {
            ColorMatrix = GetPixelsFromImageToArray(this.LocalImage);
            RGBColorMatrix = ConvertArraysOfColorToArrayOfStrings(ColorMatrix);
            HalftoneMatrix = ConvertRGBToHalftone(RGBColorMatrix);
            BinaryMatrix = HalftoneToBinary(HalftoneMatrix, DarkLimit);
        }
        public Image LocalImage { get; set; }
        public int DarkLimit { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Color[][] ColorMatrix { get; set; }
        private string[][] RGBColorMatrix { get; set; }
        public string[][] BinaryMatrix { get; set; }//main matrix
        private string[][] HalftoneMatrix { get; set; }

        private Color[][] GetPixelsFromImageToArray(Image image)
        {
            int height = image.Height;
            int width = image.Width;


            Bitmap imageBitmap = new Bitmap(image);
            Color[][] imagePixels = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                imagePixels[i] = new Color[height];
                for (int j = 0; j < height; j++)
                {
                    imagePixels[i][j] = imageBitmap.GetPixel(i, j);
                }
            }


            return imagePixels;
        }
        public string[][] ConvertArraysOfColorToArrayOfStrings(Color[][] imageColorPixels)
        {
            string[][] imageStringPixels = new string[imageColorPixels.Length][];
            for (int i = 0; i < imageColorPixels.Length; i++)
            {
                imageStringPixels[i] = new string[imageColorPixels[i].Length];
                for (int j = 0; j < imageColorPixels[i].Length; j++)
                {
                    imageStringPixels[i][j] = $"{imageColorPixels[i][j].R},{imageColorPixels[i][j].G},{imageColorPixels[i][j].B}";
                }
            }
            return imageStringPixels;
        }
        public string[][] ConvertRGBToHalftone(string[][] rgbMatrix)
        {
            //1

            //2 in: basepixelstring
            // string[] rGBstrings = basePixelString.Split(new char[] { ',', '\n' });

            string[][] stringOfHalftoneMatrix = new string[Width][];
            for (int i = 0; i < Width; i++)
            {
                stringOfHalftoneMatrix[i] = new string[Height];
                for (int j = 0; j < Height; j++)
                {
                    string[] rGBstrings = rgbMatrix[i][j].Split(new char[] { ',', '\n' });

                    double[] colorsRGBDouble = new double[3];
                    for (int z = 0; z < rGBstrings.Length; z++)
                    {
                        colorsRGBDouble[z] = Convert.ToDouble(rGBstrings[z]);
                    }

                    double sumOfPixelsValues = 0;
                    foreach (var item in colorsRGBDouble)
                    {
                        sumOfPixelsValues += item / 3;
                        sumOfPixelsValues = Math.Round(sumOfPixelsValues, 0);
                    }
                    stringOfHalftoneMatrix[i][j] = $"{sumOfPixelsValues.ToString()},{sumOfPixelsValues.ToString()},{sumOfPixelsValues.ToString()}"
                    ;
                }
            }
            return stringOfHalftoneMatrix;
        }
        private string[][] HalftoneToBinary(string[][] halftoneMatrix, int limit)
        {
            string[][] newBinarryMatrix = Array.ConvertAll(halftoneMatrix, a => (string[])a.Clone());//   ZondMatrix.HalftoneMatrix;

            for (int i = 0; i < halftoneMatrix.Length; i++)
            {
                for (int j = 0; j < halftoneMatrix[i].Length; j++)
                {
                    if (TransformToNumber(newBinarryMatrix[i][j]) >= limit)
                    {
                        newBinarryMatrix[i][j] = "1,1,1";
                    }
                    else
                        newBinarryMatrix[i][j] = "0,0,0";
                }
            }
            return newBinarryMatrix;
        }

        private int TransformToNumber(string strNum)
        {
            string[] rGBstrings = strNum.Split(new char[] { ',', '\n' });
            int newColor = (Convert.ToInt32(rGBstrings[0]) +
                Convert.ToInt32(rGBstrings[1]) +
                Convert.ToInt32(rGBstrings[2])) / 3;

            return newColor;


        }

    }
}
