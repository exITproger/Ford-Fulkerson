using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MaxFlow
{
    public partial class MaxFlowForm : Form
    {
        private List<int> currentPath = null;
        private int[,] graph;
        private int[,] residualGraph;
        private int[,] flowMatrix;
        private int source, sink;
        private int[] parent;
        private int maxFlow = 0;
        private int step = 1;
        private Queue<List<int>> paths = new Queue<List<int>>();
        private Queue<int> pathFlows = new Queue<int>();

        public MaxFlowForm()
        {
            InitializeComponent();
            btnNextStep.Enabled = false;
            btnResultNow.Enabled = false;
            Resize += (s, e) => {
                if (graph != null)
                    DrawGraph(graph, flowMatrix);
            };

        }

        private void btnLoadGraph_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Files|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    graph = ReadGraphFromFile(dlg.FileName);
                    DrawGraph(graph, null);
                    lblStatus.Text = "Граф загружен. Введите исток и сток.";
                    btnResultNow.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSource.Text, out source) ||
                !int.TryParse(txtSink.Text, out sink) ||
                source == sink ||
                source < 1 || sink < 1 ||
                source > graph.GetLength(0) || sink > graph.GetLength(0))
            {
                MessageBox.Show("Введите корректные номера истока и стока.");
                return;
            }

            source--; sink--;

            residualGraph = (int[,])graph.Clone();
            flowMatrix = new int[graph.GetLength(0), graph.GetLength(0)];
            parent = new int[graph.GetLength(0)];
            maxFlow = 0;
            step = 1;
            paths.Clear();
            pathFlows.Clear();
            currentPath = null;

            btnNextStep.Enabled = true;
            btnResultNow.Enabled = true;
            lblStatus.Text = "Нажимайте 'Следующий шаг' или Enter для продолжения.";
            btnNextStep.Focus();
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            if (currentPath == null)
            {
                if (!BFS(residualGraph, source, sink, parent))
                {
                    lblStatus.Text = $"Максимальный поток: {maxFlow}";
                    DrawGraph(graph, flowMatrix);
                    btnNextStep.Enabled = false;
                    btnResultNow.Enabled = false;
                    return;
                }

                currentPath = new List<int>();
                int pathFlow = int.MaxValue;
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    currentPath.Insert(0, v);
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }
                currentPath.Insert(0, source);
                pathFlows.Enqueue(pathFlow);
            }

            int flow = pathFlows.Dequeue();
            for (int i = 0; i < currentPath.Count - 1; i++)
            {
                int u = currentPath[i], v = currentPath[i + 1];
                residualGraph[u, v] -= flow;
                residualGraph[v, u] += flow;

                if (graph[u, v] > 0)
                    flowMatrix[u, v] += flow;
                else
                    flowMatrix[v, u] -= flow;
            }

            maxFlow += flow;
            lblStatus.Text = $"Шаг {step++}. Путь: {string.Join("→", currentPath.Select(n => n + 1))}, Поток: {flow}, Общий: {maxFlow}";
            currentPath = null;
            DrawGraph(graph, flowMatrix);
        }

        private void btnResultNow_Click(object sender, EventArgs e)
        {
            if (graph == null)
            {
                MessageBox.Show("Сначала загрузите граф и укажите исток/сток.");
                return;
            }

            // Инициализация, если еще не была выполнена
            if (residualGraph == null)
            {
                residualGraph = (int[,])graph.Clone();
                flowMatrix = new int[graph.GetLength(0), graph.GetLength(0)];
                parent = new int[graph.GetLength(0)];
                maxFlow = 0;
            }

            // Выполнение алгоритма полностью
            ExecuteMaxFlowAlgorithm();

            lblStatus.Text = $"Максимальный поток: {maxFlow}";
            DrawGraph(graph, flowMatrix);
            btnNextStep.Enabled = false;
            btnResultNow.Enabled = false;
        }
        private void ExecuteMaxFlowAlgorithm()
        {
            while (BFS(residualGraph, source, sink, parent))
            {
                int pathFlow = int.MaxValue;

                // Находим минимальную пропускную способность на найденном пути
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    pathFlow = Math.Min(pathFlow, residualGraph[u, v]);
                }

                // Обновляем остаточный граф и матрицу потока
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    residualGraph[u, v] -= pathFlow;
                    residualGraph[v, u] += pathFlow;

                    if (graph[u, v] > 0)
                        flowMatrix[u, v] += pathFlow;
                    else
                        flowMatrix[v, u] -= pathFlow;
                }

                maxFlow += pathFlow;
            }
        }
        private bool BFS(int[,] residual, int s, int t, int[] parent)
        {
            bool[] visited = new bool[residual.GetLength(0)];
            Queue<int> q = new Queue<int>();
            q.Enqueue(s);
            visited[s] = true;
            parent[s] = -1;

            while (q.Count > 0)
            {
                int u = q.Dequeue();
                for (int v = 0; v < residual.GetLength(0); v++)
                {
                    if (!visited[v] && residual[u, v] > 0)
                    {
                        q.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return visited[t];
        }

        private void DrawGraph(int[,] baseGraph, int[,] flows)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                int n = baseGraph.GetLength(0);
                PointF[] points = new PointF[n];
                float angleStep = 360f / n;
                float radius = Math.Min(pictureBox1.Width, pictureBox1.Height) / 2 - 50;
                PointF center = new PointF(pictureBox1.Width / 2f, pictureBox1.Height / 2f);

                // Настройки для центрирования текста
                using (StringFormat format = new StringFormat())
                using (Font nodeFont = new Font("Segoe UI", 10, FontStyle.Bold))
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    for (int i = 0; i < n; i++)
                    {
                        float angle = i * angleStep * (float)Math.PI / 180f;
                        float x = center.X + radius * (float)Math.Cos(angle);
                        float y = center.Y + radius * (float)Math.Sin(angle);
                        points[i] = new PointF(x, y);

                        // Рисуем узел
                        g.FillEllipse(Brushes.LightBlue, x - 15, y - 15, 30, 30);
                        g.DrawEllipse(Pens.SteelBlue, x - 15, y - 15, 30, 30);

                        // Рисуем текст с центрированием
                        RectangleF textRect = new RectangleF(x - 15, y - 15, 30, 30);
                        g.DrawString(
                            (i + 1).ToString(),
                            nodeFont,
                            Brushes.Black,
                            textRect,
                            format
                        );
                    }
                }

                // Отрисовка рёбер
                using (Pen arrowPen = new Pen(Color.DarkSlateGray, 2))
                using (Font edgeFont = new Font("Segoe UI", 9))
                {
                    arrowPen.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 6);

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (baseGraph[i, j] > 0)
                            {
                                PointF p1 = points[i], p2 = points[j];
                                PointF dir = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                                float len = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                                dir.X /= len;
                                dir.Y /= len;

                                PointF start = new PointF(p1.X + dir.X * 15, p1.Y + dir.Y * 15);
                                PointF end = new PointF(p2.X - dir.X * 15, p2.Y - dir.Y * 15);

                                arrowPen.Color = Color.DarkSlateGray;
                                if (currentPath != null)
                                {
                                    for (int k = 0; k < currentPath.Count - 1; k++)
                                    {
                                        if (currentPath[k] == i && currentPath[k + 1] == j)
                                        {
                                            arrowPen.Color = Color.OrangeRed;
                                            break;
                                        }
                                    }
                                }

                                // Рисуем ребро
                                g.DrawLine(arrowPen, start, end);

                                // Рисуем метку ребра
                                string label = flows != null
                                    ? $"{flows[i, j]}/{baseGraph[i, j]}"
                                    : baseGraph[i, j].ToString();

                                PointF mid = new PointF(
                                    (start.X + end.X) / 2 + 4,
                                    (start.Y + end.Y) / 2 + 4
                                );
                                g.DrawString(label, edgeFont, Brushes.DarkRed, mid);
                            }
                        }
                    }
                }
            }

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
        }

        private int[,] ReadGraphFromFile(string path)
        {
            var lines = File.ReadAllLines(path);
            int n = lines.Length;
            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                var values = lines[i].Split(' ');
                if (values.Length != n)
                    throw new InvalidDataException("Некорректный формат файла.");

                for (int j = 0; j < n; j++)
                {
                    if (!int.TryParse(values[j], out int val))
                        throw new InvalidDataException("Файл содержит нечисловое значение.");
                    if (i == j && val != 0)
                        throw new InvalidDataException("Диагональные элементы должны быть 0.");
                    matrix[i, j] = val;
                }
            }

            return matrix;
        }

    }
}