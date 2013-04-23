using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nave_B
{
    public partial class Main : Form
    {
        Juego jueguito;
        private BufferedGraphicsContext buffercontext;

        public Main()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            buffercontext = BufferedGraphicsManager.Current;
        }

        private void BJugar_Click(object sender, EventArgs e)
        {
            BJugar.Hide();
            jueguito = new Juego(buffercontext);
            Workspace.Controls.Add(jueguito);
        }

      

    }
}
