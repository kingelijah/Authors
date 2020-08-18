using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
 using System.Net.Http;
using Newtonsoft.Json;

namespace AuthorsCore
{
    public class GetAuthors
    {
        private const string BaseUrl = "https://jsonmock.hackerrank.com/api/article_users/search?page=2";
        static readonly HttpClient client = new HttpClient();

        public class Authors
        {

            public int id { get; set; }
            public int comment_count { get; set; }
            public int created_at { get; set; }
            public int submitted { get; set; }
            public string username { get; set; }

            public string about { get; set; }
            public string updated_at { get; set; }

            public int submission_count { get; set; }

        }

        public class ComplexAuthors
        {
   

            public int total { get; set; }
            public int per_page { get; set; }
            public int total_pages { get; set; }
            public List<Authors> data { get; set; }

            public string page { get; set; }
          
        }

        public static List<string> GetUsernames(int threshold)
        {
            
                var response = client.GetAsync(BaseUrl).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve Authors");

            }
            var responseContent = response.Content;

            // by calling .Result you are synchronously reading the result
            string responseString = responseContent.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<ComplexAuthors>(responseString);
            var GetUsernamesResult = tasks.data.Where(r => r.submission_count == threshold).Select(r => r.username).ToList();

            return GetUsernamesResult;
        }

        public static string GetUsernameWithHighestCommentCount()
        {
            var response = client.GetAsync(BaseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Author");

            }
            var responseContent = response.Content;

            // by calling .Result you are synchronously reading the result
            string responseString = responseContent.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<ComplexAuthors>(responseString);
            var getUsernameWithHighestCommentCountResult = tasks.data.MaxBy(r => r.comment_count).Select(r => r.username).FirstOrDefault();
            return getUsernameWithHighestCommentCountResult;

        }

        public static List<string> GetUsernamesSortedByRecordDate(int threshold)
        {
            var response = client.GetAsync(BaseUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Authors");

            }
            var responseContent = response.Content;

            // by calling .Result you are synchronously reading the result
            string responseString = responseContent.ReadAsStringAsync().Result;
            var tasks = JsonConvert.DeserializeObject<ComplexAuthors>(responseString);
            var getUsernamesSortedByRecordDateResult = tasks.data.Where(r => r.created_at == threshold).Select(r => r.username).ToList();
            return getUsernamesSortedByRecordDateResult;

        }
    }
}
