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
        private AdamSocket m_adamModbus;
        private AdamSocket m_adamUDP;
        private string m_szIP;
        private int m_iPort;
        private double cnt;
        private string switchState;
        private int m_iCntTotal;
        private Adam6000Type m_Adam6000Type;
        private byte[] m_byMode;
        private bool[] m_bRecordLastCount;

        public AdamCNT()
        {

            cnt = 0;
            m_szIP = Constants.DEF_IP;
            m_iPort = Constants.DEF_PORT;
            m_adamModbus = new AdamSocket();
            m_adamUDP = new AdamSocket();
            m_adamModbus.SetTimeout(1000, 1000, 1000);  // set timeout for TCP
            m_Adam6000Type = Adam6000Type.Adam6051; // the sample is for ADAM-6051

            InitAdam6051();
        }

        protected void InitAdam6051()
        {
            m_iCntTotal = Counter.GetChannelTotal(Adam6000Type.Adam6051);
            m_byMode = new byte[m_iCntTotal];
            m_bRecordLastCount = new bool[m_iCntTotal];
        }

        public void createCounterSocket()
        {
            if (m_adamModbus.Connect(Constants.DEF_IP, ProtocolType.Tcp, Constants.DEF_PORT))
            {
                Console.WriteLine("TCP socket connected successfuly...");
            }
            else
            {
                Console.WriteLine("Connecting TCP socket failed...");
            }
        }

        public void counterStart()
        {
            int iStart;             // base address
            int iConfigStart;       // index ulaznog kanala pa se po formuli dobije adresa

            iConfigStart = Counter.GetChannelStart(m_Adam6000Type);
            iStart = 32 + (iConfigStart + 0) * 4 + 1;       // + 0 za prvi kanal prakticno nema smisla al cisto da se vidi da se tu dodaje u zavisnosti od kanala
            if (m_adamModbus.Modbus().ForceSingleCoil(iStart, 1))
            {
                Console.WriteLine("Counter enabled...");
            }
            else
            {
                Console.WriteLine("ForceSingleCoil() failed...");
            }
        }
        public void counterRead()
        {
            int iCntStart = 25, iChTotal = 1;

            int[] iData;

            if (m_adamModbus.Modbus().ReadInputRegs(iCntStart, iChTotal * 2, out iData))
            {
                cnt = Counter.GetScaledValue(m_Adam6000Type, 1, iData[1], iData[0]);
                Console.WriteLine(cnt.ToString(Counter.GetFormat(m_Adam6000Type, m_byMode[0])) + " " + Counter.GetUnitName(m_Adam6000Type, m_byMode[0]));
            }
        }

        public void resetCounter()
        {
            int iStart;             
            int iConfigStart;      

            iConfigStart = Counter.GetChannelStart(m_Adam6000Type);
            iStart = 32 + (iConfigStart + 0) * 4 + 2;       // + 0 za prvi kanal prakticno nema smisla al cisto da se vidi da se tu dodaje u zavisnosti od kanala, +2 na kraju da gadja tu adresu za reset valjda
            if (m_adamModbus.Modbus().ForceSingleCoil(iStart, 1))
            {
                Console.WriteLine("Counter reset...");
            }
            else
            {
                Console.WriteLine("ForceSingleCoil() failed...");
            }
        }

        public void createSwitchSocket()
        {
            if (m_adamUDP.Connect(AdamType.Adam6000, Constants.DEF_IP, ProtocolType.Udp))
            {
                Console.WriteLine("UDP socket connected successfuly...");
            }
            else
            {
                Console.WriteLine("Connecting UDP socket failed...");

            }
        }

        /* Citanje sa switcha*/
        public void switchRead()
        {
            int iDiStart = 1;
            bool[] bDiData;
            string dataButton;

            if (m_adamModbus.Modbus().ReadCoilStatus(iDiStart, 12, out bDiData))
            {
                dataButton = bDiData[6].ToString();
                if (dataButton == "True")
                {
                    Console.WriteLine("Button OFF");
                    switchState = "OFF";
                }
                else
                {
                    Console.WriteLine("Button ON");
                    switchState = "ON";
                }
            }
            else
            {
                Console.WriteLine("Failed to read status...");
            }
        }

        /* Geteri za polja cnt i switchstate*/
        public double getCnt()
        {
            return cnt;
        }

        public string getSwitchState()
        {
            return switchState;
        }
    }
}
