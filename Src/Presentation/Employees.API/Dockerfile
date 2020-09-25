#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Src/Presentation/Employees.API/Employees.API.csproj", "Src/Presentation/Employees.API/"]
COPY ["Src/Core/Employees.Application/Employees.Application.csproj", "Src/Core/Employees.Application/"]
COPY ["Src/Core/Employees.Domain/Employees.Domain.csproj", "Src/Core/Employees.Domain/"]
COPY ["Src/Employees.Infrastructure/Employees.Infrastructure.csproj", "Src/Employees.Infrastructure/"]
RUN dotnet restore "Src/Presentation/Employees.API/Employees.API.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/Employees.API"
RUN dotnet build "Employees.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employees.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Employees.API.dll"]