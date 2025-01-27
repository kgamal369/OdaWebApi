-- Developer Table 
CREATE TABLE Developer (
    DeveloperId SERIAL PRIMARY KEY,
    DeveloperName VARCHAR(255),
    Description VARCHAR(255),
    DeveloperLogo _bytea,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- Testimonials Table 
CREATE TABLE Testimonials (
    TestimonialsID SERIAL PRIMARY KEY,
    TestimonialsName VARCHAR(255),
    TestimonialsTitle VARCHAR(255),
    Description VARCHAR(255),
    TestimonialsPhoto _bytea,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);


-- Project Table
CREATE TABLE Project (
    ProjectId SERIAL PRIMARY KEY,
    ProjectName VARCHAR(255),
    Location VARCHAR(255),
    DeveloperId INT,
    Amenities TEXT,
    TotalUnits INT,
    ProjectLogo _bytea,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (DeveloperId) REFERENCES Developer(DeveloperId) ON DELETE CASCADE

);
-- Plan Table 
CREATE TABLE Plans (
    PlanId SERIAL PRIMARY KEY,
    PlanName VARCHAR(255),
    PricePerMeter DECIMAL(10,2),
    Description VARCHAR(255),
    PlanPhoto _bytea,
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- PlanDetails Table 
CREATE TABLE PlanDetails (
    PlanDetailsID SERIAL PRIMARY KEY,
    PlanDetailsName VARCHAR(255),
    PlanDetailsType  VARCHAR(255),
    PlanId INT,
    Description VARCHAR(255),
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (PlanId) REFERENCES Plans(PlanId) ON DELETE CASCADE
);


-- Automation Table 
CREATE TABLE Automation (
    AutomationId SERIAL PRIMARY KEY,
    AutomationName VARCHAR(255),
    Description VARCHAR(255),
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- AutomationDetails Table 
CREATE TABLE AutomationDetails (
    AutomationDetailsID SERIAL PRIMARY KEY,
    AutomationDetailsName VARCHAR(255),
    AutomationId INT,
    Description VARCHAR(255),
    CreateDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (AutomationId) REFERENCES Automation(AutomationId) ON DELETE CASCADE
);


-- AddOns Table
CREATE TABLE AddOns (
    AddOnId SERIAL PRIMARY KEY,
    AddOnName VARCHAR(255),
    AddOnGroup VARCHAR(255),
    UnitOrMeter VARCHAR(255),
    Price DECIMAL(10,2),
	Description TEXT,
	Brand Text,
	CreatedDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);

-- AddPerRequest Table
CREATE TABLE AddPerRequest (
    AddPerRequestId SERIAL PRIMARY KEY,
    AddPerRequestName VARCHAR(255),
    Price DECIMAL(10,2),
	Description TEXT,
	CreatedDateTime TIMESTAMP,
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
    PlanId  INT,
    AutomationId INT,
    FloorNumber INT,
    AvailabilityDate TIMESTAMP,
    CreatedDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP,
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    FOREIGN KEY (PlanId) REFERENCES Plans(PlanId) ON DELETE CASCADE,
    FOREIGN KEY (AutomationId) REFERENCES Automation(AutomationId) ON DELETE CASCADE
);

CREATE TABLE Apartment_Addon (
    ApartmentID INT REFERENCES Apartment(ApartmentID),
    AddonID INT REFERENCES AddOns(AddonID),
    PRIMARY KEY (ApartmentID, AddonID)
);

CREATE TABLE Apartment_AddonPerRequest (
    ApartmentID INT REFERENCES Apartment(ApartmentID),
    AddPerRequestId INT REFERENCES AddPerRequest(AddPerRequestId),
    PRIMARY KEY (ApartmentID, AddPerRequestId)
);


Create Table PaymentMethod (
    PaymentMethodID SERIAL PRIMARY KEY,
    PaymentMethodName VARCHAR(255),
    PaymentMethodPhotos _bytea,
    Description TEXT,
    DepositPercentage DECIMAL(10,2),
    NumberOfInstallments INT,
    CreatedDateTime TIMESTAMP,
    LastModifiedDateTime TIMESTAMP
);


-- Booking Table
CREATE TABLE Booking (
    BookingId SERIAL PRIMARY KEY,
    CustomerId INT,
    ApartmentId INT,
    PaymentMethodID INT,
    CreateDateTime TIMESTAMP,
	LastModifiedDateTime TIMESTAMP,
	BookingStatus TEXT,
	UserId INT,
	TotalAmount DECIMAL(10,2),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
	FOREIGN KEY (ApartmentId) REFERENCES Apartment(ApartmentId) ON DELETE CASCADE,
    FOREIGN KEY (PaymentMethodID) REFERENCES PaymentMethod(PaymentMethodID) ON DELETE CASCADE,
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