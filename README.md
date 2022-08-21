# IdentityEfCore



Add-Migration InitialAppDbMigration -c AppDbContext -o Data/Migrations/IdentityServer/AppDbContextMigration  
Add-Migration InitialPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb  
Add-Migration InitialConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb  

Update-Database -Context AppDbContext  
Update-Database -Context PersistedGrantDbContext  
Update-Database -Context ConfigurationDbContext  
