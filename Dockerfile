# Build Angular front-end
FROM node:22 AS ui-build
WORKDIR /src/WavoProjects.UI/WavOps
COPY WavoProjects.UI/WavOps/package*.json ./
RUN npm ci
COPY WavoProjects.UI/WavOps/ ./
RUN npm run build -- --configuration production

# Build .NET API and integrate static assets
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS api-build
WORKDIR /src
COPY WavoProjects.sln ./
COPY WavoProjects.Api/WavoProjects.Api.csproj WavoProjects.Api/
RUN dotnet restore WavoProjects.Api/WavoProjects.Api.csproj
COPY . .
COPY --from=ui-build /src/WavoProjects.UI/WavOps/dist/wav-ops ./WavoProjects.Api/wwwroot
RUN dotnet publish WavoProjects.Api/WavoProjects.Api.csproj -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
COPY --from=api-build /app/publish ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "WavoProjects.Api.dll"]
