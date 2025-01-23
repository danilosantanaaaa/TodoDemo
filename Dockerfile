FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/TodoApp.Web/TodoApp.Web.csproj", "TodoApp.Web/"]
COPY ["src/TodoApp.Application/TodoApp.Application.csproj", "TodoApp.Application/"]
COPY ["src/TodoApp.Domain/TodoApp.Domain.csproj", "TodoApp.Domain/"]
COPY ["src/TodoApp.Infrastructure/TodoApp.Infrastructure.csproj", "TodoApp.Infrastructure/"]
RUN dotnet restore "TodoApp.Web/TodoApp.Web.csproj"
COPY . ../
WORKDIR /src/TodoApp.Web
RUN dotnet build "TodoApp.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "TodoApp.Web.dll"]