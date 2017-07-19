using System;
using System.Security.Cryptography.X509Certificates;

using System.Security.Cryptography;
using System.Security.Principal;
using System.Net.Http;
using System.Security.Authentication;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace ESV2ClassLib
{
    public class ElasticSearchClient
    {
        string uri;
        
        static string CAT_QUERY = "/_cat/indices?format=json&pretty";
        X509Certificate2 pfxCert;

        HttpClientHandler clientHandler = new HttpClientHandler();
        HttpClient client;

        public ElasticSearchClient(string _uri, string _pfxCertFileName)
        {
            uri = _uri;
            
            pfxCert = new X509Certificate2(_pfxCertFileName);

           

            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            clientHandler.SslProtocols = SslProtocols.Tls12;
            clientHandler.ServerCertificateCustomValidationCallback = delegate {return true;}; //Needs to accomodate the CACERT AT SOME POINT!!! - shouldn't always return true
            clientHandler.ClientCertificates.Add(pfxCert);

            client = new HttpClient(clientHandler);
        }


        public IEnumerable<IndexData> GetIndexDataList()
        {
            string readableJson = GetReadableJson();

           //var list = new Enumerable<TeamData>();

            var list = JsonConvert.DeserializeObject<IEnumerable<IndexData>>(readableJson);

            return list;
        }


        private string GetReadableJson()
        {

            var resp = CreateHttpRequestAndGetResponseMessage();

            string unformattedJson = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            string readableJson = GetReformattedJson(unformattedJson);

            return readableJson;
            
        }




        private HttpResponseMessage CreateHttpRequestAndGetResponseMessage()
        {

            var message = new HttpRequestMessage();
            message.Method = System.Net.Http.HttpMethod.Get;
            message.RequestUri = new Uri(uri + CAT_QUERY);


            var resp = client.SendAsync(message).GetAwaiter().GetResult();
            resp.EnsureSuccessStatusCode();

            return resp;
        }

        //Necessary reformatting because JSON was not deserializing properly because fields had '.' characters in their names (ex. docs.count would not serialize into docsCount)
        private string GetReformattedJson(string unformattedJson)
        {
            string fieldToReplace1 = "docs.count";
            string toReplaceWith1 = "docsCount";
            string fieldToReplace2 = "docs.deleted";
            string toReplaceWith2 = "docsDeleted";
            
            //Replace this first so that store.size 's replacement isnt replacing inside of pri.store.size
            string fieldToReplace3  = "pri.store.size";
            string toReplaceWith3 = "primaryStoreSize";


            string fieldToReplace4 = "store.size";
            string toReplaceWith4 = "storeSize";

            //Replace instances of static fields with deserializable field names
            var reformattedJson = "";
            reformattedJson = unformattedJson.Replace(fieldToReplace1, toReplaceWith1);
            reformattedJson = reformattedJson.Replace(fieldToReplace2, toReplaceWith2);
            reformattedJson = reformattedJson.Replace(fieldToReplace3, toReplaceWith3);
            reformattedJson = reformattedJson.Replace(fieldToReplace4, toReplaceWith4);


            return reformattedJson;
            

        }
    }
}