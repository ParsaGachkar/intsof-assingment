#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Intsoft.Exam.API/Intsoft.Exam.API.csproj", "src/Intsoft.Exam.API/"]
COPY ["src/Intsof.Exam.EfCore/Intsof.Exam.EfCore.csproj", "src/Intsof.Exam.EfCore/"]
COPY ["src/Intsof.Exam.Domain/Intsof.Exam.Domain.csproj", "src/Intsof.Exam.Domain/"]
RUN dotnet restore "src/Intsoft.Exam.API/Intsoft.Exam.API.csproj"
COPY . .
WORKDIR "/src/src/Intsoft.Exam.API"
RUN dotnet build "Intsoft.Exam.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Intsoft.Exam.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Intsoft.Exam.API.dll"]