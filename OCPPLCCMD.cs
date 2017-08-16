using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPCDA
{
    public class OPCPLCCMD
    {
        static Dictionary<string, OPCPLCCMD> CMD = new Dictionary<string, OPCPLCCMD>();
        public OPCItem[] OPCItemPLC_r { get; set; } = new OPCItem[10];
        public OPCItem[] OPCItemPLC_c { get; set; } = new OPCItem[11];
        public OPCGroup KepGroup { get; set; }
        public string PLCNUM { get; set; }
        public OPCPLCCMD(string plcnum, OPCServer KepServer)
        {
            PLCNUM = plcnum;
            if (CMD.ContainsKey(plcnum))
            {
                OPCItemPLC_r = CMD[plcnum].OPCItemPLC_r;
                OPCItemPLC_c = CMD[plcnum].OPCItemPLC_c;
                KepGroup = CMD[plcnum].KepGroup;
            }
            else
            {
                OPCGroups KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("Group" + plcnum);
                KepGroup.UpdateRate = 250;
                KepGroup.IsActive = true;
                KepGroup.IsSubscribed = true;
                OPCItemPLC_r[0] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 1);
                OPCItemPLC_r[1] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_cphigh", 2);
                OPCItemPLC_r[2] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_cpnoid", 3);
                OPCItemPLC_r[3] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_cpnoid_back", 4);
                OPCItemPLC_r[4] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_cpzik", 5);
                OPCItemPLC_r[5] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_hang", 6);
                OPCItemPLC_r[6] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_lie", 7);
                OPCItemPLC_r[7] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_high", 8);
                OPCItemPLC_r[8] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.r_typeid", 9);
                OPCItemPLC_r[9] = KepGroup.OPCItems.AddItem("!BOOL,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.ruku_in", 10);

                OPCItemPLC_c[0] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_cphigh", 1);
                OPCItemPLC_c[1] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_orderid", 2);
                OPCItemPLC_c[2] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_orderid_back", 3);
                OPCItemPLC_c[3] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_cpzik", 4);
                OPCItemPLC_c[4] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_hang", 5);
                OPCItemPLC_c[5] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_lie", 6);
                OPCItemPLC_c[6] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_high", 7);
                OPCItemPLC_c[7] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_cpnum", 8);
                OPCItemPLC_c[8] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.c_typeid", 9);
                OPCItemPLC_c[9] = KepGroup.OPCItems.AddItem("!I2,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.ischuk_lk", 10);
                OPCItemPLC_c[10] = KepGroup.OPCItems.AddItem("!UI4,PCR_" + plcnum + ",Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 11);
                CMD.Add(plcnum, this);
            }



        }
    }
}
