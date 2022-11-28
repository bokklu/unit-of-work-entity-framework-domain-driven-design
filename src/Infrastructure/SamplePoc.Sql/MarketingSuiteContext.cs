using Microsoft.EntityFrameworkCore;
using SamplePoc.Sql.Entities;

namespace SamplePoc.Sql;

public partial class MarketingSuiteContext : DbContext
{
    public MarketingSuiteContext()
    {
    }

    public MarketingSuiteContext(DbContextOptions<MarketingSuiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<CampaignsKeyword> CampaignsKeywords { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientsCampaign> ClientsCampaigns { get; set; }

    public virtual DbSet<KeywordsPrimary> KeywordsPrimaries { get; set; }

    public virtual DbSet<KeywordsSourcePrimary> KeywordsSourcePrimaries { get; set; }

    public virtual DbSet<SourcePrimary> SourcePrimaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;User ID=SA;Password=Secret1234;Database=MarketingSuite;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.ToTable("Campaign", "Campaigns");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CampaignsKeyword>(entity =>
        {
            entity.ToTable("CampaignsKeywords", "Campaigns");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.KeywordsPrimaryId).HasColumnName("KeywordsPrimaryID");

            entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignsKeywords)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("FK_CampaignsKeywords_Campaign");

            entity.HasOne(d => d.KeywordsPrimary).WithMany(p => p.CampaignsKeywords)
                .HasForeignKey(d => d.KeywordsPrimaryId)
                .HasConstraintName("FK_CampaignsKeywords_KeywordsPrimary");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client", "Clients");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(100);
        });

        modelBuilder.Entity<ClientsCampaign>(entity =>
        {
            entity.ToTable("ClientsCampaigns", "Clients");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");

            entity.HasOne(d => d.Campaign).WithMany(p => p.ClientsCampaigns)
                .HasForeignKey(d => d.CampaignId)
                .HasConstraintName("FK_ClientsCampaigns_Campaign");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsCampaigns)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ClientsCampaigns_Client");
        });

        modelBuilder.Entity<KeywordsPrimary>(entity =>
        {
            entity.ToTable("KeywordsPrimary", "Keywords");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<KeywordsSourcePrimary>(entity =>
        {
            entity.ToTable("KeywordsSourcePrimary", "Keywords");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KeywordsPrimaryId).HasColumnName("KeywordsPrimaryID");
            entity.Property(e => e.PrimarySourceId).HasColumnName("PrimarySourceID");

            entity.HasOne(d => d.KeywordsPrimary).WithMany(p => p.KeywordsSourcePrimaries)
                .HasForeignKey(d => d.KeywordsPrimaryId)
                .HasConstraintName("FK_KeywordsSourcePrimary_KeywordsPrimary");

            entity.HasOne(d => d.PrimarySource).WithMany(p => p.KeywordsSourcePrimaries)
                .HasForeignKey(d => d.PrimarySourceId)
                .HasConstraintName("FK_KeywordsSourcePrimary_SourcePrimary");
        });

        modelBuilder.Entity<SourcePrimary>(entity =>
        {
            entity.ToTable("SourcePrimary", "Keywords");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Url).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
