#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["GroupBuy/GroupBuy.csproj", "GroupBuy/"]
RUN dotnet restore "GroupBuy/GroupBuy.csproj"
COPY . .
WORKDIR "/src/GroupBuy"
RUN dotnet build "GroupBuy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GroupBuy.csproj" -c Release -o /app/publishC:\Users\coleg\source\repos\GroupBuy\GroupBuy\Startup.cs

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GroupBuy.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
RUN dotnet tool install -g dotnet-ef --version 3.1.1
RUN export PATH="$PATH:/root/.dotnet/tools"