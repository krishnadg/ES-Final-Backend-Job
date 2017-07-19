using System;
using System.Linq;

namespace ESV2ClassLib
{

    //Data for entire team's entire presence in Elastic search (all team associated indices)
    public class TeamData
    {

        public string teamName {get; set;}

        public long numberOfDocs {get; set;}

        public long storeSize {get; set;}

        public long primaryStoreSize {get; set;}


        
    }
}
