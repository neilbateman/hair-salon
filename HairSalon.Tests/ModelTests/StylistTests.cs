using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {

        public void Dispose()
        {
          Stylist.ClearAll();
        }

        public StylistTest()
        {
          DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=neil_bateman_test;";
        }

        [TestMethod]
        public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
        {
            //Arrange
            Stylist newStylist = new Stylist("Raquelle", "Nobody but Raquelle can touch this hair");

            //Assert
            Assert.AreEqual(typeof(Stylist), newStylist.GetType());
        }

        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            //Arrange
            string name = "Raquelle";
            string details = "Nobody but Raquelle can touch this hair";
            Stylist newStylist = new Stylist(name, details);

            //Act
            string result = newStylist.Name;

            //Assert
            Assert.AreEqual(name, result);
        }

        // [TestMethod]
        // public void GetClients_ReturnsEmptyClientList_ClientList()
        // {
        //     //Arrange
        //     string name = "Raquelle";
        //     string details = "Nobody but Raquelle can touch this hair";
        //     Stylist newStylist = new Stylist(name, details);
        //     List<Client> newList = new List<Client> { };
        //
        //     //Act
        //     List<Client> result = newStylist.GetClients();
        //
        //     //Assert
        //     CollectionAssert.AreEqual(newList, result);
        // }

    }
}
