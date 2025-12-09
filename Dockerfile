# --- Base image ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Important: create the data folder for the mounted disk
RUN mkdir -p /app/data

# --- Build image ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy source
COPY . .

# Restore + Build
RUN dotnet restore "RecipeManager/RecipeManager.csproj"
RUN dotnet build "RecipeManager/RecipeManager.csproj" -c Release -o /app/build

# --- Publish image ---
FROM build AS publish
RUN dotnet publish "RecipeManager/RecipeManager.csproj" -c Release -o /app/publish

# --- Final container ---
FROM base AS final
WORKDIR /app

# Copy the published output
COPY --from=publish /app/publish .

# DO NOT copy App.db → it must live only in /app/data (mounted disk)
# Render will attach the disk on top of this folder

ENTRYPOINT ["dotnet", "RecipeManager.dll"]
