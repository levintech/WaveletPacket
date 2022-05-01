using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT
{
    public static class Haar
    {
        public static double m_dbWeight_t1 = 0.5;
        public static double m_dbWeight_t2 = 0.5;
        public static double m_dbWeight_d1 = 0.5;
        public static double m_dbWeight_d2 = -0.5;

        /// <summary>
        ///   Discrete Haar Wavelet 2D Transform
        /// </summary>
        /// 
        public static double[,] Forward(double[,] data, int iterations)
        {
            int nRows = data.GetLength(0);
            int nCols = data.GetLength(1);

            double[] row = new double[nCols];
            double[] col = new double[nRows];
             
            for (int k = 0; k < iterations; k++)
            {
                for (int nI = 0; nI < nRows; nI++)
                {
                    for (int nJ = 0; nJ < row.Length; nJ++)
                        row[nJ] = data[nI, nJ];

                    Forward(row);

                    for (int nJ = 0; nJ < row.Length; nJ++)
                        data[nI, nJ] = row[nJ];
                }

                for (int nJ = 0; nJ < nCols; nJ++)
                {
                    for (int nI = 0; nI < col.Length; nI++)
                        col[nI] = data[nI, nJ];

                    Forward(col);

                    for (int nI = 0; nI < col.Length; nI++)
                        data[nI, nJ] = col[nI];
                }
            }
         
            return data;
        }

        public static void Forward(double[] data)
        {
            double[] temp = new double[data.Length];

            int h = data.Length >> 1;
            for (int nI = 0; nI < h; nI++)
            {
                int nK = (nI << 1);
                temp[nI] = data[nK] * m_dbWeight_t1 + data[nK + 1] * m_dbWeight_t2;
                temp[nI + h] = data[nK] * m_dbWeight_d1 + data[nK + 1] * m_dbWeight_d2;
            }

            for (int nI = 0; nI < data.Length; nI++)
                data[nI] = temp[nI];
        }

        /// <summary>
        ///   Inverse Haar Wavelet 2D Transform
        /// </summary>
        /// 
        public static double[,] Inverse(double[,] data, int iterations)
        {
            int nRows = data.GetLength(0);
            int nCols = data.GetLength(1);

            double[] col = new double[nRows];
            double[] row = new double[nCols];

            for (int nL = 0; nL < iterations; nL++)
            {
                for (int nJ = 0; nJ < nCols; nJ++)
                {
                    for (int nI = 0; nI < row.Length; nI++)
                        col[nI] = data[nI, nJ];

                    Inverse(col);

                    for (int nI = 0; nI < col.Length; nI++)
                        data[nI, nJ] = col[nI];
                }

                for (int nI = 0; nI < nRows; nI++)
                {
                    for (int nJ = 0; nJ < row.Length; nJ++)
                        row[nJ] = data[nI, nJ];

                    Inverse(row);

                    for (int nJ = 0; nJ < row.Length; nJ++)
                        data[nI, nJ] = row[nJ];
                }
            }

            return data;
        }

        public static void Inverse(double[] data)
        {
            double[] temp = new double[data.Length];

            int nH = data.Length >> 1;
            for (int nI = 0; nI < nH; nI++)
            {
                int nK = (nI << 1);

                temp[nK] = (data[nI] * m_dbWeight_t1 + data[nI + nH] * m_dbWeight_d1) / m_dbWeight_d1;
                temp[nK + 1] = (data[nI] * m_dbWeight_t2 + data[nI + nH] * m_dbWeight_d2) / m_dbWeight_t1;
            }

            for (int nI = 0; nI < data.Length; nI++)
                data[nI] = temp[nI];
        }
    }
}
