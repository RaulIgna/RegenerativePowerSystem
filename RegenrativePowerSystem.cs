using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agilent.AgAPS.Interop;

namespace RegenerativePowerSystem
{
    public class DMMASP
    {
        public string ResourceName { get; set; }
        public string ID { get; set; }
        public bool Run {  get; set; }
        public double OVPLimit { get; set; }
        public double VoltageLimit { get; set; }

        public ThreadStaticAttribute Work { get; set; }
        public AgAPS Driver {  get; set; }

        public DMMASP (string id)
        {
            ResourceName = "USB0::0x2A8D::0x2802::MY6300" + id + "::0::INSTR";
            ID = id;
            Run = true;
        }
    }

    public class RegenrativePowerSystem
    {
        public static Error Open(DMMASP DMM)
        {
            // Try to close the driver
            try
            {
                DMM.Driver.Close();
                DMM.Driver = null;
            }
            catch{ }

            Error er = new Error();
            try
            {
                DMM.Driver = new AgAPS();
                DMM.Driver.Initialize(DMM.ResourceName, false, false);
                // TODO: Handle errors


            }
            catch(Exception e) 
            {
                DMM.Run = false;
                return new Error(e.Message);
            }
            return er;
        }
    }
}
