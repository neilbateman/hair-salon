using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=neil_bateman_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      // Arrange
      Client newClient = new Client("Neil Bateman", "8675309", "who@gmail.com", 1, 1);

      // Assert
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      // Arrange
      string name = "Neil Bateman";
      string phoneNumber = "911";
      string emailAddress = "jamjam@gmail.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      // Act
      string result = newClient.Name;

      // Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void Get_Returns_String()
    {
      // Arrange
      string name = "Bernie Sandman";
      string phoneNumber = "123456789";
      string emailAddress = "bsforpres@gmail.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      // Act
      string result = newClient.Name;

      // Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetPhoneNumber_ReturnsPhoneNumber_String()
    {
      // Arrange
      string name = "Kendrick Lamar";
      string phoneNumber = "3232866556";
      string emailAddress = "kdotforprezkunfukennyforprez@gmail.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      // Act
      string result = newClient.PhoneNumber;

      // Assert
      Assert.AreEqual(phoneNumber, result);
    }

    [TestMethod]
    public void SetPhoneNumber_SetPhoneNumber_String()
    {
      // Arrange
      string name = "Jimbo Jones";
      string phoneNumber = "0987654432";
      string emailAddress = "jerryheller@gmail.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      //Act
      string updatedPhoneNumber = "67676767676";
      newClient.PhoneNumber(updatedPhoneNumber);
      string result = newClient.PhoneNumber;

      //Assert
      Assert.AreEqual(updatedPhoneNumber, result);
    }

    [TestMethod]
    public void GetEmailAddress_ReturnsEmailAddress_String()
    {
      // Arrange
      string name = "James Brown";
      string phoneNumber = "594939999";
      string emailAddress = "yahoo@yahoo.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      // Act
      string result = newClient.EmailAddress;

      // Assert
      Assert.AreEqual(emailAddress, result);
    }

    [TestMethod]
    public void SetEmailAddress_SetEmailAddress_String()
    {
      // Arrange
      string name = "James Brown";
      string phoneNumber = "23455666";
      string emailAddress = "what@gmail.com";
      int id= 1;
      int stylistId= 1;
      Client newClient = new Client(name, phoneNumber, emailAddress, id, stylistId);

      //Act
      string updatedEmailAddress = "who@gmail.com";
      newClient.EmailAddress(updatedEmailAddress);
      string result = newClient.EmailAddress;

      //Assert
      Assert.AreEqual(updatedEmailAddress, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ClientList()
    {
      // Arrange
      List<Client> newList = new List<Client> { };

      // Act
      List<Client> result = Client.GetAll();

      // Assert
      CollectionAssert.AreEqual(newList, result);
    }
  }
}
