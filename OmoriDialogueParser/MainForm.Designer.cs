namespace OmoriDialogueParser
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            button1 = new System.Windows.Forms.Button();
            logTextBox = new System.Windows.Forms.TextBox();
            pathProject = new System.Windows.Forms.TextBox();
            pathOutput = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            browseProject = new System.Windows.Forms.Button();
            browseOutput = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 75);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Export";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // logTextBox
            // 
            logTextBox.Location = new System.Drawing.Point(12, 104);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.Size = new System.Drawing.Size(468, 233);
            logTextBox.TabIndex = 1;
            logTextBox.TextChanged += logTextBox_TextChanged;
            // 
            // pathProject
            // 
            pathProject.Location = new System.Drawing.Point(98, 12);
            pathProject.Name = "pathProject";
            pathProject.Size = new System.Drawing.Size(301, 23);
            pathProject.TabIndex = 2;
            // 
            // pathOutput
            // 
            pathOutput.Location = new System.Drawing.Point(98, 41);
            pathOutput.Name = "pathOutput";
            pathOutput.Size = new System.Drawing.Size(301, 23);
            pathOutput.TabIndex = 3;
            pathOutput.TextChanged += pathOutput_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 15);
            label1.TabIndex = 4;
            label1.Text = "Project Folder";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 44);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(81, 15);
            label2.TabIndex = 5;
            label2.Text = "Output Folder";
            // 
            // browseProject
            // 
            browseProject.Location = new System.Drawing.Point(405, 12);
            browseProject.Name = "browseProject";
            browseProject.Size = new System.Drawing.Size(75, 23);
            browseProject.TabIndex = 6;
            browseProject.Text = "Browse";
            browseProject.UseVisualStyleBackColor = true;
            browseProject.Click += browseProject_Click;
            // 
            // browseOutput
            // 
            browseOutput.Location = new System.Drawing.Point(405, 41);
            browseOutput.Name = "browseOutput";
            browseOutput.Size = new System.Drawing.Size(75, 23);
            browseOutput.TabIndex = 7;
            browseOutput.Text = "Browse";
            browseOutput.UseVisualStyleBackColor = true;
            browseOutput.Click += browseOutput_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(492, 349);
            Controls.Add(browseOutput);
            Controls.Add(browseProject);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pathOutput);
            Controls.Add(pathProject);
            Controls.Add(logTextBox);
            Controls.Add(button1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "OmoriDialogueParser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.TextBox pathProject;
        private System.Windows.Forms.TextBox pathOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseProject;
        private System.Windows.Forms.Button browseOutput;
    }
}