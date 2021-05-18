U folder-u Vjezba.DAL:

dotnet ef migrations add WorkingExp --startup-project ../Vjezba.Web --context ClientManagerDbContext
dotnet ef database update --startup-project ../Vjezba.Web --context ClientManagerDbContext