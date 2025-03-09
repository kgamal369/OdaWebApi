INSERT INTO public.addons (addonname,addongroup,price,description,brand,createddatetime,lastmodifieddatetime,unitormeter) VALUES
	 ('E. Heaters/ 50L','Heaters',14000.00,'Heaters Supply & Installtion  , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('Shower Tempered Glass','Shower',12500.00,'Fixed Modules supply and Installation',NULL,NULL,NULL,'Unit'),
	 ('Boilers /200L','Boilers',115000.00,'Boilers Suppy & Installation',NULL,NULL,NULL,'Unit'),
	 ('4hp','AirConditioning',81000.00,'AC Supply & Installation , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('1.5hp','AirConditioning',30900.00,'AC Supply & Installation , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('2.25hp','AirConditioning',40500.00,'AC Supply & Installation , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('3hp','AirConditioning',48500.00,'AC Supply & Installation , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('5hp','AirConditioning',89600.00,'AC Supply & Installation , Service Doors Installation',NULL,NULL,NULL,'Unit'),
	 ('Interior Design','Interior Design',250.00,'Add an interior designer to your chosen plan for an extra fee',NULL,NULL,NULL,'Meter'),
	 ('Solar Heating','SolarHeating',1200.00,'Reduce your dependence on non-renewable energy and significantly reduce energy bills . High Quality Equipment . Effciency Guaranteed ',NULL,NULL,NULL,'Meter');
INSERT INTO public.addperrequest (addperrequestname,price,description,createddatetime,lastmodifieddatetime) VALUES
	 ('Kitchen Fit-Outs',NULL,'Cabinet and Counters Supply and Installtion',NULL,NULL),
	 ('WoodWorks',NULL,'Supply and Installation',NULL,NULL),
	 ('Wardrobs',NULL,'Supply and Installation',NULL,NULL),
	 ('Shutters',NULL,'Shutters supply and Installation',NULL,NULL),
	 ('Aluminum',NULL,'Supply and Installation',NULL,NULL),
	 ('Wood Cladding ',NULL,'Supply and Installation',NULL,NULL),
	 ('Garden Landscape ',NULL,'(Minimum 100 m2)  -1st Degree Grass  -Irrigation System Network & Controls - Outdoor Plumbing Foundation ',NULL,NULL);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 ('aa','Project','ForSale',65.00,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 ('bb','Project','ForSale',72.00,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 ('cc','Project','InProgress',84.00,NULL,NULL,1,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL),
	 ('dd','Project','InProgress',110.00,NULL,NULL,2,NULL,NULL,NULL,NULL,2,1,NULL,NULL,NULL),
	 ('ee','Project','ForSale',120.00,NULL,NULL,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 ('ff','Project','ForSale',115.00,NULL,NULL,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 ('gg','Project','ForSale',140.00,NULL,NULL,3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 ('ii','Kit','InProgress',136.00,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 13:10:13.720584','2025-02-17 13:10:13.720656',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 13:26:58.677371','2025-02-17 13:26:58.677462',1,1,NULL,NULL,NULL);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 13:38:28.895764','2025-02-17 13:38:28.895855',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 13:52:23.239945','2025-02-17 13:52:23.240037',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 16:54:02.833603','2025-02-17 16:54:02.833636',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',140.00,NULL,NULL,3,NULL,NULL,'2025-02-17 22:09:30.674293','2025-02-17 22:09:30.674345',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 22:22:41.933402','2025-02-17 22:22:41.93343',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-18 10:59:48.902216','2025-02-18 10:59:48.902251',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',140.00,NULL,NULL,3,NULL,NULL,'2025-02-18 22:25:07.314346','2025-02-18 22:25:07.314411',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',140.00,NULL,NULL,3,NULL,NULL,'2025-02-18 22:57:50.809268','2025-02-18 22:57:50.809364',1,1,NULL,NULL,NULL),
	 (NULL,'Project','ForSale',148.00,NULL,NULL,NULL,NULL,NULL,'2025-03-06 21:14:51.017374','2025-03-06 21:14:51.017375',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',148.00,'',NULL,NULL,NULL,NULL,'2025-03-06 21:17:30.441306','2025-03-06 21:17:30.441307',5,1,'',NULL,NULL);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-18 23:26:12.537467','2025-02-19 09:48:37.49948',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',140.00,NULL,NULL,3,NULL,NULL,'2025-02-19 10:20:53.309361','2025-02-19 10:20:53.309394',1,1,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',148.00,'',NULL,NULL,NULL,NULL,'2025-03-06 21:18:03.389309','2025-03-06 21:18:03.38931',5,1,'',NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-19 20:13:34.540804','2025-02-19 21:49:19.159016',2,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',140.00,NULL,NULL,3,NULL,NULL,'2025-02-19 22:10:51.419895','2025-02-19 22:10:51.419919',1,1,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-07 11:25:34.722608','2025-03-07 11:28:33.692916',1,2,NULL,1,5),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-07 11:36:21.085967','2025-03-07 11:36:21.085969',1,2,NULL,1,3),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-17 13:59:01.948017','2025-02-20 09:51:43.169281',1,1,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',160.00,' maddi egypt',NULL,NULL,NULL,NULL,'2025-02-20 11:44:04.897201','2025-02-20 11:44:04.897226',1,1,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',128.00,'Egypt maadi',NULL,NULL,NULL,NULL,'2025-02-20 20:58:32.845079','2025-02-20 20:58:32.84512',1,1,NULL,NULL,NULL);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-20 21:31:11.861614','2025-02-20 21:31:11.86165',1,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',65.00,NULL,NULL,1,NULL,NULL,'2025-02-20 21:33:14.560017','2025-02-20 21:36:43.449213',1,1,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',250.00,'Cairo Alex Desert Road',NULL,NULL,NULL,NULL,'2025-02-21 15:56:31.807537','2025-02-21 15:59:09.799153',1,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-07 11:59:13.414332','2025-03-07 12:01:03.069499',1,1,NULL,1,4),
	 (NULL,'Project','ForSale',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-07 12:03:40.926627','2025-03-07 12:03:40.926628',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',160.00,NULL,NULL,NULL,NULL,NULL,'2025-02-21 18:55:12.808942','2025-02-21 18:57:02.186822',1,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',120.00,NULL,NULL,2,NULL,NULL,'2025-02-21 19:12:46.828','2025-02-21 19:16:51.722612',2,2,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-07 16:05:55.671429','2025-03-07 16:06:40.578071',1,1,NULL,1,4),
	 (NULL,'Project','InProgress',120.00,NULL,NULL,2,NULL,NULL,'2025-02-21 19:12:53.817569','2025-02-21 19:18:40.657359',1,2,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-08 10:54:50.89026','2025-03-08 10:54:50.89026',1,1,NULL,1,4);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 (NULL,'Kit','InProgress',120.00,'Cairo Alex Desert Road',NULL,NULL,NULL,NULL,'2025-03-03 10:37:25.814629','2025-03-03 10:39:27.374229',1,NULL,'GeIra',NULL,NULL),
	 (NULL,'Project','InProgress',130.00,NULL,NULL,NULL,NULL,NULL,'2025-03-04 12:24:25.30341','2025-03-04 12:24:25.303417',1,NULL,NULL,NULL,1),
	 (NULL,'Project','InProgress',500.00,NULL,NULL,NULL,NULL,NULL,'2025-03-08 10:58:00.157457','2025-03-08 10:58:00.157457',1,1,NULL,1,4),
	 (NULL,'Project','InProgress',122.00,NULL,NULL,NULL,NULL,NULL,'2025-03-04 12:27:26.312054','2025-03-04 13:41:44.944957',1,NULL,NULL,NULL,2),
	 (NULL,'Project','InProgress',565.00,NULL,NULL,NULL,NULL,NULL,'2025-03-04 13:49:25.309617','2025-03-04 13:51:30.105127',2,NULL,NULL,1,1),
	 (NULL,'Project','InProgress',565.00,NULL,NULL,NULL,NULL,NULL,'2025-03-04 14:01:37.848335','2025-03-04 14:01:37.848407',2,NULL,NULL,6,1),
	 (NULL,'Project','InProgress',565.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 04:32:04.359748','2025-03-05 04:32:04.359844',2,NULL,NULL,6,1),
	 (NULL,'Project','ForSale',565.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 04:53:38.689427','2025-03-05 04:53:38.68948',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Kit','InProgress',149.00,'zayed',NULL,NULL,NULL,NULL,'2025-03-05 15:06:54.891766','2025-03-05 15:06:54.891801',4,1,'zayed',NULL,NULL),
	 (NULL,'Kit','InProgress',149.00,'zayed',NULL,NULL,NULL,NULL,'2025-03-05 15:07:54.085126','2025-03-05 15:07:54.085127',4,1,'zayed',NULL,NULL);
INSERT INTO public.apartment (apartmentname,apartmenttype,apartmentstatus,apartmentspace,description,apartmentphotos,projectid,floornumber,availabilitydate,createddatetime,lastmodifieddatetime,planid,automationid,apartmentaddress,developerid,unittypeid) VALUES
	 (NULL,'Project','ForSale',87.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 15:36:32.088079','2025-03-05 15:36:32.088079',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',505.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 16:50:35.08861','2025-03-05 16:50:35.088634',1,1,NULL,3,4),
	 (NULL,'Project','ForSale',505.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 16:51:05.657326','2025-03-05 16:51:05.657328',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','ForSale',505.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 19:45:08.540016','2025-03-05 19:45:08.540106',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','ForSale',87.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 20:04:52.918026','2025-03-05 20:04:52.91808',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','ForSale',565.00,NULL,NULL,NULL,NULL,NULL,'2025-03-05 20:07:20.629388','2025-03-05 20:07:20.629389',NULL,NULL,NULL,NULL,NULL),
	 (NULL,'Project','InProgress',230.00,NULL,NULL,NULL,NULL,NULL,'2025-03-08 19:37:05.295411','2025-03-08 19:37:05.295411',1,1,NULL,6,4),
	 (NULL,'Project','InProgress',87.00,NULL,NULL,NULL,NULL,NULL,'2025-03-04 14:15:39.065336','2025-03-06 00:53:56.615317',1,1,NULL,6,2),
	 (NULL,'Project','InProgress',148.00,NULL,NULL,NULL,NULL,NULL,'2025-03-06 21:12:19.634731','2025-03-06 21:12:19.635451',2,NULL,NULL,2,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (13,3,1),
	 (13,9,1),
	 (13,10,1),
	 (13,11,3),
	 (14,10,1),
	 (15,3,1),
	 (15,9,1),
	 (15,10,1),
	 (15,11,1),
	 (15,12,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (16,1,1),
	 (16,9,3),
	 (16,10,1),
	 (16,11,2),
	 (17,7,0),
	 (17,9,1),
	 (17,10,1),
	 (18,3,1),
	 (18,9,1),
	 (18,10,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (18,11,1),
	 (18,12,2),
	 (19,3,1),
	 (19,9,1),
	 (19,10,1),
	 (19,11,1),
	 (19,12,2),
	 (20,9,1),
	 (20,12,3),
	 (20,2,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (20,10,2),
	 (21,7,1),
	 (21,10,1),
	 (1,7,1),
	 (1,9,1),
	 (1,10,1),
	 (1,11,3),
	 (22,7,1),
	 (22,9,1),
	 (23,3,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (23,9,1),
	 (51,1,1),
	 (51,2,1),
	 (51,4,1),
	 (24,3,1),
	 (26,10,1),
	 (27,10,1),
	 (51,7,1),
	 (28,7,1),
	 (28,9,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (29,7,1),
	 (51,9,2),
	 (51,10,1),
	 (51,11,3),
	 (30,1,3),
	 (30,2,2),
	 (30,7,1),
	 (30,9,3),
	 (30,10,5),
	 (30,11,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (30,12,3),
	 (31,10,5),
	 (31,11,4),
	 (13,2,1),
	 (14,1,0),
	 (15,7,0),
	 (16,3,0),
	 (17,2,0),
	 (18,7,0),
	 (19,1,0);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (20,7,1),
	 (1,3,1),
	 (22,3,1),
	 (23,2,0),
	 (24,2,1),
	 (28,1,1),
	 (33,4,1),
	 (33,7,1),
	 (33,9,1),
	 (34,4,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (35,3,1),
	 (35,4,1),
	 (36,3,1),
	 (36,4,1),
	 (38,3,1),
	 (38,4,1),
	 (40,1,2),
	 (40,2,2),
	 (40,4,1),
	 (40,9,3);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (40,10,3),
	 (40,11,2),
	 (40,12,3),
	 (40,13,3),
	 (41,1,2),
	 (41,2,2),
	 (41,4,1),
	 (41,9,3),
	 (41,10,3),
	 (41,11,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (41,12,3),
	 (41,13,3),
	 (43,9,1),
	 (43,10,1),
	 (37,2,2),
	 (37,3,1),
	 (37,13,1),
	 (37,9,4),
	 (50,1,1),
	 (50,2,1);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (50,4,1),
	 (50,7,1),
	 (50,9,2),
	 (50,10,1),
	 (50,11,3),
	 (50,12,3),
	 (50,13,3),
	 (51,12,3),
	 (51,13,3),
	 (52,1,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (52,2,2),
	 (52,4,1),
	 (52,7,1),
	 (52,10,5),
	 (52,11,2),
	 (52,12,2),
	 (52,13,2),
	 (57,4,1),
	 (57,7,1),
	 (52,9,4);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (53,1,2),
	 (53,2,2),
	 (53,4,1),
	 (53,7,1),
	 (53,9,4),
	 (53,10,5),
	 (53,11,2),
	 (53,12,2),
	 (53,13,2),
	 (54,1,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (54,2,2),
	 (54,4,1),
	 (54,7,1),
	 (54,9,4),
	 (54,12,2),
	 (54,13,2),
	 (57,9,1),
	 (54,10,5),
	 (57,10,1),
	 (54,11,2);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (56,1,2),
	 (56,2,2),
	 (56,9,4),
	 (56,10,5),
	 (56,12,2),
	 (56,13,2),
	 (58,1,2),
	 (56,11,2),
	 (58,2,2),
	 (58,9,5);
INSERT INTO public.apartment_addon (apartmentid,addonid,quantity) VALUES
	 (58,10,4),
	 (58,11,2),
	 (58,12,2),
	 (58,13,2),
	 (59,3,1),
	 (59,4,1),
	 (59,9,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (13,1,1),
	 (13,2,1),
	 (14,1,1),
	 (14,3,1),
	 (15,2,1),
	 (15,3,1),
	 (16,1,1),
	 (16,4,1),
	 (18,2,1),
	 (18,3,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (19,2,1),
	 (19,3,1),
	 (20,2,1),
	 (20,3,1),
	 (1,1,1),
	 (1,2,1),
	 (1,3,1),
	 (22,1,1),
	 (22,3,1),
	 (24,1,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (25,4,1),
	 (28,1,1),
	 (28,2,1),
	 (28,3,1),
	 (28,4,1),
	 (29,1,1),
	 (29,2,1),
	 (30,1,1),
	 (30,2,1),
	 (30,3,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (30,4,1),
	 (33,1,1),
	 (33,2,1),
	 (34,1,1),
	 (40,1,1),
	 (40,2,1),
	 (40,3,1),
	 (40,4,1),
	 (40,5,1),
	 (40,6,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (40,7,1),
	 (41,1,1),
	 (41,2,1),
	 (41,3,1),
	 (41,4,1),
	 (41,5,1),
	 (41,6,1),
	 (41,7,1),
	 (43,1,1),
	 (43,2,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (37,3,1),
	 (50,1,1),
	 (50,2,1),
	 (50,3,1),
	 (50,4,1),
	 (50,5,1),
	 (50,6,1),
	 (50,7,1),
	 (51,1,1),
	 (51,2,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (51,3,1),
	 (51,4,1),
	 (51,5,1),
	 (51,6,1),
	 (51,7,1),
	 (52,1,1),
	 (52,2,1),
	 (52,3,1),
	 (52,4,1),
	 (52,5,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (52,6,1),
	 (52,7,1),
	 (53,1,1),
	 (53,2,1),
	 (53,3,1),
	 (53,4,1),
	 (53,5,1),
	 (53,6,1),
	 (53,7,1),
	 (54,1,1);
INSERT INTO public.apartment_addonperrequest (apartmentid,addperrequestid,quantity) VALUES
	 (56,1,1),
	 (56,3,1),
	 (57,1,1),
	 (58,3,1),
	 (58,6,1),
	 (59,1,1);
INSERT INTO public.automation (automationname,description,createdatetime,lastmodifieddatetime) VALUES
	 ('Basic',' Basic',NULL,NULL),
	 ('Advanced','Advanced',NULL,NULL);
INSERT INTO public.automationdetails (automationdetailsname,automationid,description,createdatetime,lastmodifieddatetime,icon) VALUES
	 ('Front Door Smart Lock',1,true,NULL,NULL,NULL),
	 ('Front Door Smart Lock',2,true,NULL,NULL,NULL),
	 ('CCTV',2,true,NULL,NULL,NULL),
	 ('Intercom',1,true,NULL,NULL,NULL),
	 ('Intercom',2,true,NULL,NULL,NULL),
	 ('Smart Lighting',1,false,NULL,NULL,NULL),
	 ('Smart Lighting',2,true,NULL,NULL,NULL),
	 ('Shutters\Curtains',1,false,NULL,NULL,NULL),
	 ('Shutters\Curtains',2,true,NULL,NULL,NULL),
	 ('Home Control Panel',2,true,NULL,NULL,NULL);
INSERT INTO public.automationdetails (automationdetailsname,automationid,description,createdatetime,lastmodifieddatetime,icon) VALUES
	 ('CCTV',1,true,NULL,NULL,NULL),
	 ('Home Control Panel',1,false,NULL,NULL,NULL);
INSERT INTO public.booking (customerid,apartmentid,paymentmethodid,paymentplanid,createdatetime,lastmodifieddatetime,bookingstatus,userid,totalamount) VALUES
	 (3,22,NULL,9,'2025-02-19 20:13:35.933806','2025-02-19 23:43:18.524559','InProgress',NULL,665250.00),
	 (6,24,NULL,2,'2025-02-20 11:44:06.724653','2025-02-20 11:52:31.864777','InProgress',NULL,1470500.00),
	 (1,14,NULL,1,'2025-02-17 16:54:03.935888','2025-02-20 11:55:03.516548','InProgress',NULL,614250.00),
	 (1,25,NULL,2,'2025-02-20 20:58:34.539385','2025-02-20 20:59:51.323617','InProgress',NULL,1720600.00),
	 (8,28,NULL,9,'2025-02-21 15:56:33.505422','2025-02-21 15:59:48.778139','InProgress',NULL,2308500.00),
	 (1,16,NULL,5,'2025-02-17 22:22:43.22812','2025-02-17 22:22:43.228135','InProgress',NULL,812650.00),
	 (7,29,NULL,2,'2025-02-21 18:55:14.806022','2025-02-21 18:57:15.064953','InProgress',NULL,1802000.00),
	 (8,30,NULL,5,'2025-02-21 19:12:46.961693','2025-02-21 19:21:54.316377','InProgress',NULL,2143700.00),
	 (9,31,NULL,5,'2025-02-21 19:12:54.171113','2025-02-21 19:21:56.218393','InProgress',NULL,1360600.00),
	 (3,20,NULL,2,'2025-02-18 23:26:13.999675','2025-02-19 09:48:36.882718','InProgress',NULL,1054750.00);
INSERT INTO public.booking (customerid,apartmentid,paymentmethodid,paymentplanid,createdatetime,lastmodifieddatetime,bookingstatus,userid,totalamount) VALUES
	 (4,21,NULL,2,'2025-02-19 10:20:53.799151','2025-02-19 10:20:53.799186','InProgress',NULL,1343500.00),
	 (1,13,NULL,1,'2025-02-17 13:59:02.561101','2025-02-20 09:51:42.668804','InProgress',NULL,848950.00),
	 (9,43,NULL,10,'2025-03-05 16:50:35.467345','2025-03-05 19:45:08.530518','Confirmed',NULL,4066500.00),
	 (8,40,NULL,9,'2025-03-05 15:06:55.335096','2025-03-05 20:05:33.69565','Confirmed',NULL,2070850.00),
	 (1,38,NULL,9,'2025-03-05 04:32:06.486516','2025-03-05 20:07:20.628929','Confirmed',NULL,5341250.00),
	 (1,37,NULL,2,'2025-03-04 14:15:39.197943','2025-03-06 00:53:56.42681','Confirmed',NULL,1039000.00),
	 (10,48,NULL,9,'2025-03-06 21:12:19.922217','2025-03-06 21:14:51.017315','Confirmed',NULL,1332000.00),
	 (10,50,NULL,9,'2025-03-06 21:17:30.574952','2025-03-06 21:17:30.574952','InProgress',NULL,2247500.00),
	 (10,51,NULL,10,'2025-03-06 21:18:03.552203','2025-03-06 21:18:03.552204','InProgress',NULL,2247500.00),
	 (10,52,NULL,13,'2025-03-07 11:25:35.04187','2025-03-07 11:28:33.228847','InProgress',NULL,5426000.00);
INSERT INTO public.booking (customerid,apartmentid,paymentmethodid,paymentplanid,createdatetime,lastmodifieddatetime,bookingstatus,userid,totalamount) VALUES
	 (10,53,NULL,13,'2025-03-07 11:36:21.215838','2025-03-07 11:36:21.215839','InProgress',NULL,5426000.00),
	 (8,32,NULL,9,'2025-03-03 10:37:26.384891','2025-03-03 10:39:27.245842','InProgress',NULL,960000.00),
	 (1,33,NULL,11,'2025-03-04 12:24:25.433824','2025-03-04 12:24:25.433825','InProgress',NULL,1241000.00),
	 (10,54,NULL,13,'2025-03-07 11:59:13.552768','2025-03-07 12:03:40.926265','Confirmed',NULL,5426000.00),
	 (1,35,NULL,9,'2025-03-04 13:49:25.476856','2025-03-04 13:51:29.917558','InProgress',NULL,5341250.00),
	 (1,36,NULL,9,'2025-03-04 14:01:40.402379','2025-03-04 14:01:40.402423','InProgress',NULL,5341250.00),
	 (8,41,NULL,9,'2025-03-05 15:07:54.211721','2025-03-05 15:07:54.211722','InProgress',NULL,2070850.00),
	 (11,56,NULL,13,'2025-03-07 16:05:55.905245','2025-03-07 16:06:40.18553','InProgress',NULL,4701000.00),
	 (11,57,NULL,13,'2025-03-08 10:54:51.009076','2025-03-08 10:54:51.009077','InProgress',NULL,4751500.00),
	 (11,58,NULL,13,'2025-03-08 10:58:00.260635','2025-03-08 10:58:00.260636','InProgress',NULL,4699500.00);
INSERT INTO public.booking (customerid,apartmentid,paymentmethodid,paymentplanid,createdatetime,lastmodifieddatetime,bookingstatus,userid,totalamount) VALUES
	 (11,59,NULL,12,'2025-03-08 19:37:05.415648','2025-03-08 19:37:05.415648','InProgress',NULL,2025000.00);
INSERT INTO public.contactus (firstname,lastname,phonenumber,email,"comments") VALUES
	 ('Karim','Gamal','012010011','Karim@Gamal.com','test'),
	 ('hawary','','0159633358','mo@sd.com','offfffffffffffffffffffffffffffffffff'),
	 ('Amr ','','01002175607','ahayaly@gmail.com','');
INSERT INTO public.customer (firstname,lastname,email,phonenumber,address,createdatetime,lastmodifieddatetime) VALUES
	 ('Hawary',NULL,'mohamed.k.elhawary@gmail.com','01232322223',NULL,'2025-02-17 22:09:32.236026','2025-02-17 22:09:32.236027'),
	 ('hawary',NULL,'mo@gmail.com','01221213299',NULL,'2025-02-19 10:20:53.709678','2025-02-19 10:20:53.709724'),
	 ('hawary 19/2',NULL,'hawary114@gmail.com','0121323232',NULL,'2025-02-18 23:26:13.769245','2025-02-19 21:49:19.159178'),
	 ('Hawary',NULL,'moh@sd.com','01023282828',NULL,'2025-02-19 22:10:52.651082','2025-02-19 22:10:52.651118'),
	 ('hawary',NULL,'hawary@gmail.com','01231232443',' maddi egypt','2025-02-20 11:44:06.434891','2025-02-20 11:44:06.434927'),
	 ('Amr',NULL,'amr@gmail.com','01019292272',NULL,'2025-02-20 21:31:13.087977','2025-02-21 18:57:02.186825'),
	 ('Amr Fayssal',NULL,'eng.karim.gamal369@gmail.com','+201002175607','Cairo Alex Desert Road','2025-02-21 15:56:33.24484','2025-03-03 10:39:27.374232'),
	 ('Ahmed Shawky',NULL,'ahmedshawky985@gmail.com','01003344407',NULL,'2025-02-21 19:12:53.948132','2025-02-21 19:18:40.657364'),
	 ('hawary',NULL,'mo@sd.com','01425525222',NULL,'2025-02-17 13:27:00.541993','2025-03-06 00:53:56.615319'),
	 ('Amr Faisel',NULL,'ahayaly@gmail.com','01002175607',NULL,'2025-03-06 21:12:19.824646','2025-03-07 12:01:03.069521');
INSERT INTO public.customer (firstname,lastname,email,phonenumber,address,createdatetime,lastmodifieddatetime) VALUES
	 ('Ahmed Shawky',NULL,'ashawkyh@gmail.com','01003344407',NULL,'2025-03-07 16:05:55.806833','2025-03-07 16:06:40.578073');
INSERT INTO public.developer (developername,description,developerlogo,createdatetime,lastmodifieddatetime) VALUES
	 ('Dorra',NULL,'{}',NULL,NULL),
	 ('Somabay',NULL,'{}',NULL,NULL),
	 ('El Gouna',NULL,'{}',NULL,NULL),
	 ('Palm Hills',NULL,'{}',NULL,NULL),
	 ('Inertia',NULL,'{}',NULL,NULL),
	 ('Cityview',NULL,'{}',NULL,NULL),
	 ('Marakez',NULL,'{}',NULL,NULL),
	 ('Newgiza',NULL,'{}',NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (1,2,30.00,NULL,NULL,NULL,NULL),
	 (1,3,10.00,NULL,NULL,NULL,NULL),
	 (2,2,15.00,NULL,NULL,NULL,NULL),
	 (2,3,15.00,NULL,NULL,NULL,NULL),
	 (2,4,15.00,NULL,NULL,NULL,NULL),
	 (2,5,10.00,NULL,NULL,NULL,NULL),
	 (2,6,5.00,NULL,NULL,NULL,NULL),
	 (4,1,8.33,NULL,NULL,NULL,NULL),
	 (4,2,8.33,NULL,NULL,NULL,NULL),
	 (4,3,8.33,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (4,4,8.33,NULL,NULL,NULL,NULL),
	 (4,5,8.33,NULL,NULL,NULL,NULL),
	 (4,6,8.33,NULL,NULL,NULL,NULL),
	 (4,7,8.33,NULL,NULL,NULL,NULL),
	 (4,8,8.33,NULL,NULL,NULL,NULL),
	 (4,9,8.33,NULL,NULL,NULL,NULL),
	 (4,10,8.33,NULL,NULL,NULL,NULL),
	 (4,11,8.33,NULL,NULL,NULL,NULL),
	 (4,12,8.33,NULL,NULL,NULL,NULL),
	 (5,1,5.56,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (5,2,5.56,NULL,NULL,NULL,NULL),
	 (5,3,5.56,NULL,NULL,NULL,NULL),
	 (5,4,5.56,NULL,NULL,NULL,NULL),
	 (5,5,5.56,NULL,NULL,NULL,NULL),
	 (5,6,5.56,NULL,NULL,NULL,NULL),
	 (5,7,5.56,NULL,NULL,NULL,NULL),
	 (5,8,5.56,NULL,NULL,NULL,NULL),
	 (5,9,5.56,NULL,NULL,NULL,NULL),
	 (5,10,5.56,NULL,NULL,NULL,NULL),
	 (5,11,5.56,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (5,12,5.56,NULL,NULL,NULL,NULL),
	 (8,1,8.33,NULL,NULL,NULL,NULL),
	 (8,2,8.33,NULL,NULL,NULL,NULL),
	 (8,3,8.33,NULL,NULL,NULL,NULL),
	 (8,4,8.33,NULL,NULL,NULL,NULL),
	 (8,5,8.33,NULL,NULL,NULL,NULL),
	 (8,6,8.33,NULL,NULL,NULL,NULL),
	 (8,7,8.33,NULL,NULL,NULL,NULL),
	 (8,8,8.33,NULL,NULL,NULL,NULL),
	 (8,9,8.33,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (8,10,8.33,NULL,NULL,NULL,NULL),
	 (8,11,8.33,NULL,NULL,NULL,NULL),
	 (8,12,8.33,NULL,NULL,NULL,NULL),
	 (9,1,5.56,NULL,NULL,NULL,NULL),
	 (9,2,5.56,NULL,NULL,NULL,NULL),
	 (9,3,5.56,NULL,NULL,NULL,NULL),
	 (9,4,5.56,NULL,NULL,NULL,NULL),
	 (9,5,5.56,NULL,NULL,NULL,NULL),
	 (9,6,5.56,NULL,NULL,NULL,NULL),
	 (9,7,5.56,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (9,8,5.56,NULL,NULL,NULL,NULL),
	 (9,9,5.56,NULL,NULL,NULL,NULL),
	 (9,10,5.56,NULL,NULL,NULL,NULL),
	 (9,11,5.56,NULL,NULL,NULL,NULL),
	 (9,12,5.56,NULL,NULL,NULL,NULL),
	 (9,13,5.56,NULL,NULL,NULL,NULL),
	 (9,14,5.56,NULL,NULL,NULL,NULL),
	 (9,15,5.56,NULL,NULL,NULL,NULL),
	 (9,16,5.56,NULL,NULL,NULL,NULL),
	 (9,17,5.56,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (9,18,5.56,NULL,NULL,NULL,NULL),
	 (10,1,4.17,NULL,NULL,NULL,NULL),
	 (10,2,4.17,NULL,NULL,NULL,NULL),
	 (10,3,4.17,NULL,NULL,NULL,NULL),
	 (10,4,4.17,NULL,NULL,NULL,NULL),
	 (10,5,4.17,NULL,NULL,NULL,NULL),
	 (10,6,4.17,NULL,NULL,NULL,NULL),
	 (10,7,4.17,NULL,NULL,NULL,NULL),
	 (10,8,4.17,NULL,NULL,NULL,NULL),
	 (10,9,4.17,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (10,10,4.17,NULL,NULL,NULL,NULL),
	 (10,11,4.17,NULL,NULL,NULL,NULL),
	 (10,12,4.17,NULL,NULL,NULL,NULL),
	 (10,13,4.17,NULL,NULL,NULL,NULL),
	 (10,14,4.17,NULL,NULL,NULL,NULL),
	 (10,15,4.17,NULL,NULL,NULL,NULL),
	 (10,16,4.17,NULL,NULL,NULL,NULL),
	 (10,17,4.17,NULL,NULL,NULL,NULL),
	 (10,18,4.17,NULL,NULL,NULL,NULL),
	 (10,19,4.17,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (10,20,4.17,NULL,NULL,NULL,NULL),
	 (10,21,4.17,NULL,NULL,NULL,NULL),
	 (10,22,4.17,NULL,NULL,NULL,NULL),
	 (10,23,4.17,NULL,NULL,NULL,NULL),
	 (10,24,4.17,NULL,NULL,NULL,NULL),
	 (11,1,2.78,NULL,NULL,NULL,NULL),
	 (11,2,2.78,NULL,NULL,NULL,NULL),
	 (11,3,2.78,NULL,NULL,NULL,NULL),
	 (11,4,2.78,NULL,NULL,NULL,NULL),
	 (11,5,2.78,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (11,6,2.78,NULL,NULL,NULL,NULL),
	 (11,7,2.78,NULL,NULL,NULL,NULL),
	 (11,8,2.78,NULL,NULL,NULL,NULL),
	 (11,9,2.78,NULL,NULL,NULL,NULL),
	 (11,10,2.78,NULL,NULL,NULL,NULL),
	 (11,11,2.78,NULL,NULL,NULL,NULL),
	 (11,12,2.78,NULL,NULL,NULL,NULL),
	 (11,13,2.78,NULL,NULL,NULL,NULL),
	 (11,14,2.78,NULL,NULL,NULL,NULL),
	 (11,15,2.78,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (11,16,2.78,NULL,NULL,NULL,NULL),
	 (11,17,2.78,NULL,NULL,NULL,NULL),
	 (11,18,2.78,NULL,NULL,NULL,NULL),
	 (11,19,2.78,NULL,NULL,NULL,NULL),
	 (11,20,2.78,NULL,NULL,NULL,NULL),
	 (11,21,2.78,NULL,NULL,NULL,NULL),
	 (11,22,2.78,NULL,NULL,NULL,NULL),
	 (11,23,2.78,NULL,NULL,NULL,NULL),
	 (11,24,2.78,NULL,NULL,NULL,NULL),
	 (11,25,2.78,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (11,26,2.78,NULL,NULL,NULL,NULL),
	 (11,27,2.78,NULL,NULL,NULL,NULL),
	 (11,28,2.78,NULL,NULL,NULL,NULL),
	 (11,29,2.78,NULL,NULL,NULL,NULL),
	 (11,30,2.78,NULL,NULL,NULL,NULL),
	 (11,31,2.78,NULL,NULL,NULL,NULL),
	 (11,32,2.78,NULL,NULL,NULL,NULL),
	 (11,33,2.78,NULL,NULL,NULL,NULL),
	 (11,34,2.78,NULL,NULL,NULL,NULL),
	 (11,35,2.78,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (11,36,2.78,NULL,NULL,NULL,NULL),
	 (12,1,2.08,NULL,NULL,NULL,NULL),
	 (12,2,2.08,NULL,NULL,NULL,NULL),
	 (12,3,2.08,NULL,NULL,NULL,NULL),
	 (12,4,2.08,NULL,NULL,NULL,NULL),
	 (12,5,2.08,NULL,NULL,NULL,NULL),
	 (12,6,2.08,NULL,NULL,NULL,NULL),
	 (12,7,2.08,NULL,NULL,NULL,NULL),
	 (12,8,2.08,NULL,NULL,NULL,NULL),
	 (12,9,2.08,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (12,10,2.08,NULL,NULL,NULL,NULL),
	 (12,11,2.08,NULL,NULL,NULL,NULL),
	 (12,12,2.08,NULL,NULL,NULL,NULL),
	 (12,13,2.08,NULL,NULL,NULL,NULL),
	 (12,14,2.08,NULL,NULL,NULL,NULL),
	 (12,15,2.08,NULL,NULL,NULL,NULL),
	 (12,16,2.08,NULL,NULL,NULL,NULL),
	 (12,17,2.08,NULL,NULL,NULL,NULL),
	 (12,18,2.08,NULL,NULL,NULL,NULL),
	 (12,19,2.08,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (12,20,2.08,NULL,NULL,NULL,NULL),
	 (12,21,2.08,NULL,NULL,NULL,NULL),
	 (12,22,2.08,NULL,NULL,NULL,NULL),
	 (12,23,2.08,NULL,NULL,NULL,NULL),
	 (12,24,2.08,NULL,NULL,NULL,NULL),
	 (12,25,2.08,NULL,NULL,NULL,NULL),
	 (12,26,2.08,NULL,NULL,NULL,NULL),
	 (12,27,2.08,NULL,NULL,NULL,NULL),
	 (12,28,2.08,NULL,NULL,NULL,NULL),
	 (12,29,2.08,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (12,30,2.08,NULL,NULL,NULL,NULL),
	 (12,31,2.08,NULL,NULL,NULL,NULL),
	 (12,32,2.08,NULL,NULL,NULL,NULL),
	 (12,33,2.08,NULL,NULL,NULL,NULL),
	 (12,34,2.08,NULL,NULL,NULL,NULL),
	 (12,35,2.08,NULL,NULL,NULL,NULL),
	 (12,36,2.08,NULL,NULL,NULL,NULL),
	 (12,37,2.08,NULL,NULL,NULL,NULL),
	 (12,38,2.08,NULL,NULL,NULL,NULL),
	 (12,39,2.08,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (12,40,2.08,NULL,NULL,NULL,NULL),
	 (12,42,2.08,NULL,NULL,NULL,NULL),
	 (12,43,2.08,NULL,NULL,NULL,NULL),
	 (12,44,2.08,NULL,NULL,NULL,NULL),
	 (12,45,2.08,NULL,NULL,NULL,NULL),
	 (12,46,2.08,NULL,NULL,NULL,NULL),
	 (12,47,2.08,NULL,NULL,NULL,NULL),
	 (12,48,2.08,NULL,NULL,NULL,NULL),
	 (13,1,1.67,NULL,NULL,NULL,NULL),
	 (13,2,1.67,NULL,NULL,NULL,NULL);
INSERT INTO public.installmentbreakdown (paymentplanid,installmentmonth,installmentpercentage,createddatetime,lastmodifieddatetime,"�z)[��у��K�R�z",installmentmont) VALUES
	 (13,3,1.67,NULL,NULL,NULL,NULL);
INSERT INTO public.odaambassador (ownername,ownerphonenumber,ownerunitarea,ownerunitlocation,ownerdeveloper,ownerselectbudget,referralname,referralphonenumber,referralemail,referralclientstatue) VALUES
	 ('string','string',0,'string','string',0,'string','string','string','string'),
	 ('string','string',0,'string','string',0,'string','string','string','string'),
	 ('hawry','0121202321',500,'cairo egypt','Dorra',144,'hawary2','0120212023','mo@sd.com','New Client'),
	 ('hawary','01212121222',1500,'egypt','Somabay',120000,'hawary44','0121212122','mohamed@sd.com','New Client');
INSERT INTO public.paymentplans (paymentplanname,numberofinstallmentmonths,downpayment,downpaymentpercentage,adminfees,adminfeespercentage,interestrate,interestrateperyearpercentage,paymentplanicon,description) VALUES
	 ('Oda',3,true,60.00,false,NULL,false,NULL,NULL,'Pay directly with ODA & get instant voucher cards'),
	 ('Oda',6,true,40.00,false,NULL,false,NULL,NULL,'Pay directly with ODA & get instant voucher cards'),
	 ('Valu',12,false,NULL,true,4.00,false,NULL,NULL,'Pay directly with VALU & get instant voucher cards'),
	 ('Valu',18,false,NULL,true,10.00,false,NULL,NULL,'Pay directly with VALU & get instant voucher cards'),
	 ('Valu',24,false,NULL,true,15.00,true,14.00,NULL,'Pay directly with VALU & get instant voucher cards'),
	 ('SEVEN',12,false,NULL,true,4.00,false,NULL,NULL,'Pay directly with SEVEN without Down Payment'),
	 ('SEVEN',18,false,NULL,true,10.00,false,NULL,NULL,'Pay directly with SEVEN without Down Payment'),
	 ('Valu',36,false,NULL,true,15.00,true,15.00,NULL,'Pay directly with VALU & get instant voucher cards'),
	 ('Valu',48,false,NULL,true,15.00,true,16.00,NULL,'Pay directly with VALU & get instant voucher cards'),
	 ('Valu',60,false,NULL,true,15.00,false,NULL,NULL,'Pay directly with VALU & get instant voucher cards');
INSERT INTO public.plan (planname,pricepermeter,description,createdatetime,lastmodifieddatetime,planphoto,projecttype) VALUES
	 ('GRAND PLAN',8400.00,'The most affordable kit , This kit includes everything you need to get thing up and running ',NULL,NULL,NULL,false),
	 ('PRIME PLAN',9450.00,'A more elevated plan . This kit includes everything you need to get thing up and running with higher - end materials',NULL,NULL,NULL,false),
	 ('GRAND PLAN',8000.00,'The most affordable kit , This kit includes everything you need to get thing up and running ',NULL,NULL,NULL,true),
	 ('PRIME PLAN',9000.00,'A more elevated plan . This kit includes everything you need to get thing up and running with higher - end materials',NULL,NULL,NULL,true),
	 ('SIGNATURE PLAN',10200.00,'Choose this plan for a more luxurious & elevated finish that includes imported materials',NULL,NULL,NULL,true),
	 ('SIGNATURE PLAN',10200.00,'Choose this plan for a more luxurious & elevated finish that includes imported materials',NULL,NULL,NULL,false);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Interior Doors','Decoration',1,'Basic filling density with local technical fiber board',NULL,NULL,NULL),
	 ('Interior Doors','Decoration',2,'Meduim filling  density with wood board',NULL,NULL,NULL),
	 ('Interior Doors','Decoration',3,'Highest filling density with oak board coating',NULL,NULL,NULL),
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',1,'',NULL,NULL,1),
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',1,'',NULL,NULL,1),
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',2,'',NULL,NULL,2),
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',3,'',NULL,NULL,3),
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',2,'',NULL,NULL,2),
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',3,'',NULL,NULL,3),
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',6,'',NULL,NULL,3);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',6,'',NULL,NULL,3),
	 ('Interior Plumbing and Insulation','Foundation ',6,'',NULL,NULL,3),
	 ('Interior Wall Paint & Cladding','Decoration',1,'',NULL,NULL,1),
	 ('Interior Wall Paint & Cladding','Decoration',2,'',NULL,NULL,2),
	 ('Interior Wall Paint & Cladding','Decoration',3,'',NULL,NULL,3),
	 ('Gypsom Board','Decoration',6,'',NULL,NULL,3),
	 ('Flooring ( Rooms )','Decoration',6,'Local Marble HDF',NULL,NULL,NULL),
	 ('Stairs (including handrail & tempered glass','Decoration',6,'Local Marble/ Oak Wood',NULL,NULL,NULL),
	 ('Interior Plumbing and Insulation','Foundation ',1,'',NULL,NULL,1),
	 ('Interior Plumbing and Insulation','Foundation ',2,'',NULL,NULL,2);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Interior Plumbing and Insulation','Foundation ',3,'',NULL,NULL,3),
	 ('Interior Wall Paint & Cladding','Decoration',6,'',NULL,NULL,3),
	 ('Interior Doors','Decoration',6,'Highest filling density with oak board coating',NULL,NULL,NULL),
	 ('Gypsom Board','Decoration',1,'',NULL,NULL,1),
	 ('Gypsom Board','Decoration',2,'',NULL,NULL,2),
	 ('Gypsom Board','Decoration',3,'',NULL,NULL,3),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',6,'Local Marble/Porcelain',NULL,NULL,NULL),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',6,'HDF',NULL,NULL,NULL),
	 ('Flooring ( Rooms )','Decoration',1,'Ceramic',NULL,NULL,NULL),
	 ('Flooring ( Rooms )','Decoration',2,'Porcelain/HDF',NULL,NULL,NULL);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Stairs (including handrail & tempered glass','Decoration',1,'Local Marble',NULL,NULL,NULL),
	 ('Stairs (including handrail & tempered glass','Decoration',2,'Local Marble',NULL,NULL,NULL),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',1,'Ceramic',NULL,NULL,NULL),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',2,'Porcelain / Local Marble',NULL,NULL,NULL),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',3,'HDF',NULL,NULL,NULL),
	 ('Flooring ( Rooms )','Decoration',3,'Local Marble HDF',NULL,NULL,NULL),
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',4,'',NULL,NULL,1),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',1,'Ceramic',NULL,NULL,NULL),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',2,'Ceramic',NULL,NULL,NULL),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',3,'Local Marble/Porcelain',NULL,NULL,NULL);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Stairs (including handrail & tempered glass','Decoration',3,'Local Marble/ Oak Wood',NULL,NULL,NULL),
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',4,'',NULL,NULL,1),
	 ('Interior Plumbing and Insulation','Foundation ',4,'',NULL,NULL,1),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',4,'Ceramic',NULL,NULL,NULL),
	 ('Gypsom Board','Decoration',4,'',NULL,NULL,1),
	 ('Flooring ( Rooms )','Decoration',4,'Ceramic',NULL,NULL,NULL),
	 ('Stairs (including handrail & tempered glass','Decoration',4,'Local Marble',NULL,NULL,NULL),
	 ('Interior Wall Paint & Cladding','Decoration',4,'',NULL,NULL,1),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',4,'Ceramic',NULL,NULL,NULL),
	 ('Interior Doors','Decoration',4,'Basic filling density with local technical fiber board',NULL,NULL,NULL);
INSERT INTO public.plandetails (plandetailsname,plandetailstype,planid,description,createdatetime,lastmodifieddatetime,stars) VALUES
	 ('Interior Walls Demolish , Masonry & Platering','Foundation ',5,'',NULL,NULL,2),
	 ('Electricity , Internet , Satellite , Decentral .AC & Heating with Automation & Shutters piping only','Foundation ',5,'',NULL,NULL,2),
	 ('Interior Plumbing and Insulation','Foundation ',5,'',NULL,NULL,2),
	 ('Flooring ( Reception / Hallway / Balcony )','Decoration',5,'Porcelain / Local Marble',NULL,NULL,NULL),
	 ('Gypsom Board','Decoration',5,'',NULL,NULL,2),
	 ('Flooring ( Rooms )','Decoration',5,'Porcelain/HDF',NULL,NULL,NULL),
	 ('Stairs (including handrail & tempered glass','Decoration',5,'Local Marble',NULL,NULL,NULL),
	 ('Interior Wall Paint & Cladding','Decoration',5,'',NULL,NULL,2),
	 ('Flooring (Bathrooms/Kitchen)','Decoration',5,'Ceramic',NULL,NULL,NULL),
	 ('Interior Doors','Decoration',5,'Meduim filling  density with wood board',NULL,NULL,NULL);
INSERT INTO public.project (projectname,"location",amenities,totalunits,projectlogo,createdatetime,lastmodifieddatetime,developerid) VALUES
	 ('XXX','xxx',NULL,5,NULL,NULL,NULL,1),
	 ('YYY','yyy',NULL,4,NULL,NULL,NULL,2),
	 ('ZZZ','zzz',NULL,6,NULL,NULL,NULL,1),
	 ('AAA','aaa',NULL,5,NULL,NULL,NULL,2),
	 ('BBB','bbb',NULL,4,NULL,NULL,NULL,1),
	 ('CCC','ccc',NULL,6,NULL,NULL,NULL,2);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (1,NULL,1,10),
	 (2,NULL,1,10),
	 (3,NULL,2,10),
	 (4,NULL,1,10),
	 (5,NULL,2,10),
	 (6,NULL,1,10),
	 (7,NULL,2,10),
	 (8,NULL,1,10),
	 (1,NULL,2,11),
	 (2,NULL,2,11);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (3,NULL,2,11),
	 (4,NULL,1,11),
	 (5,NULL,2,11),
	 (6,NULL,2,11),
	 (7,NULL,2,11),
	 (8,NULL,2,11),
	 (1,NULL,2,14),
	 (2,NULL,1,14),
	 (3,NULL,1,14),
	 (4,NULL,2,14);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (5,NULL,1,14),
	 (6,NULL,1,14),
	 (7,NULL,2,14),
	 (8,NULL,2,14),
	 (1,NULL,2,16),
	 (2,NULL,1,16),
	 (3,NULL,2,16),
	 (4,NULL,1,16),
	 (5,NULL,2,16),
	 (6,NULL,1,16);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (7,NULL,2,16),
	 (8,NULL,1,16),
	 (1,NULL,2,17),
	 (2,NULL,1,17),
	 (3,NULL,2,17),
	 (4,NULL,1,17),
	 (5,NULL,1,17),
	 (6,NULL,2,17),
	 (7,NULL,1,17),
	 (8,NULL,2,17);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (1,NULL,2,20),
	 (2,NULL,1,20),
	 (3,NULL,1,20),
	 (4,NULL,2,20),
	 (5,NULL,2,20),
	 (6,NULL,2,20),
	 (7,NULL,2,20),
	 (8,NULL,2,20),
	 (1,NULL,1,21),
	 (2,NULL,2,21);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (3,NULL,1,21),
	 (4,NULL,2,21),
	 (5,NULL,1,21),
	 (6,NULL,2,21),
	 (7,NULL,1,21),
	 (8,NULL,1,21),
	 (1,NULL,2,22),
	 (2,NULL,2,22),
	 (3,NULL,1,22),
	 (4,NULL,1,22);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (5,NULL,1,22),
	 (6,NULL,1,22),
	 (7,NULL,2,22),
	 (8,NULL,2,22),
	 (1,NULL,2,23),
	 (2,NULL,1,23),
	 (3,NULL,2,23),
	 (4,NULL,1,23),
	 (5,NULL,2,23),
	 (6,NULL,2,23);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (7,NULL,1,23),
	 (8,NULL,2,23),
	 (1,NULL,2,24),
	 (2,NULL,1,24),
	 (3,NULL,2,24),
	 (4,NULL,1,24),
	 (5,NULL,1,24),
	 (6,NULL,1,24),
	 (7,NULL,1,24),
	 (8,NULL,1,24);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (1,NULL,2,25),
	 (2,NULL,1,25),
	 (1,NULL,1,27),
	 (2,NULL,1,27),
	 (1,NULL,1,28),
	 (2,NULL,1,28),
	 (1,NULL,2,29),
	 (1,NULL,1,30),
	 (2,NULL,1,30),
	 (1,NULL,2,31);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (2,NULL,2,31),
	 (3,NULL,2,31),
	 (4,NULL,2,31),
	 (5,NULL,1,31),
	 (6,NULL,1,31),
	 (7,NULL,1,31),
	 (1,NULL,2,32),
	 (2,NULL,2,32),
	 (3,NULL,1,32),
	 (4,NULL,2,32);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (5,NULL,2,32),
	 (6,NULL,1,32),
	 (7,NULL,1,32),
	 (8,NULL,1,32),
	 (1,NULL,2,34),
	 (2,NULL,2,34),
	 (3,NULL,1,34),
	 (4,NULL,2,34),
	 (5,NULL,1,34),
	 (6,NULL,1,34);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (7,NULL,1,34),
	 (1,NULL,2,35),
	 (2,NULL,1,35),
	 (3,NULL,1,35),
	 (4,NULL,2,35),
	 (5,NULL,1,35),
	 (6,NULL,2,35),
	 (7,NULL,1,35),
	 (8,NULL,2,35),
	 (1,NULL,2,36);
INSERT INTO public.questions (questionsid,questionname,answer,bookingid) VALUES
	 (2,NULL,1,36),
	 (3,NULL,1,36),
	 (4,NULL,2,36),
	 (5,NULL,1,36),
	 (6,NULL,2,36),
	 (7,NULL,1,36),
	 (8,NULL,2,36);
INSERT INTO public.unittype (unittype_name) VALUES
	 ('Stand-Alone Villa'),
	 ('Twin House'),
	 ('Town House'),
	 ('Apartment'),
	 ('Penthouse'),
	 ('Other');
