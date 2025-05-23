FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copiar csproj e restaurar dependências
COPY *.csproj .
RUN dotnet restore

# Copiar todo o código e compilar
COPY . .
RUN dotnet publish -c Release -o /app

# Build da imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Criar diretório para o SQLite
RUN mkdir -p /var/lib/data
ENV ConnectionStrings__DefaultConnection="Data Source=/var/lib/data/BAITZ_BLOG_DATABASE.db"

EXPOSE 8080
ENTRYPOINT ["dotnet", "BAITZ_BLOG_API.dll"]