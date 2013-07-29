using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.Common;
using FISCA.DSAUtil;

namespace ClassStudent_ePaper
{
    public class FeatureBase
    {
        public static DSResponse CallService(string serviceName, DSRequest request)
        {
            return FISCA.Authentication.DSAServices.CallService(serviceName, request);            
        }
    }
}
