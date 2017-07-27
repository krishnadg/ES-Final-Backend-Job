using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace ESV2ClassLib
{

    public class FinalJsonRoot
    {

        public TeamDataStorage[] results{get;set;}

        public MetaData meta{get;set;}


    }

    public class TeamDataStorage
    {
        public string team{get;set;}

        public string sub_team{get;set;}

        public long value{get;set;}
    }


       public class MetaData
    {

        public string storageType{get;set;}
        public string period{get;set;}

        public string unit{get;set;}

        public float costPerUnit{get;set;}

        public string name{get;set;}

        public DateTime dateEvaluated{get;set;}

    }


}