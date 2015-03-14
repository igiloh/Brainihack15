using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BrainwaveScroller
{
    class CsvToEEG
    {
        // Const Members
        public const String k_theta = "theta";
        public const String k_delta = "delta";
        public const String k_c3 = "c3";
        public const String k_beta = "beta";
        public const String k_e3 = "e3";
        public const String k_e1 = "e1";
        public const String k_e2 = "e2";
        public const String k_gamma = "gamma";
        public const String k_c1 = "c1";
        public const String k_sigma = "sigma";
        public const String k_c2 = "c2";
        public const String k_alpha = "alpha";
        public const String k_h1 = "h1";
        public const String k_h2 = "h2";

        private const int k_NumOfInstanceInTheCSV = 28;
        private const int k_NumOfParhameters = 14;

        // Data Members
        private String[] m_Data;
        private Dictionary<String, double> m_CleanData = new Dictionary<string, double>();
        private Dictionary<String, int> m_Indexs = new Dictionary<string, int>();

        // Methodes
        public CsvToEEG()
        {
            m_Indexs.Add(k_theta, 14);
            m_Indexs.Add(k_delta, 15);
            m_Indexs.Add(k_c3, 16);
            m_Indexs.Add(k_beta, 17);
            m_Indexs.Add(k_e3, 18);
            m_Indexs.Add(k_e1, 19);
            m_Indexs.Add(k_e2, 20);
            m_Indexs.Add(k_gamma, 21);
            m_Indexs.Add(k_c1, 22);
            m_Indexs.Add(k_sigma, 23);
            m_Indexs.Add(k_c2, 24);
            m_Indexs.Add(k_alpha, 25);
            m_Indexs.Add(k_h1, 26);
            m_Indexs.Add(k_h2, 27);
        }

        public void readData(String i_filePath)
        {
            m_Data = File.ReadAllText(@"C:\temp\test.csv").Split(',');

            parseData();
        }

        private void parseData()
        {
            for (int i = 0; i < k_NumOfParhameters; i++)
            {
                double val;

                if (double.TryParse(m_Data[i + k_NumOfParhameters], out val))
                {
                    m_CleanData.Add(m_Data[i], val);
                }
            }
        }
    }
}
