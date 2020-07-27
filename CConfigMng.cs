using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UevTest
{
    [Serializable()]
    public class CConfigDO
    {
        private System.Drawing.Point m_oStartPos;
        private System.Drawing.Size m_oStartSize;

        public System.Drawing.Point StartPos
        {
            get { return m_oStartPos; }
            set { m_oStartPos = value; }
        }

        public System.Drawing.Size StartSize
        {
            get { return m_oStartSize; }
            set { m_oStartSize = value; }
        }
    }

    public class CConfigMng
    {

        private string m_sConfigFileName = "" ;
        // saves to current exe directory UevTest.xml
        // private string m_sConfigFileName = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";

        // saves to appdata C:\Users\adm\AppData\Roaming\UevTest\UevTest\1.0.0.0\UevTest.xml
        // private string m_sConfigFileName_appdata = Application.UserAppDataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";

        // saves to appdata C:\Users\adm\AppData\Roaming\UevTest\UevTest\1.0.0.0\UevTest.xml
        // private string m_sConfigFileName_localappdata = Application.LocalUserAppDataPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";



        private CConfigDO m_oConfig = new CConfigDO();

        public CConfigDO Config
        {
            get { return m_oConfig; }
            set { m_oConfig = value; }
        }

        // Load configuration file
        public void LoadConfig(string configfile)
        {
            m_sConfigFileName = configfile;
            if (System.IO.File.Exists(m_sConfigFileName))
            {
                System.IO.StreamReader srReader = System.IO.File.OpenText(m_sConfigFileName);
                Type tType = m_oConfig.GetType();
                System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                object oData = xsSerializer.Deserialize(srReader);
                m_oConfig = (CConfigDO)oData;
                srReader.Close();
            }
        }

        // Save configuration file
        public void SaveConfig(string configfile)
        {
            m_sConfigFileName = configfile;
            System.IO.StreamWriter swWriter = System.IO.File.CreateText(m_sConfigFileName);
            Type tType = m_oConfig.GetType();
            if (tType.IsSerializable)
            {
                System.Xml.Serialization.XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                xsSerializer.Serialize(swWriter, m_oConfig);
                swWriter.Close();
            }
        }
    }
}
