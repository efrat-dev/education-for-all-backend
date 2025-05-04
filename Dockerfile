# שלב בסיס להרצת האפליקציה
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# שלב הבנייה
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# העתק את קובץ הפרויקט
COPY ["Project/Project.csproj", "Project/"]

# שחזור תלויות
RUN dotnet restore "Project/Project.csproj"

# העתק את כל הקוד
COPY . .

# בניית האפליקציה
WORKDIR "/src/Project"
RUN dotnet build "Project.csproj" -c Release -o /app/build

# פרסום
RUN dotnet publish "Project.csproj" -c Release -o /app/publish

# שלב סופי
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Project.dll"]
