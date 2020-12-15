using System;
using System.Collections.Generic;
using System.Text;

namespace maoi_lab_03
{
    public class Perceptron
    {
        public int ASize { get; set; }
        private int X { get; set; }
        private int Y { get; set; }

        public int[] Yi { get; set; }
        public int[,] ConnectionArray { get; set; }//подключения [-1,0,1]
        public  string[] ImgNames { get; set; }
        public string CurrImg { get; set; }
        private int[] IA { get; set; }
        private int[] IB { get; set; }
        private int[] IC { get; set; }
        public int[] Sums { get; set; }
        private ImageMatrix ImgMatrix { get; set; }
        public List<int[]> Lambdas { get; set; } = new List<int[]>();
        public Perceptron(int a, int x, int y) 
        {

            ASize = a;
            X = x;
            Y = y;

            IA = new int[a];
            IB = new int[a];
            IC = new int[a];

            //ImgMatrix = image;

            LambdaFill(Lambdas,IA,IB,IC);
            FillPrceptronArray(Lambdas);
            FillPerceptronMatrix();


            Yi = new int[ASize];
            //FindYi(ImgMatrix, ConnectionArray);
        
        }
        public void ChangeImage(ImageMatrix image) 
        {
            ImgMatrix = image;


        }
        public void FixNames(params string[] names) 
        {
            ImgNames = new string[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                ImgNames[i] = names[i];
            }
        
        }
        public void Proceede(int givenImgClass) 
        {
            FindYi(ImgMatrix, ConnectionArray);



            int currClass = 0;
            int temp = int.MinValue;
            int[] sums = new int[Lambdas.Count];
            for (int i = 0; i < Lambdas.Count; i++)
            {
                sums[i]= FindSumForLa(Lambdas.ToArray()[i], Yi);

                Lambdas.ToArray()[i] =  ChangeLa(i, givenImgClass, Yi, Lambdas.ToArray()[i]);
            }
            for (int i = 0; i < sums.Length; i++)
            {
                if (sums[i] > temp) 
                {
                    currClass = i;
                    temp = sums[i];
                }
            }
            Sums = sums;
            CurrImg = ImgNames[currClass];
        }

        public void FindImage() 
        {
            FindYi(ImgMatrix, ConnectionArray);

            int currClass = 0;
            int temp = int.MinValue;
            int[] sums = new int[Lambdas.Count];
            for (int i = 0; i < Lambdas.Count; i++)
            {
                sums[i] = FindSumForLa(Lambdas.ToArray()[i], Yi);

                //Lambdas.ToArray()[i] = ChangeLa(i, givenImgClass, Yi, Lambdas.ToArray()[i]);
            }
            for (int i = 0; i < sums.Length; i++)
            {
                if (sums[i] > temp)
                {
                    currClass = i;
                    temp = sums[i];
                }
            }
            Sums = sums;

            CurrImg = ImgNames[currClass];
        }

        private int[,] CreateConnectionArray(int sizeA,int sizeImage) 
        {
            int[,] connectionsArray = new int[sizeA,sizeImage];
            for (int i = 0; i < sizeA; i++)
            {
                //connectionsArray[i] = new int[sizeImage];
                for (int j = 0; j < sizeImage; j++)
                {
                    connectionsArray[i,j] = 0;
                }
            }
            return connectionsArray;
        } 
        void LambdaFill(List<int[]> listOfP,params int[][] lambdas) 
        {
            foreach (var item in lambdas)
            {
                listOfP.Add(item);
            }   
        }
        void FillPrceptronArray(List<int[]> perceptronList) 
        {
            foreach (var item in perceptronList)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    item[i] = 1;
                }
            }

        }
        void FillPerceptronMatrix() 
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            ConnectionArray = CreateConnectionArray(ASize,X*Y);
            int x = 0;
            int y = 1;
            for (int i = 0; i < X*Y; i++)
            {
                if (x >= ASize)
                {
                    
                    x -= ASize;
                    x += y;
                    if (y == 0)
                    {
                        y = 1;
                    }
                    else
                    {
                        y = 0;
                    }
                }
                if (random.Next(-20, 10) >= 0)
                {
                    //1
                    ConnectionArray[x,i] = 1;

                }
                else
                {
                    ConnectionArray[x,i] = -1;
                    //-1
                }
                x += 1;
            }
            /*return ConnectionArray;*/
        }
        void FindYi(ImageMatrix image, int[,] ConnectionArray) 
        {
            for (int k = 0; k < ASize; k++)
            {
                int yi = 0;
                int sum = 0;
                var sums = 0;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        int elem = Convert.ToInt32(image.BinaryMatrix[j][i]);
                        int connection = ConnectionArray[k, j*image.Height+ i];
                        yi += elem * connection;
                        sums = j * image.Height + i;
                    }
                }
                if (yi >= 0) 
                {
                    Yi[k] = 1;//if >=0 else
                
                }
                else 
                {
                    Yi[k] = 0;//if >=0 else
                
                }
            }
        
        }
        int FindSumForLa(int[] lambda, int[] yi) 
        {
            int R = 0;
            for (int i = 0; i < ASize; i++)
            {
                R += lambda[i] * yi[i];
            }
            return R;
        }
        int[] ChangeLa(int mode, int imgClass, int[] yi, int[] la) 
        {
            if (imgClass == mode)
            {
                for (int i = 0; i < ASize; i++)
                {
                    if (yi[i] == 1)
                    {
                        la[i] += 1;     
                    }

                }
            }
            else 
            {
                for (int i = 0; i < ASize; i++)
                {

                    if (yi[i] == 1)
                    {
                        la[i] -= 1;
                    }
                }
            }
            return la;
        }
    }
}
