using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace ESV2ClassLib
{
    public class JsonFileConstructor
    {

        FinalJsonRoot rootObj;

        List<TeamDataStorage> teamData = new List<TeamDataStorage>();

        MetaData _meta;       

        //Could add in new constructor here to alter meta/teamdata field names
        public JsonFileConstructor()
        {
          

            _meta = new MetaData
            {
                storageType = "total_store_size",
                period = "all_time",
                unit = "bytes",
                name = "Elastic_Search",
                dateEvaluated = DateTime.Now
            };

            rootObj = new FinalJsonRoot()
            {
                meta = _meta
            };
        }

        public FinalJsonRoot GetFinalJsonStructure(Dictionary<string, TeamData> teamsStorage)
        {
            ConvertDictToJsonObject(teamsStorage);

            return rootObj;

        }


        

        //Sort dictionary and convert into array of TeamData for json root object's results field
        private void ConvertDictToJsonObject(Dictionary<string, TeamData> teamsStorage)
        {

            foreach (KeyValuePair<string, TeamData> teamAndStorage in teamsStorage)
            {
                var singleTeamData = new TeamDataStorage
                {
                    team = DetermineRootTeamName(teamAndStorage.Key),
                    value = teamAndStorage.Value.storeSize,
                    sub_team = teamAndStorage.Key
                };
                teamData.Add(singleTeamData);
            }

            //Add team data array/list to json root obj for serialization
            rootObj.results = teamData.ToArray();
            
        }

        //Determine the root team name if there is one, otherwise team and sub_team values will be the same
        private string DetermineRootTeamName(string fullBucketName)
        {
            string rootTeam = fullBucketName;
            if (fullBucketName.Contains("-"))
            {
                var index = fullBucketName.IndexOf("-");
                rootTeam = fullBucketName.Substring(0, index);
            }

            return rootTeam;
        }

    }
}
