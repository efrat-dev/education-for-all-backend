FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 10000
ENV PORT=10000
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project/Project.csproj", "Project/"]
RUN dotnet restore "Project/Project.csproj"
COPY . .
WORKDIR "/src/Project"
RUN dotnet build "Project.csproj" -c Release -o /app/build
RUN dotnet publish "Project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Project.dll"]
