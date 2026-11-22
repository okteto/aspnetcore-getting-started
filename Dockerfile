FROM mcr.microsoft.com/dotnet/sdk:10.0.404-noble AS dev

RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v 18.7.10521.2 -l /usr/local/bin/vsdbg

WORKDIR /usr/src/app
COPY helloworld.csproj .
# The cache mounts keep the NuGet packages folder in the Okteto Build
# Service, which reuses it between builds. The tests of the Okteto Manifest
# use the same cache
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore
COPY . .
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet build && \
    dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0.15-noble-chiseled AS prod

WORKDIR /app
COPY --from=dev /app .
ENTRYPOINT ["dotnet", "helloworld.dll"]
