using System;
using System.Collections.Generic;
using System.Linq;


namespace ESV2ClassLib
{

    public class StorageValueParser
    {

        public StorageValueParser()
        {

        }


        // public double DoStringAddition(string oldValue, string newValue)
        // {
        //     return Convert.ToDouble(oldValue) + Convert.ToDouble(newValue);

        // }

       //Converts storage value to bytes from kb, mb, etc. for simpler math 
        public long ConvertStorageToBytes(string storage)
        {

            if (storage == null)
            {
                return 0;
            }


            char[] startingChars = new char[] 
            {
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '0',
                '.'
            };

            string value = TruncateIntoValue(storage);
            string byteUnit = storage.TrimStart(startingChars);

            long byteValue = (long)DoByteConversion(value, byteUnit);

            return byteValue;
        }

        //Remove the unit attached to a given storage size string "mb", "b", "kb" etc. return in long format so simple addition of indexes can occur
        private string TruncateIntoValue(string sizeString)
        {

            if (sizeString == null)
                return "0";
            char[] terminatingChars = new char[] 
            {
                'b',
                'g',
                'k',
                'm',
                't'
            };
            string truncatedSizeString = sizeString.TrimEnd(terminatingChars);

            return truncatedSizeString;

        }

        //Must return double because of decimal values (ex. 12.4 gb)
        private double DoByteConversion(string value, string unit)
        {
            
            switch (unit)
            {
                case "b":
                return Convert.ToDouble(value);

                case "kb":
                return Convert.ToDouble(value) * 1000;

                case "mb":
                return Convert.ToDouble(value) * 1000000;

                case "gb":
                return Convert.ToDouble(value) * 1000000000;

                default :
                return Convert.ToDouble(value);
            }
        }
    }

}