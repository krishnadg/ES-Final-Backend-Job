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

    public class TeamNameParserTest 
    {

        [Fact]
        public void GetTeamName_ComplexIndexString_ReturnTeam()
        {
            //ARRANGE
            TeamNameParser sut = new TeamNameParser();
            
            string args = "pat_scpublish-event-processing_access_index-2017-07-08_1";
            string expected = "pat";
   
            //ACT
            var result = sut.GetTeamName(args);

            //ASSERT
            Assert.Equal(expected, result);

        }
    }
}
         