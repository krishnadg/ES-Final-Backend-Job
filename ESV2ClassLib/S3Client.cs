using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Newtonsoft.Json;

namespace ESV2ClassLib
{

    public static class S3Client
    {


        //Get Json and maybe put it in S3 Storage...
        public static void AddJsonFileToS3(string clusterName, Dictionary<string, TeamData> teamsStorage )
        {

            AmazonS3Client client= new AmazonS3Client();

          
            string jsonString = JsonConvert.SerializeObject(teamsStorage);

            var currentDateTime = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
            
            var jsonFileKey = clusterName + "leaderboard/elasticsearch.json";
            var jsonFileKeyWithDate = clusterName + "leaderboard/elasticsearch-history/" + currentDateTime + ".json";

                        

            PutObjectRequest putJsonRequest = new PutObjectRequest
            {
                BucketName = "datalens-hub",
                Key = jsonFileKey,
                ContentBody = jsonString,
                
            };

             PutObjectRequest putJsonRequestWithDate = new PutObjectRequest
            {
                BucketName = "datalens-hub",
                Key = jsonFileKeyWithDate,
                ContentBody = jsonString,
                
            };
            
            PutObjectResponse putJsonResponse = client.PutObjectAsync(putJsonRequest).GetAwaiter().GetResult();
            PutObjectResponse putJsonResponse2 = client.PutObjectAsync(putJsonRequestWithDate).GetAwaiter().GetResult();
            
            
        }


    }


}



 