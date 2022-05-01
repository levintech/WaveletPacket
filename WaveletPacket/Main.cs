using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using SharpWave;
using System.Drawing.Imaging;
using EncMsg;
using Stegano;
using System.Security.Cryptography;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Stegano;
using HT;

namespace WaveletPacket
{
    public partial class Main : Form
    {
        public int level = 1;
        private Bitmap origin_Img = null;
        private Bitmap inverse_Img = null;
        private double[,] matHilbR;
        private double[,] matHilbG;
        private double[,] matHilbB;
        private double[,] colorR;
        private double[,] colorG;
        private double[,] colorB;
        private int[,] symR;
        private int[,] symG;
        private int[,] symB;
        private double dBright;
        int height = 512; //bmp.Height;
        int width = 512; //bmp.Width;
        string str_Key;

        public Bitmap basic_image = null, modefide_image = null;
        public Main()
        {
            InitializeComponent();
        }
        static Random random = new Random();
        private byte[] AES_key;
        private EncMsg.Aes.KeySize keysize;
        private uint originalDataSize;
        private int compressedDataSize;
        private double totalTime;

        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        private byte[] ComputeSha256Hash(byte[] rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(rawData);

                return bytes;
            }
        }

        public void getBitmapColorMatrix(string filePath)
        {
            Bitmap objBitmap = new Bitmap(filePath);
            txtSize.Text = objBitmap.Width.ToString() + "*" + objBitmap.Height.ToString();
            width = objBitmap.Width; height = objBitmap.Height;

            origin_Img = objBitmap;
            Bitmap bmp = new Bitmap(objBitmap, new Size(width, height));
            Bitmap bmp1 = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            colorR = new double[width, height];
            colorG = new double[width, height];
            colorB = new double[width, height];
            if (height < width)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        colorR[i, j] = bmp.GetPixel(i, j).R;
                        colorG[i, j] = bmp.GetPixel(i, j).G;
                        colorB[i, j] = bmp.GetPixel(i, j).B;

                        bmp1.SetPixel(i, j, Color.FromArgb(255, Convert.ToInt32(colorR[i, j]), Convert.ToInt32(colorG[i, j]), Convert.ToInt32(colorB[i, j])));

                    }
                }
            }
            else
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        colorR[i, j] = bmp.GetPixel(i, j).R;
                        colorG[i, j] = bmp.GetPixel(i, j).G;
                        colorB[i, j] = bmp.GetPixel(i, j).B;

                        bmp1.SetPixel(i, j, Color.FromArgb(255, Convert.ToInt32(colorR[i, j]), Convert.ToInt32(colorG[i, j]), Convert.ToInt32(colorB[i, j])));
                    }
                }
            }

            picResult.Image = bmp1;
        }

        private void ImageLoad(string fileName)
        {
            getBitmapColorMatrix(fileName);
        }

        private string GetMinMax(double[,] dValue)
        {

            double max = dValue[0, 0];
            double min = dValue[0, 0];
            for (int i = 0; i < dValue.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < dValue.GetUpperBound(1) + 1; j++)
                {
                    if (min > dValue[i, j])
                        min = dValue[i, j];
                    if (max < dValue[i, j])
                        max = dValue[i, j];
                }
            }
            return max.ToString() + "," + min.ToString();
        }
        private double[,] GetRange(double[,] dValue)
        {
            int ll = width >> level;
            double[,] result = new double[ll, ll];
            for (int i = 0; i < ll; i++)
            {
                for (int j = 0; j < ll; j++)
                {
                    result[i, j] = dValue[i, j];
                }
            }

            return result;

        }
        private void InverseDrawImage(double[,] R, double[,] G, double[,] B)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            int nCount = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int RR, GG, BB;
                    
                    RR = Convert.ToInt32(R[i, j]);
                    GG = Convert.ToInt32(G[i, j]);
                    BB = Convert.ToInt32(B[i, j]);
                    if (RR > 255) { RR = 255; }
                    if (GG > 255) { GG = 255; }
                    if (BB > 255) { BB = 255; }

                    if (RR < 0) { RR = 0; }
                    if (GG < 0) { GG = 0; }
                    if (BB < 0) { BB = 0; }
                    bmp.SetPixel(i, j, Color.FromArgb(255, RR, GG, BB));
                }
            }
            inverse_Img = bmp;
            picResult.Image = bmp;
        }
        
        private void DrawImage(double[,] R, double[,] G, double[,] B)
        {
            string strR = GetMinMax(GetRange(R));
            string strG = GetMinMax(GetRange(G));
            string strB = GetMinMax(GetRange(B));

            symR = new int[width, height];
            symG = new int[width, height];
            symB = new int[width, height];

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            int L = 256;
            double delta = Convert.ToDouble(maxR - minR) / 256;
            double I = 0.0;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            int ll = width >> level;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double RR, GG, BB;
                    if (i < ll && j < ll)
                    {
                        symR[i, j] = 1;
                        symG[i, j] = 1;
                        symB[i, j] = 1;
                        //                         RR = Convert.ToInt32(255 / (maxR - minR) * (R[i, j] - minR));
                        //                         GG = Convert.ToInt32(255 / (maxG - minG) * (G[i, j] - minG));
                        //                         BB = Convert.ToInt32(255 / (maxB - minB) * (B[i, j] - minB));
                        I = Math.Round(Convert.ToDouble(R[i, j] - minR) / delta );
                        if (I == L)
                            I -= 1;
                        else if (I < 0)
                            I = 0;
                        RR = minR + I * delta;
                        RR = Math.Round(255 / (maxR - minR) * (R[i, j] - minR));
                        GG = Math.Round(255 / (maxG - minG) * (G[i, j] - minG));
                        BB = Math.Round(255 / (maxB - minB) * (B[i, j] - minB));
                    }
                    else
                    {
                        symR[i, j] = R[i, j] >= 0 ? 1 : -1;
                        symG[i, j] = G[i, j] >= 0 ? 1 : -1;
                        symB[i, j] = B[i, j] >= 0 ? 1 : -1;
                        RR = Math.Round(Math.Abs(R[i, j]) * (0.5 + 0.5 * dBright));// Convert.ToInt32(255 / (maxR - minR) * (R[i, j] - minR));
                        if (RR > 255)
                            RR = 255;
                        GG = Math.Round(Math.Abs(G[i, j]) * (0.5 + 0.5 * dBright));// Convert.ToInt32(255 / (maxG - minG) * (G[i, j] - minG));
                        if (GG > 255)
                            GG = 255;
                        BB = Math.Round(Math.Abs(B[i, j]) * (0.5 + 0.5 * dBright));// Convert.ToInt32(255 / (maxB - minB) * (B[i, j] - minB));
                        if (BB > 255)
                            BB = 255;
                    }
                    bmp.SetPixel(i, j, Color.FromArgb(255, Convert.ToInt32(RR), Convert.ToInt32(GG), Convert.ToInt32(BB)));
                }
            }
            basic_image = bmp;
            picResult.Image = bmp;
            Get_PartImage(bmp);
        }
        Bitmap bmpOriginal;
        Bitmap bmpLL;
        Bitmap bmpHH;
        Bitmap bmpLH;
        Bitmap bmpHL;

        private void Get_PartImage(Bitmap myBitmap)
        {
            int w = width >> level;
            int h = width >> level;
            bmpHH = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            bmpHL = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            bmpLH = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            bmpLL = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            Rectangle cloneRect = new Rectangle(0, 0, w, h);
            System.Drawing.Imaging.PixelFormat format =
                myBitmap.PixelFormat;
            bmpLL = myBitmap.Clone(cloneRect, format);

            cloneRect = new Rectangle(w, h, w, h);
            bmpHH = myBitmap.Clone(cloneRect, format);

            cloneRect = new Rectangle(w, 0, w, h);
            bmpLH = myBitmap.Clone(cloneRect, format);

            cloneRect = new Rectangle(0, h, w, h);
            bmpHL = myBitmap.Clone(cloneRect, format);
        }

        private Transform SetTransform()
        {
            Transform t = new Transform(
                  new WaveletPacketTransform(
                    new Haar1()));
            if (cmbType.Text == "haar")
            {
                t = new Transform(
                    new WaveletPacketTransform(
                    new Haar1()));
            }
            else
            {
                switch (cmbDbNum.Text)
                {
                    case "1":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Haar1()));
                        break;
                    case "2":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies3()));
                        break;
                    case "3":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies4()));
                        break;
                    case "4":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies5()));
                        break;
                    case "5":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies6()));
                        break;
                    case "6":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies7()));
                        break;
                    case "7":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies8()));
                        break;
                    case "8":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies9()));
                        break;
                    case "9":
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies10()));
                        break;
                    default:
                        t = new Transform(
                            new WaveletPacketTransform(
                            new Daubechies2()));
                        break;
                }
            }
            return t;
        }

        private void Analyzer()
        {
            try
            {
                Transform t = SetTransform();
                // 2-D example
                double[,] matTimeR = colorR;
                double[,] matTimeG = colorG;
                double[,] matTimeB = colorB;

                //try the wavelet packet transform
                matHilbR = t.forward(matTimeR, level); // 2-D FWT Haar forward
                matHilbG = t.forward(matTimeG, level);
                matHilbB = t.forward(matTimeB, level);

                DrawImage(matHilbR, matHilbG, matHilbB);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            } // try
        }

        private void Analyzer_New()
        {
            // 2-D example
            double[,] matTimeR = colorR;
            double[,] matTimeG = colorG;
            double[,] matTimeB = colorB;

            //try the wavelet packet transform
            matHilbR = Haar.Forward(matTimeR, level); // 2-D FWT Haar forward
            matHilbG = Haar.Forward(matTimeG, level);
            matHilbB = Haar.Forward(matTimeB, level);

            DrawImage_New(matHilbR, matHilbG, matHilbB);
        }

        public void DrawImage_New(double[,] R, double[,] G, double[,] B)
        {
            string strR = GetMinMax(GetRange(R));
            string strG = GetMinMax(GetRange(G));
            string strB = GetMinMax(GetRange(B));

            symR = new int[width, height];
            symG = new int[width, height];
            symB = new int[width, height];

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            int L = 256;
            double delta = Convert.ToDouble(maxR - minR) / 256;
            double I = 0.0;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            int ll = width >> level;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double RR, GG, BB;
                    if (i < ll && j < ll)
                    {
                        symR[i, j] = 1;
                        symG[i, j] = 1;
                        symB[i, j] = 1;
                        //                         RR = Convert.ToInt32(255 / (maxR - minR) * (R[i, j] - minR));
                        //                         GG = Convert.ToInt32(255 / (maxG - minG) * (G[i, j] - minG));
                        //                         BB = Convert.ToInt32(255 / (maxB - minB) * (B[i, j] - minB));
                        I = Math.Round(Convert.ToDouble(R[i, j] - minR) / L);
                        if (I == L)
                            I -= 1;
                        else if (I < 0)
                            I = 0;
                        RR = minR + I * delta;

                        RR = Math.Round(R[i, j]);
                        GG = Math.Round(G[i, j]);
                        BB = Math.Round(B[i, j]);
                    }
                    else
                    {
                        symR[i, j] = R[i, j] >= 0 ? 1 : -1;
                        symG[i, j] = G[i, j] >= 0 ? 1 : -1;
                        symB[i, j] = B[i, j] >= 0 ? 1 : -1;
                        RR = Math.Round(Math.Abs(R[i, j]));
                        if (RR > 255)
                            RR = 255;
                        GG = Math.Round(Math.Abs(G[i, j]));
                        if (GG > 255)
                            GG = 255;
                        BB = Math.Round(Math.Abs(B[i, j]));
                        if (BB > 255)
                            BB = 255;

                    }
                    bmp.SetPixel(i, j, Color.FromArgb(255, Convert.ToInt32(RR), Convert.ToInt32(GG), Convert.ToInt32(BB)));
                }
            }
            basic_image = bmp;
            picResult.Image = bmp;
            Get_PartImage(bmp);

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Analyzer();
//             Analyzer_New();
            btnImgCompress.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbBright.Text = cmbBright.Items[0].ToString();
            cmbDbNum.Enabled = false;
            cmbType.Text = cmbType.Items[0].ToString();
            cmbLevel.Text = cmbLevel.Items[0].ToString();
        }

        private void cmbType_TextChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "db")
            {
                cmbDbNum.Enabled = true;
                cmbDbNum.Text = cmbDbNum.Items[0].ToString();
            }
            else
            {
                cmbDbNum.Enabled = false;
            }
        }

        private void cmbBright_TextChanged(object sender, EventArgs e)
        {
            dBright = Convert.ToDouble(cmbBright.Text);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Open Image";
            dialog.Filter = "Files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImageLoad(dialog.FileName);
            }

            dialog.Dispose();


        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void cmbLevel_TextChanged(object sender, EventArgs e)
        {
            level = Convert.ToInt32(cmbLevel.Text) - 1;
        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            Inverse();
//             Inverse_New();
        }

        //         private void Inverse()
        //         {
        //             Transform t = SetTransform();
        // 
        //             double[,] matRecoR = t.reverse(matHilbR, level); // 1-D FWT Haar reverse
        //             double[,] matRecoG = t.reverse(matHilbG, level); // 1-D FWT Haar reverse
        //             double[,] matRecoB = t.reverse(matHilbB, level); // 1-D FWT Haar reverse
        // 
        //             colorR = matRecoR;
        //             colorG = matRecoG;
        //             colorB = matRecoB;
        // 
        //             InverseDrawImage(colorR, colorG, colorB);
        //         }

        private void Inverse()
        {
            int RR, GG, BB;

            string strR = GetMinMax(GetRange(matHilbR));
            string strG = GetMinMax(GetRange(matHilbG));
            string strB = GetMinMax(GetRange(matHilbB));

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            Bitmap bmpResult = (Bitmap)picResult.Image;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matHilbR[i, j] = bmpResult.GetPixel(i, j).R;
                    matHilbG[i, j] = bmpResult.GetPixel(i, j).G;
                    matHilbB[i, j] = bmpResult.GetPixel(i, j).B;
                }
            }

            int ll = width >> level;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
//                     RR = Convert.ToInt32(matHilbR[i, j]);
//                     GG = Convert.ToInt32(matHilbG[i, j]);
//                     BB = Convert.ToInt32(matHilbB[i, j]);

                    if (i < ll && j < ll)
                    {
                        matHilbR[i, j] = symR[i, j] * (matHilbR[i, j] * (maxR - minR) / 255 + minR);
                        matHilbG[i, j] = symG[i, j] * (matHilbG[i, j] * (maxG - minG) / 255 + minG);
                        matHilbB[i, j] = symB[i, j] * (matHilbB[i, j] * (maxB - minB) / 255 + minB);
                    }
                    else
                    {
                        matHilbR[i, j] = symR[i, j] * (matHilbR[i, j] / (0.5 + 0.5 * dBright));
                        matHilbG[i, j] = symG[i, j] * (matHilbG[i, j] / (0.5 + 0.5 * dBright));
                        matHilbB[i, j] = symB[i, j] * (matHilbB[i, j] / (0.5 + 0.5 * dBright));
                    }
                }
            }

            Transform t = SetTransform();

            double[,] matRecoR = t.reverse(matHilbR, level); // 1-D FWT Haar reverse
            double[,] matRecoG = t.reverse(matHilbG, level); // 1-D FWT Haar reverse
            double[,] matRecoB = t.reverse(matHilbB, level); // 1-D FWT Haar reverse

            int nCount = 0;
            double dbTemp = 0;
            for (int nI = 0; nI < width; nI ++)
                for (int nJ = 0; nJ < height; nJ++)
                {
                    dbTemp = Math.Round(matRecoR[nI, nJ]);
                    if (dbTemp != colorR[nI, nJ])
                        nCount++;

                    colorR[nI, nJ] = Math.Round(matRecoR[nI, nJ]);
                    colorG[nI, nJ] = Math.Round(matRecoG[nI, nJ]);
                    colorB[nI, nJ] = Math.Round(matRecoB[nI, nJ]);
                }

            nCount = 0;
            InverseDrawImage(colorR, colorG, colorB);
        }

        private void Inverse_New()
        {
            Bitmap bmpResult = (Bitmap)picResult.Image;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matHilbR[i, j] = bmpResult.GetPixel(i, j).R;
                    matHilbG[i, j] = bmpResult.GetPixel(i, j).G;
                    matHilbB[i, j] = bmpResult.GetPixel(i, j).B;
                }
            }

            int ll = width >> level;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //                     RR = Convert.ToInt32(matHilbR[i, j]);
                    //                     GG = Convert.ToInt32(matHilbG[i, j]);
                    //                     BB = Convert.ToInt32(matHilbB[i, j]);

                    if (i < ll && j < ll)
                    {
                        matHilbR[i, j] = symR[i, j] * matHilbR[i, j];
                        matHilbG[i, j] = symG[i, j] * matHilbG[i, j];
                        matHilbB[i, j] = symB[i, j] * matHilbB[i, j];
                    }
                    else
                    {
                        matHilbR[i, j] = symR[i, j] * matHilbR[i, j];
                        matHilbG[i, j] = symG[i, j] * matHilbG[i, j];
                        matHilbB[i, j] = symB[i, j] * matHilbB[i, j];
                    }
                }
            }

            double[,] matRecoR = Haar.Inverse(matHilbR, level); // 1-D FWT Haar reverse
            double[,] matRecoG = Haar.Inverse(matHilbG, level); // 1-D FWT Haar reverse
            double[,] matRecoB = Haar.Inverse(matHilbB, level); // 1-D FWT Haar reverse

            int nCount = 0;
            double dbTemp = 0;
            for (int nI = 0; nI < width; nI++)
                for (int nJ = 0; nJ < height; nJ++)
                {
                    dbTemp = Math.Round(matRecoR[nI, nJ]);
                    if (dbTemp != colorR[nI, nJ])
                        nCount++;

                    colorR[nI, nJ] = Math.Round(matRecoR[nI, nJ]);
                    colorG[nI, nJ] = Math.Round(matRecoG[nI, nJ]);
                    colorB[nI, nJ] = Math.Round(matRecoB[nI, nJ]);
                }

            nCount = 0;
            InverseDrawImage(colorR, colorG, colorB);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "files (.txt)|*.txt|All files (.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                picResult.Image.Save(dialog.FileName, ImageFormat.Bmp);
//                 bmpLL.Save(dialog.FileName, ImageFormat.Bmp);
            }
        }

        private void imageQuanti(double[,] R, double[,] G, double[,] B)
        {
            Compress comp = new Compress();
            int magnitude = Convert.ToInt32(txtEntropy.Text);
            string strR = GetMinMax(GetRange(R));
            string strG = GetMinMax(GetRange(G));
            string strB = GetMinMax(GetRange(B));

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            int ll = width >> level;
            Bitmap bmp = new Bitmap(ll, ll, PixelFormat.Format24bppRgb);
            double[,] RR = new double[ll, ll];
            double[,] BB = new double[ll, ll];
            double[,] GG = new double[ll, ll];

            /////////Quantize////////
            for (int i = 0; i < ll; i++)
            {
                for (int j = 0; j < ll; j++)
                {
                    RR[i, j] = 255 / (maxR - minR) * (R[i, j] - minR);
                    GG[i, j] = 255 / (maxG - minG) * (G[i, j] - minG);
                    BB[i, j] = 255 / (maxB - minB) * (B[i, j] - minB);
                    bmp.SetPixel(i, j, Color.FromArgb(255, Convert.ToInt32(RR[i, j]), Convert.ToInt32(GG[i, j]), Convert.ToInt32(BB[i, j])));
                }
            }

            //////////threshold//////
            //double[,] RRR=  comp.compress(RR, magnitude);
            //double[,] GGG=  comp.compress(GG, magnitude);
            //double[,] BBB=  comp.compress(BB, magnitude);
            //for (int i = 0; i < ll; i++)
            //{
            //    for (int j = 0; j < ll; j++)
            //    {
            //        int RC = 0;
            //        int GC = 0;
            //        int BC = 0;
            //        RC = Convert.ToInt32(RRR[i, j]);
            //        GC = Convert.ToInt32(GGG[i, j]);
            //        BC = Convert.ToInt32(BBB[i, j]);
            //        bmp.SetPixel(i, j, Color.FromArgb(255, RC, GC, BC));
            //    }
            //}
            Size newSize = new Size(width, height);
            Bitmap newbmp = new Bitmap(bmp, newSize);
            picResult.Image = newbmp;

        }

        private void btnCompress_Click(object sender, EventArgs e)
        {

            imageQuanti(matHilbR, matHilbG, matHilbB);

        }
        private void Decompress(double[,] R, double[,] G, double[,] B)
        {
            Compress comp = new Compress();
            int magnitude = Convert.ToInt32(txtEntropy.Text);
            string strR = GetMinMax(R);
            string strG = GetMinMax(G);
            string strB = GetMinMax(B);

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            double[,] RR = new double[width, height];
            double[,] BB = new double[width, height];
            double[,] GG = new double[width, height];

            /////////Quantize////////
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    RR[i, j] = 255 / (maxR - minR) * (R[i, j] - minR);
                    GG[i, j] = 255 / (maxG - minG) * (G[i, j] - minG);
                    BB[i, j] = 255 / (maxB - minB) * (B[i, j] - minB);
                }
            }

            //////////threshold//////
            double[,] RRR = comp.compress(RR, magnitude);
            double[,] GGG = comp.compress(GG, magnitude);
            double[,] BBB = comp.compress(BB, magnitude);

            /////////Decompress///////

            Transform t = SetTransform();
            double[,] matRecoR = t.reverse(matHilbR, level); // 1-D FWT Haar reverse
            double[,] matRecoG = t.reverse(matHilbG, level); // 1-D FWT Haar reverse
            double[,] matRecoB = t.reverse(matHilbB, level); // 1-D FWT Haar reverse


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int RC = 0;
                    int GC = 0;
                    int BC = 0;
                    RC = Convert.ToInt32(matRecoR[i, j]);
                    GC = Convert.ToInt32(matRecoG[i, j]);
                    BC = Convert.ToInt32(matRecoB[i, j]);
                    bmp.SetPixel(i, j, Color.FromArgb(255, RC, GC, BC));
                }
            }
            Size newSize = new Size(width, height);
            Bitmap newbmp = new Bitmap(bmp, newSize);
            picResult.Image = newbmp;
        }
        private void btnDecompress_Click(object sender, EventArgs e)
        {
            Decompress(matHilbR, matHilbG, matHilbB);

        }

        private void btnKeyGen_Click(object sender, EventArgs e)
        {
            byte[] plainText = new byte[64];
            byte[] cipherText = new byte[64];
            byte[] salsa_key = new byte[32];
            byte[] salsa_iv = new byte[8];
            random.NextBytes(plainText);
            random.NextBytes(salsa_key);
            random.NextBytes(salsa_iv);
            // for (int i = 0; i < 64; i++) plainText[i] = 69;
            // for (int i = 0; i < 32; i++) key[i] = 96;
            // for (int i = 0; i < 8; i++) iv[i] = 97;

            // plainText = Encoding.Unicode.GetBytes(lblKey.Text.PadRight(8, ' '));
            Salsa20.Salsa20CryptoTransform cryptoTransform = new Salsa20.Salsa20CryptoTransform(salsa_key, salsa_iv);
            cipherText = cryptoTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            AES_key = ComputeSha256Hash(cipherText);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < AES_key.Length; i++)
            {
                builder.Append(AES_key[i].ToString("x2"));
            }
            lblKey.Text = builder.ToString();
            str_Key = lblKey.Text;

            btnTxtCompress.Enabled = true;
            btnTxtDecompress.Enabled = true;
            btnTxtDecrypt.Enabled = true;
            btnTxtEncrypt.Enabled = true;
            totalTime = 0;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            long startTick = DateTime.Now.Ticks;
            keysize = EncMsg.Aes.KeySize.Bits256;

            byte[] plainText = new byte[16];
            byte[] cipherText = new byte[16];

            StringBuilder builder = new StringBuilder();

            string plain = txtPlainText.Text;
            int len = plain.Length;

            for (int i = 0; i < len; i += 8)
            {
                int sublen = 8;
                if (i + 8 > len) sublen = len - i;
                plainText = Encoding.Unicode.GetBytes(plain.Substring(i, sublen).PadRight(8, ' '));
                EncMsg.Aes a = new EncMsg.Aes(keysize, AES_key);
                a.Cipher(plainText, cipherText);
                for (int j = 0; j < cipherText.Length; j++)
                {
                    builder.Append(cipherText[j].ToString("x2"));
                }
            }
            txtCypherText.Text = builder.ToString();



            lblPlainText.Text = (Math.Round((double)len * 2 / 1024 * 1000) / 1000).ToString() + "KB";
            lblCipherText.Text = (Math.Round((double)txtCypherText.Text.Length / 2 / 1024 * 1000) / 1000).ToString() + "KB";

            long endTick = DateTime.Now.Ticks;
            long tick = (endTick - startTick) / 10;
            double t = (double)tick / 1000;
            totalTime = t;
            lblEncryptTime.Text = t.ToString() + "ms";
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            long startTick = DateTime.Now.Ticks;
            keysize = EncMsg.Aes.KeySize.Bits256;

            byte[] cipherText = new byte[16];
            byte[] decipheredText = new byte[16];

            StringBuilder builder = new StringBuilder();

            byte[] cipher = StringToByteArray(txtDecompressed.Text);
            int len = cipher.Length;

            for (int i = 0; i < len; i += 16)
            {
                int sublen = 16;
                if (i + 16 > len) sublen = len - i;
                for (int j = 0; j < sublen; j++) cipherText[j] = cipher[i + j];
                for (int j = sublen; j < 16; j++) cipherText[j] = 0;
                EncMsg.Aes a = new EncMsg.Aes(keysize, AES_key);
                a.InvCipher(cipherText, decipheredText);
                builder.Append(Encoding.Unicode.GetString(decipheredText));
            }

            txtDecrypted.Text = builder.ToString().Trim();
            lblDecrypted.Text = (Math.Round((double)txtDecrypted.Text.Length * 2 / 1024 * 1000) / 1000).ToString() + "KB";
            long endTick = DateTime.Now.Ticks;
            long tick = (endTick - startTick) / 10;
            double t = (double)tick / 1000;
            totalTime += t;
            lblDecryptTime.Text = t.ToString() + "ms";
            lblTotalTime.Text = totalTime.ToString() + "ms";
        }

        private void btnTxtCompress_Click(object sender, EventArgs e)
        {
            long startTick = DateTime.Now.Ticks;
            byte[] originalData = StringToByteArray(txtCypherText.Text);
            originalDataSize = (uint)originalData.Length;
            byte[] compressedData = new byte[originalDataSize * (101 / 100) + 320];

            compressedDataSize = HuffmanEncode.Compress(originalData, compressedData, originalDataSize);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < compressedDataSize; i++)
            {
                builder.Append(compressedData[i].ToString("x2"));
            }
            txtCompressed.Text = builder.ToString();
            lblCompressed.Text = (Math.Round((double)compressedDataSize / 1024 * 1000) / 1000).ToString() + "KB";
            long endTick = DateTime.Now.Ticks;
            long tick = (endTick - startTick) / 10;
            double t = (double)tick / 1000;
            totalTime += t;
            lblCompressTime.Text = t.ToString() + "ms";
        }

        private void btnTxtDecompress_Click(object sender, EventArgs e)
        {
            long startTick = DateTime.Now.Ticks;
            byte[] compressedData = StringToByteArray(txtCompressed.Text);
            byte[] decompressedData = new byte[originalDataSize];

            HuffmanDecode.Decompress(compressedData, decompressedData, (uint)compressedDataSize, originalDataSize);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < decompressedData.Length; i++)
            {
                builder.Append(decompressedData[i].ToString("x2"));
            }
            txtDecompressed.Text = builder.ToString();
            lblDecompressed.Text = (Math.Round((double)decompressedData.Length / 1024 * 1000) / 1000).ToString() + "KB";
            long endTick = DateTime.Now.Ticks;
            long tick = (endTick - startTick) / 10;
            double t = (double)tick / 1000;
            totalTime += t;
            lblDecompressTime.Text = t.ToString() + "ms";
        }

        private void stegoImage(Bitmap bmp)
        {
            int w = width >> level;
            int h = height >> level;
            int RR, GG, BB;

            Bitmap result = (Bitmap) picResult.Image;
            string strR = GetMinMax(GetRange(matHilbR));
            string strG = GetMinMax(GetRange(matHilbG));
            string strB = GetMinMax(GetRange(matHilbB));

            string[] dR = strR.Split(',');
            string[] dG = strG.Split(',');
            string[] dB = strB.Split(',');

            double maxR = Convert.ToDouble(dR[0]); double minR = Convert.ToDouble(dR[1]);
            double maxG = Convert.ToDouble(dG[0]); double minG = Convert.ToDouble(dG[1]);
            double maxB = Convert.ToDouble(dB[0]); double minB = Convert.ToDouble(dB[1]);

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    RR = bmp.GetPixel(x, y).R;
                    GG = bmp.GetPixel(x, y).G;
                    BB = bmp.GetPixel(x, y).B;

                    result.SetPixel(x, y, Color.FromArgb(255, RR, GG, BB));
//                     matHilbR[x, y] = Convert.ToInt32((Convert.ToDouble(RR)) * (maxR - minR) / 255 + minR + 0.5);
//                     matHilbG[x, y] = Convert.ToInt32((Convert.ToDouble(GG)) * (maxG - minG) / 255 + minG + 0.5);
//                     matHilbB[x, y] = Convert.ToInt32((Convert.ToDouble(BB)) * (maxB - minB) / 255 + minB + 0.5);
                }
            }

            picResult.Image = result;
//             DrawImage(matHilbR, matHilbG, matHilbB);
        }

        private void saveHHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "files (.txt)|*.txt|All files (.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bmpHH.Save(dialog.FileName, ImageFormat.Bmp);
            }
        }

        private void btnPsnr_Click(object sender, EventArgs e)
        {

            double n, sum, mseR, msec, psnr, mseB, mseG, mseg = 0;
            int r1, r2, g1, g2, b1, b2;

            Bitmap bmp3 = origin_Img;
            Bitmap bmp4 = inverse_Img;
            int width = bmp3.Width;
            int height = bmp3.Height;


            for (int y = 0; y < bmp3.Height; y++)
            {

                for (int i = 0; i < bmp3.Width; i++)
                {


                    r1 = bmp3.GetPixel(i, y).R;
                    r2 = bmp4.GetPixel(i, y).R;
                    g1 = bmp3.GetPixel(i, y).G;
                    g2 = bmp4.GetPixel(i, y).G;
                    b1 = bmp3.GetPixel(i, y).B;
                    b2 = bmp4.GetPixel(i, y).B;
                    mseR = Math.Pow(r1 - r2, 2);
                    mseG = Math.Pow(g1 - g2, 2);
                    mseB = Math.Pow(b1 - b2, 2);



                    sum = mseR + mseG + mseB;
                    mseg += sum;

                }

                n = bmp3.Width * bmp4.Height;
                msec = mseg / (n);

                if (msec > 0)
                    psnr = (20 * Math.Log10(255)) - (10 * Math.Log10(msec));
                //psnr = (20 * Math.Log(255 * 255 / mse) / Math.Log(10);
                else
                    psnr = 99;

                // txtPsnr.Text = psnr.ToString();
                // txtMse.Text = msec.ToString();

            }
        }

        private void stegoHidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat mLL = ConvertBitmaptoMat(bmpLL);
//             bmpOriginal = (Bitmap)picResult.Image;
//             Mat mOriginal = ConvertBitmaptoMat(bmpOriginal);
            CvInvoke.Imshow("image", mLL);
            CvInvoke.WaitKey(0);

            ///// get the keyPoints to hid text...
            List<Point> keyPoints = new List<Point>();
            Image<Rgb, Byte> src_Image = mLL.ToImage<Rgb, Byte>();
            Image<Gray, Byte> gray_Image = src_Image.Convert<Gray, Byte>();

            Image<Gray, float> corner_Image = new Image<Gray, float>(gray_Image.Size);
            CvInvoke.CornerHarris(gray_Image, corner_Image, 3, 3, 0.01);

            /// Standard Harris Detector
            CvInvoke.Normalize(corner_Image, corner_Image, 0, 255, NormType.MinMax, DepthType.Cv32F, new Mat());
            CvInvoke.ConvertScaleAbs(corner_Image, corner_Image, 1, 0);

            /// Drawing a circle around corners
            for (int j = 0; j < corner_Image.Rows; j++)
            {
                for (int i = 0; i < corner_Image.Cols; i++)
                {
                    if ((int)corner_Image.Data[j, i, 0] > 20)
                    {
                        Point pt = new Point(i, j);
                        keyPoints.Add(pt);
                    }
                }
            }

            int radius = 6;
            keyPoints = ANMS(keyPoints, radius);

            ///// Get the strong block...
            List<int> strong_block = new List<int>();

            int block_rows, block_cols;
            int nGoal = 0;

            // calculate number of blocks.
            block_rows = mLL.Height / 3;
            block_cols = mLL.Width / 3;
            int numberBlocks = block_cols * block_rows;

            // calculate threshold value.
            double threshold = keyPoints.Count() / Convert.ToDouble(numberBlocks);

            int nRow, nCol;
            int[] blocks = new int[numberBlocks];
            for (int nI = 0; nI < numberBlocks; nI++)
                blocks[nI] = 0;

            // suppression.
            for (int nIndex = 0; nIndex < keyPoints.Count(); nIndex++)
            {
                nRow = keyPoints[nIndex].Y / 3;
                nCol = keyPoints[nIndex].X / 3;

                if ((nRow * block_cols + nCol) <= numberBlocks)
                    blocks[nRow * block_cols + nCol]++;
            }

            for (int nI = 0; nI < numberBlocks; nI++)
            {
                if (blocks[nI] > threshold)
                    strong_block.Add(nI);
            }
            
            showBlock(strong_block);

            ///// Hid text to 
            StegoHid(strong_block);

            ///// finally set center point of each block
            SetCenterPoint(strong_block);

            stegoImage(bmpLL);

            txtCompressed.Text = "";
        }

        private void saveLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            string strFileName = "";

            dialog.Filter = "files (.txt)|*.txt|All files (.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                strFileName = dialog.FileName;
            }

            if (strFileName.Length == 0)
            {
                return;
            }

            Mat mLL = ConvertBitmaptoMat(bmpLL);

            List<Point> keyPoints = new List<Point>();
            Image<Rgb, Byte> src_Image = mLL.ToImage<Rgb, Byte>();
            Image<Rgb, Byte> normal_Image = mLL.ToImage<Rgb, Byte>();
            Image<Rgb, Byte> anms_Image = mLL.ToImage<Rgb, Byte>();

            Image<Gray, Byte> gray_Image = src_Image.Convert<Gray, Byte>();
            Image<Gray, Byte> threshold_Image = src_Image.Convert<Gray, Byte>();

            Image<Gray, float> corner_Image = new Image<Gray, float>(gray_Image.Size);
            CvInvoke.CornerHarris(gray_Image, corner_Image, 3, 3, 0.01);

            /// Standard Harris Detector
            CvInvoke.Normalize(corner_Image, corner_Image, 0, 255, NormType.MinMax, DepthType.Cv32F, new Mat());
            CvInvoke.ConvertScaleAbs(corner_Image, corner_Image, 1, 0);

            /// Drawing a circle around corners
            for (int j = 0; j < corner_Image.Rows; j++)
            {
                for (int i = 0; i < corner_Image.Cols; i++)
                {
                    if ((int)corner_Image.Data[j, i, 0] > 20)
                    {
                        Point pt = new Point(i, j);
                        keyPoints.Add(pt);
                        CvInvoke.Circle(normal_Image, pt, 3, new MCvScalar(0, 255, 0), 1, LineType.EightConnected, 0);
                    }
                }
            }

            int radius = 3;
            keyPoints = ANMS(keyPoints, radius);

            for (int nI = 0; nI < keyPoints.Count; nI++)
                CvInvoke.Circle(anms_Image, keyPoints[nI], 3, new MCvScalar(0, 255, 0), 1, LineType.EightConnected, 0);

            CvInvoke.Imshow("Original Image", src_Image);
            CvInvoke.Imshow("Normal Harris Image", normal_Image);
            CvInvoke.Imshow("ANMS Image", anms_Image);

            CvInvoke.WaitKey(0);
            CvInvoke.DestroyWindow("LL");

            //            bmpLL.Save(strFileName, ImageFormat.Bmp);
        }

        private void calc_Measurat(Bitmap ori_Bmp, Bitmap stego_bmp)
        {
            Bitmap bmp3 = ori_Bmp;
            Bitmap bmp4 = stego_bmp;
            double n, sum, mseR, msec, psnr, mseB, mseG, mseg = 0;
            int r1, r2, g1, g2, b1, b2;

            for (int j = 0; j < bmpHH.Height; j++)
            {
                for (int i = 0; i < bmpHH.Width; i++)
                {
                    r1 = bmp3.GetPixel(j, i).R;
                    r2 = bmp4.GetPixel(j, i).R;
                    g1 = bmp3.GetPixel(j, i).G;
                    g2 = bmp4.GetPixel(j, i).G;
                    b1 = bmp3.GetPixel(j, i).B;
                    b2 = bmp4.GetPixel(j, i).B;
                    mseR = Math.Pow(r1 - r2, 2);
                    mseG = Math.Pow(g1 - g2, 2);
                    mseB = Math.Pow(b1 - b2, 2);

                    sum = mseR + mseG + mseB;
                    mseg += sum;
                }
            }
            n = bmp3.Width * bmp4.Height;
            msec = mseg / (n);

            if (msec > 0)
                psnr = (20 * Math.Log10(255)) - (10 * Math.Log10(msec));
            //psnr = (20  Math.Log(255  255 / mse) / Math.Log(10);
            else
                psnr = 99;
            //  txtPsnr.Text = psnr.ToString();
            // txtMse.Text = msec.ToString();
            //double h = pictureBox1.Height;
            //double w = pictureBox1.Width;
            //double size = h * w;
            //textBox1.Text = size.ToString();
            //double z = payload / size;
            //textBox2.Text = payload.ToString();
            //textBox5.Text = z.ToString();
        }

        private Mat GetMatFromBitmap(Bitmap bmp)
        {
            int stride = 0;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                stride = bmp.Width * 4;
            }
            else
            {
                stride = bmp.Width * 3;
            }

            Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

            bmp.UnlockBits(bmpData);

            return cvImage.Mat;
        }

        private Mat ConvertBitmaptoMat(Bitmap bmp)
        {
            // Lock the Bitmap's bits
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            BitmapData bmp_data = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            // pointer to our memory block
            IntPtr data = bmp_data.Scan0;

            int step = bmp_data.Stride;

            Mat mat = new Mat(bmp.Height, bmp.Width, Emgu.CV.CvEnum.DepthType.Cv8U, 3, data, step);
            bmp.UnlockBits(bmp_data);

            return mat;
        }

        public static List<Point> ANMS(List<Point> keyPoints, int radius)
        {
            Dictionary<Point, float> local_max = new Dictionary<Point, float>();
            List<Point> result = new List<Point>();

            for (int nI = 1; nI < keyPoints.Count; nI++)
            {
                float minDist = float.MaxValue;
                for (int nJ = 0; nJ < nI; nJ++)
                {
                    float exp1 = (keyPoints[nJ].X - keyPoints[nI].X);
                    float exp2 = (keyPoints[nJ].Y - keyPoints[nI].Y);
                    float curDist = (float)Math.Sqrt((double)(exp1 * exp1 + exp2 * exp2));
                    minDist = Math.Min(curDist, minDist);
                }

                local_max.Add(keyPoints[nI], minDist);
            }

            foreach (KeyValuePair<Point, float> item in local_max)
            {
                if (item.Value > radius)
                {
                    result.Add(item.Key);
                }
            }

            return result;
        }

        public void StegoHid(List<int> idx_block)
        {
            // calculate number of blocks.
            int block_rows = bmpLL.Height / 3;
            int block_cols = bmpLL.Width / 3;

            Color pedge;
            int r, g, b;
            string bin1, bin2, bin3, sub1, sub2, sub3;

            Bitmap bmp_Src = bmpLL;
            Bitmap bmp_Hide = bmp_Src;

            string msg = txtCompressed.Text;
            int lenmsg = msg.Length;
            int lenth = (lenmsg * 8) + 10;
            string results = Convert.ToString(lenmsg, 2).PadLeft(10, '0');

            int acc = 0;
            string rr = "";
            string[] msgs = new string[(lenmsg * 8) + 10];
            for (int w = 0; w < 10; w++)
            {
                msgs[acc] = results.Substring(w, 1);
                rr += msgs[acc];
                acc++;
            }
            string[] asc = new string[lenmsg];
            int ac = 0;
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(msg);
            foreach (byte vb in ASCIIValues)
            {
                asc[ac] = vb.ToString();
                ac++;
            }

            string gg;
            for (int h = 0; h < lenmsg; h++)
            {
                //  int u = (int)msg[h];
                string uy = asc[h];
                int u = Convert.ToInt32(uy);
//                txtPlainText.Text = u.ToString();
                gg = SteganoProcessing.conbinary(u);


                for (int w = 0; w < 8; w++)
                {
                    msgs[acc] = gg.Substring(w, 1);
                    rr += msgs[acc];
                    acc++;
                }

            }

            int d = 0;
            int payload = 0;

            if (txtCompressed.Text == string.Empty)
            {
                MessageBox.Show("No secret message");
            }
            else
            {
                payload = 0;
                string v;

                for (int idx = 0; idx < idx_block.Count; idx++)
                {
                    int nCol = idx_block[idx] / block_cols;
                    int nRow = idx_block[idx] % block_cols;

                    for (int nI = nRow * 3; nI < (nRow + 1) * 3; nI++)
                        for (int nJ = nCol * 3; nJ < (nCol + 1) * 3; nJ++)
                        {
                            if ((nI == nRow * 3 + 1) && (nJ == nCol * 3 + 1))
                                continue;

                            if (d < lenth)
                            {
                                pedge = bmp_Src.GetPixel(nI, nJ);
                                r = pedge.R;
                                g = pedge.G;
                                b = pedge.B;
                                bin1 = SteganoProcessing.conbinary(r);
                                bin2 = SteganoProcessing.conbinary(g);
                                bin3 = SteganoProcessing.conbinary(b);
                                sub1 = bin1.Substring(0, 3);
                                sub2 = bin2.Substring(0, 3);
                                sub3 = bin3.Substring(0, 3);
                                if (sub1 == "000")
                                {
                                    v = msgs[d];
                                    d++;

                                    bin1 = bin1.Remove(6, 1).Insert(6, v);
                                    r = SteganoProcessing.convdecimal(bin1);
                                    payload++;
                                    // secretextract.Text = bin1;
                                    if (d < lenth)
                                    {
                                        v = msgs[d];
                                        d++;

                                        bin1 = bin1.Remove(7, 1).Insert(7, v);
                                        // secretextract.Text = bin1;
                                        r = SteganoProcessing.convdecimal(bin1);
                                        payload++;
                                    }


                                }
                                else
                                {
                                    if (d < lenth)
                                    {
                                        payload++;
                                        v = msgs[d];
                                        d++;
                                        bin1 = bin1.Remove(7, 1).Insert(7, v);


                                        r = SteganoProcessing.convdecimal(bin1);
                                    }
                                }



                                if (sub2 == "000")
                                {
                                    if (d < lenth)
                                    {
                                        v = msgs[d];
                                        d++;

                                        bin2 = bin2.Remove(6, 1).Insert(6, v);
                                        g = SteganoProcessing.convdecimal(bin2);
                                        payload++;
                                        // secretextract.Text = bin1;
                                    }
                                    if (d < lenth)
                                    {
                                        v = msgs[d];
                                        d++;

                                        bin2 = bin2.Remove(7, 1).Insert(7, v);
                                        g = SteganoProcessing.convdecimal(bin2);
                                        payload++;
                                    }


                                }
                                else
                                {
                                    if (d < lenth)
                                    {
                                        payload++;
                                        v = msgs[d];
                                        d++;
                                        bin2 = bin2.Remove(7, 1).Insert(7, v);


                                        g = SteganoProcessing.convdecimal(bin2);
                                    }
                                }
                                if (sub3 == "000")
                                {
                                    if (d < lenth)
                                    {
                                        v = msgs[d];
                                        d++;

                                        bin3 = bin3.Remove(6, 1).Insert(6, v);
                                        b = SteganoProcessing.convdecimal(bin3);
                                        payload++;
                                        // secretextract.Text = bin1;
                                    }
                                    if (d < lenth)
                                    {
                                        v = msgs[d];
                                        d++;

                                        bin3 = bin3.Remove(7, 1).Insert(7, v);
                                        b = SteganoProcessing.convdecimal(bin3);
                                        payload++;
                                    }
                                }
                                else
                                {
                                    if (d < lenth)
                                    {
                                        payload++;
                                        v = msgs[d];
                                        d++;
                                        bin3 = bin3.Remove(7, 1).Insert(7, v);


                                        b = SteganoProcessing.convdecimal(bin3);
                                    }
                                }

                                bmp_Hide.SetPixel(nI, nJ, Color.FromArgb(r, g, b));

                            }
                        }
                }
            }

            bmpLL = bmp_Hide;
//             picResult.Image = bmpOriginal;
            //            bmpLL = bmp_Hide;
        }

        public string StegoExtract()
        {
            Color pedge;
            int r, g, b;
            string bin1, bin2, bin3, sub1, sub2, sub3;
            //             Bitmap gr = (Bitmap)picResult.Image;

            Bitmap gr = bmpLL;
            int block_rows = bmpLL.Height / 3;
            int block_cols = bmpLL.Width / 3;
            
            string s = "";
            string sub;
            int zl = 0;

            for (int nRow = 0; nRow < block_rows; nRow++)
            {
                for (int nCol = 0; nCol < block_cols; nCol++)
                {
                    if (is_strong(nRow, nCol))
                    {

                        for (int nI = nCol * 3; nI < (nCol + 1) * 3; nI++)
                            for (int nJ = nRow * 3; nJ < (nRow + 1) * 3; nJ++)
                            {

                                if ((nI == nCol * 3 + 1) && (nJ == nRow * 3 + 1))
                                    continue;

                                pedge = gr.GetPixel(nI, nJ);
                                r = pedge.R;
                                g = pedge.G;
                                b = pedge.B;
                                bin1 = SteganoProcessing.conbinary(r);
                                bin2 = SteganoProcessing.conbinary(g);
                                bin3 = SteganoProcessing.conbinary(b);
                                sub1 = bin1.Substring(0, 3);
                                sub2 = bin2.Substring(0, 3);
                                sub3 = bin3.Substring(0, 3);
                                if (sub1 == "000")
                                {

                                    sub = bin1.Substring(6, 2);
                                    s = s.Insert(zl, sub);
                                    zl += 2;
                                    //  s += sub;
                                    //paytxt.Text = v;




                                }
                                else
                                {
                                    sub = bin1.Substring(7, 1);
                                    s = s.Insert(zl, sub);
                                    zl++;
                                    //s += sub;
                                }

                                if (sub2 == "000")
                                {

                                    sub = bin2.Substring(6, 2);
                                    s = s.Insert(zl, sub);
                                    zl += 2;
                                    //s += sub;
                                    //paytxt.Text = v;




                                }
                                else
                                {
                                    sub = bin2.Substring(7, 1);
                                    s = s.Insert(zl, sub);
                                    zl++;
                                    //s += sub;
                                }
                                if (sub3 == "000")
                                {

                                    sub = bin3.Substring(6, 2);
                                    s = s.Insert(zl, sub);
                                    zl += 2;
                                    // s += sub;
                                    //paytxt.Text = v;




                                }
                                else
                                {
                                    sub = bin3.Substring(7, 1);
                                    s = s.Insert(zl, sub);
                                    zl++;
                                    // s += sub;
                                }
                            }

                    }
                }
            }

            string dd = "";
            int l = 0;
            string cc = s.Substring(0, 10);
            int uu = SteganoProcessing.convdecimal(cc);

            int mn = 10;

            //            psnrtxt.Text = uu.ToString();
            for (int i = 0; i < uu; i++)
            {

                string rr = s.Substring(mn, 8);
                mn += 8;

                int u = SteganoProcessing.convdecimal(rr);


                char ccc = (char)u;
                string ux = ccc.ToString();

                dd = dd.Insert(l, ux);
                l++;
            }

            return dd;
        }

        public void SetCenterPoint(List<int> idx_blocks)
        {
            Mat mImage = ConvertBitmaptoMat(bmpLL);
            Image<Rgb, Byte> center_Image = mImage.ToImage<Rgb, Byte>();
            int pos_X, pos_Y;
            
            Color pedge;
            int r, g, b;
            string bin;

            int block_rows = bmpLL.Height / 3;
            int block_cols = bmpLL.Width / 3;

            int strong = 0;

            for (int nI = 0; nI < block_rows; nI++)
            {
                for (int nJ = 0; nJ < block_cols; nJ++)
                {
                    if (idx_blocks.Contains(nI * block_cols + nJ) == true)
                    {
                        strong = 1;
//                         CvInvoke.Rectangle(center_Image, new Rectangle(3 * nJ + 1, 3 * nI + 1, 3, 3), new MCvScalar(0, 255, 0), 2, LineType.EightConnected);
                    }
                    else
                    {
                        strong = 0;
                    }

                    pedge = bmpLL.GetPixel(nJ * 3 + 1, nI * 3 + 1);

                    r = pedge.R;
                    g = pedge.G;
                    b = pedge.B;

                    bin = SteganoProcessing.conbinary(r);
                    bin = bin.Remove(7, 1).Insert(7, Convert.ToString(strong));

                    // secretextract.Text = bin1;
                    r = SteganoProcessing.convdecimal(bin);

                    bmpLL.SetPixel(nJ * 3 + 1, nI * 3 + 1, Color.FromArgb(r, g, b));
                }
            }

//             CvInvoke.Imshow("Center Point", center_Image);
        }

        private void stegoExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //             bmpOriginal = (Bitmap)picResult.Image;
            Mat mLL = ConvertBitmaptoMat(bmpLL);
//             Mat mLL = GetMatFromBitmap(bmpLL);
            Image<Rgb, Byte> center_Image = mLL.ToImage<Rgb, Byte>();
            
            List<int> blocks = new List<int>();

            int rows = mLL.Height / 3;
            int cols = mLL.Width / 3;
            int n = 0;
            for (int nI = 0; nI < rows; nI ++)
                for (int nJ = 0; nJ < cols; nJ ++)
                    if (is_strong(nI, nJ))
                    {
                        blocks.Add(nI * cols + nJ);
                        CvInvoke.Rectangle(center_Image, new Rectangle(3 * nJ, 3 * nI, 3, 3), new MCvScalar(0, 255, 0), 2, LineType.EightConnected);

                    }

            CvInvoke.Imshow("center", center_Image);
            CvInvoke.WaitKey(0);

            showBlock(blocks);

            string chiper_text = "";
            chiper_text = StegoExtract();

            txtCompressed.Text = chiper_text;
        }

        public bool is_strong(int nRow, int nCol)
        {
            int pos_Y = nRow * 3 + 1;
            int pos_X = nCol * 3 + 1;

            Color pedge;
            int r, g, b;

            string bin, sub;

            pedge = bmpLL.GetPixel(pos_X, pos_Y);
            r = pedge.R;
            g = pedge.G;
            b = pedge.B;

            bin = SteganoProcessing.conbinary(r);
            sub = bin.Substring(7, 1);

            if (sub == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void showBlock(List<int> idx_blocks)
        {
            Mat mImage = ConvertBitmaptoMat(bmpLL);
            Image<Rgb, Byte> block_Image = mImage.ToImage<Rgb, Byte>();
            int pos_X, pos_Y;

            int rows = mImage.Height / 3;
            int cols = mImage.Width / 3;

            int row, col;

            for (int idx = 0; idx < idx_blocks.Count; idx++)
            {
                col = idx_blocks[idx] / cols;
                row = idx_blocks[idx] % cols;

                CvInvoke.Rectangle(block_Image, new Rectangle(3 * row, 3 * col, 3, 3), new MCvScalar(0, 255, 0), 2, LineType.EightConnected);
            }

            CvInvoke.Imshow("Blocks", block_Image);
            CvInvoke.WaitKey(0);
        }
    }
}
