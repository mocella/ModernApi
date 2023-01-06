# ModernApi
.NET6 API with Docker, Mediatr, FluentValidation, EF Core

### Running via Docker:
- cd ModernApi
- `docker build -t modern-api -f Dockerfile --build-arg VERSION="1.2.3.4" ..`
- `docker run -p 8080:8080 -e Logging__Loglevel__Default=Debug -e Logging__Loglevel__Microsoft.AspNetCore=Debug modern-api`
- open browser to http://localhost:8080/healthz to make sure the site is running properly.
- notes: 
  - healthcheck endpoint will show unhealthy status when running in container, as it's expecting localhost.... 
 