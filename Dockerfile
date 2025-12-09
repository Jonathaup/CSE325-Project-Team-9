# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["RecipeManager/RecipeManager.csproj", "RecipeManager/"]
RUN dotnet restore "RecipeManager/RecipeManager.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/RecipeManager"
RUN dotnet build "RecipeManager.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "RecipeManager.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a directory for the database
RUN mkdir -p /app/data

# Configure ASP.NET to listen on port 8080 (Required by Render)
ENV ASPNETCORE_URLS=http://+:8080

# OVERRIDE the connection string to use the persistent folder
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/App.db"

EXPOSE 8080
ENTRYPOINT ["dotnet", "RecipeManager.dll"]