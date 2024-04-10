FROM okteto/dotnetcore:8 AS dev

RUN apt-get update && \
    apt-get install -y procps zip && \
    curl -L "https://data.services.jetbrains.com/products/download?code=RRD&platform=linux64" -o /tmp/RiderRemoteDebugger.zip && \
    mkdir -p /root/.local/share/JetBrains/RiderRemoteDebugger/2024.1 && \
    unzip /tmp/RiderRemoteDebugger.zip -d ~/.local/share/JetBrains/RiderRemoteDebugger/2024.1 && \
    curl -L "https://download.jetbrains.com/rider/ssh-remote-debugging/linux-x64/jetbrains_debugger_agent_20230319.24.0" -o /usr/local/bin/debugger && \
    chmod +x /usr/local/bin/debugger && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /src
    
FROM dev as build   

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build -c Release -o /app
RUN dotnet publish  -c Release -o /app

####################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS prod

WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "helloworld.dll"]
