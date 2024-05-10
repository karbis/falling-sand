using falling_sand.Ui;

namespace falling_sand
{
    public partial class FallingSand : Form {
        public static FallingSand Form;
        public FallingSand() {
            Form = this;
            InitializeComponent();
            // init
            Resizer.Init();
            ElementSelectorBar.FillElements(Elements);
            Game.Init();
            BrushGame.Init();
            ElementSelection.UpdateAll();
            SelectedDisplayPanel.Update();
            Buttons.Init();
        }
    }
}