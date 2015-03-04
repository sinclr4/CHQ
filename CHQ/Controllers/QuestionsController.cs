using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contentful.NET.DataModels;
using Contentful.NET.Exceptions;
using Contentful.NET.Search;
using Contentful.NET;
using Contentful.NET.Search.Filters;
using CHQ.Models;


namespace CHQ.Controllers
{
    public class QuestionsController : Controller
    {
        public string strspaceID = "w4gsrh5e985a";
        private string strAPIToken = "39426304569b88d9debca37cc08b55f8b08a88415ba518347db824f4a0a75d31";

        IContentfulClient Contentfulclient = new ContentfulClient("39426304569b88d9debca37cc08b55f8b08a88415ba518347db824f4a0a75d31", "w4gsrh5e985a");


        public async Task<ActionResult> Topics()
        //Get all Topics and display them as a list for people to select the topic they want to explore more
        {
            var cancellationToken = new CancellationToken();
            try
            {

                var results = await Contentfulclient.SearchAsync<Entry>(cancellationToken, new[]
            {
                // Only search for the 'Topic' content type
                new  EqualitySearchFilter(BuiltInProperties.ContentType, "60X20scHHqEQkeQ8OE4uqG"),
            }
                    //,  includeLevels: 1 // Ensure we retrieve the linked assets inside this one request - we want to get the Images for the dogs too
                );



                return View((GetAllTopicsFromContentfulResult(results)));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ActionResult> QuestionList(string id)
        //Get all Topics and display them as a list for people to select the topic they want to explore more
        {
            var cancellationToken = new CancellationToken();
            try
            {

                var results = await Contentfulclient.SearchAsync<Entry>(cancellationToken, new ISearchFilter[]
            {
                // Only search for the 'Question' content type
                new  EqualitySearchFilter(BuiltInProperties.ContentType, "3kMNZy7VCEc2oW2yy68GyK"),
                new  FullTextSearchFilter(id)
            }
                    //,  includeLevels: 1 // Ensure we retrieve the linked assets inside this one request - we want to get the Images for the dogs too
                );



                return View((GetAllQuestionsFromContentfulResult(results)));
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ActionResult> Question(string id)
        //Get all Topics and display them as a list for people to select the topic they want to explore more
        {
            {
                var cancellationToken = new CancellationToken();
                try
                {

                    var results = await Contentfulclient.SearchAsync<Entry>(cancellationToken, new ISearchFilter[]
            {
                // Only search for the 'Question' content type
                new  EqualitySearchFilter(BuiltInProperties.SysId, id),
               
            }
                        //,  includeLevels: 1 // Ensure we retrieve the linked assets inside this one request - we want to get the Images for the dogs too
                    );


                    return View((GetAllQuestionsFromContentfulResult(results)));
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        private static Topics GetAllTopicsFromContentfulResult(SearchResult<Entry> results)
        {
            return new Topics
            {

                AllTopics = results.Items
                    // Retrieve the ImageId from the linked 'mainPicture' asset
                    // NOTE: We could merge all of these Select() statements into one, but this way we only have to call dog.GetLink() and dog.GetString()
                    //       once, which improves performance.
                    .Select(subject => new Topic()
                    {
                        ID = subject.SystemProperties.Id,

                        // Title = subject.Name




                    })
            };
        }

        private static Questions GetAllQuestionsFromContentfulResult(SearchResult<Entry> results)
        {
            return new Questions
            {

                AllQuestions = results.Items
                    // Retrieve the ImageId from the linked 'mainPicture' asset
                    // NOTE: We could merge all of these Select() statements into one, but this way we only have to call dog.GetLink() and dog.GetString()
                    //       once, which improves performance.
                    .Select(subject => new Question()
                    {
                        Title = subject.SystemProperties.Id,
                        Body = subject.GetString("body")
                        // Title = subject.Name




                    })
            };
        }
    }

}


