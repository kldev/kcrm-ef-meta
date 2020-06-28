# Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
param ($NAME)

if (!$NAME) {
	Write-Host "Migration name is required"
	exit -1
}

Write-Host "Migration name $NAME"
cd ../KCrm

dotnet ef  migrations add $NAME -o Migrations/Users -c KCrm.Data.Context.AppUserContext -p KCrm.Data --startup-project KCrm.Server.Api
