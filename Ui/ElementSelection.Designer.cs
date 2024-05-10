namespace falling_sand {
    partial class ElementSelection {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Visual = new Panel();
            Border = new Panel();
            SuspendLayout();
            // 
            // Visual
            // 
            Visual.ForeColor = Color.Black;
            Visual.Location = new Point(4, 4);
            Visual.Name = "Visual";
            Visual.Size = new Size(32, 32);
            Visual.TabIndex = 0;
            // 
            // Border
            // 
            Border.BackColor = Color.Black;
            Border.ForeColor = Color.Black;
            Border.Location = new Point(2, 2);
            Border.Name = "Border";
            Border.Size = new Size(36, 36);
            Border.TabIndex = 1;
            // 
            // ElementSelection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(Visual);
            Controls.Add(Border);
            Name = "ElementSelection";
            Size = new Size(40, 40);
            ResumeLayout(false);
        }

        #endregion

        private Panel Visual;
        private Panel Border;
    }
}
