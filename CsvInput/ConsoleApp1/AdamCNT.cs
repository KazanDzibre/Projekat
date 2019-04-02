using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Advantech.Adam;




namespace ServerApp
{
    public class AdamCNT
    {
        private bool m_bStart;
        private AdamSocket m_adamModbus;
        private string m_szIP;
        private int m_iPort;
        private Adam6000Type m_Adam6000Type;


        public AdamCNT()
        {
            //InitializeComponent();

            m_bStart = false;
            m_szIP = Constants.DEF_IP;
            m_iPort = Constants.DEF_PORT;
            m_adamModbus = new AdamSocket();
            m_adamModbus.SetTimeout(1000, 1000, 1000);  // set timeout for TCP
            m_Adam6000Type = Adam6000Type.Adam6051; // the sample is for ADAM-6051


        }
    }
}
