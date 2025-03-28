﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OdaWepApi.Domain.Models;
namespace OdaWepApi.Infrastructure;

public partial class OdaDbContext : DbContext
{
    public OdaDbContext()
    {
    }

    public OdaDbContext(DbContextOptions<OdaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Addon> Addons { get; set; }

    public virtual DbSet<Addperrequest> Addperrequests { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<ApartmentAddon> ApartmentAddons { get; set; }

    public virtual DbSet<ApartmentAddonperrequest> ApartmentAddonperrequests { get; set; }

    public virtual DbSet<Automation> Automations { get; set; }

    public virtual DbSet<Automationdetail> Automationdetails { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Contactus> Contactus { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customeranswer> Customeranswers { get; set; }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<Faceliftaddon> Faceliftaddons { get; set; }

    public virtual DbSet<Faceliftaddperrequest> Faceliftaddperrequests { get; set; }

    public virtual DbSet<Faceliftroom> Faceliftrooms { get; set; }

    public virtual DbSet<FaceliftroomAddon> FaceliftroomAddons { get; set; }

    public virtual DbSet<FaceliftroomAddonperrequest> FaceliftroomAddonperrequests { get; set; }

    public virtual DbSet<Installmentbreakdown> Installmentbreakdowns { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Odaambassador> Odaambassadors { get; set; }

    public virtual DbSet<Paymentmethod> Paymentmethods { get; set; }

    public virtual DbSet<Paymentplan> Paymentplans { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Plandetail> Plandetails { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projecttype> Projecttypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Unittype> Unittypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=dpg-cuc1s39opnds738s419g-a.oregon-postgres.render.com;Database=odadb;Username=odadb_user;Password=iwiEqjZ2mwcqFuREbb8U1GNTyfxKbgGw;Port=5432;SslMode=Require;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Addon>(entity =>
        {
            entity.HasKey(e => e.Addonid).HasName("addons_pkey");

            entity.ToTable("addons");

            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Addongroup)
                .HasMaxLength(50)
                .HasColumnName("addongroup");
            entity.Property(e => e.Addonname)
                .HasMaxLength(255)
                .HasColumnName("addonname");
            entity.Property(e => e.Displayorder).HasColumnName("displayorder");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Unitormeter)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert enum to string
                .HasColumnName("unitormeter");
        });

        modelBuilder.Entity<Addperrequest>(entity =>
        {
            entity.HasKey(e => e.Addperrequestid).HasName("addperrequest_pkey");

            entity.ToTable("addperrequest");

            entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
            entity.Property(e => e.Addperrequestname)
                .HasMaxLength(255)
                .HasColumnName("addperrequestname");
            entity.Property(e => e.Displayorder).HasColumnName("displayorder");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Answerid).HasName("answers_pkey");

            entity.ToTable("answers");

            entity.Property(e => e.Answerid).HasColumnName("answerid");
            entity.Property(e => e.Answercode)
                .HasMaxLength(1)
                .HasColumnName("answercode");
            entity.Property(e => e.Answertext).HasColumnName("answertext");
            entity.Property(e => e.Answerphoto).HasColumnName("answerphoto");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("answers_questionid_fkey");
        });

        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.Apartmentid).HasName("apartment_pkey");

            entity.ToTable("apartment");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Apartmentname)
                .HasMaxLength(255)
                .HasColumnName("apartmentname");
            entity.Property(e => e.Apartmentaddress)
                .HasMaxLength(255)
                .HasColumnName("apartmentaddress");
            entity.Property(e => e.Apartmentphotos).HasColumnName("apartmentphotos");
            entity.Property(e => e.Apartmentspace)
                .HasPrecision(10, 2)
                .HasColumnName("apartmentspace");
            entity.Property(e => e.Apartmentstatus)
                .HasMaxLength(50)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("apartmentstatus");
            entity.Property(e => e.Apartmenttype)
                .HasMaxLength(50)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("apartmenttype");
            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Availabilitydate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("availabilitydate");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Floornumber).HasColumnName("floornumber");
            entity.Property(e => e.Lastmodifieddatetime)
            .HasColumnType("timestamp without time zone")
               .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Projectid).HasColumnName("projectid");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Unittypeid).HasColumnName("unittypeid");
            entity.HasOne(d => d.Automation).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Automationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartment_automationid_fkey");
            entity.HasOne(d => d.Developer).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Developerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartment_developerid_fkey");
            entity.HasOne(d => d.Plan).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartment_planid_fkey");
            entity.HasOne(d => d.Unittype).WithMany(p => p.Apartments)
                .HasForeignKey(d => d.Unittypeid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("apartment_unittypeid_fkey");
        });

        modelBuilder.Entity<ApartmentAddon>(entity =>
        {
            entity.HasKey(e => new { e.Apartmentid, e.Addonid }).HasName("apartment_addon_pkey");

            entity.ToTable("apartment_addon");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Addon).WithMany(p => p.ApartmentAddons)
                .HasForeignKey(d => d.Addonid)
                .HasConstraintName("apartment_addons_addonid_fkey");
            entity.HasOne(d => d.Apartment).WithMany(p => p.ApartmentAddons)
                .HasForeignKey(d => d.Apartmentid)
                .HasConstraintName("apartment_addon_apartmentid_fkey");
        });

        modelBuilder.Entity<ApartmentAddonperrequest>(entity =>
        {
            entity.HasKey(e => new { e.Apartmentid, e.Addperrequestid }).HasName("apartment_addonperrequest_pkey");

            entity.ToTable("apartment_addonperrequest");

            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Addperrequest).WithMany(p => p.ApartmentAddonperrequests)
                .HasForeignKey(d => d.Addperrequestid)
                .HasConstraintName("apartment_addonperrequest_addperrequestid_fkey");
            entity.HasOne(d => d.Apartment).WithMany(p => p.ApartmentAddonperrequests)
                .HasForeignKey(d => d.Apartmentid)
                .HasConstraintName("apartment_addon_apartmentid_fkey");
        });

        modelBuilder.Entity<Automation>(entity =>
        {
            entity.HasKey(e => e.Automationid).HasName("automation_pkey");

            entity.ToTable("automation");

            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Automationname)
                .HasMaxLength(255)
                .HasColumnName("automationname");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Automationdetail>(entity =>
        {
            entity.HasKey(e => e.Automationdetailsid).HasName("automationdetails_pkey");

            entity.ToTable("automationdetails");

            entity.Property(e => e.Automationdetailsid).HasColumnName("automationdetailsid");
            entity.Property(e => e.Automationdetailsname)
                .HasMaxLength(255)
                .HasColumnName("automationdetailsname");
            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Icon).HasColumnName("icon");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");

            entity.HasOne(d => d.Automation).WithMany(p => p.Automationdetails)
           .HasForeignKey(d => d.Automationid)
           .OnDelete(DeleteBehavior.Cascade)
           .HasConstraintName("automationdetails_automationid_fkey");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("booking_pkey");

            entity.ToTable("booking");

            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Bookingstatus)
            .HasColumnName("bookingstatus")
            .HasConversion<string>()
            .HasColumnType("text"); ;
            entity.Property(e => e.Createdatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Apartmentid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_apartmentid_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_customerid_fkey");

            entity.HasOne(d => d.Paymentmethod).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Paymentmethodid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_paymentmethodid_fkey");

            entity.HasOne(d => d.Paymentplan).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Paymentplanid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_paymentplanid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("booking_userid_fkey");
        });

        modelBuilder.Entity<Contactus>(entity =>
      {
          entity.HasKey(e => e.Contactusid).HasName("contactus_pkey");

          entity.ToTable("contactus");

          entity.Property(e => e.Contactusid).HasColumnName("contactusid");
          entity.Property(e => e.Comments)
              .HasMaxLength(255)
              .HasColumnName("comments");
          entity.Property(e => e.Email)
              .HasColumnType("character varying")
              .HasColumnName("email");
          entity.Property(e => e.Firstname)
              .HasColumnType("character varying")
              .HasColumnName("firstname");
          entity.Property(e => e.Lastname)
              .HasColumnType("character varying")
              .HasColumnName("lastname");
          entity.Property(e => e.Phonenumber)
              .HasColumnType("character varying")
              .HasColumnName("phonenumber");
      });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<Customeranswer>(entity =>
        {
            entity.HasKey(e => e.Customeranswerid).HasName("customeranswers_pkey");

            entity.ToTable("customeranswers");

            entity.HasIndex(e => new { e.Bookingid, e.Questionid }, "customeranswers_bookingid_questionid_key").IsUnique();

            entity.Property(e => e.Customeranswerid).HasColumnName("customeranswerid");
            entity.Property(e => e.Answerid).HasColumnName("answerid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Questionid).HasColumnName("questionid");

            entity.HasOne(d => d.Answer).WithMany(p => p.Customeranswers)
                .HasForeignKey(d => d.Answerid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("customeranswers_answerid_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.Customeranswers)
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("customeranswers_bookingid_fkey");

            entity.HasOne(d => d.Question).WithMany(p => p.Customeranswers)
                .HasForeignKey(d => d.Questionid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("customeranswers_questionid_fkey");
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.Developerid).HasName("developer_pkey");

            entity.ToTable("developer");

            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Developerlogo).HasColumnName("developerlogo");
            entity.Property(e => e.Developername)
                .HasMaxLength(255)
                .HasColumnName("developername");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Faceliftaddon>(entity =>
        {
            entity.HasKey(e => e.Addonid).HasName("faceliftaddons_pkey");

            entity.ToTable("faceliftaddons");

            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Addongroup)
                .HasMaxLength(50)
                .HasColumnName("addongroup");
            entity.Property(e => e.Addonname)
                .HasMaxLength(255)
                .HasColumnName("addonname");
            entity.Property(e => e.Displayorder).HasColumnName("displayorder");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.Faceliftroomtype)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("faceliftroomtype");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Unitormeter)
                .HasMaxLength(255)
                .HasColumnName("unitormeter");
        });

        modelBuilder.Entity<Faceliftaddperrequest>(entity =>
        {
            entity.HasKey(e => e.Addperrequestid).HasName("faceliftaddperrequest_pkey");

            entity.ToTable("faceliftaddperrequest");

            entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
            entity.Property(e => e.Addperrequestname)
                .HasMaxLength(255)
                .HasColumnName("addperrequestname");
            entity.Property(e => e.Displayorder).HasColumnName("displayorder");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Faceliftroom>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("faceliftroom_pkey");

            entity.ToTable("faceliftroom");

            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Automationid).HasColumnName("automationid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Roomtype)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnType("text")
                .HasColumnName("roomtype");

            // Update navigation property names to match the domain models
            entity.HasOne(d => d.Automation).WithMany(p => p.Faceliftrooms) // Corrected to match Automation.FaceLiftRooms
                .HasForeignKey(d => d.Automationid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("faceliftroom_automationid_fkey");

            entity.HasOne(d => d.Booking).WithMany(p => p.Faceliftrooms) // Corrected to match Booking.FaceliftRooms
                .HasForeignKey(d => d.Bookingid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("faceliftroom_bookingid_fkey");

            entity.HasOne(d => d.Apartment).WithMany(p => p.Faceliftrooms)
                 .HasForeignKey(d => d.Apartmentid)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("faceliftroom_apartmentid_fkey");
        });


        modelBuilder.Entity<FaceliftroomAddon>(entity =>
        {
            entity.HasKey(e => new { e.Roomid, e.Addonid }).HasName("faceliftroomaddon_pkey");

            entity.ToTable("faceliftroom_addon");

            entity.Property(e => e.Roomid).HasColumnName("roomid");
            entity.Property(e => e.Addonid).HasColumnName("addonid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Addon).WithMany(p => p.FaceliftroomAddons)
                .HasForeignKey(d => d.Addonid)
                .HasConstraintName("faceliftroomaddon_addonid_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.FaceliftroomAddons)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("faceliftroomaddon_roomid_fkey");
        });

        modelBuilder.Entity<FaceliftroomAddonperrequest>(entity =>
                {
                    entity.HasKey(e => new { e.Apartmentid, e.Addperrequestid }).HasName("room_addonperrequest_pkey");

                    entity.ToTable("faceliftroom_addonperrequest");

                    entity.Property(e => e.Apartmentid).HasColumnName("apartmentid");
                    entity.Property(e => e.Addperrequestid).HasColumnName("addperrequestid");
                    entity.Property(e => e.Quantity)
                        .HasDefaultValue(1)
                        .HasColumnName("quantity");

                    entity.HasOne(d => d.Addperrequest).WithMany(p => p.FaceliftroomAddonperrequests)
                        .HasForeignKey(d => d.Addperrequestid)
                        .HasConstraintName("apartment_addonperrequest_addperrequestid_fkey");

                    entity.HasOne(d => d.Apartment).WithMany(p => p.FaceliftroomAddonperrequests)
                        .HasForeignKey(d => d.Apartmentid)
                        .HasConstraintName("room_addon_apartmentid_fkey");
                });

        modelBuilder.Entity<Installmentbreakdown>(entity =>
        {
            entity.HasKey(e => e.Breakdownid).HasName("installmentbreakdown_pkey");

            entity.ToTable("installmentbreakdown");

            entity.Property(e => e.Breakdownid).HasColumnName("breakdownid");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Installmentmonth).HasColumnName("installmentmonth");
            entity.Property(e => e.Installmentpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("installmentpercentage");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");

            entity.HasOne(d => d.Paymentplan).WithMany(p => p.Installmentbreakdowns)
                .HasForeignKey(d => d.Paymentplanid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("installmentbreakdown_paymentplanid_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Invoiceamount)
                .HasPrecision(10, 2)
                .HasColumnName("invoiceamount");
            entity.Property(e => e.Invoiceduedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("invoiceduedate");
            entity.Property(e => e.Invoicestatus)
                .HasMaxLength(50)
                .HasColumnName("invoicestatus");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
        });

        modelBuilder.Entity<Odaambassador>(entity =>
   {
       entity.HasKey(e => e.Odaambassadorid).HasName("oda_pkey");

       entity.ToTable("odaambassador");

       entity.Property(e => e.Odaambassadorid).HasColumnName("odaambassadorid");
       entity.Property(e => e.Ownerdeveloper)
           .HasColumnType("character varying")
           .HasColumnName("ownerdeveloper");
       entity.Property(e => e.Ownername)
           .HasColumnType("character varying")
           .HasColumnName("ownername");
       entity.Property(e => e.Ownerphonenumber)
           .HasColumnType("character varying")
           .HasColumnName("ownerphonenumber");
       entity.Property(e => e.Ownerselectbudget).HasColumnName("ownerselectbudget");
       entity.Property(e => e.Ownerunitarea).HasColumnName("ownerunitarea");
       entity.Property(e => e.Ownerunitlocation)
           .HasColumnType("character varying")
           .HasColumnName("ownerunitlocation");
       entity.Property(e => e.Referralclientstatue)
           .HasColumnType("character varying")
           .HasColumnName("referralclientstatue");
       entity.Property(e => e.Referralemail)
           .HasColumnType("character varying")
           .HasColumnName("referralemail");
       entity.Property(e => e.Referralname)
           .HasColumnType("character varying")
           .HasColumnName("referralname");
       entity.Property(e => e.Referralphonenumber)
           .HasColumnType("character varying")
           .HasColumnName("referralphonenumber");
   });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity.HasKey(e => e.Paymentmethodid).HasName("paymentmethod_pkey");

            entity.ToTable("paymentmethod");

            entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
            entity.Property(e => e.Createddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddatetime");
            entity.Property(e => e.Depositpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("depositpercentage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Numberofinstallments).HasColumnName("numberofinstallments");
            entity.Property(e => e.Paymentmethodname)
                .HasMaxLength(255)
                .HasColumnName("paymentmethodname");
            entity.Property(e => e.Paymentmethodphotos).HasColumnName("paymentmethodphotos");
        });

        modelBuilder.Entity<Paymentplan>(entity =>
        {
            entity.HasKey(e => e.Paymentplanid).HasName("paymentplans_pkey");

            entity.ToTable("paymentplans");

            entity.Property(e => e.Paymentplanid).HasColumnName("paymentplanid");
            entity.Property(e => e.Adminfees).HasColumnName("adminfees");
            entity.Property(e => e.Adminfeespercentage)
                .HasPrecision(10, 2)
                .HasColumnName("adminfeespercentage");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Downpayment).HasColumnName("downpayment");
            entity.Property(e => e.Downpaymentpercentage)
                .HasPrecision(10, 2)
                .HasColumnName("downpaymentpercentage");
            entity.Property(e => e.Interestrate).HasColumnName("interestrate");
            entity.Property(e => e.Interestrateperyearpercentage)
                .HasPrecision(5, 2)
                .HasColumnName("interestrateperyearpercentage");
            entity.Property(e => e.Numberofinstallmentmonths).HasColumnName("numberofinstallmentmonths");
            entity.Property(e => e.Paymentplanicon).HasColumnName("paymentplanicon");
            entity.Property(e => e.Paymentplanname)
                .HasMaxLength(255)
                .HasColumnName("paymentplanname");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Permissionid).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Permissionid).HasColumnName("permissionid");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Entityname)
                .HasMaxLength(255)
                .HasColumnName("entityname");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Planid).HasName("plan_pkey");

            entity.ToTable("plan");

            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Planname)
                .HasMaxLength(255)
                .HasColumnName("planname");
            entity.Property(e => e.Planphoto).HasColumnName("planphoto");
            entity.Property(e => e.Pricepermeter)
                .HasPrecision(10, 2)
                .HasColumnName("pricepermeter");
            entity.Property(e => e.Projecttype)
                .HasComment("true = locate your home / false build your kit")
                .HasColumnName("projecttype");
        });

        modelBuilder.Entity<Plandetail>(entity =>
        {
            entity.HasKey(e => e.Plandetailsid).HasName("plandetails_pkey");

            entity.ToTable("plandetails");

            entity.Property(e => e.Plandetailsid).HasColumnName("plandetailsid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Displayorder).HasColumnName("displayorder");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Plandetailsname)
                .HasMaxLength(255)
                .HasColumnName("plandetailsname");
            entity.Property(e => e.Plandetailstype)
                .HasMaxLength(255)
                .HasConversion<string>() // Convert Enum to string
                .HasColumnName("plandetailstype");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Stars).HasColumnName("stars");

            entity.HasOne(d => d.Plan).WithMany(p => p.Plandetails)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("plandetails_plan_plandetailsid_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Projectid).HasName("project_pkey");

            entity.ToTable("project");

            entity.Property(e => e.Projectid).HasColumnName("projectid");
            entity.Property(e => e.Amenities).HasColumnName("amenities");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Projectlogo).HasColumnName("projectlogo");
            entity.Property(e => e.Projectname)
                .HasMaxLength(255)
                .HasColumnName("projectname");
            entity.Property(e => e.Totalunits).HasColumnName("totalunits");
        });

        modelBuilder.Entity<Projecttype>(entity =>
        {
            entity.HasKey(e => e.Projecttypeid).HasName("projecttype_pkey");
            entity.ToTable("projecttype");
            entity.Property(e => e.Projecttypeid).HasColumnName("projecttypeid");
            entity.Property(e => e.Projecttypedetail)
                .HasMaxLength(255)
                .HasColumnName("projecttypedetail");
            entity.Property(e => e.Projecttypename)
                .HasMaxLength(255)
                .HasColumnName("projecttypename");
        });

        modelBuilder.Entity<Question>(entity =>
          {
              entity.HasKey(e => e.Questionid).HasName("questions_pkey");

              entity.ToTable("questions");

              entity.Property(e => e.Questionid).HasColumnName("questionid");
              entity.Property(e => e.Createdat)
                  .HasDefaultValueSql("now()")
                  .HasColumnType("timestamp without time zone")
                  .HasColumnName("createdat");
              entity.Property(e => e.Questiontext).HasColumnName("questiontext");
          });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialsid).HasName("testimonials_pkey");

            entity.ToTable("testimonials");

            entity.Property(e => e.Testimonialsid).HasColumnName("testimonialsid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Testimonialsname)
                .HasMaxLength(255)
                .HasColumnName("testimonialsname");
            entity.Property(e => e.Testimonialsphoto).HasColumnName("testimonialsphoto");
            entity.Property(e => e.Testimonialstitle)
                .HasMaxLength(255)
                .HasColumnName("testimonialstitle");
        });


        modelBuilder.Entity<Unittype>(entity =>
        {
            entity.HasKey(e => e.Unittypeid).HasName("unittype_pkey");
            entity.ToTable("unittype");
            entity.Property(e => e.Unittypeid).HasColumnName("unittypeid");
            entity.Property(e => e.UnittypeName)
                .HasMaxLength(50)
                .HasColumnName("unittype_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdatetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(255)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastlogin");
            entity.Property(e => e.Lastmodifieddatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastmodifieddatetime");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
