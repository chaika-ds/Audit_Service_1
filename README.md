# Audit Service



## Общие сведения

Все базовую информацию и аналитику сервиса можно узнать в [Confluence](https://confluence.platform.live/pages/viewpage.action?pageId=63984679).

 ----

## Установка проекта в контейнере Docker


1. Откройте PowerShell
2. Перейдите в корневую папку проекта, где лежит файл **docker-compose.yml**
3. Выполнить команду
   ``` powershell
   docker-compose --env-file ./config/docker.development.env  up -d
   ```
4. В Docker Desktop должен появиться новый контейнер с названием **audit-service-p2**, он будет включать в себя все необходимые сервисы для работы, например: ElasticSearch, Kibana, Redis и тд.
5. По умолчанию AuditService размещается на порту 3000, но его можно изменить в **docker-compose.yml**



> Примечание: Если при запуске ElasticSearch в контейнере, будет ошибка
> ```` powershell
> [2] bootstrap checks failed
> [1]: max file descriptors [4096] for elasticsearch process is too low, increase to at least [65535]
> [2]: max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]
>````
> То нужно будет выполнить следующий скрипт в PowerShell
> ```` powershell
> 1. wsl -d docker-desktop
> 2. sysctl -w vm.max_map_count=262144
>````


## Локальный запуск генерации данных для ELK


Для того, что бы создать тестовые данные, вы можете запустить "elk-filler" в контейнере, либо запустить консоль локально.


## Создание топиков для KAFKA


1. Откройте PowerShell
2. Перейдите в корневую папку проекта, где лежит файл **docker-compose.yml**
3. Выполнить команду
   ``` powershell
   pwsh\createTopicsForKafka.ps1
   ```
