#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
ARG dotnet_configuration=Release
ENV DOTNET_CONFIGURATION=${dotnet_configuration}
WORKDIR /project
COPY . .
RUN ls -la
RUN dotnet restore *.sln

FROM base AS build
RUN dotnet build *.sln -c ${DOTNET_CONFIGURATION} -o ./bin

FROM build AS publish
RUN dotnet publish ./dotnet/WebApp/WebApp.csproj -c ${DOTNET_CONFIGURATION} -o ./publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS app
WORKDIR /app
COPY --from=publish /project/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "JCystems.MSLearn.Observability.WebApp.dll"]
