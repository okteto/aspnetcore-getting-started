FROM okteto/dotnetcore:8 AS dev
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app
RUN dotnet publish  -c Release -o /app

## Needed for Visual Studio Debugger to work
RUN apt-get update && apt-get install -y --no-install-recommends \
    gdb \
    procps \
    && rm -rf /var/lib/apt/lists/*

####################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod

WORKDIR /app
COPY --from=dev /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "helloworld.dll"]
