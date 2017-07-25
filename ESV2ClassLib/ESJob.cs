using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESV2ClassLib
{

    public class ESJob
    {
        Dictionary<string, TeamData> teamData;

        ElasticSearchClient client;

        public ESJob(ElasticSearchClient _client)
        {
            teamData = new Dictionary<string, TeamData>();
            
            //Initialize entry to be used for entire ES cluster info
            var totalESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 0,
                primaryStoreSize = 0,
                storeSize = 0

            };

            teamData.Add("Elastic_Search", totalESData);

            client = _client;
        }

        public Dictionary<string, TeamData> GetData()
        {
            var indexDataList = client.GetIndexDataList().ToList();

            IndexDataParser indexParser = new IndexDataParser();

            //Parse each index record into our dictionary
            foreach (IndexData indexData in indexDataList)
            {
                teamData = indexParser.ParseIndexDataIntoDict(teamData, indexData);
            }

    
            return teamData;

        }


        public void PrintData()
        {
            string teamLeader = "none";
            long leaderStorage = 0;
            Console.WriteLine("Total Primary Storage In Elastic Search:  " + teamData["Elastic_Search"].primaryStoreSize + "\n");
            foreach (KeyValuePair<string, TeamData> pair in teamData)
            {
                Console.WriteLine("Team: " + pair.Key + " Total Primary storage size: " + pair.Value.primaryStoreSize);

                if (pair.Key != "Elastic_Search" && Convert.ToInt64(pair.Value.primaryStoreSize) > leaderStorage)
                {
                    teamLeader = pair.Key;
                    leaderStorage = Convert.ToInt64(pair.Value.primaryStoreSize);
                }
            }

            Console.WriteLine("\nLeader: " + teamLeader + " Storage: " + leaderStorage);

        }

    }
}