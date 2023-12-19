using CarConnect.Repository;
using CarConnect.Interfaces;
using NUnit.Framework;
using CarConnect.Model;
using CarConnect.Exception;
using System.Security.Authentication;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System;

namespace CarConnectTest
{
    [TestFixture]
    public class CarConnectTest
    {
        public string connectionString = "Server=DESKTOP-HM1OL79\\MSSQLSERVER01;Database=CarControl;Trusted_Connection=True";
        private ICustomerRepository customerRepository = new CustomerRepository();
        private IVehicleRepository vehicleRepository = new VehicleRepository();

        [Test]
        public void TestCustomerAuthenticationWithInvalidCredentials()
        {
            // Arrange
            var customerRepository = new CustomerRepository();
            var username = "invalid";
            var password = "invalid";

            // Act
            var result = customerRepository.customerAuthenticate(username, password);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetVehicleById_ExistingVehicleId_ReturnsVehicle()
        {
            // Arrange
            int existingVehicleId = 7;

            // Act
            var vehicle = vehicleRepository.GetVehicleById(existingVehicleId);

            // Assert
            Assert.That(vehicle, Is.Not.Null);
        }

        [Test]
        public void UpdateCustomer_ValidData_SuccessfullyUpdatesCustomer()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                CustomerID = 3,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
            };

            // Act
            Assert.That(() => customerRepository.UpdateCustomer(existingCustomer.CustomerID, existingCustomer.Email), Throws.Nothing);

            // Assert
            Assert.That(existingCustomer.Email, Is.EqualTo("john.doe@example.com"));
        }

        [Test]
        public void AddVehicle_ShouldAddNewVehicle()
        {
            // Arrange
            var newVehicle = new Vehicle
            {
                Model = "NewModel",
                Make = "NewMake",
                Year = 2023,
                Color = "Blue",
                RegistrationNumber = "ABC123",
                Availability = true,
                DailyRate = 50.00F
            };

            // Act
            Assert.That(() => vehicleRepository.AddVehicle(newVehicle), Throws.Nothing);

            // Assert
            Assert.That(newVehicle.VehicleID, Is.EqualTo(0));
        }

        [Test]
        public void UpdateVehicle_ShouldUpdateVehicleDetails()
        {

            Vehicle existingVehicle = new Vehicle
            {
                VehicleID = 4,
                Color = "purple",

            };


            // Act
            Assert.That(() => vehicleRepository.UpdateVehicle(existingVehicle.VehicleID, existingVehicle.Color), Throws.Nothing);

            // Assert
            Assert.That(existingVehicle.Color, Is.EqualTo("purple"));
        }


        //[Test]
        //public void getavailablevehicles_shouldreturnlistofavailablevehicles()
        //{
        //    // arrange
        //    vehiclerepository vehiclerepository = new vehiclerepository();
        //    vehicle availablevehicle1 = new vehicle { availability = true };
        //    vehicle availablevehicle2 = new vehicle { availability = true };
        //    vehicle unavailablevehicle = new vehicle { availability = false };
        //    vehiclerepository.addvehicle(availablevehicle1);
        //    vehiclerepository.addvehicle(availablevehicle2);
        //    vehiclerepository.addvehicle(unavailablevehicle);

        //    // act
        //    list<vehicle> availablevehicles = vehiclerepository.getavailablevehicles();

        //    // assert
        //    assert.that(availablevehicles.count, is.equalto(2));
        //}

        //[Test]
        //public void GetAllVehicles_ShouldReturnListOfAllVehicles()
        //{
        //    // Arrange
        //    var vehicleRepository = new VehicleRepository();

        //    // Act
        //    var allVehicles = vehicleRepository.GetAllVehicles();

        //    // Assert
        //    Assert.That(allVehicles, Is.Not.Null & Is.Not.Empty);
        //    Assert.That(allVehicles, Has.Count.GreaterThan(0));

        //    // Additional assertions based on your requirements
        //    foreach (var vehicle in allVehicles)
        //    {
        //        Assert.That(vehicle.VehicleID, Is.GreaterThan(0));
        //        Assert.That(vehicle.Model, Is.Not.Null.And.Not.Empty);
        //        Assert.That(vehicle.Make, Is.Not.Null.And.Not.Empty);
        //    }
        //}


    }

}