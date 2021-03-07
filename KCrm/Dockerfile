FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build

COPY KCrm.Core/*.csproj KCrm.Core/ 
RUN  cd KCrm.Core && dotnet restore

COPY KCrm.Data/*.csproj KCrm.Data/ 
RUN  cd KCrm.Data && dotnet restore

COPY KCrm.Aegis.Migrator/*.csproj KCrm.Aegis.Migrator/
RUN cd KCrm.Aegis.Migrator && dotnet restore

COPY KCrm.Logic/*.csproj KCrm.Logic/ 
RUN  cd KCrm.Logic && dotnet restore

COPY KCrm.Data.Aegis/*.csproj KCrm.Data.Aegis/ 
RUN  cd KCrm.Data.Aegis && dotnet restore

COPY KCrm.Server.Api/*.csproj KCrm.Server.Api/ 
RUN cd KCrm.Server.Api && dotnet restore

COPY . .
RUN ls -la

RUN cd KCrm.Server.Api && dotnet publish -c release -o /build/out
RUN ls -la /build/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app
COPY --from=build /build/out .
RUN ls -la
ENV ASPNETCORE_ENVIRONMENT Docker 
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
ENTRYPOINT ["dotnet", "KCrm.Server.Api.dll"]
