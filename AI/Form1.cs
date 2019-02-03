using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            m_oWorker = new BackgroundWorker();
            m_oWorker.DoWork += new DoWorkEventHandler(Ai2_Against_Ai1);
            m_oWorker.WorkerReportsProgress = true;
            m_oWorker.WorkerSupportsCancellation = true;
            delayTime = gameSpeed.Value;
        }
        Graphics g;
        
        class Pointt
        {
            float y;
            float x;

            // 0 = right   1 = bottom   2 = left   3 = top
            public int[] dir = new int[4];
            
            public Pointt(float x, float y)
            {
                this.X = x;
                this.Y = y;
                dir[0] = dir[1] = dir[2] = dir[3] = 0;
            }

            public float X { get => x; set => x = value; }
            public float Y { get => y; set => y = value; }

            public override string ToString()
            {
                return ("X: " + X + " Y: " + Y);
            }
        }

        private class Line
        {
            public PointF p1, p2;
            public Line(PointF p1, PointF p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }
            public Line(Line line)
            {
                this.p1 = new PointF(line.p1.X, line.p1.Y);
                this.p2 = new PointF(line.p2.X, line.p2.Y);
            }


            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return p1.ToString() + " " + p2.ToString();
            }

            public static bool operator == (Line l1, Line l2)
            {
                if (l1.p1 == l2.p1 && l1.p2 == l2.p2)
                    return true;
                if (l1.p2 == l2.p1 && l1.p1 == l2.p2)
                    return true;

                return false;
            }

            public static bool operator != (Line l1, Line l2)
            {
                if (l1.p1 == l2.p1 && l1.p2 == l2.p2)
                    return false;
                if (l1.p2 == l2.p1 && l1.p1 == l2.p2)
                    return false;

                return true;
            }
        }

        int n = 4;
        int m = 4;

        // فاصله نقاط اطراف آرایه با کناره فرم
        int offset = 25;

        // اندازه هر نقطه
        int sizeofdot = 10;

        // حالت بازی
        // 0 = player   1 = Ai1
        // 0 = player   1 = Random
        // 0 = Ai2      1 = Ai1
        int turn = 0;

        // -1 = not started     1 = AI2 And AI1     2 = Player And AI1       3 = Player And Random
        int gameMode = -1; 

        // آرایه برای ذخیره نقاط
        Pointt[,] array;
        
        // فاصله بین خطوط در محور 
        // x
        float distanceBetweenDotX;

        // فاصله بین خطوط در محور 
        // y
        float distanceBetweenDotY;

        //پیدا کردن خانه نقطه در آرایه
        // ( i * distanceBetweenDotX ) + offset
        // ( j * distanceBetweenDotY ) + offset


        // حداکثر تعداد خط
        int maxLine;

        // لیست کل خطوط
        List<Pointt> points = new List<Pointt>();

        // لیست خطوط کشیده شده
        List<Line> lines;
        List<Line> lines_notDrawen;

        // در خانه اول، مقدار امتیاز بازیکن اول
        // در خانه دوم، بازیکن دوم
        int [] scores = new int[2];

        // زمان وقفه حرکت کامپیوتر
        int delayTime; // 200

        // رنگ نقاط
        Brush pointColor = Brushes.Black;

        // رنگ بازیکن یک
        Pen lineRedColor = Pens.Red;
        Brush squareRedColor = Brushes.Red;

        // رتگ بازیکن دو
        Pen lineBlueColor = Pens.Blue;
        Brush squareBlueColor = Brushes.Blue;
        
        // چک کردن صحیح بودن نقطه
        bool IsTruePoint(PointF p)
        {
            if (p.X == -1 || p.Y == -1)
                return false;

            foreach(var q in points)
                if (q.X == p.X && q.Y == p.Y)
                    return true;

            return false;
        }
        
        // چک کردن اینکه این خط قبلا رسم شده است یا نه
        bool IsDrawedLine(List<Line> lines, PointF p1, PointF p2)
        {
            foreach(var l in lines)
            {
                if(l.p1 == p1)
                {
                    if (l.p2 == p2)
                        return true;
                }
                else if(l.p1 == p2)
                {
                    if (l.p2 == p1)
                        return true;
                }
                else if(l.p2 == p1)
                {
                    if (l.p1 == p2)
                        return true;
                }
                else if(l.p2 == p2)
                {
                    if (l.p1 == p1)
                        return true;
                }
            }
            return false;
        }
        bool IsDrawedLine(List<Line> lines, Line line)
        {
            return IsDrawedLine(lines, line.p1, line.p2);
        }


        // چک کردن صحیح بودن خط
        bool IsTrueLine(List<Line> lines, PointF p1, PointF p2)
        {
            if (!IsTruePoint(p1) || !IsTruePoint(p2))
                return false;

            double d = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            if (p1.X == p2.X && d == distanceBetweenDotY)
            {
                if (IsDrawedLine(lines, p1, p2))
                    return false;

                return true;
            }
            else if(p1.Y == p2.Y && d == distanceBetweenDotX)
            {
                if (IsDrawedLine(lines, p1, p2))
                    return false;

                return true;
            }
            return false;
        }
        bool IsTrueLine(List<Line> lines, Line line)
        {
            return IsTrueLine(lines, line.p1, line.p2);
        }

        // Logging
        void Log(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new MethodInvoker(delegate {
                    textBox1.Text = (text + Environment.NewLine) + textBox1.Text;
                }));
            }
            else
            {
                textBox1.Text = (text + Environment.NewLine) + textBox1.Text;
            }
        }

        // برای نمایش پیغام امیتاز گرفتن بازیکن
        bool MakeLine(List<Line> lines, PointF point1, PointF point2)
        {
            
            if (!IsTrueLine(lines, point1, point2))
                return false;

            int i1 = Convert.ToInt32((point1.X - offset) / distanceBetweenDotX); 
            int j1 = Convert.ToInt32((point1.Y - offset) / distanceBetweenDotY);

            int i2 = Convert.ToInt32((point2.X - offset) / distanceBetweenDotX);
            int j2 = Convert.ToInt32((point2.Y - offset) / distanceBetweenDotY);

            // آیا این خط باعث کامل شدن یک خانه میشود؟
            int[] check = CheckState(lines, point1,point2);

            // apply change to array
            if(point1.X == point2.X) // top or bottom
            {
                if(point2.Y < point1.Y) // top
                {
                    array[i1, j1].dir[3] = 1;
                    array[i2, j2].dir[1] = 1;
                }
                else // bottom
                {
                    array[i1, j1].dir[1] = 1;
                    array[i2, j2].dir[3] = 1;
                }
            }
            else // left or right
            {
                if (point2.X > point1.X) // right
                {
                    array[i1, j1].dir[0] = 1;
                    array[i2, j2].dir[2] = 1;
                }
                else // left
                {
                    array[i1, j1].dir[2] = 1;
                    array[i2, j2].dir[0] = 1;
                }
            }

            bool scored = false;

            // draw line
            switch (turn)
            {
                case 0:
                    g.FillPie(squareRedColor, point1.X, point1.Y, sizeofdot, sizeofdot, 0, 360);
                    g.FillPie(squareRedColor, point2.X, point2.Y, sizeofdot, sizeofdot, 0, 360);

                    Pen red = new Pen(lineRedColor.Color, 8);
                    red.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    g.DrawLine(
                        red,
                        new PointF(point1.X + (sizeofdot / 2), point1.Y + (sizeofdot / 2)),
                        new PointF(point2.X + (sizeofdot / 2), point2.Y + (sizeofdot / 2))
                        );

                    Log("Red (" + (i1 + 1) + ", " + (j1 + 1) + ") To (" + (i2 + 1) + ", " + (j2 + 1) + ")");

                    scored = FillSquare(squareRedColor, check, point1, point2);

                    if (!scored)
                    {
                        Log("Blue turn...");
                        turnStatus.ForeColor = lineBlueColor.Color;
                    }
                    else
                    {
                        turnStatus.ForeColor = lineRedColor.Color;
                        Log("Got a reward.\nRed turn");
                    }

                    break;

                case 1:
                    g.FillPie(squareBlueColor, point1.X, point1.Y, sizeofdot, sizeofdot, 0, 360);
                    g.FillPie(squareBlueColor, point2.X, point2.Y, sizeofdot, sizeofdot, 0, 360);

                    Pen blue = new Pen(lineBlueColor.Color, 8);
                    blue.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    g.DrawLine(
                        blue,
                        new PointF(point1.X + (sizeofdot / 2), point1.Y + (sizeofdot / 2)),
                        new PointF(point2.X + (sizeofdot / 2), point2.Y + (sizeofdot / 2))
                        );

                    Log("Blue (" + (i1 + 1) + ", " + (j1 + 1) + ") To (" + (i2 + 1) + ", " + (j2 + 1) + ")");

                    scored = FillSquare(squareBlueColor, check, point1, point2);

                    if (!scored)
                    {
                        Log("Red turn...");
                        turnStatus.ForeColor = lineRedColor.Color;
                    }
                    else
                    {
                        turnStatus.ForeColor = lineBlueColor.Color;
                        Log("Got a reward.\nBlue turn...");
                    }

                    break;
            }

            UpdateScores();

            if (!scored)
            {
                turn++;
                turn = turn % 2;
            }

            lines.Add(new Line(point1, point2));
            RemoveFromNotDrawenLines(lines_notDrawen, new Line(point1, point2));
            
            return true;
        }
        bool MakeLine(List<Line> lines, int i1, int j1, int i2, int j2)
        {
            PointF p1 = new PointF(-1, -1), p2 = new PointF(-1, -1);

            p1.X = i1 * distanceBetweenDotX + offset;
            p1.Y = j1 * distanceBetweenDotY + offset;

            p2.X = i2 * distanceBetweenDotX + offset;
            p2.Y = j2 * distanceBetweenDotY + offset;
            return MakeLine(lines, p1, p2);
        }
        bool MakeLine(List<Line> lines, Line line)
        {
            return MakeLine(lines, line.p1, line.p2);
        }

        // چک کردن پایان بازی
        bool IsGameFinished(List<Line> lines)
        {
            if (maxLine > lines.Count)
                return false;
            return true;
        }

        int distanceFromPoint = 5;
        Pointt FindPoint(PointF p1)
        {
            foreach(var p in points)
            {
                double d = Math.Sqrt(Math.Pow(p1.X - p.X, 2) + Math.Pow(p1.Y - p.Y, 2));
                if (d <= distanceFromPoint + distanceFromPoint)
                    return p;
            }
            return new Pointt(-1,-1);
        }

        // [0] = BottomRight
        // [1] = TopRight
        // [2] = TopLeft
        // [3] = BottomLeft
        int[] CheckState(List<Line> lines, PointF Cp1, PointF Cp2)
        {
            int[] res = new int[4];
            res[0] = res[1] = res[2] = res[3] = 0;

            float x = distanceBetweenDotX;
            float y = distanceBetweenDotY;

            if (Cp1.X == Cp2.X)
            {
                if(Cp1.Y > Cp2.Y)
                {
                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X + x, Cp1.Y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y), new PointF(Cp1.X + x, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y - y), new PointF(Cp1.X, Cp1.Y - y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[3] = 1;

                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X - x, Cp1.Y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y), new PointF(Cp1.X - x, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y - y), new PointF(Cp1.X, Cp1.Y - y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[2] = 1;
                }
                else
                {
                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X - x, Cp1.Y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y), new PointF(Cp1.X - x, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y + y), new PointF(Cp1.X, Cp1.Y + y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[1] = 1;

                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X + x, Cp1.Y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y), new PointF(Cp1.X + x, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y + y), new PointF(Cp1.X, Cp1.Y + y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[0] = 1;
                }
            }
            else if(Cp1.Y == Cp2.Y)
            {
                if(Cp1.X > Cp2.X)
                {
                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X, Cp1.Y - y), new PointF(Cp1.X - x, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y - y), new PointF(Cp1.X - x, Cp1.Y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                       )
                        res[2] = 1;

                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X, Cp1.Y + y), new PointF(Cp1.X - x, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X - x, Cp1.Y + y), new PointF(Cp1.X - x, Cp1.Y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[1] = 1;
                }
                else
                {
                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X, Cp1.Y - y), new PointF(Cp1.X + x, Cp1.Y - y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y - y), new PointF(Cp1.X + x, Cp1.Y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[3] = 1;

                    if (IsDrawedLine(lines, Cp1, new PointF(Cp1.X, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X, Cp1.Y + y), new PointF(Cp1.X + x, Cp1.Y + y)) &&
                        IsDrawedLine(lines, new PointF(Cp1.X + x, Cp1.Y + y), new PointF(Cp1.X + x, Cp1.Y)) &&
                        !IsDrawedLine(lines, Cp1, Cp2)
                        )
                        res[0] = 1;
                }
            }

            return res;
        }
        int[] CheckState(List<Line> lines, Line line)
        {
            return CheckState(lines, line.p1, line.p2);
        }

        // تابع کامپیوتر بصورت خط تصادفی
        void ComUsingRand(int turnNumber)
        {
            a:
            Delay(delayTime);
            if (turn != turnNumber || IsGameFinished(lines))
            {
                return;
            }

            /*
            // since there is RandomLine function, we don't need the following code
            PointF Cp1 = new PointF(-1, -1), Cp2 = new PointF(-1, -1);
            Random r = new Random();

            int i1 = r.Next() % n;
            int j1 = r.Next() % m;

            int i2 = r.Next() % n;
            int j2 = r.Next() % m;

            while (!IsTrueLine(lines, Cp1, Cp2))
            {
                i1 = r.Next() % n;
                j1 = r.Next() % m;

                i2 = r.Next() % n;
                j2 = r.Next() % m;

                Cp1 = new PointF(
                    (i1 * distanceBetweenDotX) + offset,
                    (j1 * distanceBetweenDotY) + offset
                    );

                Cp2 = new PointF(
                    (i2 * distanceBetweenDotX) + offset,
                    (j2 * distanceBetweenDotY) + offset
                    );
            }
            */

            if (!MakeLine(lines, RandomLine()))
            {
                Log("Rand couldn't move!");
                button2_Click(this, null);
                return;
            }
            goto a;
        }

        int evalaute(int[] checks, bool Max)
        {
            int res = 0;
            foreach(var i in checks)
            {
                if(i == 1 && Max)
                {
                    res += 10;
                }
                else if (i == 1 && !Max)
                {
                    res -= 10;
                }
            }
            return res;
        }

        List<Line> CopyOfLineList (List<Line> list)
        {
            List<Line> res = new List<Line>();
            foreach(var l in list)
            {
                res.Add(new Line(l));
            }
            return res;
        }

        bool Scored(int[] checks)
        {
            foreach (var i in checks)
                if (i == 1)
                    return true;
            return false;
        }

        int MinMax(List<Line> lines, List<Line> lines_notDrawen, int depth, bool Max)
        {
            if(IsGameFinished(lines) || depth == 0 || lines_notDrawen.Count == 0)
            {
                return 0;
            }

            if (Max)
            {
                int value = int.MinValue;
                foreach(var l in lines_notDrawen)
                {
                    if (IsTrueLine(lines, l))
                    {
                        List<Line> clines = CopyOfLineList(lines);
                        List<Line> clines_notDrawen = CopyOfLineList(lines_notDrawen);
                        int[] check = CheckState(lines, l);
                        int v = evalaute(check, Max);

                        clines.Add(l);
                        RemoveFromNotDrawenLines(clines_notDrawen, l);

                        if (Scored(check))
                            v += MinMax(clines, clines_notDrawen, depth - 1, true);
                        else
                            v += MinMax(clines, clines_notDrawen, depth - 1, false);

                        if (v > value)
                        {
                            value = v;
                        }
                    }
                }
                return value;
            }
            else
            {
                int value = int.MaxValue;
                foreach (var l in lines_notDrawen)
                {
                    if (IsTrueLine(lines, l))
                    {
                        List<Line> clines = CopyOfLineList(lines);
                        List<Line> clines_notDrawen = CopyOfLineList(lines_notDrawen);

                        int[] check = CheckState(lines, l);
                        int v = evalaute(check, Max);

                        clines.Add(l);
                        RemoveFromNotDrawenLines(clines_notDrawen, l);

                        if (Scored(check))
                            v += MinMax(clines, clines_notDrawen, depth - 1, false);
                        else
                            v += MinMax(clines, clines_notDrawen, depth - 1, true);

                        if (v < value)
                        {
                            value = v;
                        }
                    }
                }
                return value;
            }
        }

        Line RandomLine()
        {
            PointF Cp1 = new PointF(-1, -1), Cp2 = new PointF(-1, -1);
            Random r = new Random();

            int i1 = r.Next() % n;
            int j1 = r.Next() % m;

            int i2 = r.Next() % n;
            int j2 = r.Next() % m;

            while (!IsTrueLine(lines, Cp1, Cp2))
            {
                i1 = r.Next() % n;
                j1 = r.Next() % m;

                i2 = r.Next() % n;
                j2 = r.Next() % m;

                Cp1 = new PointF(
                    (i1 * distanceBetweenDotX) + offset,
                    (j1 * distanceBetweenDotY) + offset
                    );

                Cp2 = new PointF(
                    (i2 * distanceBetweenDotX) + offset,
                    (j2 * distanceBetweenDotY) + offset
                    );
            }

            return new Line(Cp1, Cp2);
        }

        int depth = 3;
        // تابع اول هوش مصنوعی
        void ComUsingAi1(int turnNumber)
        {
            a:
            Delay(delayTime);
            if (turn != turnNumber || IsGameFinished(lines))
            {
                return;
            }

            int max = int.MinValue;
            Line line = null;
            if(lines.Count > 2)
            {
                foreach (var l in lines_notDrawen)
                {
                    List<Line> clines = CopyOfLineList(lines);
                    List<Line> clines_notDrawen = CopyOfLineList(lines_notDrawen);

                    clines.Add(l);
                    RemoveFromNotDrawenLines(clines_notDrawen, l);

                    int[] check = CheckState(lines, l);
                    int m = MinMax(clines, clines_notDrawen, depth, Scored(check));

                    if (m > max)
                    {
                        max = m;
                        line = l;
                    }
                }
            }
            else
            {
                line = RandomLine();
            }
            

            if (!MakeLine(lines, line))
            {
                Log("Ai1 couldn't move!");
                button2_Click(this, null);
                return;
            }

            goto a;
        }
        
        // تابع دوم هوش مصنوعی
        void ComUsingAi2(int turnNumber)
        {
            a:
            Delay(delayTime);
            if (turn != turnNumber || IsGameFinished(lines))
            {
                return;
            }

            int max = int.MinValue;
            Line line = null;
            if (lines.Count > 2)
            {
                foreach (var l in lines_notDrawen)
                {
                    List<Line> clines = CopyOfLineList(lines);
                    List<Line> clines_notDrawen = CopyOfLineList(lines_notDrawen);

                    clines.Add(l);
                    RemoveFromNotDrawenLines(clines_notDrawen, l);

                    int[] check = CheckState(lines, l);
                    int m = MinMax(clines, clines_notDrawen, depth, Scored(check));

                    if (m > max)
                    {
                        max = m;
                        line = l;
                    }
                }
            }
            else
            {
                line = RandomLine();
            }


            if (!MakeLine(lines, line))
            {
                Log("Ai2 couldn't move!");
                button2_Click(this, null);
                return;
            }
            goto a;
        }

        void Ai2_Against_Ai1(object sender, DoWorkEventArgs e)
        {
            while (!IsGameFinished(lines))
            {
                if (IsGameFinished(lines))
                    break;

                ComUsingAi2(0); //0
                //ComUsingRand(0);

                if (IsGameFinished(lines))
                    break;

                ComUsingAi1(1); //1
                //ComUsingRand(1);

                if (m_oWorker.CancellationPending)
                {
                    e.Cancel = true;
                    m_oWorker.ReportProgress(0);
                    return;
                }
            }
            button2_Click(this, null);
        }

        void Player_Against_ComAi1(PointF point)
        {
            Player(point);
            ComUsingAi1(1);
        }

        void Player_Against_Random(PointF point)
        {
            Player(point);
            ComUsingRand(1);
        }



        // از این تابع به پایین بچه ها نیازی ندارند
        
        BackgroundWorker m_oWorker;
        private void Stop(object sender, EventArgs e)
        {
            if (m_oWorker.IsBusy)
            {
                m_oWorker.CancelAsync();
            }
        }

        // player clicked point
        PointF point1, point2;
        int indexUpdatePoint = 0;
        void Player(PointF point)
        {
            if (turn != 0 || IsGameFinished(lines))
                return;

            if (!IsTruePoint(point))
                return;


            switch (indexUpdatePoint)
            {
                case 0:
                    point1.X = point.X;
                    point1.Y = point.Y;

                    Log("click on next point...");

                    indexUpdatePoint = 1;
                    break;

                case 1:
                    point2.X = point.X;
                    point2.Y = point.Y;

                    int i1, i2, j1, j2;

                    i1 = Convert.ToInt32((point1.X - offset) / distanceBetweenDotX);
                    j1 = Convert.ToInt32((point1.Y - offset) / distanceBetweenDotX);
                    i2 = Convert.ToInt32((point2.X - offset) / distanceBetweenDotX);
                    j2 = Convert.ToInt32((point2.Y - offset) / distanceBetweenDotX);

                    if (!MakeLine(lines, point1, point2))
                    {
                        Log("Wrong points, try again!"); 
                    }
                    
                    indexUpdatePoint = 0;
                    break;

                default:
                    Log("Something's wrong with indexUpdatePoint: " + indexUpdatePoint);
                    indexUpdatePoint = 0;
                    break;
            }

        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsGameFinished(lines))
            {
                button2_Click(this, null);
            }
            else if(gameMode == 2)
            {
                Pointt point = FindPoint(new PointF(e.X, e.Y));
                Player_Against_ComAi1(new PointF(point.X,point.Y));
            }
            else if(gameMode == 3)
            {
                Pointt point = FindPoint(new PointF(e.X, e.Y));
                Player_Against_Random(new PointF(point.X, point.Y));
            }
        }

        bool FillSquare(Brush brush, int[] check, PointF point1, PointF point2)
        {
            bool scored = false;
            if (check[0] == 1)
            {
                scores[turn]++;
                scored = true;
                Log("BR");
                g.FillRectangle(brush, point1.X + (sizeofdot / 2), point1.Y + (sizeofdot / 2), distanceBetweenDotX, distanceBetweenDotY);
            }
            if (check[1] == 1)
            {
                scores[turn]++;
                scored = true;
                Log("TR");
                g.FillRectangle(brush, point1.X + (sizeofdot / 2) - distanceBetweenDotX, point1.Y + (sizeofdot / 2), distanceBetweenDotX, distanceBetweenDotY);
            }
            if (check[2] == 1)
            {
                scores[turn]++;
                scored = true;
                Log("TL");
                g.FillRectangle(brush, point1.X + (sizeofdot / 2) - distanceBetweenDotX, point1.Y + (sizeofdot / 2) - distanceBetweenDotY, distanceBetweenDotX, distanceBetweenDotY);
            }
            if (check[3] == 1)
            {
                scores[turn]++;
                scored = true;
                Log("BL");
                g.FillRectangle(brush, point1.X + (sizeofdot / 2), point1.Y + (sizeofdot / 2) - distanceBetweenDotY, distanceBetweenDotX, distanceBetweenDotY);
            }
            return scored;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void UpdateScores()
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new MethodInvoker(delegate {
                    scorePlayer.Text = scores[0].ToString();
                    scoreCom.Text = scores[1].ToString();
                }));
            }
            else
            {
                scorePlayer.Text = scores[0].ToString();
                scoreCom.Text = scores[1].ToString();
            }
        }

        void Init()
        {
            textBox1.Clear();
            turnStatus.Text = "TURN";
            turnStatus.ForeColor = Color.Red;
            lines_notDrawen = new List<Line>();
            array = new Pointt[n, m];
            maxLine = ((n - 1) * m) + ((m - 1) * n);
            lines = new List<Line>();
            point1 = new PointF(-1, -1);
            point2 = new PointF(-1, -1);
            distanceBetweenDotY = (Height - (offset * 1)) / (float)n;
            distanceBetweenDotX = (Width - (offset * 1) - groupBox1.Width) / (float)m;

            scores[0] = scores[1] = 0;
            UpdateScores();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    g.FillPie(pointColor, (i * distanceBetweenDotX) + offset, (j * distanceBetweenDotY) + offset, sizeofdot, sizeofdot, 0, 360);

                    Pointt t = new Pointt((i * distanceBetweenDotX) + offset, (j * distanceBetweenDotY) + offset);

                    // top
                    if (i == 0)
                    {
                        t.dir[3] = 1;
                    }
                    // left
                    if (j == 0)
                    {
                        t.dir[2] = 1;
                    }
                    // bottom
                    if (i == n - 1)
                    {
                        t.dir[1] = 1;
                    }
                    // right
                    if (j == m - 1)
                    {
                        t.dir[0] = 1;
                    }

                    points.Add(t);
                    array[i, j] = t;
                }
            }

            foreach (var t in points) 
            {
                if (t.dir[0] == 0)
                {
                    PointF p1 = new PointF(t.X, t.Y);
                    PointF p2 = new PointF(t.X + distanceBetweenDotX, t.Y);
                    AddToNotDrawenLines(new Line(p1, p2));
                }
                if (t.dir[1] == 0)
                {
                    PointF p1 = new PointF(t.X, t.Y);
                    PointF p2 = new PointF(t.X, t.Y + distanceBetweenDotY);
                    AddToNotDrawenLines(new Line(p1, p2));
                }
                if (t.dir[2] == 0)
                {
                    PointF p1 = new PointF(t.X, t.Y);
                    PointF p2 = new PointF(t.X - distanceBetweenDotX, t.Y);
                    AddToNotDrawenLines(new Line(p1, p2));
                }
                if (t.dir[3] == 0)
                {
                    PointF p1 = new PointF(t.X, t.Y);
                    PointF p2 = new PointF(t.X, t.Y - distanceBetweenDotY);
                    AddToNotDrawenLines(new Line(p1, p2));
                }
            }

            //turnStatus.Text += Environment.NewLine + lines_notDrawen.Count;
        }

        void AddToNotDrawenLines(Line line)
        {
            if (!IsTrueLine(lines, line))
                return;
            bool f = true;
            foreach(var l in lines_notDrawen)
                if (l == line)
                    f = false;

            if (f)
                lines_notDrawen.Add(line);
        }

        void RemoveFromNotDrawenLines(List<Line> lines_NotDrawen, Line line)
        {
            for (int i = 0; i < lines_NotDrawen.Count; i++)
            {
                if(lines_NotDrawen[i] == line)
                {
                    lines_NotDrawen.RemoveAt(i);
                    break;
                }
            }
        }

        bool IsLineInNotDrawenLines(Line line)
        {
            foreach(var l in lines_notDrawen)
            {
                if(line == l)
                {
                    return true;
                }
            }
            return false;
        }

        void Delay(int milliSecondsDelay)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(milliSecondsDelay);
                return 42;
            });
            t.Wait();
        }

        private void gameSpeed_Click(object sender, EventArgs e)
        {
            Point CP = gameSpeed.PointToClient(Cursor.Position);
            gameSpeed.Value = gameSpeed.Minimum + (gameSpeed.Maximum - gameSpeed.Minimum) * CP.X / gameSpeed.Width;
            delayTime = gameSpeed.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Start")
            {
                delayTime = 800;
                g.Clear(Color.DarkGray);
                button2.Text = "Stop";
                if (rbAi2AndAi1.Checked)
                {
                    Init();
                    gameMode = 1;
                    delayTime = 200;
                    rbPlayerAndRandom.Enabled = false;
                    rbPlayerAndAi1.Enabled = false;
                    rbAi2AndAi1.Enabled = false;
                    m_oWorker.RunWorkerAsync();
                }
                else if (rbPlayerAndAi1.Checked)
                {
                    Init();
                    gameMode = 2;
                    rbPlayerAndRandom.Enabled = false;
                    rbAi2AndAi1.Enabled = false;
                    rbPlayerAndAi1.Enabled = false;
                }
                else if (rbPlayerAndRandom.Checked)
                {
                    Init();
                    gameMode = 3;
                    rbAi2AndAi1.Enabled = false;
                    rbPlayerAndAi1.Enabled = false;
                    rbPlayerAndRandom.Enabled = false;
                }
                else
                {
                    gameMode = -1;
                    MessageBox.Show("Please select a method...");
                }
            }
            else
            {
                Stop(this, null);
                if (lines.Count >= maxLine)
                {
                    string s = "";
                    if (scores[0] > scores[1])
                        s = "Red wins!";
                    else if (scores[1] > scores[0])
                        s = "Blue wins!";
                    else
                        s = "Equals";

                    Log(s);
                    MessageBox.Show(s);
                }
                else
                {
                    MessageBox.Show("Game Stopped!");
                }

                gameMode = -1;
                if (rbAi2AndAi1.InvokeRequired)
                {
                    rbAi2AndAi1.Invoke(new MethodInvoker(delegate {
                        rbAi2AndAi1.Enabled = true;
                    }));
                }
                else
                {
                    rbAi2AndAi1.Enabled = true;
                }

                if (rbPlayerAndAi1.InvokeRequired)
                {
                    rbPlayerAndAi1.Invoke(new MethodInvoker(delegate {
                        rbPlayerAndAi1.Enabled = true;
                    }));
                }
                else
                {
                    rbPlayerAndAi1.Enabled = true;
                }

                if (rbPlayerAndRandom.InvokeRequired)
                {
                    rbPlayerAndRandom.Invoke(new MethodInvoker(delegate {
                        rbPlayerAndRandom.Enabled = true;
                    }));
                }
                else
                {
                    rbPlayerAndRandom.Enabled = true;
                }

                if (button2.InvokeRequired)
                {
                    button2.Invoke(new MethodInvoker(delegate {
                        button2.Text = "Start";
                    }));
                }
                else
                {
                    button2.Text = "Start";
                }
            }
        }
    }
}
