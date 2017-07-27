using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.S3.Model;
using Amazon.S3;
 namespace ESV2ClassLib {
    static class Program {
        
        //@Author Krishna Ganesan
        //Runs entire Elastic Search backend job, ending with the output data structure

        // Args in format []
        static void Main(string[] args) {


        Console.WriteLine("Collected Args: " + args.Length);
        var node = args[0]; 
        var pfxCert = args[1];//"merged-cert-and-key.pfx";
        


        ElasticSearchClient client = new ElasticSearchClient(node, pfxCert );
        ESJob jobDoer = new ESJob(client);

        Dictionary<string, TeamData> teamsData = jobDoer.GetData();

        jobDoer.PrintData();
        
        S3Client.AddJsonFileToS3("Prod V2", teamsData);
        
        }
    }
 }