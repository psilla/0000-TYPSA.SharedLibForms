using System;
using System.Windows.Forms;

namespace TYPSA.SharedLib.UserForms
{
    public partial class PasswordCodeDialogTask : Form
    {
        private Label label1;
        private Button button1;
        private TextBox textBox1;

        private const string CorrectPassword = "DeleteModelFromDB"; // Contraseña correcta

        public string PasswordCode { get; private set; }

        public PasswordCodeDialogTask()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(96, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please, introduce the password for deleting models from the database:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(151, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PasswordCodeDialogTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 120);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PasswordCodeDialogTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password Code";
            this.Load += new System.EventHandler(this.PasswordCodeDialogTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == CorrectPassword)
            {
                this.PasswordCode = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("The password for deleting models is incorrect. Please contact support.",
                                "Invalid Password",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void PasswordCodeDialogTask_Load(object sender, EventArgs e)
        {
            textBox1.Focus(); // Para que el usuario pueda escribir directamente
        }
    }
}
