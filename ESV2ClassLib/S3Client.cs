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

            var currentDateTime = DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year;
            var jsonFileKey = clusterName + "-leaderboard-data/es-leaderboard/" + currentDateTime + ".json";

                        

            PutObjectRequest putJsonRequest = new PutObjectRequest
            {
                BucketName = "datalens-leaderboard",
                Key = jsonFileKey,
                ContentBody = jsonString,
                
            };
            
            PutObjectResponse putJsonResponse = client.PutObjectAsync(putJsonRequest).GetAwaiter().GetResult();
            
        }


    }


}



 