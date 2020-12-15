using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace maoi_lab_03
{
    class ImMatrix
    {
        public ImMatrix(Image image)
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
        private Color[,] ColorMatrix { get; set; }
        private string[,] RGBColorMatrix { get; set; }
        public string[,] BinaryMatrix { get; set; }//main matrix
        private string[,] HalftoneMatrix { get; set; }

        private Color[,] GetPixelsFromImageToArray(Image image)
        {
            int height = image.Height;
            int width = image.Width;


            Bitmap imageBitmap = new Bitmap(image);
            Color[,] imagePixels = new Color[height,width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    imagePixels[i,j] = imageBitmap.GetPixel(i, j);
                }
            }
            return imagePixels;
        }
        public string[,] ConvertArraysOfColorToArrayOfStrings(Color[,] imageColorPixels)
        {
            int height = Height;
            int width = Width;


            string[,] imageStringPixels = new string[height, width];
            for (int i = 0; i < imageColorPixels.Length; i++)
            {
                //imageStringPixels[i] = new string[imageColorPixels[i].Length];
                for (int j = 0; j < height; j++)
                {
                    imageStringPixels[i,j] = $"{imageColorPixels[i,j].R},{imageColorPixels[i,j].G},{imageColorPixels[i,j].B}";
                }
            }
            return imageStringPixels;
        }
        public string[,] ConvertRGBToHalftone(string[,] rgbMatrix)
        {
            //1

            //2 in: basepixelstring
            // string[] rGBstrings = basePixelString.Split(new char[] { ',', '\n' });
            int height = Height;
            int width = Width;

            string[,] stringOfHalftoneMatrix = new string[height, width];
            for (int i = 0; i < Width; i++)
            {
                //stringOfHalftoneMatrix[i] = new string[Height];
                for (int j = 0; j < Height; j++)
                {
                    string[] rGBstrings = rgbMatrix[i,j].Split(new char[] { ',', '\n' });

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
                    stringOfHalftoneMatrix[i,j] = $"{sumOfPixelsValues.ToString()},{sumOfPixelsValues.ToString()},{sumOfPixelsValues.ToString()}"
                    ;
                }
            }
            return stringOfHalftoneMatrix;
        }
        private string[,] HalftoneToBinary(string[,] halftoneMatrix, int limit)
        {
            //string[,] newBinarryMatrix = Array.ConvertAll(halftoneMatrix, a => (string[,])a.Clone());//   ZondMatrix.HalftoneMatrix;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (TransformToNumber(halftoneMatrix[i,j]) >= limit)
                    {
                        halftoneMatrix[i,j] = "1,1,1";
                    }
                    else
                        halftoneMatrix[i,j] = "0,0,0";
                }
            }
            return halftoneMatrix;
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
