FROM theharbor.xyz/docker-images/dotnet/aspnet:6.0 AS base
WORKDIR /app

ARG PORT=""
ENV ASPNETCORE_URLS=http://+:${PORT}

FROM theharbor.xyz/docker-images/dotnet/sdk:6.0 AS build

ARG SRCDIR="src"
ARG APP_NAME=""
ARG APPATH=""

COPY ${SRCDIR}/ /src/
COPY ["nuget.config", ""]
WORKDIR /src

RUN dotnet restore "${APPATH}${APP_NAME}/${APP_NAME}.csproj"
RUN dotnet publish "/src/${APPATH}${APP_NAME}/${APP_NAME}.csproj" -c Release -o /app/publish --no-restore
RUN echo "#!/bin/bash\nset -e\nruncmd=\"dotnet ${APP_NAME}.dll\"\nexec \$runcmd" > /app/publish/entrypoint.sh && chmod +x /app/publish/entrypoint.sh

FROM base AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80/tcp
ENTRYPOINT ["./entrypoint.sh"]