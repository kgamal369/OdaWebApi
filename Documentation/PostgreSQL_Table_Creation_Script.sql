
-- Project Table
CREATE TABLE Project (
    ProjectId SERIAL PRIMARY KEY,
    ProjectName VARCHAR(255),
    Location VARCHAR(255),
    Amenities TEXT,
    TotalUnits INT,
    ProjectLogo _bytea,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- Apartment Table
CREATE TABLE Apartment (
    ApartmentId SERIAL PRIMARY KEY,
    ApartmentName VARCHAR(255),
    ApartmentType VARCHAR(50),
    ApartmentStatus VARCHAR(50),
    ApartmentSpace DECIMAL(10,2),
    Description TEXT,
    ApartmentPhotos _bytea,
    ProjectId INT,
    FloorNumber INT,
    AvailabilityDate TIMESTAMP,
    CreatedDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE
);

-- AddOns Table
CREATE TABLE AddOns (
    AddOnId SERIAL PRIMARY KEY,
    AddOnName VARCHAR(255),
    AddOnType VARCHAR(50),
    PricePerUnit DECIMAL(10,2),
	Description TEXT,
	Brand Text,
	CreatedDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- ApartmentAddons Table
CREATE TABLE ApartmentAddons (
    ApartmentAddonsId SERIAL PRIMARY KEY,
    ApartmentId INT,
    AddOnId INT,
    AvailableAddons BOOLEAN,
    AssignedAddons BOOLEAN,
    MaxAvailable INT,
    InstalledAmount INT,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (ApartmentId) REFERENCES Apartment(ApartmentId) ON DELETE CASCADE,
    FOREIGN KEY (AddOnId) REFERENCES AddOns(AddOnId) ON DELETE CASCADE
);

-- Package Table
CREATE TABLE Package (
    PackageId SERIAL PRIMARY KEY,
    PackageName VARCHAR(255),
    ApartmentId INT,
    Price DECIMAL(10,2),
    Description TEXT,
    AssignedPackage BOOLEAN,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (ApartmentId) REFERENCES Apartment(ApartmentId) ON DELETE CASCADE
);



-- Customer Table
CREATE TABLE Customer (
    CustomerId SERIAL PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
	Address VARCHAR(255),
	 CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP
);


-- Role Table
CREATE TABLE Role (
    RoleId SERIAL PRIMARY KEY,
    RoleName VARCHAR(50),
	Description VARCHAR(255),
	CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP
);

-- User Table
CREATE TABLE Users (
    UserId SERIAL PRIMARY KEY,
    Username VARCHAR(255),
    PasswordHash TEXT,
	FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    PhoneNumber VARCHAR(20),
	CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP,
	LastLogin TIMESTAMP,
    RoleId INT,
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId) ON DELETE CASCADE
);
-- Permission Table
CREATE TABLE Permission (
    PermissionId SERIAL PRIMARY KEY,
    EntityName VARCHAR(255),
	Action VARCHAR(50),
    RoleId INT,
	CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId) ON DELETE CASCADE
);

-- Booking Table
CREATE TABLE Booking (
    BookingId SERIAL PRIMARY KEY,
    CustomerId INT,
    ApartmentId INT,
    CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP,
	BookingStatus TEXT,
	UserId INT,
	TotalAmount DECIMAL(10,2),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
	FOREIGN KEY (ApartmentId) REFERENCES Apartment(ApartmentId) ON DELETE CASCADE
);

-- Invoices Table
CREATE TABLE Invoices (
    InvoiceId SERIAL PRIMARY KEY,
    BookingId INT,
	CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP,
    InvoiceAmount DECIMAL(10,2),
    InvoiceStatus VARCHAR(50),
    invoiceDueDate TIMESTAMP,
    FOREIGN KEY (BookingId) REFERENCES Booking(BookingId) ON DELETE CASCADE
);