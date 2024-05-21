namespace falling_sand.Ui {
    partial class SettingsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            label1 = new Label();
            GameSizeWidth = new NumericUpDown();
            label2 = new Label();
            GameSizeHeight = new NumericUpDown();
            label3 = new Label();
            SpeedBar = new TrackBar();
            SpeedInput = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            Cancel = new Button();
            Confirm = new Button();
            ((System.ComponentModel.ISupportInitialize)GameSizeWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GameSizeHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpeedBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpeedInput).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 0;
            label1.Text = "Simulation Size";
            // 
            // GameSizeWidth
            // 
            GameSizeWidth.Location = new Point(218, 8);
            GameSizeWidth.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            GameSizeWidth.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            GameSizeWidth.Name = "GameSizeWidth";
            GameSizeWidth.Size = new Size(46, 23);
            GameSizeWidth.TabIndex = 1;
            GameSizeWidth.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(269, 10);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 2;
            label2.Text = "x";
            // 
            // GameSizeHeight
            // 
            GameSizeHeight.Location = new Point(287, 8);
            GameSizeHeight.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            GameSizeHeight.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            GameSizeHeight.Name = "GameSizeHeight";
            GameSizeHeight.Size = new Size(50, 23);
            GameSizeHeight.TabIndex = 3;
            GameSizeHeight.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 38);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 4;
            label3.Text = "Simulation Speed";
            // 
            // SpeedBar
            // 
            SpeedBar.LargeChange = 100;
            SpeedBar.Location = new Point(120, 38);
            SpeedBar.Maximum = 400;
            SpeedBar.Minimum = 25;
            SpeedBar.Name = "SpeedBar";
            SpeedBar.Size = new Size(167, 45);
            SpeedBar.SmallChange = 25;
            SpeedBar.TabIndex = 5;
            SpeedBar.TickFrequency = 25;
            SpeedBar.Value = 100;
            // 
            // SpeedInput
            // 
            SpeedInput.DecimalPlaces = 2;
            SpeedInput.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            SpeedInput.Location = new Point(287, 38);
            SpeedInput.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            SpeedInput.Minimum = new decimal(new int[] { 4, 0, 0, 131072 });
            SpeedInput.Name = "SpeedInput";
            SpeedInput.Size = new Size(50, 23);
            SpeedInput.TabIndex = 6;
            SpeedInput.Value = new decimal(new int[] { 100, 0, 0, 131072 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(118, 68);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 7;
            label4.Text = "0.25x";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(263, 68);
            label5.Name = "label5";
            label5.Size = new Size(28, 15);
            label5.TabIndex = 8;
            label5.Text = "4.0x";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(340, 40);
            label6.Name = "label6";
            label6.Size = new Size(13, 15);
            label6.TabIndex = 9;
            label6.Text = "x";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.Control;
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(10, 108);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 10;
            label7.Text = "v1.0";
            // 
            // Cancel
            // 
            Cancel.Location = new Point(278, 100);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(75, 23);
            Cancel.TabIndex = 11;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // Confirm
            // 
            Confirm.Location = new Point(197, 100);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(75, 23);
            Confirm.TabIndex = 12;
            Confirm.Text = "Confirm";
            Confirm.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 133);
            Controls.Add(Confirm);
            Controls.Add(Cancel);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(SpeedInput);
            Controls.Add(SpeedBar);
            Controls.Add(label3);
            Controls.Add(GameSizeHeight);
            Controls.Add(label2);
            Controls.Add(GameSizeWidth);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)GameSizeWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)GameSizeHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpeedBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpeedInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown GameSizeWidth;
        private Label label2;
        private NumericUpDown GameSizeHeight;
        private Label label3;
        private TrackBar SpeedBar;
        private NumericUpDown SpeedInput;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button Cancel;
        private Button Confirm;
    }
}