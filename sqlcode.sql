---Creating table for customer table
CREATE TABLE Customer (
    CustomerID INT IDENTITY PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(15),
    Address VARCHAR(255),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255),
    RegistrationDate DATE
);


--Creating table for vehicle table
CREATE TABLE Vehicle (
    VehicleID INT IDENTITY PRIMARY KEY,
    Model VARCHAR(255),
    Make VARCHAR(255),
    Year INT,
    Color VARCHAR(50),
    RegistrationNumber VARCHAR(20) UNIQUE,
    Availability BIT,
    DailyRate DECIMAL(10, 2)
);

----Creating table for reservation table
CREATE TABLE Reservation (
    ReservationID INT IDENTITY PRIMARY KEY,
    CustomerID INT,
    VehicleID INT,
    StartDate DATETIME,
    EndDate DATETIME,
    TotalCost DECIMAL(10, 2),
    Status VARCHAR(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (VehicleID) REFERENCES Vehicle(VehicleID)
);

--Creating table for Admin Table
CREATE TABLE Admin(
    AdminID INT IDENTITY PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(15),
    Username VARCHAR(50) UNIQUE,
    Password VARCHAR(255),
    Role VARCHAR(50),
    JoinDate DATE
);



-----Inserting 10 samples into customer table
INSERT INTO Customer(FirstName,LastName,Email,PhoneNumber,Address,Username,Password,RegistrationDate)
VALUES
    ( 'Swathi', 'R', 'swat@gmail.com', '9734567890','123 Main St, Cityville', 'swathi_r', 'swat@1234', '2023-01-01'),
    ('Afra', 'Batool', 'afuu@gmail.com', '9876543210','456 Oak St, Townsville', 'afra_batool', 'afra@3459', '2023-02-02'),
    ( 'MOhammed', 'Ruhaib', 'kabbu@gmail.com', '9551234567','789 Pine St, Villagetown', 'kabbu_ruhaib', 'kabbu_2323', '2023-05-13'),
    ( 'Zaid', 'Amir', 'zaid@gmail.com', '9112223333','101 Elm St, Hamletville', 'zaid_amir', 'zaid_amir', '2023-06-24'),
    ( 'Prajju', 'SK', 'prajju@gmail.com', '9998887777', '202 Cedar St, Boroughburg', 'prajju_sk', 'prajju#1234', '2023-07-05'),
    ( 'Sonika', 'CP', 'soni@gmail.com', '9445556666','303 Maple St, Villageburg', 'sonika_cp', 'soni@cp12', '2023-09-06'),
    ( 'sanju', 'H', 'sanju@gmail.com', '7776665555', '505 Birch St, Citytown', 'sanju_h', 'sanjusanju_123', '2023-10-07'),
    ( 'Trisha', 'M', 'trisha@gmail.com', '9334445555', '505 Birch St, Citytown', 'trisha_m', 'trish_123', '2023-12-17'),
    ( 'Ryan', 'Garcia', 'ryan.garcia@gamil.com', '9223334444','707 Pine St, Townborough', 'ryan_g', 'ryan@123', '2024-01-08'),
    ('Olivia', 'White', 'olivia.white@gmail.com', '9667778888','909 Oak St, Boroughville', 'olivia_w', 'olivia@9898', '2024-02-09');

-----Inserting 10 samples into vehicle table
INSERT INTO Vehicle ( Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES
    ( 'Sedan', 'Toyota', 2022, 'Blue', 'ABC123', 0, 50.00),
    ( 'SUV', 'Ford', 2021, 'Red', 'XYZ456', 1, 70.00),
    ( 'Hatchback', 'Honda', 2023, 'Silver', 'LMN789', 0, 60.00),
    ( 'Truck', 'Chevrolet', 2020, 'Black', 'PQR101', 1, 80.00),
    ( 'Coupe', 'BMW', 2022, 'White', 'JKL202', 1, 90.00),
    ( 'Convertible', 'Mazda', 2023, 'Yellow', 'GHI303', 0, 85.00),
    ( 'Minivan', 'Kia', 2021, 'Green', 'DEF404', 1, 65.00),
    ( 'Sports Car', 'Ferrari', 2022, 'Orange', 'UVW505', 0, 150.00),
    ( 'Crossover', 'Nissan', 2023, 'Purple', 'RST606', 1, 75.00),
    ('Electric Car', 'Tesla', 2023, 'Gray', 'OPQ707', 1, 100.00);

-----Inserting 10 samples into reservation table
  INSERT INTO Reservation ( CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES
    ( 1, 5, '2023-01-15 10:00:00', '2023-01-17 15:30:00', 180.00, 'Confirmed'),
    ( 3, 2, '2023-02-05 08:45:00', '2023-02-08 12:00:00', 210.00, 'Pending'),
    ( 2, 7, '2023-03-12 14:00:00', '2023-03-15 17:45:00', 195.00, 'Completed'),
    ( 4, 9, '2023-04-03 09:30:00', '2023-04-06 16:15:00', 225.00, 'Confirmed'),
    ( 6, 3, '2023-05-20 11:15:00', '2023-05-22 18:00:00', 120.00, 'Pending'),
    ( 8, 1, '2023-06-08 13:45:00', '2023-06-12 10:30:00', 320.00, 'Confirmed'),
    ( 5, 10, '2023-07-04 08:00:00', '2023-07-06 14:45:00', 200.00, 'Pending'),
    ( 9, 4, '2023-08-17 16:30:00', '2023-08-20 09:15:00', 240.00, 'Completed'),
    ( 7, 6, '2023-09-22 12:00:00', '2023-09-24 17:30:00', 170.00, 'Confirmed'),
    ( 10, 8, '2023-10-10 10:30:00', '2023-10-14 14:00:00', 600.00, 'Pending');

-----Inserting 10 samples into admin table
 INSERT INTO Admin ( FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
VALUES
    ('Arun', 'Kumar', 'arun_ku@gmail.com', '123-456-7890', 'arun_kumar', 'arun_123', 'Super Admin', '2023-01-01'),
    ( 'Adithya', 'Rahane', 'adithya_23@gmail.com', '987-654-3210', 'adithya_rahane', 'adithya@345', 'Fleet Manager', '2023-02-02'),
    ( 'Akshay', 'Kumar', 'akshay_ku34@gmail.com', '555-123-4567', 'akshay_kumar', 'akshay@883', 'System Admin', '2023-03-03'),
    ( 'Darshan', 'MC', 'darsh34@gmail.com', '111-222-3333', 'darshan_mc', 'darshu#454', 'Fleet Manager', '2023-04-04'),
    ( 'Chandan', 'Gowda', 'chandugow@gmail.com', '777-888-9999', 'chandhan_gowda', 'chandu_785', 'Super Admin', '2023-05-05'),
    ( 'Mithun', 'Shetty', 'mithu_shet@gmail.com', '444-555-6666', 'mithun_sheety', 'mithu_shet6', 'Fleet Manager', '2023-06-06'),
    ( 'Mugesh', 'Rao', 'mugi8@gmail.com', '999-111-2222', 'mugesh_rao','muge@8989', 'System', '2023-07-07'),
    ( 'Oviya', 'R', 'oviya23@gmail.com', '666-777-8888', 'oviya_r','oviya#456', 'Super Admin', '2023-08-08'),
    ( 'Haarika', 'Lokesh', 'haari_lok@gmail.com', '333-444-5555', 'haarika_lokesh', 'haar23', 'Fleet Manager', '2023-09-09'),
    ( 'Sinchana', 'NK', 'sinchu_nk@gmail.com', '222-333-4444', 'sinchana_nk', 'sinch5656', 'System Admin', '2023-10-10');

	
--displaying 
select * from Customer;
select * from Vehicle;
select * from Reservation;
select * from Admin;


---GetCustomerById, GetCustomerByUsername, RegisterCustomer, UpdateCustomer, DeleteCustomer
SELECT * FROM Customer
WHERE CustomerID=5;

SELECT * FROM Customer
WHERE Username='kabbu_ruhaib';

UPDATE Customer
SET FirstName='Appi'
WHERE CustomerID=9;

DELETE FROM Customer
WHERE CustomerID=10;

--GetVehicleById, GetAvailableVehicles, AddVehicle, UpdateVehicle, RemoveVehicle
SELECT * FROM Vehicle
WHERE VehicleID=5;


SELECT * FROM Vehicle
WHERE Availability=1;

INSERT INTO Vehicle (VehicleID, Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES (11, 'Sedan', 'Toyota', 2024, 'Purple', 'ABC123', 0, 20.00);

UPDATE Vehicle
SET Make='Honda'
WHERE VehicleID=9;

DELETE FROM Vehicle
WHERE Model='Sedan';

---GetReservationById, GetReservationsByCustomerId, CreateReservation, UpdateReservation, CancelReservation
SELECT * From Reservation
WHERE ReservationID=5;

SELECT * From Reservation
WHERE CustomerID=5;

INSERT INTO Reservation (ReservationID, CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES (11, 5, 3, '2023-01-15 10:00:00', '2023-01-17 15:30:00', 180.00, 'Confirmed');

UPDATE Reservation
SET StartDate = '2023-01-15 10:00:00',EndDate = '2023-01-17 15:30:00'
WHERE ReservationID = 5;

UPDATE Reservation
SET Status = 'Canceled'
WHERE ReservationID = 6;

---GetAdminById, GetAdminByUsername, RegisterAdmin, UpdateAdmin, DeleteAdmin
 SELECT * FROM Admin
 WHERE AdminID = 4;

SELECT * FROM Admin
WHERE Username = 'haarika_lokesh';

INSERT INTO Admin (AdminID, FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
VALUES
    (11, 'Appu', 'Kumar', 'appu_ku@gmail.com', '9844567890', 'appu_kumar', 'appu_123', 'Super Admin', '2024-04-11');

UPDATE Admin
SET FirstName = 'Arun',LastName = 'Kumar'
WHERE AdminID = 2;

DELETE FROM Admin
WHERE AdminID = 9;





