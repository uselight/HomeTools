using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegExTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        protected static string sourceTagCommand =
            "(?:[<]){1}" +  //
            "(?<Command>[^!][/{0,1}\\w]*)" +  //
            "(?:[\\s]{0,1})" +
            "(?<Text>[\\w\\d= \":,.!`^>()\\[\\]-]*?)" +
            "(?:[>]){1}";

        private void Form1_Load(object sender, EventArgs e) {
            SetDefaultRegEx();
            SetDefaultTestString();
        }

        private void SetDefaultRegEx() {
            txtExpression.Text = sourceTagCommand;
        }

        private void SetDefaultTestString() {
            memSample.Text = "<para attr=\"Cool - hooray!\" attr2=\"test\">This document is intended to test how commented tags are generated in our documentation.</para>\r\n\r\n"+
                "<para>Various tags: <b>bold</b>, <i>italic</i>, <u>underline</u>.</para>\r\n\r\n"+
                "<!--group>\r\nGroup Header\r\n<groupbody>\r\n<para>group body</para>\r\n</group-->\r\n\r\n"+
                "<!--%CodeStart%>\r\n<%CodeLanguage$C#%>\r\ntest\r\n<%CodeLanguage$VB%>\r\ntest\r\n<%CodeEnd%>\r\n\r\n"+
                "<%ObjectCodeCentralLink$E221#Some link%>\r\n<%ObjectCodeCentral$E221#Some link%>\r\n\r\n<note>Note</note-->\r\n\r\n\r\n"+
                "<!--para>This is a link to a custom document: <%UrlDocument$16725#Help Tests.x. Home%>.</para-->\r\n\r\n"+
                "<para>This is a link to a class: <see cref=\"T: HelpTests.BaseClass\"/>.</para>";
        }

        private void btnFindMatches_Click(object sender, EventArgs e) {
            memMatches.Text = String.Empty;
            string expr = txtExpression.Text;
            string str = memSample.Text;

            MatchCollection mc = Regex.Matches(str, expr);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < mc.Count; i++) {
                string s = String.Format("Match {0}: ", i);
                if(i < 10) s += "\t";
                foreach(Group gr in mc[i].Groups) {
                    s += String.Format("\t{0}\t", gr.Value);
                    if(gr.Value.Length < 9) s += "\t||"; else s += "||";
                }
                sb.AppendLine(s);
            }


            //memMatches.Lines = mc.Cast<Match>().Select(match => match.Value).ToArray(); ;
            memMatches.Text = sb.ToString();
        }

        private void btnDefault_Click(object sender, EventArgs e) {
            SetDefaultRegEx();
        }
    }
}
