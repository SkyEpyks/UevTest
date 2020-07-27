using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UevTest
{
    //https://stackoverflow.com/questions/453161/how-to-save-application-settings-in-a-windows-forms-application

    public partial class Form1 : Form
    {
        public string configFile = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
        }

        private CConfigMng oConfigMng = new CConfigMng();

        private void Form1_Load(object sender, EventArgs e)
        {

            // load radio buttons
            radioButton1.Checked = Convert.ToBoolean(Properties.Settings.Default.isCheckedRadioButton1);
            radioButton2.Checked = Convert.ToBoolean(Properties.Settings.Default.isCheckedRadioButton2);
            radioButton3.Checked = Convert.ToBoolean(Properties.Settings.Default.isCheckedRadioButton3);


            // Load configuration
            setconfigFile();
            oConfigMng.LoadConfig(configFile);
            if (oConfigMng.Config.StartPos.X != 0 || oConfigMng.Config.StartPos.Y != 0)
            {
                Location = oConfigMng.Config.StartPos;
                Size = oConfigMng.Config.StartSize;
            }
        }

        private void setconfigFile()
        {
            if (radioButton1.Checked)
            {
                // saves to current exe directory UevTest.xml
                configFile = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";
            }
            else if (radioButton2.Checked)
            {
                // saves to appdata C:\Users\adm\AppData\Roaming\UevTest\UevTest\1.0.0.0\UevTest.xml
                configFile = Application.UserAppDataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";
            }
            else if (radioButton3.Checked)
            {
                // saves to local appdata C:\Users\adm\AppData\Local\UevTest\UevTest\1.0.0.0\UevTest.xml
                configFile = Application.LocalUserAppDataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Save configuration file
            setconfigFile();
            oConfigMng.Config.StartPos = Location;
            oConfigMng.Config.StartSize = Size;
            oConfigMng.SaveConfig(configFile);


            // save radio button states in Desinger settings to the application configuration file
            Properties.Settings.Default["isCheckedRadioButton1"] = radioButton1.Checked;
            Properties.Settings.Default["isCheckedRadioButton2"] = radioButton2.Checked;
            Properties.Settings.Default["isCheckedRadioButton3"] = radioButton3.Checked;
            Properties.Settings.Default.Save();

        }
    }


}
