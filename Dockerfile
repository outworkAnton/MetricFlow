FROM microsoft/dotnet:sdk
WORKDIR /app

COPY API/MetricFlow.WebApi/bin/Release/netcoreapp2.2/publish /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://localhost:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "MetricFlow.WebApi.dll"]