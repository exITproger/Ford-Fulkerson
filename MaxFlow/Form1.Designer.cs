namespace MaxFlow
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnLoadGraph = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtSink = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblSink = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnNextStep = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnResultNow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadGraph
            // 
            this.btnLoadGraph.Location = new System.Drawing.Point(12, 12);
            this.btnLoadGraph.Name = "btnLoadGraph";
            this.btnLoadGraph.Size = new System.Drawing.Size(120, 30);
            this.btnLoadGraph.TabIndex = 0;
            this.btnLoadGraph.Text = "Загрузить граф";
            this.btnLoadGraph.UseVisualStyleBackColor = true;
            this.btnLoadGraph.Click += new System.EventHandler(this.btnLoadGraph_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(200, 18);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(50, 20);
            this.txtSource.TabIndex = 1;
            // 
            // txtSink
            // 
            this.txtSink.Location = new System.Drawing.Point(310, 18);
            this.txtSink.Name = "txtSink";
            this.txtSink.Size = new System.Drawing.Size(50, 20);
            this.txtSink.TabIndex = 2;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(140, 21);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(41, 13);
            this.lblSource.TabIndex = 3;
            this.lblSource.Text = "Исток:";
            // 
            // lblSink
            // 
            this.lblSink.AutoSize = true;
            this.lblSink.Location = new System.Drawing.Point(260, 21);
            this.lblSink.Name = "lblSink";
            this.lblSink.Size = new System.Drawing.Size(34, 13);
            this.lblSink.TabIndex = 4;
            this.lblSink.Text = "Сток:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(380, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(480, 12);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(140, 30);
            this.btnNextStep.TabIndex = 6;
            this.btnNextStep.Text = "Следующий шаг";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(12, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 19);
            this.lblStatus.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnResultNow
            // 
            this.btnResultNow.Location = new System.Drawing.Point(626, 12);
            this.btnResultNow.Name = "btnResultNow";
            this.btnResultNow.Size = new System.Drawing.Size(99, 30);
            this.btnResultNow.TabIndex = 8;
            this.btnResultNow.Text = "Результат";
            this.btnResultNow.UseVisualStyleBackColor = true;
            this.btnResultNow.Click += new System.EventHandler(this.btnResultNow_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(830, 700);
            this.Controls.Add(this.btnResultNow);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblSink);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.txtSink);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnLoadGraph);
            this.Name = "Form1";
            this.Text = "Ford Fulkerson";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadGraph;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtSink;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblSink;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnResultNow;
    }
}
