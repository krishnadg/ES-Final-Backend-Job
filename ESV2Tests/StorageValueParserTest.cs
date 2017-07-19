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

    public class StorageValueParserTest 
    {


        [Fact]
        public void ConvertStorageToBytes_ValueInkb_ReturnValueInBytes()
        {
            //ARRANGE
            StorageValueParser sut = new StorageValueParser();
            
            string storage = "1000kb";

            long expected = 1000000;
   
            //ACT
            var result = sut.ConvertStorageToBytes(storage);

            //ASSERT
            Assert.Equal(expected, result);

        }

        [Fact]
        public void ConvertStorageToBytes_ValueInb_ReturnValueInBytes()
        {
            //ARRANGE
            StorageValueParser sut = new StorageValueParser();
            
            string storage = "1000b";

            long expected = 1000;
   
            //ACT
            var result = sut.ConvertStorageToBytes(storage);

            //ASSERT
            Assert.Equal(expected, result);

        }

        [Fact]
        public void ConvertStorageToBytes_ValueInmb_ReturnValueInBytes()
        {
            //ARRANGE
            StorageValueParser sut = new StorageValueParser();
            
            string storage = "2345.2mb";

            long expected = 2345200000;
   
            //ACT
            var result = sut.ConvertStorageToBytes(storage);

            //ASSERT
            Assert.Equal(expected, result);

        }

        [Fact]
        public void ConvertStorageToBytes_ValueIngb_ReturnValueInBytes()
        {
            //ARRANGE
            StorageValueParser sut = new StorageValueParser();
            
            string storage = "1.2gb";

            long expected = 1200000000;
   
            //ACT
            var result = sut.ConvertStorageToBytes(storage);

            //ASSERT
            Assert.Equal(expected, result);

        }

    }
}
         