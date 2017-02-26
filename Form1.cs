using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agents.Model;
using Agents.Utils;
using Newtonsoft.Json;

namespace Agents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simulation Sim = new Simulation(100);
            textBox1.AppendText(Log.SimulationUserInfo(Sim));
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MaxDepth = 2;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //string JSON = JsonConvert.SerializeObject(Sim, Formatting.Indented, settings);
            //textBox1.AppendText(JSON);
        }
    }
}
