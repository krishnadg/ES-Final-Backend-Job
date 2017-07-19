using System;
using System.Linq;

namespace ESV2ClassLib
{
    public class IndexData
    {
        public string health {get; set;}

        public string status {get; set;}

        public string index {get; set;}

        public string uuid {get; set;}

        public long pri {get; set;}

        public long rep {get; set;}
        
        public long docsCount {get; set;}

        public long docsDeleted {get; set;}

        public string storeSize{get;set;}

        public string primaryStoreSize {get; set;}

       
         public override string ToString()
         {
             return health + "| " + status + "| " + index + "| " + uuid + "| " + pri + "| " + rep + "| " + docsCount + "| " + docsDeleted + "| " + primaryStoreSize + "| " + storeSize;
         }
    }
     
}
