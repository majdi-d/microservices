#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices.customersvc/microservices.customersvc.csproj", "microservices.customersvc/"]
RUN dotnet restore "microservices.customersvc/microservices.customersvc.csproj"
COPY . .
WORKDIR "/src/microservices.customersvc"
RUN dotnet build "microservices.customersvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "microservices.customersvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservices.customersvc.dll"]