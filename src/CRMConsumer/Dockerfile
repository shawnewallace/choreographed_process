FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY CRMConsumer/CRMConsumer.csproj CRMConsumer/
COPY Core/Core.csproj Core/
COPY Services/Services.csproj Services/
RUN dotnet restore "CRMConsumer/CRMConsumer.csproj"
COPY . .
WORKDIR "/src/CRMConsumer"
RUN dotnet build "CRMConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRMConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRMConsumer.dll"]
