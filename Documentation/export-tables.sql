-- public.addons definition

-- Drop table

-- DROP TABLE public.addons;

CREATE TABLE public.addons (
	addonid serial4 NOT NULL,
	addonname varchar(255) NULL,
	addongroup varchar(50) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	brand text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	unitormeter varchar(255) NULL,
	CONSTRAINT addons_pkey PRIMARY KEY (addonid)
);

-- Permissions

ALTER TABLE public.addons OWNER TO odadb_user;
GRANT ALL ON TABLE public.addons TO odadb_user;


-- public.addperrequest definition

-- Drop table

-- DROP TABLE public.addperrequest;

CREATE TABLE public.addperrequest (
	addperrequestid serial4 NOT NULL,
	addperrequestname varchar(255) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT addperrequest_pkey PRIMARY KEY (addperrequestid)
);

-- Permissions

ALTER TABLE public.addperrequest OWNER TO odadb_user;
GRANT ALL ON TABLE public.addperrequest TO odadb_user;


-- public.automation definition

-- Drop table

-- DROP TABLE public.automation;

CREATE TABLE public.automation (
	automationid serial4 NOT NULL,
	automationname varchar(255) NULL,
	description varchar(255) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT automation_pkey PRIMARY KEY (automationid)
);

-- Permissions

ALTER TABLE public.automation OWNER TO odadb_user;
GRANT ALL ON TABLE public.automation TO odadb_user;


-- public.contactus definition

-- Drop table

-- DROP TABLE public.contactus;

CREATE TABLE public.contactus (
	firstname varchar NOT NULL,
	lastname varchar NOT NULL,
	phonenumber varchar NOT NULL,
	email varchar NULL,
	"comments" varchar(255) NOT NULL,
	contactusid serial4 NOT NULL,
	CONSTRAINT contactus_pkey PRIMARY KEY (contactusid)
);

-- Permissions

ALTER TABLE public.contactus OWNER TO odadb_user;
GRANT ALL ON TABLE public.contactus TO odadb_user;


-- public.customer definition

-- Drop table

-- DROP TABLE public.customer;

CREATE TABLE public.customer (
	customerid serial4 NOT NULL,
	firstname varchar(255) NULL,
	lastname varchar(255) NULL,
	email varchar(255) NULL,
	phonenumber varchar(20) NULL,
	address varchar(255) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT customer_pkey PRIMARY KEY (customerid)
);

-- Permissions

ALTER TABLE public.customer OWNER TO odadb_user;
GRANT ALL ON TABLE public.customer TO odadb_user;


-- public.developer definition

-- Drop table

-- DROP TABLE public.developer;

CREATE TABLE public.developer (
	developerid serial4 NOT NULL,
	developername varchar(255) NULL,
	description varchar(255) NULL,
	developerlogo _bytea NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT developer_pkey PRIMARY KEY (developerid)
);

-- Permissions

ALTER TABLE public.developer OWNER TO odadb_user;
GRANT ALL ON TABLE public.developer TO odadb_user;


-- public.invoices definition

-- Drop table

-- DROP TABLE public.invoices;

CREATE TABLE public.invoices (
	invoiceid serial4 NOT NULL,
	bookingid int4 NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	invoiceamount numeric(10, 2) NULL,
	invoicestatus varchar(50) NULL,
	invoiceduedate timestamp NULL,
	CONSTRAINT invoices_pkey PRIMARY KEY (invoiceid)
);

-- Permissions

ALTER TABLE public.invoices OWNER TO odadb_user;
GRANT ALL ON TABLE public.invoices TO odadb_user;


-- public.odaambassador definition

-- Drop table

-- DROP TABLE public.odaambassador;

CREATE TABLE public.odaambassador (
	ownername varchar NOT NULL,
	ownerphonenumber varchar NULL,
	ownerunitarea numeric NULL,
	ownerunitlocation varchar NULL,
	ownerdeveloper varchar NULL,
	ownerselectbudget numeric NULL,
	referralname varchar NOT NULL,
	referralphonenumber varchar NULL,
	referralemail varchar NULL,
	referralclientstatue varchar NULL,
	odaambassadorid serial4 NOT NULL,
	CONSTRAINT oda_pkey PRIMARY KEY (odaambassadorid)
);

-- Permissions

ALTER TABLE public.odaambassador OWNER TO odadb_user;
GRANT ALL ON TABLE public.odaambassador TO odadb_user;


-- public.paymentmethod definition

-- Drop table

-- DROP TABLE public.paymentmethod;

CREATE TABLE public.paymentmethod (
	paymentmethodid serial4 NOT NULL,
	paymentmethodname varchar(255) NULL,
	paymentmethodphotos _bytea NULL,
	description text NULL,
	depositpercentage numeric(10, 2) NULL,
	numberofinstallments int4 NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT paymentmethod_pkey PRIMARY KEY (paymentmethodid)
);

-- Permissions

ALTER TABLE public.paymentmethod OWNER TO odadb_user;
GRANT ALL ON TABLE public.paymentmethod TO odadb_user;


-- public.paymentplans definition

-- Drop table

-- DROP TABLE public.paymentplans;

CREATE TABLE public.paymentplans (
	paymentplanid serial4 NOT NULL,
	paymentplanname varchar(255) NOT NULL,
	numberofinstallmentmonths int4 NOT NULL,
	downpayment bool NOT NULL,
	downpaymentpercentage numeric(10, 2) NULL,
	adminfees bool NOT NULL,
	adminfeespercentage numeric(10, 2) NULL,
	interestrate bool NOT NULL,
	interestrateperyearpercentage numeric(5, 2) NULL,
	paymentplanicon bytea NULL,
	description varchar(255) NULL,
	CONSTRAINT paymentplans_pkey PRIMARY KEY (paymentplanid)
);

-- Permissions

ALTER TABLE public.paymentplans OWNER TO odadb_user;
GRANT ALL ON TABLE public.paymentplans TO odadb_user;


-- public."permission" definition

-- Drop table

-- DROP TABLE public."permission";

CREATE TABLE public."permission" (
	permissionid serial4 NOT NULL,
	entityname varchar(255) NULL,
	"action" varchar(50) NULL,
	roleid int4 NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT permission_pkey PRIMARY KEY (permissionid)
);

-- Permissions

ALTER TABLE public."permission" OWNER TO odadb_user;
GRANT ALL ON TABLE public."permission" TO odadb_user;


-- public.plan definition

-- Drop table

-- DROP TABLE public.plan;

CREATE TABLE public.plan (
	planid serial4 NOT NULL,
	planname varchar(255) NULL,
	pricepermeter numeric(10, 2) NULL,
	description varchar(255) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	planphoto bytea NULL,
	projecttype bool NULL,
	CONSTRAINT plan_pkey PRIMARY KEY (planid)
);

-- Permissions

ALTER TABLE public.plan OWNER TO odadb_user;
GRANT ALL ON TABLE public.plan TO odadb_user;


-- public.project definition

-- Drop table

-- DROP TABLE public.project;

CREATE TABLE public.project (
	projectid serial4 NOT NULL,
	projectname varchar(255) NULL,
	"location" varchar(255) NULL,
	amenities text NULL,
	totalunits int4 NULL,
	projectlogo _bytea NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	developerid int4 NULL,
	CONSTRAINT project_pkey PRIMARY KEY (projectid)
);

-- Permissions

ALTER TABLE public.project OWNER TO odadb_user;
GRANT ALL ON TABLE public.project TO odadb_user;


-- public."role" definition

-- Drop table

-- DROP TABLE public."role";

CREATE TABLE public."role" (
	roleid serial4 NOT NULL,
	rolename varchar(50) NULL,
	description varchar(255) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT role_pkey PRIMARY KEY (roleid)
);

-- Permissions

ALTER TABLE public."role" OWNER TO odadb_user;
GRANT ALL ON TABLE public."role" TO odadb_user;


-- public.testimonials definition

-- Drop table

-- DROP TABLE public.testimonials;

CREATE TABLE public.testimonials (
	testimonialsid serial4 NOT NULL,
	testimonialsname varchar(255) NULL,
	testimonialstitle varchar(255) NULL,
	description varchar(255) NULL,
	testimonialsphoto _bytea NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT testimonials_pkey PRIMARY KEY (testimonialsid)
);

-- Permissions

ALTER TABLE public.testimonials OWNER TO odadb_user;
GRANT ALL ON TABLE public.testimonials TO odadb_user;


-- public.unittype definition

-- Drop table

-- DROP TABLE public.unittype;

CREATE TABLE public.unittype (
	unittypeid serial4 NOT NULL,
	unittype_name varchar(50) NOT NULL,
	CONSTRAINT unittype_pkey PRIMARY KEY (unittypeid)
);

-- Permissions

ALTER TABLE public.unittype OWNER TO odadb_user;
GRANT ALL ON TABLE public.unittype TO odadb_user;


-- public.users definition

-- Drop table

-- DROP TABLE public.users;

CREATE TABLE public.users (
	userid serial4 NOT NULL,
	username varchar(255) NULL,
	passwordhash text NULL,
	firstname varchar(255) NULL,
	lastname varchar(255) NULL,
	email varchar(255) NULL,
	phonenumber varchar(20) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	lastlogin timestamp NULL,
	roleid int4 NULL,
	CONSTRAINT users_pkey PRIMARY KEY (userid)
);

-- Permissions

ALTER TABLE public.users OWNER TO odadb_user;
GRANT ALL ON TABLE public.users TO odadb_user;


-- public.apartment definition

-- Drop table

-- DROP TABLE public.apartment;

CREATE TABLE public.apartment (
	apartmentid serial4 NOT NULL,
	apartmentname varchar(255) NULL,
	apartmenttype varchar(50) NULL,
	apartmentstatus varchar(50) NULL,
	apartmentspace numeric(10, 2) NULL,
	description text NULL,
	apartmentphotos _bytea NULL,
	projectid int4 NULL,
	floornumber int4 NULL,
	availabilitydate timestamp NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	planid int4 NULL,
	automationid int4 NULL,
	apartmentaddress varchar(255) NULL,
	developerid int4 NULL,
	unittypeid int4 NULL,
	CONSTRAINT apartment_pkey PRIMARY KEY (apartmentid),
	CONSTRAINT apartment_automationid_fkey FOREIGN KEY (automationid) REFERENCES public.automation(automationid) ON DELETE CASCADE,
	CONSTRAINT apartment_developerid_fkey FOREIGN KEY (developerid) REFERENCES public.developer(developerid) ON DELETE CASCADE,
	CONSTRAINT apartment_planid_fkey FOREIGN KEY (planid) REFERENCES public.plan(planid) ON DELETE CASCADE,
	CONSTRAINT apartment_unittypeid_fkey FOREIGN KEY (unittypeid) REFERENCES public.unittype(unittypeid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.apartment OWNER TO odadb_user;
GRANT ALL ON TABLE public.apartment TO odadb_user;


-- public.apartment_addon definition

-- Drop table

-- DROP TABLE public.apartment_addon;

CREATE TABLE public.apartment_addon (
	apartmentid int4 NOT NULL,
	addonid int4 NOT NULL,
	quantity int4 NOT NULL,
	CONSTRAINT apartment_addon_pkey PRIMARY KEY (apartmentid, addonid),
	CONSTRAINT apartment_addon_apartmentid_fkey FOREIGN KEY (apartmentid) REFERENCES public.apartment(apartmentid) ON DELETE CASCADE,
	CONSTRAINT apartment_addons_addonid_fkey FOREIGN KEY (addonid) REFERENCES public.addons(addonid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.apartment_addon OWNER TO odadb_user;
GRANT ALL ON TABLE public.apartment_addon TO odadb_user;


-- public.apartment_addonperrequest definition

-- Drop table

-- DROP TABLE public.apartment_addonperrequest;

CREATE TABLE public.apartment_addonperrequest (
	apartmentid int4 NOT NULL,
	addperrequestid int4 NOT NULL,
	quantity int4 DEFAULT 1 NULL,
	CONSTRAINT apartment_addonperrequest_pkey PRIMARY KEY (apartmentid, addperrequestid),
	CONSTRAINT apartment_addon_apartmentid_fkey FOREIGN KEY (apartmentid) REFERENCES public.apartment(apartmentid) ON DELETE CASCADE,
	CONSTRAINT apartment_addonperrequest_addperrequestid_fkey FOREIGN KEY (addperrequestid) REFERENCES public.addperrequest(addperrequestid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.apartment_addonperrequest OWNER TO odadb_user;
GRANT ALL ON TABLE public.apartment_addonperrequest TO odadb_user;


-- public.automationdetails definition

-- Drop table

-- DROP TABLE public.automationdetails;

CREATE TABLE public.automationdetails (
	automationdetailsid serial4 NOT NULL,
	automationdetailsname varchar(255) NULL,
	automationid int4 NULL,
	description bool NOT NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	icon bytea NULL,
	CONSTRAINT automationdetails_pkey PRIMARY KEY (automationdetailsid),
	CONSTRAINT automationdetails_automationid_fkey FOREIGN KEY (automationid) REFERENCES public.automation(automationid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.automationdetails OWNER TO odadb_user;
GRANT ALL ON TABLE public.automationdetails TO odadb_user;


-- public.booking definition

-- Drop table

-- DROP TABLE public.booking;

CREATE TABLE public.booking (
	bookingid serial4 NOT NULL,
	customerid int4 NULL,
	apartmentid int4 NULL,
	paymentmethodid int4 NULL,
	paymentplanid int4 NULL,
	createdatetime timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	lastmodifieddatetime timestamp DEFAULT CURRENT_TIMESTAMP NOT NULL,
	bookingstatus text NULL,
	userid int4 NULL,
	totalamount numeric(10, 2) NOT NULL,
	CONSTRAINT booking_pkey PRIMARY KEY (bookingid),
	CONSTRAINT booking_apartmentid_fkey FOREIGN KEY (apartmentid) REFERENCES public.apartment(apartmentid) ON DELETE CASCADE,
	CONSTRAINT booking_customerid_fkey FOREIGN KEY (customerid) REFERENCES public.customer(customerid) ON DELETE CASCADE,
	CONSTRAINT booking_paymentmethodid_fkey FOREIGN KEY (paymentmethodid) REFERENCES public.paymentmethod(paymentmethodid) ON DELETE CASCADE,
	CONSTRAINT booking_paymentplanid_fkey FOREIGN KEY (paymentplanid) REFERENCES public.paymentplans(paymentplanid) ON DELETE CASCADE,
	CONSTRAINT booking_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(userid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.booking OWNER TO odadb_user;
GRANT ALL ON TABLE public.booking TO odadb_user;


-- public.installmentbreakdown definition

-- Drop table

-- DROP TABLE public.installmentbreakdown;

CREATE TABLE public.installmentbreakdown (
	breakdownid serial4 NOT NULL,
	paymentplanid int4 NULL,
	installmentmonth int4 NOT NULL,
	installmentpercentage numeric(10, 2) NOT NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	"�z)[��у��K�R�z" varchar(512) NULL,
	installmentmont int4 NULL,
	CONSTRAINT installmentbreakdown_pkey PRIMARY KEY (breakdownid),
	CONSTRAINT installmentbreakdown_paymentplanid_fkey FOREIGN KEY (paymentplanid) REFERENCES public.paymentplans(paymentplanid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.installmentbreakdown OWNER TO odadb_user;
GRANT ALL ON TABLE public.installmentbreakdown TO odadb_user;


-- public.plandetails definition

-- Drop table

-- DROP TABLE public.plandetails;

CREATE TABLE public.plandetails (
	plandetailsid serial4 NOT NULL,
	plandetailsname varchar(255) NULL,
	plandetailstype varchar(255) NULL,
	planid int4 NULL,
	description varchar(255) NULL,
	createdatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	stars int4 NULL,
	CONSTRAINT plandetails_pkey PRIMARY KEY (plandetailsid),
	CONSTRAINT plandetails_plan_plandetailsid_fkey FOREIGN KEY (planid) REFERENCES public.plan(planid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.plandetails OWNER TO odadb_user;
GRANT ALL ON TABLE public.plandetails TO odadb_user;


-- public.questions definition

-- Drop table

-- DROP TABLE public.questions;

CREATE TABLE public.questions (
	questionsid int4 NULL,
	questionname varchar(255) NULL,
	answer int4 NULL,
	bookingid int4 NULL,
	CONSTRAINT questions_bookingid_fkey FOREIGN KEY (bookingid) REFERENCES public.booking(bookingid) ON DELETE CASCADE
);

-- Permissions

ALTER TABLE public.questions OWNER TO odadb_user;
GRANT ALL ON TABLE public.questions TO odadb_user;








CREATE TABLE public.FaceLiftaddons (
	addonid serial4 NOT NULL,
	addonname varchar(255) NULL,
	addongroup varchar(50) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	brand text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	unitormeter varchar(255) NULL,
	CONSTRAINT addons_pkey PRIMARY KEY (addonid)
);

CREATE TABLE public.FaceLiftaddperrequest (
	addperrequestid serial4 NOT NULL,
	addperrequestname varchar(255) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT addperrequest_pkey PRIMARY KEY (addperrequestid)
);

CREATE TABLE public.FaceLiftRoom (
	roomid serial4 NOT NULL,
	roomtype varchar(255) NOT NULL,
	automationid int4 NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	bookingid int4 NULL,
	CONSTRAINT faceliftroom_pkey PRIMARY KEY (roomid),
	CONSTRAINT faceliftroom_automationid_fkey FOREIGN KEY (automationid) REFERENCES public.automation(automationid) ON DELETE CASCADE,
	CONSTRAINT faceliftroom_bookingid_fkey FOREIGN KEY (bookingid) REFERENCES public.booking(bookingid) ON DELETE CASCADE
)

CREATE Table public.FaceLiftRoom_addon (
	roomid int4 NOT NULL,
	addonid int4 NOT NULL,
	quantity int4 NOT NULL,
	CONSTRAINT faceliftroomaddon_pkey PRIMARY KEY (roomid, addonid),
	CONSTRAINT faceliftroomaddon_roomid_fkey FOREIGN KEY (roomid) REFERENCES public.FaceLiftRoom(roomid) ON DELETE CASCADE,
	CONSTRAINT faceliftroomaddon_addonid_fkey FOREIGN KEY (addonid) REFERENCES public.FaceLiftaddons(addonid) ON DELETE CASCADE
)

CREATE TABLE public.FaceLiftRoom_addonperrequest (
	roomid int4 NOT NULL,
	addperrequestid int4 NOT NULL,
	quantity int4 DEFAULT 1 NULL,
	CONSTRAINT room_addonperrequest_pkey PRIMARY KEY (roomid, addperrequestid),
	CONSTRAINT room_addon_apartmentid_fkey FOREIGN KEY (roomid) REFERENCES public.FaceLiftRoom(roomid) ON DELETE CASCADE,
	CONSTRAINT apartment_addonperrequest_addperrequestid_fkey FOREIGN KEY (addperrequestid) REFERENCES public.FaceLiftaddperrequest(addperrequestid) ON DELETE CASCADE
);