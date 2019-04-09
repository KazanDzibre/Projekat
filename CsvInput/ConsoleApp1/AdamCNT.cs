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
        private double cnt;
        private double cnt_last;
        private string switchState;
        private AdamSocket m_adamModbus;
        private AdamSocket m_adamUDP;
        private string m_szIP;
        private int m_iPort;
        private int m_iCntTotal;
        private int cnt_enable;
        private Adam6000Type m_Adam6000Type;

        private static List<CntOutput> cntListOut = new List<CntOutput>();

        public AdamCNT()
        {

            cnt = 0;
            cnt_last = 0;
            m_szIP = Constants.DEF_IP;
            m_iPort = Constants.DEF_PORT;
            m_adamModbus = new AdamSocket();
            m_adamUDP = new AdamSocket();
            m_adamModbus.SetTimeout(1000, 1000, 1000);  // set timeout for TCP
            m_Adam6000Type = Adam6000Type.Adam6051; // the sample is for ADAM-6051
            cnt_enable = 0;

            InitAdam6051();
        }

        protected void InitAdam6051()
        {
            m_iCntTotal = Counter.GetChannelTotal(Adam6000Type.Adam6051);
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
                Environment.Exit(0);
            }
        }

        public void counterStart()
        {
            int iStart;             // base address
            int iConfigStart;       // index ulaznog kanala pa se po formuli dobije adresa

            iConfigStart = Counter.GetChannelStart(m_Adam6000Type);
            iStart = 32 + (iConfigStart + 0) * 4 + 1;       // + 0 za prvi kanal prakticno nema smisla al cisto da se vidi da se tu dodaje u zavisnosti od kanala
            if (!m_adamModbus.Modbus().ForceSingleCoil(iStart, cnt_enable))
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
                if (cnt_last != cnt)
                {
                    FileIO.outputCnt(cntListOut,cnt);
                    cnt_last = cnt;
                }
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
                Environment.Exit(0);

            }
        }

        /* Citanje sa switcha*/
        public int switchRead()
        {

            int iDiStart = 1;
            bool[] bDiData;
            string dataButton;

            if (m_adamModbus.Modbus().ReadCoilStatus(iDiStart, 12, out bDiData))
            {
                dataButton = bDiData[6].ToString();
                if (dataButton == "True")
                {
                    switchState = "OFF";  
                    if (cnt_enable != 0)
                    {
                        cnt_enable = 0;
                        counterStart();
                    }
                    return 0;
                }
                else
                {
                    switchState = "ON";             
                    if (cnt_enable != 1)
                    {
                        cnt_enable = 1;
                        counterStart();
                    }
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("Failed to read status...");
                return 69;                                          //kod greske
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
