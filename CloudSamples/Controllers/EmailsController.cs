using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raven.Client.Document;

namespace CloudSamples.Controllers
{
    public class EmailsController : ApiController
    {
        // GET api/emails
        public Email[] Get()
        {
            return new Email[]
                { 
                    new Email
                    {
                        From = "Gerrit@puddig.net",
                        To = "Paul.Paulsen@test.de",
                        Subject = "Hello1"
                    }, 
                    new Email
                    {
                        From = "Gerrit@puddig.net",
                        To = "Paul.Paulsen@test.de",
                        Subject = "Hello2"
                    }, 
            };
        }

        // GET api/emails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/emails
        public HttpResponseMessage Post(Email email)
        {
            if (ModelState.IsValid)
            {
                var documentStore = new DocumentStore
                {
                    ConnectionStringName = "RavenDB"
                };
                documentStore.Initialize();

               // Store the company in our RavenDB server
                using (var session = documentStore.OpenSession())
                {
                    session.Store(email);
                    session.SaveChanges();
                }

               

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/emails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/emails/5
        public void Delete(int id)
        {
        }
    }

    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }

        public string Subject { get; set; }
    }
}
