#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 2222

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./src/ToDoApp.UI/ToDoApp.UI.csproj", "src/ToDoApp.UI/"]
RUN dotnet restore "src/ToDoApp.UI/ToDoApp.UI.csproj"
COPY . .
WORKDIR "/src/src/ToDoApp.UI"
RUN dotnet build "ToDoApp.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.UI.csproj" -c Release -o /app/publish -r linux-x64 --self-contained

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ssh
ENV SSH_PASSWD "root:Docker!"
RUN apt-get update \
        && apt-get install -y --no-install-recommends dialog \
        && apt-get update \
  && apt-get install -y --no-install-recommends openssh-server \
  && echo "$SSH_PASSWD" | chpasswd 

COPY ["./src/ToDoApp.UI/sshd_config","/etc/ssh/"]
COPY ["./src/ToDoApp.UI/init.sh","/usr/local/bin/"]
RUN chmod 777 /usr/local/bin/init.sh
RUN chmod +x /usr/local/bin/init.sh
#ENTRYPOINT ["init.sh"]
ENTRYPOINT ["dotnet", "ToDoApp.UI.dll"]