FROM mcr.microsoft.com/dotnet/framework/runtime:4.8
WORKDIR /app
COPY ./SimpleConsoleApp/bin/Debug .
ENTRYPOINT ["SimpleConsoleApp.exe"]