namespace falling_sand {
    partial class FallingSand {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FallingSand));
            Canvas = new Panel();
            CanvasInput = new Panel();
            FPSCounter = new Label();
            BottomBar = new Panel();
            BottomCenterBar = new Panel();
            SettingsButton = new Button();
            PlayButton = new Button();
            BrushSizeButton = new Button();
            ClearButton = new Button();
            ToolButton = new Button();
            SelectedName = new Label();
            SelectedDisplayPanel = new Panel();
            Elements = new Panel();
            Canvas.SuspendLayout();
            BottomBar.SuspendLayout();
            BottomCenterBar.SuspendLayout();
            SuspendLayout();
            // 
            // Canvas
            // 
            Canvas.BackColor = Color.Black;
            Canvas.Controls.Add(CanvasInput);
            Canvas.Location = new Point(0, 0);
            Canvas.Name = "Canvas";
            Canvas.Size = new Size(450, 450);
            Canvas.TabIndex = 0;
            // 
            // CanvasInput
            // 
            CanvasInput.BackColor = Color.Transparent;
            CanvasInput.Location = new Point(0, 0);
            CanvasInput.Name = "CanvasInput";
            CanvasInput.Size = new Size(450, 450);
            CanvasInput.TabIndex = 1;
            // 
            // FPSCounter
            // 
            FPSCounter.BackColor = Color.Transparent;
            FPSCounter.Font = new Font("Consolas", 10F);
            FPSCounter.ForeColor = Color.Black;
            FPSCounter.Location = new Point(360, 98);
            FPSCounter.Name = "FPSCounter";
            FPSCounter.Size = new Size(80, 17);
            FPSCounter.TabIndex = 0;
            FPSCounter.Text = "TPS: 30";
            FPSCounter.TextAlign = ContentAlignment.TopRight;
            // 
            // BottomBar
            // 
            BottomBar.BackColor = SystemColors.ControlDarkDark;
            BottomBar.Controls.Add(BottomCenterBar);
            BottomBar.Location = new Point(0, 450);
            BottomBar.Name = "BottomBar";
            BottomBar.Size = new Size(450, 125);
            BottomBar.TabIndex = 2;
            // 
            // BottomCenterBar
            // 
            BottomCenterBar.BackColor = SystemColors.Control;
            BottomCenterBar.Controls.Add(SettingsButton);
            BottomCenterBar.Controls.Add(PlayButton);
            BottomCenterBar.Controls.Add(BrushSizeButton);
            BottomCenterBar.Controls.Add(ClearButton);
            BottomCenterBar.Controls.Add(ToolButton);
            BottomCenterBar.Controls.Add(SelectedName);
            BottomCenterBar.Controls.Add(SelectedDisplayPanel);
            BottomCenterBar.Controls.Add(FPSCounter);
            BottomCenterBar.Controls.Add(Elements);
            BottomCenterBar.Location = new Point(0, 0);
            BottomCenterBar.Name = "BottomCenterBar";
            BottomCenterBar.Size = new Size(450, 125);
            BottomCenterBar.TabIndex = 1;
            // 
            // SettingsButton
            // 
            SettingsButton.Image = Properties.Resources.SettingsIcon;
            SettingsButton.Location = new Point(417, 38);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(23, 23);
            SettingsButton.TabIndex = 8;
            SettingsButton.UseVisualStyleBackColor = true;
            // 
            // PlayButton
            // 
            PlayButton.Image = Properties.Resources.PauseIcon;
            PlayButton.ImageAlign = ContentAlignment.MiddleLeft;
            PlayButton.Location = new Point(338, 66);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(64, 23);
            PlayButton.TabIndex = 7;
            PlayButton.Text = "Pause";
            PlayButton.TextAlign = ContentAlignment.MiddleRight;
            PlayButton.UseVisualStyleBackColor = true;
            // 
            // BrushSizeButton
            // 
            BrushSizeButton.Image = Properties.Resources.ResizeIcon;
            BrushSizeButton.ImageAlign = ContentAlignment.MiddleLeft;
            BrushSizeButton.Location = new Point(300, 38);
            BrushSizeButton.Name = "BrushSizeButton";
            BrushSizeButton.Size = new Size(112, 23);
            BrushSizeButton.TabIndex = 6;
            BrushSizeButton.Text = "Eraser Size: 9x9";
            BrushSizeButton.TextAlign = ContentAlignment.MiddleRight;
            BrushSizeButton.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.ImageAlign = ContentAlignment.MiddleLeft;
            ClearButton.Location = new Point(397, 10);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(43, 23);
            ClearButton.TabIndex = 5;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            // 
            // ToolButton
            // 
            ToolButton.Font = new Font("Segoe UI", 9F);
            ToolButton.Image = Properties.Resources.BrushIcon;
            ToolButton.ImageAlign = ContentAlignment.MiddleLeft;
            ToolButton.Location = new Point(300, 10);
            ToolButton.Name = "ToolButton";
            ToolButton.Size = new Size(92, 23);
            ToolButton.TabIndex = 4;
            ToolButton.Text = "Tool: Brush";
            ToolButton.TextAlign = ContentAlignment.MiddleRight;
            ToolButton.UseVisualStyleBackColor = true;
            // 
            // SelectedName
            // 
            SelectedName.AutoSize = true;
            SelectedName.Font = new Font("Segoe UI Semibold", 10F);
            SelectedName.Location = new Point(33, 97);
            SelectedName.Name = "SelectedName";
            SelectedName.Size = new Size(32, 19);
            SelectedName.TabIndex = 3;
            SelectedName.Text = "Fire";
            // 
            // SelectedDisplayPanel
            // 
            SelectedDisplayPanel.BorderStyle = BorderStyle.FixedSingle;
            SelectedDisplayPanel.Location = new Point(10, 96);
            SelectedDisplayPanel.Name = "SelectedDisplayPanel";
            SelectedDisplayPanel.Size = new Size(22, 22);
            SelectedDisplayPanel.TabIndex = 2;
            // 
            // Elements
            // 
            Elements.AutoScroll = true;
            Elements.BackColor = Color.Silver;
            Elements.Location = new Point(10, 10);
            Elements.Name = "Elements";
            Elements.Size = new Size(280, 80);
            Elements.TabIndex = 1;
            // 
            // FallingSand
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(450, 575);
            Controls.Add(BottomBar);
            Controls.Add(Canvas);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(466, 164);
            Name = "FallingSand";
            Text = "Falling sand";
            Canvas.ResumeLayout(false);
            BottomBar.ResumeLayout(false);
            BottomCenterBar.ResumeLayout(false);
            BottomCenterBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public Panel Canvas;
        public Panel BottomCenterBar;
        private Panel Elements;
        public Panel BottomBar;
        public Label FPSCounter;
        public Panel CanvasInput;
        public Label SelectedName;
        public Panel SelectedDisplayPanel;
        public Button BrushSizeButton;
        public Button ToolButton;
        public Button ClearButton;
        public Button PlayButton;
        public Button SettingsButton;
    }
}
