FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dev
WORKDIR /src

RUN apt-get update && apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /usr/local/bin/vsdbg

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app
RUN dotnet publish  -c Release -o /app

###############################################################

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS prod

WORKDIR /app
COPY --from=dev /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "helloworld.dll"]
