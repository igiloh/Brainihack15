using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NeuroSky.ThinkGear;

namespace MindWaveApp
{
    class ThinkGearUtils
    {
        // Event

        // Data Members
        private Connector m_Conector = new Connector();       
       
        // C'tor
        public ThinkGearUtils()
        {
            
        }

        // Properties
        public EventHandler DeviceConnected { get; set; }

        // Methodes
    }
}
