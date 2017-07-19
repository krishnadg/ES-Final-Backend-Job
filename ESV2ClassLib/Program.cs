using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

 namespace ESV2ClassLib {
    static class Program {
        
        //@Author Krishna Ganesan
        //Runs entire Elastic Search backend job, ending with the output data structure

        // Args in format []
        static void Main(string[] args) {

        var node = args[0]; //args[0]
        var pfxCert = "merged-cert-and-key.pfx"; //args[1]
        

        ElasticSearchClient client = new ElasticSearchClient(node, pfxCert );
        ESJob jobDoer = new ESJob(client);

        Dictionary<string, TeamData> teamsData = jobDoer.GetData();

        jobDoer.PrintData();

        
        }
    }
 }