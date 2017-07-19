using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using ESV2ClassLib;

namespace ESV2Tests
{

    public abstract class IndexDataParserTestBase : IDisposable    
    {   
        
        protected Dictionary<string, TeamData> expectedTeamDataDict;

        protected IndexDataParser sut;
        protected IndexDataParserTestBase()
        {
            // Do "global" initialization here; Called before every test method.
            expectedTeamDataDict = new Dictionary<string, TeamData>();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.

        }
    } 


    public class IndexRecordParserTest : IndexDataParserTestBase 
    {


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

            // bool sameTotalStoreSize = expected.totalStoreSize == result.totalStoreSize;
            // Assert.True(sameTotalStoreSize, String.Format("expected total store size {0}, found total store size {1}", expected.totalStoreSize, result.totalStoreSize));

            bool sameNumberOfDocs = expected.numberOfDocs == result.numberOfDocs;
            Assert.True(sameNumberOfDocs, String.Format("expected number of docs: {0}, found number of docs: {1}", expected.numberOfDocs, result.numberOfDocs));



        }

        [Fact]
        public void ParseIndexDataIntoDict_EmptySourceDictionary_Return1TeamData()
        {
            //ARRANGE
            sut = new IndexDataParser();
            
            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();

            var resultESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 0,
                primaryStoreSize = 0,
                storeSize = 0
                
            };

            result.Add("Elastic_Search", resultESData);

            //stm_uiiviewer_flume:tomcatlog_index-2017-07-08_1    1   1    23048   0    36.8mb   18.4mb 
            IndexData arg = new IndexData
            {
                index = "stm_uiiviewer_flume:tomcatlog_index-2017-07-08_1",
                docsCount = 23048, 
                storeSize = "3688800b",
                primaryStoreSize = "1844400b",
            };
            
            
            
            var expectedTeamData = new TeamData
            {
                teamName = "stm",
                numberOfDocs = 23048,
                primaryStoreSize = 1844400,
                storeSize = 3688800
                
            };

            var expectedESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 23048,
                primaryStoreSize = 1844400,
                storeSize = 3688800
                
            };

            expectedTeamDataDict.Add("stm", expectedTeamData);
            expectedTeamDataDict.Add("Elastic_Search", expectedESData);

            //ACT
            
            result = sut.ParseIndexDataIntoDict(result, arg);

            //ASSERT
            AreSameDictionaries(expectedTeamDataDict, result);

        }

        [Fact]
        public void ParseIndexDataIntoDict_NonEmptySourceDictionary_Return2TeamData()
        {
            //ARRANGE
            sut = new IndexDataParser();
            
            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();
            var preexisitngTeamData = new TeamData
            {
                teamName = "team1",
                numberOfDocs = 123,
                primaryStoreSize = 213123,
                storeSize = 426000
            };

            var resultESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 123,
                primaryStoreSize = 213123,
                storeSize = 426000
                
            };
            
            result.Add("team1", preexisitngTeamData);
            result.Add("Elastic_Search", resultESData);

            //ngenp2ac_masapps_applogs_index-2017-06-17_1    1   1     336     0   1mb     550.6kb 
            IndexData arg = new IndexData
            {
                index = "ngenp2ac_masapps_applogs_index-2017-06-17_1 ",
                docsCount = 336, 
                primaryStoreSize = "1844400b",
                storeSize = "500000b",
                
            };

            
            var expectedTeamData = new TeamData
            {
                teamName = "ngenp2ac",
                numberOfDocs = 336,
                primaryStoreSize = 1844400,
                storeSize = 500000
                
            };

            var expectedESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 123 + 336,
                primaryStoreSize = 2057523,
                storeSize = 426000 + 500000
                
            };

            expectedTeamDataDict.Add("team1", preexisitngTeamData);
            expectedTeamDataDict.Add("ngenp2ac", expectedTeamData);
            expectedTeamDataDict.Add("Elastic_Search", expectedESData);


            //ACT

            result = sut.ParseIndexDataIntoDict(result, arg);


            //ASSERT

            AreSameDictionaries(expectedTeamDataDict, result);
        }

        [Fact]
        public void ParseIndexRecordIntoDict_DuplicateTeamInDictionary_Return1UpdatedTeamData()
        {
            //ARRANGE
            sut = new IndexDataParser();


            Dictionary<string, TeamData> result = new Dictionary<string, TeamData>();
            //wcm_publisher_tridion_index-2017-07-10_1      1   1       4999    0    5.7mb    3.8mb 
            var preexisitngTeamData = new TeamData
            {
                teamName = "wcm",
                numberOfDocs = 4999,
                primaryStoreSize = 380000,
                storeSize = 570000
            };

            var resultESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 4999,
                primaryStoreSize = 380000,
                storeSize = 570000
                
            };
            result.Add("wcm", preexisitngTeamData);
            result.Add("Elastic_Search", resultESData);

            IndexData arg = new IndexData
            {
                index = "wcm_some_other_index-2017-07-10_1",
                docsCount = 1000, 
                primaryStoreSize = "20.3gb",
                storeSize = "40000b",
            };



            var expectedUpdatedTeamData = new TeamData
            {
                teamName = "wcm",
                numberOfDocs = 5999,
                primaryStoreSize = 20300380000,
                storeSize = 610000
                
            };

             var expectedESData = new TeamData
            {
                teamName = "Elastic_Search",
                numberOfDocs = 5999,
                primaryStoreSize = 20300380000,
                storeSize = 610000
                
            };
            expectedTeamDataDict.Add("wcm", expectedUpdatedTeamData);
            expectedTeamDataDict.Add("Elastic_Search", expectedESData);


            //ACT

            result = sut.ParseIndexDataIntoDict(result, arg);


            //ASSERT
            AreSameDictionaries(expectedTeamDataDict, result);


        }
    }
}