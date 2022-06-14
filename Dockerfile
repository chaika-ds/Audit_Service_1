FROM theharbor.xyz/docker-images/dotnet/aspnet:6.0 AS base
WORKDIR /app

ARG PORT=""
ENV ASPNETCORE_URLS=http://+:${PORT}

FROM theharbor.xyz/docker-images/dotnet/sdk:6.0 AS build

ARG SRCDIR="src"
ARG APPNAME=""
ARG APPATH=""

COPY ${SRCDIR}/ /src/
COPY ["nuget.config", ""]
WORKDIR /src

RUN dotnet restore "${APPATH}${APPNAME}/${APPNAME}.csproj"
RUN dotnet publish "/src/${APPATH}${APPNAME}/${APPNAME}.csproj" -c Release -o /app/publish --no-restore
RUN echo "#!/bin/bash\nset -e\nruncmd=\"dotnet ${APPNAME}.dll\"\nexec \$runcmd" > /app/publish/entrypoint.sh && chmod +x /app/publish/entrypoint.sh

FROM base AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80/tcp
ENTRYPOINT ["./entrypoint.sh"]