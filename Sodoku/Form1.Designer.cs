namespace Sodoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdnSubMatrix = new System.Windows.Forms.RadioButton();
            this.rdnVertical = new System.Windows.Forms.RadioButton();
            this.rdnHorizontal = new System.Windows.Forms.RadioButton();
            this.btnPick = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(1138, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "AI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1158, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdnSubMatrix);
            this.groupBox1.Controls.Add(this.rdnVertical);
            this.groupBox1.Controls.Add(this.rdnHorizontal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(1030, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 165);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CHECKING";
            // 
            // rdnSubMatrix
            // 
            this.rdnSubMatrix.AutoSize = true;
            this.rdnSubMatrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdnSubMatrix.Location = new System.Drawing.Point(41, 122);
            this.rdnSubMatrix.Name = "rdnSubMatrix";
            this.rdnSubMatrix.Size = new System.Drawing.Size(162, 24);
            this.rdnSubMatrix.TabIndex = 7;
            this.rdnSubMatrix.TabStop = true;
            this.rdnSubMatrix.Text = "Check SubMatrix";
            this.rdnSubMatrix.UseVisualStyleBackColor = true;
            // 
            // rdnVertical
            // 
            this.rdnVertical.AutoSize = true;
            this.rdnVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdnVertical.Location = new System.Drawing.Point(41, 76);
            this.rdnVertical.Name = "rdnVertical";
            this.rdnVertical.Size = new System.Drawing.Size(143, 24);
            this.rdnVertical.TabIndex = 6;
            this.rdnVertical.TabStop = true;
            this.rdnVertical.Text = "Check Vertical";
            this.rdnVertical.UseVisualStyleBackColor = true;
            // 
            // rdnHorizontal
            // 
            this.rdnHorizontal.AutoSize = true;
            this.rdnHorizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rdnHorizontal.Location = new System.Drawing.Point(41, 29);
            this.rdnHorizontal.Name = "rdnHorizontal";
            this.rdnHorizontal.Size = new System.Drawing.Size(164, 24);
            this.rdnHorizontal.TabIndex = 5;
            this.rdnHorizontal.TabStop = true;
            this.rdnHorizontal.Text = "Check Horizontal";
            this.rdnHorizontal.UseVisualStyleBackColor = true;
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(1138, 324);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(114, 46);
            this.btnPick.TabIndex = 6;
            this.btnPick.Text = "Chọn";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 749);
            this.Controls.Add(this.btnPick);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdnSubMatrix;
        private System.Windows.Forms.RadioButton rdnVertical;
        private System.Windows.Forms.RadioButton rdnHorizontal;
        private System.Windows.Forms.Button btnPick;







    }
}

