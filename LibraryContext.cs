using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Students_Management_Api.Models;

namespace Students_Management_Api
{
    public class LibraryContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
        {
        }

        //public DbSet<User> User { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Supervisor> Supervisor { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentMessages> StudentMessages { get; set; }
        public DbSet<TeacherMessage> TeacherMessage { get; set; }
        public DbSet<Lecture> Lecture { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<LectureStudent> LectureStudent { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
        public DbSet<StudentTeacherMessage> StudentTeacherMessage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname);
                entity.Property(entity => entity.Surname);
                entity.Property(entity => entity.Phone);
                entity.Property(entity => entity.IdentityNo);
                entity.Property(entity => entity.Study);
                entity.HasOne(e => e.ApplicationUser).WithOne().HasForeignKey<Teacher>(e => e.UserId);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname);
                entity.Property(entity => entity.Surname);
                entity.Property(entity => entity.Phone);
                entity.Property(entity => entity.IdentityNo);
                entity.HasOne(e => e.ApplicationUser).WithOne().HasForeignKey<Supervisor>(e => e.UserId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname).IsRequired(false);
                entity.Property(entity => entity.Surname).IsRequired(false);
                entity.Property(entity => entity.Phone).IsRequired(false);
                entity.Property(entity => entity.IdentityNo).IsRequired(false);
                entity.Property(entity => entity.Faculty).IsRequired(false);
                entity.Property(entity => entity.Department).IsRequired(false);
                entity.Property(entity => entity.Year).IsRequired(false);
                //entity.Property(entity => entity.Birth).IsRequired(false);
                entity.HasOne(e => e.ApplicationUser).WithOne().HasForeignKey<Student>(e => e.UserId);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Title);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Teacher).WithMany(e => e.Lectures).HasForeignKey(e => e.TeacherID);
                entity.HasMany(e => e.Students).WithMany(e => e.Lectures)
                      .UsingEntity<LectureStudent>(
                    l => l.HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentsId),
                    r => r.HasOne(e => e.Lecture).WithMany().HasForeignKey(e => e.LecturesId));
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Time);
                entity.HasOne(e => e.Teacher).WithMany(e => e.Classes);
                entity.HasMany(e => e.Students).WithMany(e => e.Classes)
                      .UsingEntity<ClassStudent>(
                      l => l.HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentsId),
                      r => r.HasOne(e => e.Class).WithMany().HasForeignKey(e => e.ClassesId));
            });

            modelBuilder.Entity<StudentMessages>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Subject);
                entity.Property(entity => entity.Body);
                entity.Property(entity => entity.Status);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Sender).WithMany(e => e.Sents).HasForeignKey(e => e.SenderId);
                entity.HasOne(e => e.Receiver).WithMany(e => e.Received).HasForeignKey(e => e.ReceiverId);
            });

            modelBuilder.Entity<TeacherMessage>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Subject);
                entity.Property(entity => entity.Body);
                entity.Property(entity => entity.Status);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Sender).WithMany(e => e.Sents);
                entity.HasMany(e => e.Receivers).WithMany(e => e.Received)
                      .UsingEntity<StudentTeacherMessage>(
                      l => l.HasOne(e => e.Receiver).WithMany().HasForeignKey(e => e.ReceiverId),
                      r => r.HasOne(e => e.Received).WithMany().HasForeignKey(e => e.ReceivedId));
            });

            /*modelBuilder.Entity<LectureStudent>()
            .HasIndex(e => e.StudentsId)
            .IsUnique();*/
        }
    }
}
