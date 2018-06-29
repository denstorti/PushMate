FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY PushMate.FcmPushService.API/PushMate.FcmPushService.API.csproj PushMate.FcmPushService.API/
COPY PushMate.FcmPushService/PushMate.FcmPushService.csproj PushMate.FcmPushService/
RUN dotnet restore PushMate.FcmPushService.API/PushMate.FcmPushService.API.csproj
COPY . .
WORKDIR /src/PushMate.FcmPushService.API
RUN dotnet build PushMate.FcmPushService.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PushMate.FcmPushService.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PushMate.FcmPushService.API.dll"]
