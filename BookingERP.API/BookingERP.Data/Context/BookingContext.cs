using BookingERP.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BookingERP.Data.Context
{
    public class BookingContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

        }

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<PaymentDetails> PaymentDetails { get; set; }

        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().Property(x => x.Email).IsRequired();

            builder.Entity<Guest>().HasKey(x => x.Id);
            builder.Entity<Hotel>().HasKey(x => x.Id);

            builder.Entity<ReservationRoom>()
                 .HasOne(r => r.Reservation)
                 .WithMany(r => r.ReservationRooms)
                 .HasForeignKey(r => r.ReservationId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ReservationRoom>()
                .HasOne(r => r.Room)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ReservationRoom>()
                 .HasKey(rr => new { rr.ReservationId, rr.RoomId });


            base.OnModelCreating(builder);
        }
    }
}
