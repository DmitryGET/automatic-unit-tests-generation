FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
ADD src/. .
RUN dotnet restore SampleProject.API.sln
RUN dotnet publish -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 3000

CMD ["dotnet", "SampleProject.API.dll", "--urls=http://0.0.0.0:3000"]