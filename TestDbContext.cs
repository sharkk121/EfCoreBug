using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreBug
{
    internal class TestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostsConfiguration).Assembly);
        }
    }

    internal class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(x => x.Id);

            builder
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .UsingEntity<PostTag>(
                    l => l
                        .HasOne(x => x.Tag)
                        .WithMany(x => x.PostTags)
                        //.HasForeignKey(x => x.TagId)
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade),
                    r => r
                        .HasOne(x => x.Post)
                        .WithMany(x => x.PostTags)
                        //.HasForeignKey(x => x.PostId)
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Restrict),
                    j => j.ToTable("PostTags"));
        }
    }

    internal class TagsConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(x => x.Id);
        }
    }
}
