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
	displayorder int4 NULL,
	CONSTRAINT addons_pkey PRIMARY KEY (addonid)
);


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
	displayorder int4 NULL,
	CONSTRAINT addperrequest_pkey PRIMARY KEY (addperrequestid)
);


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


-- public.faceliftaddons definition

-- Drop table

-- DROP TABLE public.faceliftaddons;

CREATE TABLE public.faceliftaddons (
	addonid serial4 NOT NULL,
	addonname varchar(255) NULL,
	addongroup varchar(50) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	brand text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	unitormeter varchar(255) NULL,
	faceliftroomtype varchar(255) NOT NULL,
	displayorder int4 NULL,
	CONSTRAINT faceliftaddons_pkey PRIMARY KEY (addonid)
);


-- public.faceliftaddperrequest definition

-- Drop table

-- DROP TABLE public.faceliftaddperrequest;

CREATE TABLE public.faceliftaddperrequest (
	addperrequestid serial4 NOT NULL,
	addperrequestname varchar(255) NULL,
	price numeric(10, 2) NULL,
	description text NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	CONSTRAINT faceliftaddperrequest_pkey PRIMARY KEY (addperrequestid)
);


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


-- public.projecttype definition

-- Drop table

-- DROP TABLE public.projecttype;

CREATE TABLE public.projecttype (
	projecttypeid serial4 NOT NULL,
	projecttypedetail varchar(255) NOT NULL,
	projecttypename varchar(255) NOT NULL,
	CONSTRAINT projecttype_pkey PRIMARY KEY (projecttypeid)
);


-- public.questions definition

-- Drop table

-- DROP TABLE public.questions;

CREATE TABLE public.questions (
	questionid serial4 NOT NULL,
	questiontext text NOT NULL,
	createdat timestamp DEFAULT now() NULL,
	CONSTRAINT questions_pkey PRIMARY KEY (questionid)
);


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


-- public.unittype definition

-- Drop table

-- DROP TABLE public.unittype;

CREATE TABLE public.unittype (
	unittypeid serial4 NOT NULL,
	unittype_name varchar(50) NOT NULL,
	CONSTRAINT unittype_pkey PRIMARY KEY (unittypeid)
);


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


-- public.answers definition

-- Drop table

-- DROP TABLE public.answers;

CREATE TABLE public.answers (
	answerid serial4 NOT NULL,
	questionid int4 NULL,
	answertext text NOT NULL,
	answercode bpchar(1) NOT NULL,
	createdat timestamp DEFAULT now() NULL,
	answerphoto bytea NULL,
	CONSTRAINT answers_pkey PRIMARY KEY (answerid),
	CONSTRAINT answers_questionid_fkey FOREIGN KEY (questionid) REFERENCES public.questions(questionid) ON DELETE CASCADE
);


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


-- public.customeranswers definition

-- Drop table

-- DROP TABLE public.customeranswers;

CREATE TABLE public.customeranswers (
	customeranswerid serial4 NOT NULL,
	bookingid int4 NULL,
	questionid int4 NULL,
	answerid int4 NULL,
	createdat timestamp DEFAULT now() NULL,
	CONSTRAINT customeranswers_bookingid_questionid_key UNIQUE (bookingid, questionid),
	CONSTRAINT customeranswers_pkey PRIMARY KEY (customeranswerid),
	CONSTRAINT customeranswers_answerid_fkey FOREIGN KEY (answerid) REFERENCES public.answers(answerid) ON DELETE CASCADE,
	CONSTRAINT customeranswers_bookingid_fkey FOREIGN KEY (bookingid) REFERENCES public.booking(bookingid) ON DELETE CASCADE,
	CONSTRAINT customeranswers_questionid_fkey FOREIGN KEY (questionid) REFERENCES public.questions(questionid) ON DELETE CASCADE
);


-- public.faceliftroom definition

-- Drop table

-- DROP TABLE public.faceliftroom;

CREATE TABLE public.faceliftroom (
	roomid serial4 NOT NULL,
	roomtype varchar(255) NOT NULL,
	automationid int4 NULL,
	createddatetime timestamp NULL,
	lastmodifieddatetime timestamp NULL,
	bookingid int4 NULL,
	apartmentid int4 NULL,
	CONSTRAINT faceliftroom_pkey PRIMARY KEY (roomid),
	CONSTRAINT faceliftroom_apartmentid_fkey FOREIGN KEY (apartmentid) REFERENCES public.apartment(apartmentid) ON DELETE CASCADE,
	CONSTRAINT faceliftroom_automationid_fkey FOREIGN KEY (automationid) REFERENCES public.automation(automationid) ON DELETE CASCADE,
	CONSTRAINT faceliftroom_bookingid_fkey FOREIGN KEY (bookingid) REFERENCES public.booking(bookingid) ON DELETE CASCADE
);


-- public.faceliftroom_addon definition

-- Drop table

-- DROP TABLE public.faceliftroom_addon;

CREATE TABLE public.faceliftroom_addon (
	roomid int4 NOT NULL,
	addonid int4 NOT NULL,
	quantity int4 NOT NULL,
	CONSTRAINT faceliftroomaddon_pkey PRIMARY KEY (roomid, addonid),
	CONSTRAINT faceliftroomaddon_addonid_fkey FOREIGN KEY (addonid) REFERENCES public.faceliftaddons(addonid) ON DELETE CASCADE,
	CONSTRAINT faceliftroomaddon_roomid_fkey FOREIGN KEY (roomid) REFERENCES public.faceliftroom(roomid) ON DELETE CASCADE
);


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
	CONSTRAINT installmentbreakdown_pkey PRIMARY KEY (breakdownid),
	CONSTRAINT installmentbreakdown_paymentplanid_fkey FOREIGN KEY (paymentplanid) REFERENCES public.paymentplans(paymentplanid) ON DELETE CASCADE
);


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
	displayorder int4 NULL,
	CONSTRAINT plandetails_pkey PRIMARY KEY (plandetailsid),
	CONSTRAINT plandetails_plan_plandetailsid_fkey FOREIGN KEY (planid) REFERENCES public.plan(planid) ON DELETE CASCADE
);