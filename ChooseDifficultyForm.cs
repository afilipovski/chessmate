using ChessMate.AlphaBeta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChessMate
{
    public partial class ChooseDifficultyForm : Form
    {
        public OpponentDifficulty ChosenDifficulty { get; set; }
        private static Dictionary<string, OpponentDifficulty> difficultybyStr = new Dictionary<string, OpponentDifficulty>();

        static ChooseDifficultyForm()
        {
            difficultybyStr["Easy"] = OpponentDifficulty.EASY;
            difficultybyStr["Medium"] = OpponentDifficulty.MEDIUM;
            difficultybyStr["Hard"] = OpponentDifficulty.HARD;
        }

        public ChooseDifficultyForm()
        {
            InitializeComponent();
            cmbbxDifficulty.Items.AddRange(difficultybyStr.Keys.ToArray());
            cmbbxDifficulty.SelectedIndex = 0;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            ChosenDifficulty = difficultybyStr[cmbbxDifficulty.SelectedItem.ToString()];
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
