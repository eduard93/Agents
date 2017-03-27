using System;
using System.Windows.Forms;
using Agents.Model;
using Agents.Utils;
using Newtonsoft.Json;

namespace Agents
{
    public partial class Form1 : Form
    {
        Simulation Simulation;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Simulation = new Simulation((int)numericUpDown1.Value);
            watch.Stop();

            textBox1.AppendText(Log.SimulationUserInfo(Simulation));
            textBox1.AppendText("Time: " + watch.ElapsedMilliseconds+ " ms");
            textBox1.AppendText(Environment.NewLine);

            /*JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MaxDepth = 2;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //string JSON = JsonConvert.SerializeObject(Simulation, Formatting.Indented, settings);
            //textBox1.AppendText(JSON);*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Display.DisplaySimulation(Simulation);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Simulation.ProcessSimulation((int)numericUpDown2.Value);
            textBox1.AppendText(Log.SimulationMessageInfo(Simulation));
        }
    }
}
