


-- Insert data into the Project table
INSERT INTO Project (ProjectName, Location, Amenities, TotalUnits, ProjectLogo, CreateDateTime, LastModifiedDateTime) 
VALUES
('XXX', 'xxx', NULL, 5, NULL, NULL, NULL),
('YYY', 'yyy', NULL, 4, NULL, NULL, NULL),
('ZZZ', 'zzz', NULL, 6, NULL, NULL, NULL);

-- Insert data into the Apartment table
INSERT INTO Apartment (ApartmentName, ApartmentType, ApartmentStatus, ApartmentSpace, Description, ApartmentPhotos, ProjectId, FloorNumber, AvailabilityDate, CreatedDateTime, LastModifiedDateTime)
VALUES
('aa', 'Template', 'Template', 65, NULL, NULL, 1, NULL, NULL, NULL, NULL),
('bb', 'Standalone', 'ForSale', 72, NULL, NULL, 1, NULL, NULL, NULL, NULL),
('cc', 'Project', 'InProgress', 84, NULL, NULL, 1, NULL, NULL, NULL, NULL),
('dd', 'Template', 'Template', 110, NULL, NULL, 2, NULL, NULL, NULL, NULL),
('ee', 'Standalone', 'ForSale', 120, NULL, NULL, 2, NULL, NULL, NULL, NULL),
('ff', 'Project', 'InReview', 115, NULL, NULL, 2, NULL, NULL, NULL, NULL),
('gg', 'Template', 'Template', 140, NULL, NULL, 3, NULL, NULL, NULL, NULL),
('hh', 'Standalone', 'ForSale', 155, NULL, NULL, 3, NULL, NULL, NULL, NULL),
('ii', 'Project', 'Booked', 136, NULL, NULL, 3, NULL, NULL, NULL, NULL);

-- Insert data into the AddOns table
INSERT INTO AddOns (AddOnName, AddOnType, PricePerUnit, Description)
VALUES
('AirConditioning_5hp', 'AirConditioning_5hp', 3000, NULL),
('AirConditioning_3hp', 'AirConditioning_3hp', 2500, NULL),
('SmartLighting', 'SmartLighting', 6000, NULL),
('Boilers', 'Boilers', 5000, NULL),
('SolarHeating', 'SolarHeating', 7500, NULL),
('ShowerTemperedGlass', 'ShowerTemperedGlass', 6000, NULL),
('ShuttersAluminum', 'ShuttersAluminum', 2500, NULL);

-- Insert data into the ApartmentAddons table
INSERT INTO ApartmentAddons (ApartmentId, AddOnId,AvailableAddons,AssignedAddons,MaxAvailable,InstalledAmount)
VALUES
(1,1,true,false,1,0),
(1,2,true,false,3,0),
(1,3,true,false,1,0),
(1,4,false,false,0,0),
(1,5,true,false,2,0),
(1,6,true,false,1,0),
(1,7,true,false,1,0),
(3,1,true,true,1,1),
(3,2,true,true,3,2),
(3,3,true,true,1,1),
(3,4,false,false,1,1),
(3,5,true,false,2,2),
(3,6,true,true,1,1),
(3,7,true,true,1,1);

-- Insert data into the Package table
INSERT INTO Package (PackageName, apartmentid ,price ,Description, assignedpackage)
VALUES
('Golden',1,1000,NUll,false),
('Platinum',1,1500,NUll,false),
('Silver',1,3000,NUll,false),

('Golden',2,1000,NUll,false),
('Platinum',2,1500,NUll,false),
('Silver',2,3000,NUll,false),

('Golden',3,1000,NUll,false),
('Platinum',3,1500,NUll,false),
('Silver',3,3000,NUll,false),

('Golden',4,1000,NUll,false),
('Platinum',4,1500,NUll,false),
('Silver',4,3000,NUll,false),

('Golden',5,1000,NUll,false),
('Platinum',5,1500,NUll,false),
('Silver',5,3000,NUll,false),

('Golden',6,1000,NUll,true),
('Platinum',6,1500,NUll,false),
('Silver',6,3000,NUll,false);



-- Insert data into the Customer table
INSERT INTO Customer (firstname , lastname , Email)
VALUES
('Karim','Gamal','xxx@xxx.com'),
('Ahmed','Gamal','yyy@xxx.com'),
('Yousef','Karim','aaa@xxx.com'),
('Yehia','Karim','bbb@xxx.com'),
('Asmaa','Mamdouh','ccc@xxx.com'),
('Gamal','Galal','ddd@xxx.com'),
('Dina','Galal','eee@xxx.com'),
('Salwa','Ibrahim','fff@xxx.com');


-- Insert data into the User table
INSERT INTO Users (UserId, RoleId, UserName)
VALUES
(98,5, 'Abc'),
(72,5, 'Xyz'),
(56,1, 'Kgama369'),
(36,2, 'lmn589'),
(96,3, 'Abc123');

-- Insert data into the Role table
INSERT INTO Role (rolename, Description, CreateDateTime, LastModifiedDateTime)
VALUES
('SuperAdmin', NULL, NULL, NULL),
('Admin', NULL, NULL, NULL),
('Operator', NULL, NULL, NULL),
('Sales', NULL, NULL, NULL),
('Customer', NULL, NULL, NULL);

-- Insert data into the Permission table
INSERT INTO Permission (EntityName, Action, RoleId,CreateDateTime,LastModifiedDateTime)
VALUES
('Project', 'Get', 1, NULL, NULL),
('Project', 'Add', 1, NULL, NULL),
('Project', 'Edit', 1, NULL, NULL),
('Project', 'Remove', 1, NULL, NULL),
('Apartment', 'Get', 1, NULL, NULL),
('Apartment', 'Add', 1, NULL, NULL),
('Apartment', 'Edit', 1, NULL, NULL),
('Apartment', 'Remove', 1, NULL, NULL),
('Booking', 'Get', 1, NULL, NULL),
('Booking', 'Add', 1, NULL, NULL),
('Booking', 'Edit', 1, NULL, NULL),
('Booking', 'Remove', 1, NULL, NULL),
('Project', 'Get', 5, NULL, NULL),
('Apartment', 'Get', 5, NULL, NULL),
('Package', 'Get', 5, NULL, NULL),
('AddOns', 'Get', 5, NULL, NULL),
('ApartmentAddons', 'Get', 5, NULL, NULL),
('Project', 'Get', 4, NULL, NULL),
('Apartment', 'Get', 4, NULL, NULL),
('Package', 'Get', 4, NULL, NULL),
('AddOns', 'Get', 4, NULL, NULL),
('ApartmentAddons', 'Get', 4, NULL, NULL),
('Invoices', 'Get', 4, NULL, NULL),
('Customer', 'Get', 4, NULL, NULL),
('Booking', 'Get', 4, NULL, NULL);


-- Insert data into the Booking table
INSERT INTO Booking (CustomerId, ApartmentId,CreateDateTime,LastModifiedDateTime,BookingStatus,UserId,TotalAmount)
VALUES
(1, 3, '2024-09-12','2024-10-09', 'InProgress',98, 75000),
(4, 6, '2024-08-07','2024-09-10', 'Done',72, 100000),
(7, 9, '2023-09-01','2024-10-10', 'Rejected',72, 98000);


-- Insert data into the Invoices table
INSERT INTO Invoices (BookingId, invoiceamount , invoicestatus)
VALUES
(1,  10000, 'Paid'),
(1,  15000, 'Paid'),
(1,  20000, 'PartiallyPaid'),
(1,  15000, 'Pending'),
(1,  10000, 'Pending'),
(1,  5000, 'Pending'),
(2,  25000, 'Paid'),
(2,  25000, 'Paid'),
(2,  25000, 'Paid'),
(2,  25000, 'Overdue'),
(3,  98000, 'Cancelled');
