using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegExTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnFindMatches_Click(object sender, EventArgs e) {
            memMatches.Text = String.Empty;
            string expr = txtExpression.Text;
            string str = memSample.Text;

            MatchCollection mc = Regex.Matches(str, expr);
            memMatches.Lines = mc.Cast<Match>().Select(match => match.Value).ToArray(); ;
        }

    }
}
