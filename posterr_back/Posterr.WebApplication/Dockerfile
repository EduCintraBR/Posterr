# Use the official ASP.NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Posterr.WebApplication/Posterr.WebApplication.csproj", "Posterr.WebApplication/"]
COPY ["Posterr.Application/Posterr.Application.csproj", "Posterr.Application/"]
COPY ["Posterr.Repository/Posterr.Repository.csproj", "Posterr.Repository/"]
COPY ["Posterr.Shared/Posterr.Domain.csproj", "Posterr.Shared/"]
RUN dotnet restore "./Posterr.WebApplication/Posterr.WebApplication.csproj"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
COPY . .
WORKDIR "/src/Posterr.WebApplication"
RUN dotnet build "./Posterr.WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Posterr.WebApplication.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posterr.WebApplication.dll"]