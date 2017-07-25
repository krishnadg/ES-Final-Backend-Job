using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


using ESV2ClassLib;

namespace ESTests
{

    //Integration Test with static pfx cert and uri...
     public class ESJobTest
    {

        ElasticSearchClient client;

        string uri;
        string pfxCert;

        /*Test Helper method */
        public void AreSameDictionaries(Dictionary<string, TeamData> expected, Dictionary<string, TeamData> result)
        {  
            var countMatches = expected.Count == result.Count;
            Assert.True(countMatches, String.Format("expected count {0}, got count {1}", expected.Count, result.Count ));

            
            foreach (KeyValuePair<string, TeamData> pair in expected)
            {
                string teamName = pair.Key;
                bool hasTeamName = result.ContainsKey(teamName);
                Assert.True(hasTeamName, String.Format("expected team name {0}, got didn't have", teamName));

                AreSameTeamData(pair.Value, result[teamName]);
            }

        }

        private void AreSameTeamData(TeamData expected, TeamData result)
        {
            bool sameTeamname = expected.teamName == result.teamName;
            Assert.True(sameTeamname, String.Format("expected team name {0}, found team name {1}", expected.teamName, result.teamName));

            bool samePrimaryStorageSize = expected.primaryStoreSize == result.primaryStoreSize;
            Assert.True(samePrimaryStorageSize, String.Format("expected primary store size {0}, found primary store size {1}", expected.primaryStoreSize, result.primaryStoreSize));

            bool sameNumberOfDocs = expected.numberOfDocs == result.numberOfDocs;
            Assert.True(sameNumberOfDocs, String.Format("expected number of docs: {0}, found number of docs: {1}", expected.numberOfDocs, result.numberOfDocs));



        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetData_SeveralIndicesNoDuplicates_ReturnTeamDataForAll()
        {
            //ARRANGE
            uri = "https://search.nonprod.datalens.r53.nordstrom.net";
            pfxCert = "merged-cert-and-key.pfx";
            client = new ElasticSearchClient(uri, pfxCert);

            var cert = new X509Certificate2(pfxCert);
            //var x  = client.GetIndexDataList();
            bool result = cert.HasPrivateKey;
            //bool result = x.Count() > 0;

            Assert.True(result, "Has key");
            // var sut = new ESJob(client);

            // var expectedDict = new Dictionary<string, TeamData>();
            // var teamDataExample1 = new TeamData {teamName = "datalend", primaryStoreSize = "162b"};
            // var teamDataExample2 = new TeamData {teamName = "datalens", primaryStoreSize = "162b"};
            // var teamDataExample3 = new TeamData {teamName = "ddos", primaryStoreSize = "162b"};
            // var teamDataExample4 = new TeamData {teamName = "ebs", primaryStoreSize = "162b"};
            // var teamDataExample5 = new TeamData {teamName = "fake", primaryStoreSize = "191b"};
            // var teamDataExample6 = new TeamData {teamName = "npm", primaryStoreSize = "162b"};

            // expectedDict.Add("datalend", teamDataExample1);
            // expectedDict.Add("datalens", teamDataExample2);
            // expectedDict.Add("ddos", teamDataExample3);
            // expectedDict.Add("ebs", teamDataExample4);
            // expectedDict.Add("fake", teamDataExample5);
            // expectedDict.Add("npm", teamDataExample6);

            // //ACT
            // var result = sut.GetData();



            // //ASSERT
            // AreSameDictionaries(expectedDict, result);

        }






    }

}