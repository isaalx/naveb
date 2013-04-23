using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace Nave_B
{
    public partial class Main : Form
    {
        Juego jueguito;
        private BufferedGraphicsContext buffercontext;
        SoundPlayer intro;
        public Main()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            buffercontext = BufferedGraphicsManager.Current;
            intro = new SoundPlayer();
            intro.Stream = Properties.Resources.introgame;
            intro.PlayLooping();
        }

        private void BJugar_Click(object sender, EventArgs e)
        {
            BJugar.Hide();
            jueguito = new Juego(buffercontext);
            Workspace.Controls.Add(jueguito);
            
        }

      

    }
}
