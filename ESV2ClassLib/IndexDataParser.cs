using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESV2ClassLib
{

    public class IndexDataParser
    {
        TeamNameParser teamNameParser;
        StorageValueParser storageValueParser;

        public IndexDataParser()
        {
            teamNameParser= new TeamNameParser();
            storageValueParser = new StorageValueParser();
        }
    

        //Parses a new Index into our Dictionary by either adding a new K,V pair or updating an already existing pair. Returns our dictionary back
        public Dictionary<string, TeamData> ParseIndexDataIntoDict(Dictionary<string, TeamData> teamDataDict, IndexData index)
        {
            //Get the official team name from the index name
            string indexTeamName = teamNameParser.GetTeamName(index.index);

            //If this team is not already in our dictionary, instantiate a new TeamData and add the pair in
            if (!teamDataDict.ContainsKey(indexTeamName))
            {
                //Convert memory value to bytes (from kb, gb etc.) and obtain a string which can convert to long
                long teamPrimaryStoreSize =  storageValueParser.ConvertStorageToBytes(index.primaryStoreSize);
                long teamStoreSize = storageValueParser.ConvertStorageToBytes(index.storeSize);
                
                long totalNumberOfDocs = index.docsCount;

                TeamData teamData = new TeamData
                {
                    teamName = indexTeamName,
                    primaryStoreSize = teamPrimaryStoreSize,
                    numberOfDocs = index.docsCount,
                    storeSize = teamStoreSize,
                    
                };
                
                teamDataDict["Elastic_Search"].primaryStoreSize +=  teamPrimaryStoreSize;
                teamDataDict["Elastic_Search"].storeSize += teamStoreSize;
                teamDataDict["Elastic_Search"].numberOfDocs += index.docsCount;

                teamDataDict.Add(indexTeamName,teamData);

            }
            //Otherwise, we have to update the TeamData value for this team's additional index in ES
            else
            {
                TeamData previousTeamData = teamDataDict[indexTeamName];

                long newPrimaryStoreSize =  previousTeamData.primaryStoreSize + storageValueParser.ConvertStorageToBytes(index.primaryStoreSize); 
                long newStoreSize =  previousTeamData.storeSize + storageValueParser.ConvertStorageToBytes(index.storeSize); 
                
                long newNumberOfDocs = previousTeamData.numberOfDocs +  index.docsCount;


                TeamData updatedTeamData = new TeamData
                {
                    teamName = previousTeamData.teamName,
                    primaryStoreSize = newPrimaryStoreSize,
                    numberOfDocs = newNumberOfDocs,
                    storeSize = newStoreSize
                };
             
                teamDataDict["Elastic_Search"].primaryStoreSize += storageValueParser.ConvertStorageToBytes(index.primaryStoreSize);
                teamDataDict["Elastic_Search"].storeSize += storageValueParser.ConvertStorageToBytes(index.storeSize);
                teamDataDict["Elastic_Search"].numberOfDocs += index.docsCount;
                teamDataDict[indexTeamName] = updatedTeamData;
            }


            return teamDataDict;

        }
    }
}