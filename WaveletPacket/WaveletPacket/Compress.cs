using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveletPacket
{
    class Compress
    {
        
        protected double _threshold = 1;
        protected double _magnitude = 0;

        public Compress()
        {
            _magnitude = 0;
            _threshold = 1;
        } // Compressor
        public Compress(double threshold)
        {
            _magnitude = 0;

            try
            {

                if (threshold <= 0 )
                {
                    //throw new JWaveFailure(
                    //    "Compressor - given threshold should be larger than zero!");
        
                }
                        }
            catch (Exception e)
            {
                  //      e.showMessage();
                  //      System.out
                  //.println("Compressor - setting threshold to default value: " + 1. );
                  //      threshold = 1.;
            }

            _threshold = threshold;

        } // Compressor
        public double[] compress(double[] arr, double magnitude)
        {
            int arrLength = arr.Length;

            double[] arrComp = new double[arrLength];

            for (int i = 0; i < arrLength; i++)
                if (Math.Abs(arr[i]) >= magnitude * _threshold)
                    arrComp[i] = arr[i];
                else
                    arrComp[i] = 0; // compression be setting to zero

            return arrComp;

        } // compress

        public double[,] compress(double[,] mat, double magnitude)
        {

            int matHilbNoOfRows = mat.GetUpperBound(0) + 1;
            int matHilbNoOfCols = mat.GetUpperBound(1) + 1;

            double[,] matComp = new double[matHilbNoOfRows,matHilbNoOfCols];

            for (int i = 0; i < matHilbNoOfRows; i++)
                for (int j = 0; j < matHilbNoOfCols; j++)
                    if (Math.Abs(mat[i,j]) >= magnitude * _threshold)
                        matComp[i,j] = mat[i,j];
                    else
                        matComp[i,j] = 0;

            return matComp;

        } // compress
        public double calcCompressionRate(double[] arr)
        {
            double compressionRate = 0;
            int noOfZeros = 0;
            int length = arr.Length;
            for (int i = 0; i < length; i++)
            {
                if (arr[i] == 0)
                    noOfZeros++;
            } // i
            if (noOfZeros != 0)
                compressionRate = (double)noOfZeros / (double)length * 100;
            else
                compressionRate = (double)noOfZeros;
            return compressionRate;

        } // calcCompressionRate

        public double getThreshold()
        {
            return _threshold;
        } // getThreshold

        public double getMagnitude()
        {
            return _magnitude;
        } // getMagnitude

    }
}
