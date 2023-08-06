using Microsoft.EntityFrameworkCore;

namespace SmartChourey.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<AccidentGalleryInformation> AccidentGalleryInformations { get; set; }

        public virtual DbSet<AccidentPhotoInformation> AccidentPhotoInformations { get; set; }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual DbSet<AuditAccessInformation> AuditAccessInformations { get; set; }

        public virtual DbSet<AuditAccidentGalleryInformation> AuditAccidentGalleryInformations { get; set; }

        public virtual DbSet<AuditAccidentPhotoInformation> AuditAccidentPhotoInformations { get; set; }

        public virtual DbSet<AuditChoureyOneGalleryInformation> AuditChoureyOneGalleryInformations { get; set; }

        public virtual DbSet<AuditChoureyOneInformation> AuditChoureyOneInformations { get; set; }

        public virtual DbSet<AuditChoureyOnePhotoInformation> AuditChoureyOnePhotoInformations { get; set; }

        public virtual DbSet<AuditChoureyOneVideoInformation> AuditChoureyOneVideoInformations { get; set; }

        public virtual DbSet<AuditChoureyTwoGalleryInformation> AuditChoureyTwoGalleryInformations { get; set; }

        public virtual DbSet<AuditChoureyTwoInformation> AuditChoureyTwoInformations { get; set; }

        public virtual DbSet<AuditChoureyTwoPhotoInformation> AuditChoureyTwoPhotoInformations { get; set; }

        public virtual DbSet<AuditChoureyTwoVideoInformation> AuditChoureyTwoVideoInformations { get; set; }

        public virtual DbSet<AuditDisasterGalleryInformation> AuditDisasterGalleryInformations { get; set; }

        public virtual DbSet<AuditDisasterInformation> AuditDisasterInformations { get; set; }

        public virtual DbSet<AuditDisasterPhotoInformation> AuditDisasterPhotoInformations { get; set; }

        public virtual DbSet<AuditDisasterVideoInformation> AuditDisasterVideoInformations { get; set; }

        public virtual DbSet<AuditEmployeeDetailInformation> AuditEmployeeDetailInformations { get; set; }

        public virtual DbSet<AuditErrorMessageInformation> AuditErrorMessageInformations { get; set; }

        public virtual DbSet<AuditNewsGalleryInformation> AuditNewsGalleryInformations { get; set; }

        public virtual DbSet<AuditNewsPhotoInformation> AuditNewsPhotoInformations { get; set; }

        public virtual DbSet<AuditNoticeGalleryInformation> AuditNoticeGalleryInformations { get; set; }

        public virtual DbSet<AuditNoticePhotoInformation> AuditNoticePhotoInformations { get; set; }

        public virtual DbSet<AuditQrcodeInformation> AuditQrcodeInformations { get; set; }

        public virtual DbSet<AuditSafetyGalleryInformation> AuditSafetyGalleryInformations { get; set; }

        public virtual DbSet<AuditSafetyPhotoInformation> AuditSafetyPhotoInformations { get; set; }

        public virtual DbSet<AuditSiteAccidentInformation> AuditSiteAccidentInformations { get; set; }

        public virtual DbSet<AuditSiteDeclarationInformation> AuditSiteDeclarationInformations { get; set; }

        public virtual DbSet<AuditSiteInformation> AuditSiteInformations { get; set; }

        public virtual DbSet<AuditSiteNewsInformation> AuditSiteNewsInformations { get; set; }

        public virtual DbSet<AuditSiteNoticeInformation> AuditSiteNoticeInformations { get; set; }

        public virtual DbSet<AuditSiteSafetyInformation> AuditSiteSafetyInformations { get; set; }

        public virtual DbSet<AuditSiteWorkInformation> AuditSiteWorkInformations { get; set; }

        public virtual DbSet<AuditUserSiteDeclarationInformation> AuditUserSiteDeclarationInformations { get; set; }

        public virtual DbSet<AuditUserSiteInformation> AuditUserSiteInformations { get; set; }

        public virtual DbSet<AuditVideoMessageInformation> AuditVideoMessageInformations { get; set; }

        public virtual DbSet<ChangeDetailLog> ChangeDetailLogs { get; set; }

        public virtual DbSet<ChangeLog> ChangeLogs { get; set; }

        public virtual DbSet<ChoureyCustomName> ChoureyCustomNames { get; set; }

        public virtual DbSet<ChoureyMediaComment> ChoureyMediaComments { get; set; }

        public virtual DbSet<ChoureyOneGalleryInformation> ChoureyOneGalleryInformations { get; set; }

        public virtual DbSet<ChoureyOneInformation> ChoureyOneInformations { get; set; }

        public virtual DbSet<ChoureyOnePhotoInformation> ChoureyOnePhotoInformations { get; set; }

        public virtual DbSet<ChoureyOneVideoInformation> ChoureyOneVideoInformations { get; set; }

        public virtual DbSet<ChoureyTwoGalleryInformation> ChoureyTwoGalleryInformations { get; set; }

        public virtual DbSet<ChoureyTwoInformation> ChoureyTwoInformations { get; set; }

        public virtual DbSet<ChoureyTwoPhotoInformation> ChoureyTwoPhotoInformations { get; set; }

        public virtual DbSet<ChoureyTwoVideoInformation> ChoureyTwoVideoInformations { get; set; }

        public virtual DbSet<DeviceLog> DeviceLogs { get; set; }

        public virtual DbSet<DeviceRegistration> DeviceRegistrations { get; set; }

        public virtual DbSet<DeviceSiteLog> DeviceSiteLogs { get; set; }

        public virtual DbSet<DeviceSiteMapper> DeviceSiteMappers { get; set; }

        public virtual DbSet<DisasterGalleryInformation> DisasterGalleryInformations { get; set; }

        public virtual DbSet<DisasterInformation> DisasterInformations { get; set; }

        public virtual DbSet<DisasterPhotoInformation> DisasterPhotoInformations { get; set; }

        public virtual DbSet<DisasterVideoInformation> DisasterVideoInformations { get; set; }

        public virtual DbSet<EmployeeDetailInformation> EmployeeDetailInformations { get; set; }

        public virtual DbSet<ErrorMessageInformation> ErrorMessageInformations { get; set; }

        public virtual DbSet<FileUpload> FileUploads { get; set; }

        public virtual DbSet<LanguageInformation> LanguageInformations { get; set; }

        public virtual DbSet<LoginDetailInformation> LoginDetailInformations { get; set; }

        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

        public virtual DbSet<NewsGalleryInformation> NewsGalleryInformations { get; set; }

        public virtual DbSet<NewsPhotoInformation> NewsPhotoInformations { get; set; }

        public virtual DbSet<NoticeGalleryInformation> NoticeGalleryInformations { get; set; }

        public virtual DbSet<NoticePhotoInformation> NoticePhotoInformations { get; set; }

        public virtual DbSet<PublicUserboardInformation> PublicUserboardInformations { get; set; }

        public virtual DbSet<PublicUserboardPhotoInformation> PublicUserboardPhotoInformations { get; set; }

        public virtual DbSet<QrcodeInformation> QrcodeInformations { get; set; }

        public virtual DbSet<Qrlog> Qrlogs { get; set; }

        public virtual DbSet<SafetyGalleryInformation> SafetyGalleryInformations { get; set; }

        public virtual DbSet<SafetyPhotoInformation> SafetyPhotoInformations { get; set; }

        public virtual DbSet<SiteAccessInformation> SiteAccessInformations { get; set; }

        public virtual DbSet<SiteAccidentInformation> SiteAccidentInformations { get; set; }

        public virtual DbSet<SiteCodeAccessInformation> SiteCodeAccessInformations { get; set; }

        public virtual DbSet<SiteDeclarationDeviceRegistrationMapper> SiteDeclarationDeviceRegistrationMappers { get; set; }

        public virtual DbSet<SiteDeclarationInformation> SiteDeclarationInformations { get; set; }

        public virtual DbSet<SiteInformation> SiteInformations { get; set; }

        public virtual DbSet<SiteNewsInformation> SiteNewsInformations { get; set; }

        public virtual DbSet<SiteNoticeInformation> SiteNoticeInformations { get; set; }

        public virtual DbSet<SiteSafetyInformation> SiteSafetyInformations { get; set; }

        public virtual DbSet<SiteSpaceAggregate> SiteSpaceAggregates { get; set; }

        public virtual DbSet<SiteSpaceDetail> SiteSpaceDetails { get; set; }

        public virtual DbSet<SiteWorkInformation> SiteWorkInformations { get; set; }

        public virtual DbSet<UserInformation> UserInformations { get; set; }

        public virtual DbSet<UserSiteDeclarationInformation> UserSiteDeclarationInformations { get; set; }

        public virtual DbSet<UserSiteInformation> UserSiteInformations { get; set; }

        public virtual DbSet<VideoMessageInformation> VideoMessageInformations { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-H31F8F2;Initial Catalog=smartchourey;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=True;");
*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccidentGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__Accident__CF4F7BB5DC5A97A2");

                entity.ToTable("AccidentGallery_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteAccidentGallery_Information");
                    tb.HasTrigger("T_InsertAccidentGallery_Information");
                    tb.HasTrigger("T_UpdateAccidentGallery_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Accident).WithMany(p => p.AccidentGalleryInformations)
                    .HasForeignKey(d => d.AccidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccidentGallery_Information_SiteAccident_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.AccidentGalleryInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccidentGallery_Information_Site_Information");
            });

            modelBuilder.Entity<AccidentPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__Accident__21B7B5E297D217F8");

                entity.ToTable("AccidentPhoto_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteAccidentPhoto_Information");
                    tb.HasTrigger("T_InsertAccidentPhoto_Information");
                    tb.HasTrigger("T_UpdateAccidentPhoto_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Accident).WithMany(p => p.AccidentPhotoInformations)
                    .HasForeignKey(d => d.AccidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccidentPhoto_Information_SiteAccident_Information");

                entity.HasOne(d => d.Gallery).WithMany(p => p.AccidentPhotoInformations)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_AccidentPhoto_Information_AccidentGallery_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.AccidentPhotoInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccidentPhoto_Information_Site_Information");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");

                entity.HasIndex(e => e.Name, "RoleNameIndex").IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

                entity.HasIndex(e => e.UserName, "UserNameIndex").IsUnique();

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(256);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.LockoutEnd).HasColumnType("datetime");
                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        r => r.HasOne<AspNetRole>().WithMany()
                            .HasForeignKey("RoleId")
                            .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                        l => l.HasOne<AspNetUser>().WithMany()
                            .HasForeignKey("UserId")
                            .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                            j.ToTable("AspNetUserRoles");
                            j.HasIndex(new[] { "RoleId" }, "IX_RoleId");
                            j.HasIndex(new[] { "UserId" }, "IX_UserId");
                            j.IndexerProperty<string>("UserId").HasMaxLength(128);
                            j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

                entity.HasIndex(e => e.UserId, "IX_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);
                entity.Property(e => e.ProviderKey).HasMaxLength(128);
                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AuditAccessInformation>(entity =>
            {
                entity.HasKey(e => e.AuditSiteAccessId).HasName("PK__Audit_ac__CB92ADD4B87295FF");

                entity.ToTable("Audit_access_Information");

                entity.Property(e => e.AuditSiteAccessId).HasColumnName("Audit_SiteAccessId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.JoinedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteAccessGivenTo).HasMaxLength(300);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<AuditAccidentGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__AuditAcc__0DB76A4DD5D7019B");

                entity.ToTable("AuditAccidentGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditAccidentPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__AuditAcc__C6CE7D67FA2D45D4");

                entity.ToTable("AuditAccidentPhoto_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyOneGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__Audit_Ch__0DB76A4DBB6493B1");

                entity.ToTable("Audit_ChoureyOneGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyOneInformation>(entity =>
            {
                entity.HasKey(e => e.AuditChoureyOneId).HasName("PK__Audit_Ch__D161454C33EBD206");

                entity.ToTable("Audit_ChoureyOne_Information");

                entity.Property(e => e.AuditChoureyOneId).HasColumnName("Audit_ChoureyOneId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyOnePhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__Audit_Ch__571A8D9C8579C7CA");

                entity.ToTable("Audit_ChoureyOnePhoto_Information");

                entity.Property(e => e.AuditPhotoId).HasColumnName("Audit_PhotoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyOneVideoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditVideoId).HasName("PK__Audit_Ch__892A1F9918819032");

                entity.ToTable("Audit_ChoureyOneVideo_Information");

                entity.Property(e => e.AuditVideoId).HasColumnName("Audit_VideoId");
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyTwoGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__Audit_Ch__0DB76A4DF706D955");

                entity.ToTable("Audit_ChoureyTwoGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyTwoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditChoureyTwoId).HasName("PK__Audit_Ch__419B6D3C4B8972F0");

                entity.ToTable("Audit_ChoureyTwo_Information");

                entity.Property(e => e.AuditChoureyTwoId).HasColumnName("Audit_ChoureyTwoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyTwoPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__Audit_Ch__571A8D9C106D867F");

                entity.ToTable("Audit_ChoureyTwoPhoto_Information");

                entity.Property(e => e.AuditPhotoId).HasColumnName("Audit_PhotoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditChoureyTwoVideoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditVideoId).HasName("PK__Audit_Ch__892A1F99EAEAEF80");

                entity.ToTable("Audit_ChoureyTwoVideo_Information");

                entity.Property(e => e.AuditVideoId).HasColumnName("Audit_VideoId");
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditDisasterGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__Audit_Di__0DB76A4DE9359F00");

                entity.ToTable("Audit_DisasterGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditDisasterInformation>(entity =>
            {
                entity.HasKey(e => e.AuditDisasterId).HasName("PK__Audit_Di__FB19C6240A96293E");

                entity.ToTable("Audit_Disaster_Information");

                entity.Property(e => e.AuditDisasterId).HasColumnName("Audit_DisasterId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditDisasterPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__Audit_Di__571A8D9C984A3E6B");

                entity.ToTable("Audit_DisasterPhoto_Information");

                entity.Property(e => e.AuditPhotoId).HasColumnName("Audit_PhotoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditDisasterVideoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditVideoId).HasName("PK__Audit_Di__892A1F99E78166EB");

                entity.ToTable("Audit_DisasterVideo_Information");

                entity.Property(e => e.AuditVideoId).HasColumnName("Audit_VideoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditEmployeeDetailInformation>(entity =>
            {
                entity.HasKey(e => e.AuditEmployeeId).HasName("PK__Audit_Em__EDDA472D3A63ACD3");

                entity.ToTable("Audit_EmployeeDetail_Information");

                entity.Property(e => e.AuditEmployeeId).HasColumnName("Audit_EmployeeId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Dob)
                    .HasMaxLength(200)
                    .HasColumnName("DOB");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.EmergencyContactNumber).HasMaxLength(60);
                entity.Property(e => e.FamilyNameChinese)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Chinese");
                entity.Property(e => e.FamilyNameKana)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Kana");
                entity.Property(e => e.FamilyNameRoman)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Roman");
                entity.Property(e => e.Gender).HasMaxLength(20);
                entity.Property(e => e.GivenNameChinese)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Chinese");
                entity.Property(e => e.GivenNameKana)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Kana");
                entity.Property(e => e.GivenNameRoman)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Roman");
                entity.Property(e => e.MobileNumber).HasMaxLength(60);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(300)
                    .HasColumnName("phoneNumber");
                entity.Property(e => e.PhotoPath).HasMaxLength(300);
                entity.Property(e => e.Photoname)
                    .HasMaxLength(300)
                    .HasColumnName("photoname");
                entity.Property(e => e.Postbox).HasMaxLength(200);
                entity.Property(e => e.Prefecture).HasMaxLength(200);
                entity.Property(e => e.RoleName).HasMaxLength(20);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<AuditErrorMessageInformation>(entity =>
            {
                entity.HasKey(e => e.AuditErrorId).HasName("PK__Audit_Er__48ED8D0931A2AFB6");

                entity.ToTable("Audit_ErrorMessage_Information");

                entity.Property(e => e.AuditErrorId).HasColumnName("auditErrorId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(100);
                entity.Property(e => e.MethodName).HasMaxLength(100);
                entity.Property(e => e.OperationDoneBy).HasMaxLength(100);
                entity.Property(e => e.SolvedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditNewsGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__AuditNew__0DB76A4DF0480D56");

                entity.ToTable("AuditNewsGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditNewsPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__AuditNew__C6CE7D670C152A6F");

                entity.ToTable("AuditNewsPhoto_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditNoticeGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__AuditNot__0DB76A4D6161FC0D");

                entity.ToTable("AuditNoticeGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditNoticePhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__AuditNot__C6CE7D67CD1B6CE9");

                entity.ToTable("AuditNoticePhoto_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditQrcodeInformation>(entity =>
            {
                entity.HasKey(e => e.AuditQrid).HasName("PK__Audit_QR__9230C72E0E49A0C2");

                entity.ToTable("Audit_QRCode_Information");

                entity.Property(e => e.AuditQrid).HasColumnName("AuditQRId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.QrcodeValue).HasColumnName("QRCodeValue");
                entity.Property(e => e.Qrid).HasColumnName("QRId");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSafetyGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.AuditGalleryId).HasName("PK__AuditSaf__0DB76A4D167C2DEF");

                entity.ToTable("AuditSafetyGallery_Information");

                entity.Property(e => e.AuditGalleryId).HasColumnName("Audit_GalleryId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSafetyPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.AuditPhotoId).HasName("PK__AuditSaf__C6CE7D67F892F5CA");

                entity.ToTable("AuditSafetyPhoto_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteAccidentInformation>(entity =>
            {
                entity.HasKey(e => e.AuditAccidentId).HasName("PK__Audit_Si__FE188823C685E616");

                entity.ToTable("Audit_SiteAccident_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteDeclarationInformation>(entity =>
            {
                entity.HasKey(e => e.AuditChoureyOneId).HasName("PK__Audit_Si__D161454CE399E73C");

                entity.ToTable("Audit_SiteDeclaration_Information");

                entity.Property(e => e.AuditChoureyOneId).HasColumnName("Audit_ChoureyOneId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteInformation>(entity =>
            {
                entity.HasKey(e => e.AuditSiteId).HasName("PK__Audit_Si__485045048FCAF1F3");

                entity.ToTable("Audit_Site_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.ActivateOn).HasColumnType("datetime");
                entity.Property(e => e.BrowseTimeFrom).HasMaxLength(20);
                entity.Property(e => e.BrowseTimeTo).HasMaxLength(20);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Gpsrange).HasColumnName("GPSRange");
                entity.Property(e => e.QrcodeValue)
                    .HasMaxLength(300)
                    .HasColumnName("QRCodeValue");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.SiteName).HasMaxLength(100);
                entity.Property(e => e.TrialExpireOn).HasColumnType("datetime");
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteNewsInformation>(entity =>
            {
                entity.HasKey(e => e.AuditNewsId).HasName("PK__Audit_Si__871ACB8FDF0ACCE4");

                entity.ToTable("Audit_SiteNews_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteNoticeInformation>(entity =>
            {
                entity.HasKey(e => e.AuditNoticeId).HasName("PK__Audit_Si__2B4CEE7DE11FE7AA");

                entity.ToTable("Audit_SiteNotice_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteSafetyInformation>(entity =>
            {
                entity.HasKey(e => e.AuditSafetyId).HasName("PK__Audit_Si__CA4B56C511AFBC64");

                entity.ToTable("Audit_SiteSafety_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditSiteWorkInformation>(entity =>
            {
                entity.HasKey(e => e.AuditWorkId).HasName("PK__Audit_Si__B94CF0AA6021F051");

                entity.ToTable("Audit_SiteWork_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(500);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditUserSiteDeclarationInformation>(entity =>
            {
                entity.HasKey(e => e.AuditUserSiteDeclarationId).HasName("PK__Audit_Us__5E84E4B3D135EAC9");

                entity.ToTable("Audit_UserSiteDeclaration_Information");

                entity.Property(e => e.AuditUserSiteDeclarationId).HasColumnName("Audit_UserSiteDeclarationId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<AuditUserSiteInformation>(entity =>
            {
                entity.HasKey(e => e.AuditUserSiteId).HasName("PK__Audit_Us__7B44AFF286D452FD");

                entity.ToTable("Audit_UserSite_Information");

                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.JoinedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserAddedBy).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<AuditVideoMessageInformation>(entity =>
            {
                entity.HasKey(e => e.AuditVideoId).HasName("PK__Audit_Vi__892A1F99D4027827");

                entity.ToTable("Audit_VideoMessage_Information");

                entity.Property(e => e.AuditVideoId).HasColumnName("Audit_VideoId");
                entity.Property(e => e.ActionDone).HasMaxLength(20);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChangeDetailLog>(entity =>
            {
                entity.HasKey(e => e.ChangeDetailLogId).HasName("PK__ChangeDe__2E451D14B007B60B");

                entity.ToTable("ChangeDetailLog");

                entity.Property(e => e.FkChangeDetailId)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.Property).HasMaxLength(200);
            });

            modelBuilder.Entity<ChangeLog>(entity =>
            {
                entity.HasKey(e => e.ChangeLogId).HasName("PK__ChangeLo__6AD2E8C79FAEE543");

                entity.ToTable("ChangeLog");

                entity.Property(e => e.ChangeLogId)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.ChangedProperties).HasMaxLength(3000);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Ecategory).HasColumnName("ECategory");
                entity.Property(e => e.EchangeCategory).HasColumnName("EChangeCategory");
                entity.Property(e => e.EchangeType).HasColumnName("EChangeType");
                entity.Property(e => e.Id)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.SiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<ChoureyCustomName>(entity =>
            {
                entity.HasKey(e => e.ChoureyCustomNameId).HasName("PK__ChoureyC__378D1DC774AF289C");

                entity.ToTable("ChoureyCustomName");

                entity.Property(e => e.Chourey1).HasMaxLength(300);
                entity.Property(e => e.Chourey1Japanese).HasMaxLength(300);
                entity.Property(e => e.Chourey2).HasMaxLength(300);
                entity.Property(e => e.Chourey2Japanese).HasMaxLength(300);
                entity.Property(e => e.Disaster).HasMaxLength(300);
                entity.Property(e => e.DisasterJapanese).HasMaxLength(300);
                entity.Property(e => e.SaftetyDeclaration).HasMaxLength(300);
                entity.Property(e => e.SaftetyDeclarationJapanese).HasMaxLength(300);
                entity.Property(e => e.SiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<ChoureyMediaComment>(entity =>
            {
                entity.HasKey(e => e.ChoureyMediaCommentId).HasName("PK__ChoureyM__5984D0AA26E00354");

                entity.ToTable("ChoureyMediaComment");

                entity.Property(e => e.Comment).HasMaxLength(800);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Ecategory).HasColumnName("ECategory");
                entity.Property(e => e.EdeviceType).HasColumnName("EDeviceType");
                entity.Property(e => e.EfileType).HasColumnName("EFileType");
                entity.Property(e => e.EuploadType).HasColumnName("EUploadType");
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyOneGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__ChoureyO__CF4F7BB52CE3491F");

                entity.ToTable("ChoureyOneGallery_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyOneInformation>(entity =>
            {
                entity.HasKey(e => e.ChoureyOneId).HasName("PK__ChoureyO__93B335EAA7E0715D");

                entity.ToTable("ChoureyOne_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ViewMode).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ChoureyOnePhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__ChoureyO__21B7B5E24A226CA2");

                entity.ToTable("ChoureyOnePhoto_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyOneVideoInformation>(entity =>
            {
                entity.HasKey(e => e.VideoId).HasName("PK__ChoureyO__BAE5126A9E6CA858");

                entity.ToTable("ChoureyOneVideo_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.ThumbFileName).HasMaxLength(200);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyTwoGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__ChoureyT__CF4F7BB56AB24CD6");

                entity.ToTable("ChoureyTwoGallery_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyTwoInformation>(entity =>
            {
                entity.HasKey(e => e.ChoureyTwoId).HasName("PK__ChoureyT__BED4C3319946DDA4");

                entity.ToTable("ChoureyTwo_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ViewMode).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ChoureyTwoPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__ChoureyT__21B7B5E27E43F2BD");

                entity.ToTable("ChoureyTwoPhoto_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ChoureyTwoVideoInformation>(entity =>
            {
                entity.HasKey(e => e.VideoId).HasName("PK__ChoureyT__BAE5126A8E03E168");

                entity.ToTable("ChoureyTwoVideo_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.ThumbFileName).HasMaxLength(200);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeviceLog>(entity =>
            {
                entity.HasKey(e => e.DeviceLogId).HasName("PK__DeviceLo__04DBE96EC3AD57A4");

                entity.ToTable("DeviceLog");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FkDeviceUniqueId).HasMaxLength(300);
            });

            modelBuilder.Entity<DeviceRegistration>(entity =>
            {
                entity.HasKey(e => e.DeviceRegistrationId).HasName("PK__DeviceRe__6EC249BF00A4E1A9");

                entity.ToTable("DeviceRegistration");

                entity.HasIndex(e => new { e.DeviceUniqueId, e.EdeviceType }, "UC_DeviceUniqueId_EDeviceType").IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "UC_PhoneNumber").IsUnique();

                entity.Property(e => e.CompanyName).HasMaxLength(255);
                entity.Property(e => e.DeviceUniqueId).HasMaxLength(300);
                entity.Property(e => e.EdeviceType).HasColumnName("EDeviceType");
                entity.Property(e => e.FullName).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<DeviceSiteLog>(entity =>
            {
                entity.HasKey(e => e.DeviceSiteLogId).HasName("PK__DeviceSi__E06B6D06EA7262A4");

                entity.ToTable("DeviceSiteLog");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FkDeviceUniqueId).HasMaxLength(300);
                entity.Property(e => e.FkSiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<DeviceSiteMapper>(entity =>
            {
                entity.HasKey(e => e.DeviceSiteMapperId).HasName("PK__DeviceSi__BEC4B3666F4B4555");

                entity.ToTable("DeviceSiteMapper");

                entity.Property(e => e.FkSiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<DisasterGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__Disaster__CF4F7BB589F56143");

                entity.ToTable("DisasterGallery_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DisasterInformation>(entity =>
            {
                entity.HasKey(e => e.DisasterId).HasName("PK__Disaster__B487740ED7F7B0FC");

                entity.ToTable("Disaster_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ViewMode).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DisasterPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__Disaster__21B7B5E27C16B947");

                entity.ToTable("DisasterPhoto_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DisasterVideoInformation>(entity =>
            {
                entity.HasKey(e => e.VideoId).HasName("PK__Disaster__BAE5126A1044F429");

                entity.ToTable("DisasterVideo_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.ThumbFileName).HasMaxLength(200);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeDetailInformation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC072DB1B5DB");

                entity.ToTable("EmployeeDetail_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteNewEmpInfo");
                    tb.HasTrigger("T_InsertNewEmpInfo");
                    tb.HasTrigger("T_UpdateNewEmpInfo");
                });

                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Dob)
                    .HasMaxLength(200)
                    .HasColumnName("DOB");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.EmergencyContactNumber).HasMaxLength(60);
                entity.Property(e => e.FamilyNameChinese)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Chinese");
                entity.Property(e => e.FamilyNameKana)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Kana");
                entity.Property(e => e.FamilyNameRoman)
                    .HasMaxLength(300)
                    .HasColumnName("FamilyName_Roman");
                entity.Property(e => e.Gender).HasMaxLength(20);
                entity.Property(e => e.GivenNameChinese)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Chinese");
                entity.Property(e => e.GivenNameKana)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Kana");
                entity.Property(e => e.GivenNameRoman)
                    .HasMaxLength(300)
                    .HasColumnName("GivenName_Roman");
                entity.Property(e => e.MobileNumber).HasMaxLength(60);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(300)
                    .HasColumnName("phoneNumber");
                entity.Property(e => e.PhotoPath).HasMaxLength(300);
                entity.Property(e => e.Photoname)
                    .HasMaxLength(300)
                    .HasColumnName("photoname");
                entity.Property(e => e.Postbox).HasMaxLength(200);
                entity.Property(e => e.Prefecture).HasMaxLength(200);
                entity.Property(e => e.RoleName).HasMaxLength(20);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<ErrorMessageInformation>(entity =>
            {
                entity.HasKey(e => e.ErrorId).HasName("PK__ErrorMes__35856A2A2D6B7CE6");

                entity.ToTable("ErrorMessage_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteErrorMessage_Information");
                    tb.HasTrigger("T_NewErrorMessage_Information");
                    tb.HasTrigger("T_UpdateErrorMessage_Information");
                });

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(100);
                entity.Property(e => e.MethodName).HasMaxLength(100);
                entity.Property(e => e.OperationDoneBy).HasMaxLength(100);
                entity.Property(e => e.SolvedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.HasKey(e => e.FileUploadId).HasName("PK__FileUplo__6F30D8F6504ADA61");

                entity.ToTable("FileUpload");

                entity.Property(e => e.ActualFileName).HasMaxLength(200);
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.CustomName).HasMaxLength(200);
                entity.Property(e => e.Ecategory).HasColumnName("ECategory");
                entity.Property(e => e.EfileType).HasColumnName("EFileType");
                entity.Property(e => e.Estatus).HasColumnName("EStatus");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(255);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ViewMode).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<LanguageInformation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("Language_Information");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");
                entity.Property(e => e.LanguageName).HasMaxLength(100);
                entity.Property(e => e.LanguageNameEnglish)
                    .HasMaxLength(100)
                    .HasColumnName("LanguageName_English");
            });

            modelBuilder.Entity<LoginDetailInformation>(entity =>
            {
                entity.HasKey(e => e.LoginId).HasName("PK__LoginDet__4DDA2818B99D2467");

                entity.ToTable("LoginDetail_Information");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.DeviceId).HasMaxLength(300);
                entity.Property(e => e.DeviceName).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);
                entity.Property(e => e.ContextKey).HasMaxLength(300);
                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<NewsGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__NewsGall__CF4F7BB52C225616");

                entity.ToTable("NewsGallery_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteNewsGallery_Information");
                    tb.HasTrigger("T_InsertNewsGallery_Information");
                    tb.HasTrigger("T_UpdateNewsGallery_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.News).WithMany(p => p.NewsGalleryInformations)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsGallery_Information_SiteNews_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.NewsGalleryInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsGallery_Information_Site_Information");
            });

            modelBuilder.Entity<NewsPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__NewsPhot__21B7B5E21B6C94A0");

                entity.ToTable("NewsPhoto_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteNewsPhoto_Information");
                    tb.HasTrigger("T_InsertNewsPhoto_Information");
                    tb.HasTrigger("T_UpdateNewsPhoto_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Gallery).WithMany(p => p.NewsPhotoInformations)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_NewsPhoto_Information_NewsGallery_Information");

                entity.HasOne(d => d.News).WithMany(p => p.NewsPhotoInformations)
                    .HasForeignKey(d => d.NewsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsPhoto_Information_SiteNews_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.NewsPhotoInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NewsPhoto_Information_Site_Information");
            });

            modelBuilder.Entity<NoticeGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__NoticeGa__CF4F7BB5A483EE85");

                entity.ToTable("NoticeGallery_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteNoticeGallery_Information");
                    tb.HasTrigger("T_InsertNoticeGallery_Information");
                    tb.HasTrigger("T_UpdateNoticeGallery_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Notice).WithMany(p => p.NoticeGalleryInformations)
                    .HasForeignKey(d => d.NoticeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoticeGallery_Information_SiteNotice_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.NoticeGalleryInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoticeGallery_Information_Site_Information");
            });

            modelBuilder.Entity<NoticePhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__NoticePh__21B7B5E263D96D66");

                entity.ToTable("NoticePhoto_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteNoticePhoto_Information");
                    tb.HasTrigger("T_InsertNoticePhoto_Information");
                    tb.HasTrigger("T_UpdateNoticePhoto_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Gallery).WithMany(p => p.NoticePhotoInformations)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_NoticePhoto_Information_NoticeGallery_Information");

                entity.HasOne(d => d.Notice).WithMany(p => p.NoticePhotoInformations)
                    .HasForeignKey(d => d.NoticeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoticePhoto_Information_SiteNotice_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.NoticePhotoInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoticePhoto_Information_Site_Information");
            });

            modelBuilder.Entity<PublicUserboardInformation>(entity =>
            {
                entity.HasKey(e => e.PublicUserboardId).HasName("PK__PublicUs__4D72FCDEB35BBC74");

                entity.ToTable("PublicUserboard_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Title).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<PublicUserboardPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__PublicUs__21B7B5E2606D41CD");

                entity.ToTable("PublicUserboardPhoto_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(300);
                entity.Property(e => e.FilePath).HasMaxLength(300);
                entity.Property(e => e.PhotoName).HasMaxLength(300);
                entity.Property(e => e.PublicUserboardInformationId).HasColumnName("PublicUserboard_informationId");
            });

            modelBuilder.Entity<QrcodeInformation>(entity =>
            {
                entity.HasKey(e => e.Qrid).HasName("PK__QRCode_I__D8E9E698082F9E33");

                entity.ToTable("QRCode_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteQRCode_Information");
                    tb.HasTrigger("T_InsertQRCode_Information");
                    tb.HasTrigger("T_UpdateQRCode_Information");
                });

                entity.Property(e => e.Qrid).HasColumnName("QRId");
                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.QrcodeValue).HasColumnName("QRCodeValue");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.QrcodeInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QRCode_Information_Site_Information");
            });

            modelBuilder.Entity<Qrlog>(entity =>
            {
                entity.HasKey(e => e.QrlogId).HasName("PK__QRLog__21CDF70BFE15C0F5");

                entity.ToTable("QRLog");

                entity.Property(e => e.QrlogId).HasColumnName("QRLogId");
                entity.Property(e => e.EntryTime).HasColumnType("datetime");
                entity.Property(e => e.SiteId)
                    .HasMaxLength(300)
                    .HasColumnName("siteId");
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<SafetyGalleryInformation>(entity =>
            {
                entity.HasKey(e => e.GalleryId).HasName("PK__SafetyGa__CF4F7BB520E1FF45");

                entity.ToTable("SafetyGallery_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.GalleyName).HasMaxLength(100);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Safety).WithMany(p => p.SafetyGalleryInformations)
                    .HasForeignKey(d => d.SafetyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyGallery_Information_SiteSafety_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.SafetyGalleryInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyGallery_Information_Site_Information");
            });

            modelBuilder.Entity<SafetyPhotoInformation>(entity =>
            {
                entity.HasKey(e => e.PhotoId).HasName("PK__SafetyPh__21B7B5E27B14194A");

                entity.ToTable("SafetyPhoto_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FilePath).HasMaxLength(200);
                entity.Property(e => e.PhotoName).HasMaxLength(200);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(200);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Gallery).WithMany(p => p.SafetyPhotoInformations)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_SafetyPhoto_Information_SafetyGallery_Information");

                entity.HasOne(d => d.Safety).WithMany(p => p.SafetyPhotoInformations)
                    .HasForeignKey(d => d.SafetyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyPhoto_Information_SiteSafety_Information");

                entity.HasOne(d => d.Site).WithMany(p => p.SafetyPhotoInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SafetyPhoto_Information_Site_Information");
            });

            modelBuilder.Entity<SiteAccessInformation>(entity =>
            {
                entity.HasKey(e => e.SiteAccessId).HasName("PK__Site_Acc__31704471179C76A2");

                entity.ToTable("Site_Access_Information");

                entity.Property(e => e.SiteAccessId).HasColumnName("siteAccessId");
                entity.Property(e => e.JoinedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteAccessGivenTo).HasMaxLength(300);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<SiteAccidentInformation>(entity =>
            {
                entity.HasKey(e => e.AccidentId).HasName("PK__SiteAcci__8133DEAFD9AEA3DC");

                entity.ToTable("SiteAccident_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSiteAccident_Information");
                    tb.HasTrigger("T_InsertSiteAccident_Information");
                    tb.HasTrigger("T_UpdateSiteAccident_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteAccidentInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteAccident_Information_Site_Information");
            });

            modelBuilder.Entity<SiteCodeAccessInformation>(entity =>
            {
                entity.HasKey(e => e.SiteCodeId).HasName("PK__SiteCode__3E4AD439E31A3DA1");

                entity.ToTable("SiteCodeAccess_Information");

                entity.HasIndex(e => e.SiteCode, "UQ__SiteCode__A12A3367688F31D5").IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.OccupiedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteCode).HasMaxLength(300);
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<SiteDeclarationDeviceRegistrationMapper>(entity =>
            {
                entity.HasKey(e => e.SiteDeclarationDeviceRegistrationMapperId).HasName("PK__SiteDecl__C20523642A7D0F03");

                entity.ToTable("SiteDeclarationDeviceRegistrationMapper");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.DeviceUniqueId).HasMaxLength(300);
                entity.Property(e => e.Estatus).HasColumnName("EStatus");
            });

            modelBuilder.Entity<SiteDeclarationInformation>(entity =>
            {
                entity.HasKey(e => e.SiteDeclarationId).HasName("PK__SiteDecl__EB3ADD99CB93CEA5");

                entity.ToTable("SiteDeclaration_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
                entity.Property(e => e.ViewMode).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SiteInformation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Site_Inf__3214EC07FF2DCD3F");

                entity.ToTable("Site_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSite_Information");
                    tb.HasTrigger("T_InsertSite_Information");
                    tb.HasTrigger("T_UpdateSite_Information");
                });

                entity.HasIndex(e => e.SiteName, "UQ__Site_Inf__686162EF50B531E3").IsUnique();

                entity.HasIndex(e => e.QrCodeValue, "UQ__Site_Inf__ED88D331DC0114A0").IsUnique();

                entity.Property(e => e.Id).HasMaxLength(300);
                entity.Property(e => e.ActivateOn).HasColumnType("datetime");
                entity.Property(e => e.BrowseTimeFrom).HasMaxLength(20);
                entity.Property(e => e.BrowseTimeTo).HasMaxLength(20);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(100);
                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.Gpsrange).HasColumnName("GPSRange");
                entity.Property(e => e.QrCodeValue).HasMaxLength(300);
                entity.Property(e => e.SiteName).HasMaxLength(100);
                entity.Property(e => e.TrialExpireOn).HasColumnType("datetime");
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SiteNewsInformation>(entity =>
            {
                entity.HasKey(e => e.NewsId).HasName("PK__SiteNews__954EBDF3DBFE4449");

                entity.ToTable("SiteNews_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSiteNews_Information");
                    tb.HasTrigger("T_InsertSiteNews_Information");
                    tb.HasTrigger("T_UpdateSiteNews_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteNewsInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteNews_Information_Site_Information");
            });

            modelBuilder.Entity<SiteNoticeInformation>(entity =>
            {
                entity.HasKey(e => e.NoticeId).HasName("PK__SiteNoti__CE83CBE5A1B3589D");

                entity.ToTable("SiteNotice_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSiteNotice_Information");
                    tb.HasTrigger("T_InsertSiteNotice_Information");
                    tb.HasTrigger("T_UpdateSiteNotice_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteNoticeInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteNotice_Information_Site_Information");
            });

            modelBuilder.Entity<SiteSafetyInformation>(entity =>
            {
                entity.HasKey(e => e.SafetyId).HasName("PK__SiteSafe__1BC90E197D7DB76C");

                entity.ToTable("SiteSafety_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSiteSafety_Information");
                    tb.HasTrigger("T_InsertSiteSafety_Information");
                    tb.HasTrigger("T_UpdateSiteSafety_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteSafetyInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteSafety_Information_Site_Information");
            });

            modelBuilder.Entity<SiteSpaceAggregate>(entity =>
            {
                entity.HasKey(e => e.SiteSpaceUsedId).HasName("PK__SiteSpac__50C0DB4C1D50F58F");

                entity.ToTable("SiteSpaceAggregate");

                entity.Property(e => e.Estatus).HasColumnName("EStatus");
                entity.Property(e => e.SiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<SiteSpaceDetail>(entity =>
            {
                entity.HasKey(e => e.SiteSpaceDetailId).HasName("PK__SiteSpac__80AB2F149EFE5EA8");

                entity.ToTable("SiteSpaceDetail");

                entity.Property(e => e.Ecategory).HasColumnName("ECategory");
                entity.Property(e => e.Estatus).HasColumnName("EStatus");
                entity.Property(e => e.EuploadType).HasColumnName("EUploadType");
                entity.Property(e => e.SiteId).HasMaxLength(300);
            });

            modelBuilder.Entity<SiteWorkInformation>(entity =>
            {
                entity.HasKey(e => e.WorkId).HasName("PK__SiteWork");

                entity.ToTable("SiteWork_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteSiteWork_Information");
                    tb.HasTrigger("T_InsertSiteWork_Information");
                    tb.HasTrigger("T_UpdateSiteWork_Information");
                });

                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteWorkInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteWork_Information_Site_Information");
            });

            modelBuilder.Entity<UserInformation>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC074A5537D1");

                entity.ToTable("UserInformation");

                entity.Property(e => e.CompanyName).HasMaxLength(255);
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.SiteId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.SiteName).HasMaxLength(255);
                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<UserSiteDeclarationInformation>(entity =>
            {
                entity.HasKey(e => e.UserSiteDeclarationId).HasName("PK__UserSite__8AD1ABF17E072F99");

                entity.ToTable("UserSiteDeclaration_Information");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);
            });

            modelBuilder.Entity<UserSiteInformation>(entity =>
            {
                entity.HasKey(e => e.UserSiteId).HasName("PK__UserSite__5C85C85F94AB1DB8");

                entity.ToTable("UserSite_Information", tb =>
                {
                    tb.HasTrigger("T_DeleteUserSite_Information");
                    tb.HasTrigger("T_InsertNewUser_Site_Information");
                    tb.HasTrigger("T_UpdateUserSite_Information");
                });

                entity.Property(e => e.JoinedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.UserAddedBy).HasMaxLength(300);
                entity.Property(e => e.UserId).HasMaxLength(300);

                entity.HasOne(d => d.Site).WithMany(p => p.UserSiteInformations)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSite_Information_Site_Information");
            });

            modelBuilder.Entity<VideoMessageInformation>(entity =>
            {
                entity.HasKey(e => e.VideoId).HasName("PK__VideoMes__BAE5126A0F702059");

                entity.ToTable("VideoMessage_Information");

                entity.Property(e => e.CreatedBy).HasMaxLength(300);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.SiteId).HasMaxLength(300);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(300);
                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

        }

    }
}