﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        MyGraphics myGraphics;
        Random rnd;
        float a = 0, b = 0;
        public Form1()
        {
            InitializeComponent();
            myGraphics = new MyGraphics(pictureBox1);
            rnd = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DrawPolygon(myGraphics.grp, Pr(4, new PointF(250, 250), 150, 0));

            //for (float a = 0; a <= (float)Math.PI * 2; a += 0.01f)
            //{
            //	DrawPolygon(myGraphics.grp, Pr(4, new PointF(250, 250), 150, a));
            //}

            //for (float a = 0; a <= 200; a += 10)
            //{
            //	DrawPolygon(myGraphics.grp, Pr(4, new PointF(250, 250), a, 0));
            //}
            //float b = 0;
            //for (float a = 0; a <= 200; a += 10, b += 0.1f)
            //{
            //	DrawPoints(myGraphics.grp, Pr(6, new PointF(250, 250), a, b));
            //}


            //DrawPolygon(myGraphics.grp, IregularPolygon(10, new PointF(250,250), 100, 200, 0.1f));

            //PentagonRec(myGraphics.grp, 5, new PointF(pictureBox1.Width/2, pictureBox1.Height/2), 150, 200, 0);
            //RegularRec(myGraphics.grp, 5, new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2), 150, 0);
            
            /*float n = 200;
            float pi = (float)Math.PI;
            FillPolygon(myGraphics.grp, Pr(3, new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2), n, pi + pi / 6), Color.White);

            FillPolygon(myGraphics.grp, Pr(3, new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2), 0.96f*n, pi+pi/6), Color.Red);

            FillPolygon(myGraphics.grp, Pr(3, new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2), 0.5f*n, pi+pi/6),Color.White);
            */
            PointF A = new PointF
            DrawSierprnski(myGraphics.grp, )
            myGraphics.Refresh();

        }

        public void DrawTriungle(Graphics grp, PointF A, PointF B, PointF C)
        {
            grp.DrawLine(Pens.Black, A, B);
            grp.DrawLine(Pens.Black, B, C);
            grp.DrawLine(Pens.Black, C, A);
        }

        public void DrawSierprnski(Graphics grp, PointF A, PointF B, PointF C)
        {
            if (PointDistance(A, B) > 1 && PointDistance(A, C) > 1 && PointDistance(C, B) > 1)
            {
                float k1 = 1;
                float k2 = 3;

                DrawTriungle(grp, A, B, C);
                PointF M = new PointF((A.X + B.X) / 2, (A.Y + B.Y) / 2);
                PointF N = new PointF((C.X + A.X) / 2, (C.Y + A.Y) / 2);
                PointF P = new PointF((B.X + C.X) / 2, (B.Y + C.Y) / 2);

                DrawTriungle(grp, A, M, N);
                DrawTriungle(grp, M, B, P);
                DrawTriungle(grp, N, P, C);
            }

        }

        public float PointDistance(PointF A, PointF B)
        {
            return (float)Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
        }

        private void RegularRec(Graphics grp, int n, PointF C, float r, float fi)
        {
            List<PointF> t = Pr(n, C, r, fi);

            if (AreaPolygon(C, t) > 100)
            {
                DrawPolygon(grp, t);

                foreach (PointF p in t)
                {
                    //if(rnd.Next(2) == 0)
                    RegularRec(grp, n, p, r / 2, fi + 0.2f);
                }
            }
        }

        private void PentagonRec(Graphics grp, int n, PointF C, float minR, float maxR, float fi)
        {
            List<PointF> t = IregularPolygon(n, C, minR, maxR, fi);

            if (AreaPolygon(C, t) > 10)
            {
                DrawPolygon(grp, t);

                foreach (PointF p in t)
                {
                    PentagonRec(grp, n, p, minR / 2, maxR / 2, fi);
                }
            }
        }

        public float Determinant(PointF A, PointF B, PointF C)
        {
            return (A.X * B.Y + B.X * C.Y + A.Y * C.X - C.X * B.Y - A.Y * B.X - C.Y * A.X);
        }

        public float Area(PointF A, PointF B, PointF C)
        {
            return 0.5f * (float)Math.Abs(Determinant(A, B, C));
        }

        public float AreaPolygon(PointF C, List<PointF> points)
        {
            float sum = 0;

            for (int i = 0; i < points.Count; i++)
            {
                sum += Area(C, points[i], points[(i + 1) % points.Count]);
            }

            return sum;
        }

        private List<PointF> Pr(int n, PointF C, float R, float fi)
        {
            List<PointF> toReturn = new List<PointF>();

            float alpha = (float)(2 * Math.PI / n);

            for (int i = 0; i < n; i++)
            {
                float x = C.X + R * (float)Math.Cos(i * alpha + fi);
                float y = C.Y + R * (float)Math.Sin(i * alpha + fi);

                toReturn.Add(new PointF(x, y));
            }

            return toReturn;
        }

        private List<PointF> IregularPolygon(int n, PointF C, float minR, float maxR, float fi) //random values
        {
            List<PointF> toReturn = new List<PointF>();

            float[] alpha = new float[n];
            float[] dist = new float[n];

            for (int i = 0; i < n; i++)
            {
                alpha[i] = (float)rnd.NextDouble() * (float)(2 * 3.1415);
                dist[i] = (float)rnd.NextDouble() * (maxR - minR) + minR;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (alpha[i] > alpha[j])
                    {
                        float aux = alpha[i];
                        alpha[i] = alpha[j];
                        alpha[j] = aux;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                float x = C.X + dist[i] * (float)Math.Cos(alpha[i] + fi);
                float y = C.Y + dist[i] * (float)Math.Sin(alpha[i] + fi);

                toReturn.Add(new PointF(x, y));
                myGraphics.grp.DrawLine(Pens.Red, x, y, C.X, C.Y);
            }

            return toReturn;
        }

        public void FillPolygon(Graphics grp, List<PointF> points, Color fillColor)
        {
            grp.FillPolygon(new SolidBrush(fillColor), points.ToArray());
            grp.DrawPolygon(new Pen(Color.Black), points.ToArray());
        }

        public void DrawPolygon(Graphics grp, List<PointF> points)
        {
            grp.DrawPolygon(new Pen(Color.Black), points.ToArray());
        }

        public void DrawPoints(Graphics grp, List<PointF> points)
        {
            foreach (var point in points)
            {
                grp.DrawEllipse(new Pen(Color.Black), point.X - 1, point.Y - 1, 3, 3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a += 10;
            b += 0.1f;

            if (a >= 200)
            {
                a = 10;
            }

            myGraphics.Clear();
            DrawPoints(myGraphics.grp, Pr(100, new PointF(250, 250), a, b));
            myGraphics.Refresh();
        }
    }
}
