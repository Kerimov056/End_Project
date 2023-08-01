##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
##Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
##For more information, please see https://aka.ms/containercompat
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["src/Presentation/EndProject.API/EndProject.API.csproj", "src/Presentation/EndProject.API/"]
#COPY ["src/Infrastructure/EndProjet.Persistance/EndProjet.Persistance.csproj", "src/Infrastructure/EndProjet.Persistance/"]
#COPY ["src/Core/EndProject.Application/EndProject.Application.csproj", "src/Core/EndProject.Application/"]
#COPY ["src/Core/EndProject.Domain/EndProject.Domain.csproj", "src/Core/EndProject.Domain/"]
#COPY ["src/Infrastructure/EndProject.Infrastructure/EndProject.Infrastructure.csproj", "src/Infrastructure/EndProject.Infrastructure/"]
#RUN dotnet restore "src/Presentation/EndProject.API/EndProject.API.csproj"
#COPY . .
#WORKDIR "/src/src/Presentation/EndProject.API"
#RUN dotnet build "EndProject.API.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "EndProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "EndProject.API.dll"]


#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Use Windows-based .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0-windowsservercore-ltsc2022 AS build

WORKDIR /src
COPY ["src/Presentation/EndProject.API/EndProject.API.csproj", "src/Presentation/EndProject.API/"]
#COPY ["src/Infrastructure/EndProjet.Persistance/EndProjet.Persistance.csproj", "src/Infrastructure/EndProjet.Persistance/"]
#COPY ["src/Core/EndProject.Application/EndProject.Application.csproj", "src/Core/EndProject.Application/"]
#COPY ["src/Core/EndProject.Domain/EndProject.Domain.csproj", "src/Core/EndProject.Domain/"]
#COPY ["src/Infrastructure/EndProject.Infrastructure/EndProject.Infrastructure.csproj", "src/Infrastructure/EndProject.Infrastructure/"]

RUN dotnet restore "src/Presentation/EndProject.API/EndProject.API.csproj"

COPY . .
WORKDIR "/src/src/Presentation/EndProject.API"

# Use Windows-based .NET Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS publish
WORKDIR "/src/src/Presentation/EndProject.API"
RUN dotnet publish "EndProject.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use Windows-based ASP.NET Core runtime image for final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EndProject.API.dll"]
