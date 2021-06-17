#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
#COPY ["*.crt", "/usr/local/share/ca-certificates/"]
#RUN /usr/sbin/update-ca-certificates
WORKDIR /src
COPY ["AksDemo.csproj", "AksDemo/"]
RUN dotnet restore "AksDemo/AksDemo.csproj"
COPY . AksDemo/.
WORKDIR "/src/AksDemo"
RUN dotnet build "AksDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AksDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AksDemo.dll"]