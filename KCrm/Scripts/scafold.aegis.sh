
dotnet ef  dbcontext scaffold --force Name=AegisConnection  -c AegisContext --schema aegis --context-dir . -o Entity -p KCrm.Data.Aegis --startup-project KCrm.Server.Api  Npgsql.EntityFrameworkCore.PostgreSQL