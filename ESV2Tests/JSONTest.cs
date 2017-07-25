using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ESV2ClassLib;

namespace ESV2Tests
{

    public class JSONTest
    {

        [Fact]
        public void TestJson()
        {

            Dictionary <string, TeamData> teamData = new Dictionary<string, TeamData>();

            var td1 = new TeamData
            {
                 teamName = "team1",
                numberOfDocs = 123,
                primaryStoreSize = 213123,
                storeSize = 426000
            };
            
            var td2 = new TeamData
            {
                 teamName = "team2",
                numberOfDocs = 213,
                primaryStoreSize = 12343,
                storeSize = 256000
            };

            var td3 = new TeamData
            {
                 teamName = "team3",
                numberOfDocs = 321,
                primaryStoreSize = 413123,
                storeSize = 126000
            };

            teamData.Add("team1", td1);
            teamData.Add("team2", td2);
            teamData.Add("team3", td3);


            string json = JsonConvert.SerializeObject(teamData);


            Assert.True(true, json);



        }



    }

}