using NUnit.Framework;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using AddressBookDatabase;

namespace NUnitTestUsingRestSharp
{
    public class Tests
    {
        RestClient client;
        [SetUp]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse GetResponse()
        {
            //arrange
            RestRequest request = new RestRequest("/Contacts", Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            return response;

        }

        [Test]
        public void PassGETRequest_ReturnsContactList()
        {
            IRestResponse response = GetResponse();
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contacts> contactsList = JsonConvert.DeserializeObject<List<Contacts>>(response.Content);
            Assert.AreEqual(5, contactsList.Count);

            foreach (Contacts contact in contactsList)
            {
                System.Console.WriteLine(contact.FirstName + " " + contact.LastName + " " + contact.Address + " " + contact.City + " " + contact.State + " " + contact.ZipCode + " " + contact.PhoneNumber + " " + contact.Email);
            }
        }

        [Test]
        public void PassPOSTRequest_ReturnsStatusCreated()
        {
            //arrange
            Contacts contacts = new Contacts
            {
                FirstName = "suresh",
                LastName = "babu",
                Address = "some where",
                City = "bangalore",
                State = "Karnataka",
                ZipCode = 876540,
                PhoneNumber = 6668888222,
                Email = "babusuresh@yahoo.com"
            };
            //act
            RestRequest request = new RestRequest("/Contacts", Method.POST);
            object jObject = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Contacts contacts1 = JsonConvert.DeserializeObject<Contacts>(response.Content);
            Assert.AreEqual("suresh", contacts1.FirstName);
            Assert.AreEqual("babu", contacts1.LastName);
        }
        [Test]
        public void PassPOSTRequestAddMultipleContacts_ReturnsStatusCreated()
        {
            //arrange
            List<Contacts> contactsList = new List<Contacts>
            {
                new Contacts(){FirstName= "Sugresh", LastName ="ambani", Address = "some where", City = "Bangalore", State = "Karnataka", ZipCode = 12341, PhoneNumber = 09764546412, Email = "sugreshambani@gmail.com"},
                new Contacts(){FirstName= "mahesh", LastName ="ambani", Address = "some where", City = "Bangalore", State = "Karnataka", ZipCode = 12341, PhoneNumber = 09764546412, Email = "sugreshambani@gmail.com"},
                new Contacts(){FirstName= "ramesh", LastName ="ambani", Address = "some where", City = "Bangalore", State = "Karnataka", ZipCode = 12341, PhoneNumber = 09764546412, Email = "sugreshambani@gmail.com"},
                new Contacts(){FirstName= "Suresh", LastName ="ambani", Address = "some where", City = "Bangalore", State = "Karnataka", ZipCode = 12341, PhoneNumber = 09764546412, Email = "sugreshambani@gmail.com"},
                new Contacts(){FirstName= "mahesh", LastName ="ambani", Address = "some where", City = "Bangalore", State = "Karnataka", ZipCode = 12341, PhoneNumber = 09764546412, Email = "sugreshambani@gmail.com"}
            };
            IRestResponse response;
            //act
            foreach (Contacts contact1 in contactsList)
            {
                RestRequest request = new RestRequest("/Contacts", Method.POST);
                object jObject = JsonConvert.SerializeObject(contact1, Formatting.Indented);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                response = client.Execute(request);
                //assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }
        }


        [Test]
        public void PassPUTRequest_ReturnsStatusOK()
        {
            //arrange
            Contacts contacts = new Contacts
            {
                FirstName = "suresh",
                LastName = "babu",
                Address = "some where",
                City = "bangalore",
                State = "Karnataka",
                ZipCode = 876540,
                PhoneNumber = 6668888222,
                Email = "babusuresh@yahoo.com"
            };
            //act
            RestRequest request = new RestRequest("/Contacts/1", Method.PUT);
            object jObject = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Contacts contacts1 = JsonConvert.DeserializeObject<Contacts>(response.Content);
            Assert.AreEqual("suresh", contacts1.FirstName);
            Assert.AreEqual("babu", contacts1.LastName);
        }

        [Test]
        public void PassDELETERequest_ReturnsStatusOK()
        {
            //arrange
            int id = 1;
            RestRequest request = new RestRequest("/Contacts/" + id, Method.DELETE);
            //act
            IRestResponse response = client.Execute(request);
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response = GetResponse();
            List<Contacts> contactsList = JsonConvert.DeserializeObject<List<Contacts>>(response.Content);
            Assert.AreEqual(6, contactsList.Count);
        }

    }
}