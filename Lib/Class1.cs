using Lib.PaymentManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Class1
    {
        public void InitatePayment()
        {
            BAJPaymentManagerClient c = new BAJPaymentManagerClient();
            c.Open();
            BAJEnterpriseContext_Type enterpriseContext=new BAJEnterpriseContext_Type ();
            enterpriseContext.contextInfo.ProcessContextId = "";


            enterpriseContext.requestOriginator.channelId = 1;
            enterpriseContext.requestOriginator.machineIPAddress = "";
            enterpriseContext.requestOriginator.requestedTimestamp = DateTime.Now;
            enterpriseContext.requestOriginator.requesterCode = "";
            enterpriseContext.requestOriginator.userPrincipleName = "CNC";

            InitiatePaymentDetailsRequest req=new InitiatePaymentDetailsRequest ();

            c.InitiatePaymentDetails(enterpriseContext, req);


            c.Close();
            
        }
    }
}
