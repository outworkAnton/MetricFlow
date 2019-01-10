FROM microsoft/dotnet:sdk
COPY . /app

WORKDIR /app/API/MetricFlow.WebApi
RUN ["dotnet", "restore"]

RUN dotnet publish -c Release -o out --no-restore

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://localhost:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "out/MetricFlow.WebApi.dll"]