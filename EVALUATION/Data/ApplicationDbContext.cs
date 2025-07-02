using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
     
   public DbSet<Administrateur> _administrateur {get; set;}
 
    public DbSet<Genre> _genre {get; set;}
 
    public DbSet<Coureur> _coureur {get; set;}
    public DbSet<CoureurArrive> _coureur_arrive {get; set;}
    public DbSet<CoureurDepart> _coureur_depart {get; set;}
 
    public DbSet<Equipe> _equipe {get; set;}
 
    public DbSet<CategorieCoureur> _categorie_coureur {get; set;}
 
    public DbSet<Categorie> _categorie {get; set;}
 
    public DbSet<Etape> _etape {get; set;}
 
    public DbSet<EtapeCoureur> _etape_coureur {get; set;}
 
    public DbSet<TempsCoureur> _temps_coureur {get; set;}
 
    public DbSet<PointsCoureur> _points_coureur {get; set;}
 
    public DbSet<Points> _points {get; set;}
 
    public DbSet<EtapeEquipePenalite> _etape_equipe_penalite {get; set;}
    public DbSet<ClassementGeneralView> _classement_general_views {get; set;}
    public DbSet<ClassementGeneralCoefficientEtapeView> _classement_general_coefficient_etape_views {get; set;}
    public DbSet<ClassementGeneralCategorieView> _classement_general_categorie_views {get; set;}

    public DbSet<TempEtape> _temp_etape {get; set;}
    public DbSet<TempPoints> _temp_points {get; set;}
    public DbSet<TempResultat> _temp_resultat {get; set;}
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);
        // Configuration de la séquence pour TypeProduits
        
            modelBuilder.Entity<Administrateur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR administrateur_seq");

            modelBuilder.Entity<Genre>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR genre_seq");

            modelBuilder.Entity<Coureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR coureur_seq");

            modelBuilder.Entity<Equipe>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR equipe_seq");

            modelBuilder.Entity<CategorieCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR categorie_coureur_seq");

            modelBuilder.Entity<Categorie>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR categorie_seq");

            modelBuilder.Entity<Etape>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_seq");

            modelBuilder.Entity<EtapeCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_coureur_seq");

            modelBuilder.Entity<TempsCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR temps_coureur_seq");

            modelBuilder.Entity<PointsCoureur>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR points_coureur_seq");

            modelBuilder.Entity<Points>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR points_seq");

            modelBuilder.Entity<EtapeEquipePenalite>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"NEXT VALUE FOR etape_equipe_penalite_seq");
                
            modelBuilder.Entity<ClassementGeneralView>().HasNoKey();
            modelBuilder.Entity<ClassementGeneralCoefficientEtapeView>().HasNoKey();
            modelBuilder.Entity<ClassementGeneralCategorieView>().HasNoKey();
            modelBuilder.Entity<TempEtape>().HasNoKey();
            modelBuilder.Entity<TempResultat>().HasNoKey();
            modelBuilder.Entity<TempPoints>().HasNoKey();

            modelBuilder.Entity<EtapeCoureur>()
                .HasOne(d => d.Etape)
                .WithMany(e => e.EtapeCoureurs)
                .HasForeignKey(d => d.IdEtape);
                
            modelBuilder.Entity<EtapeCoureur>()
                .HasOne(d => d.Coureur)
                .WithMany(e => e.EtapeCoureurs)
                .HasForeignKey(d => d.IdCoureur);

            modelBuilder.Entity<Coureur>()
                .HasOne(d => d.Genre)
                .WithMany(e => e.Coureurs)
                .HasForeignKey(d => d.IdGenre);

            // modelBuilder.Entity<ClassementGeneralView>()
            //     .HasOne(d => d.Coureur)
            //     .WithMany(e => e.ClassementGeneralViews)
            //     .HasForeignKey(d => d.IdCoureur);
           
    }
}