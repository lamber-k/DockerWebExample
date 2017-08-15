FROM microsoft/aspnetcore:2.0
ARG source
ENV ASPNETCORE_URLS="http://+:8080"
WORKDIR /app
EXPOSE 8080
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "BooksWebAPI.dll"]
