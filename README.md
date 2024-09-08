# Banking and Transaction APIs Deployment Guide

## Prerequisites
- Docker
- Docker Compose

## Services
- `bankingaccountapi`: API for managing banking accounts and customers.
- `transactionapi`: API for handling transactions with Hangfire background processing.

## Deployment Steps

1. **Clone the Repository**
   ```sh
   git clone <repository-url>
   cd <repository-directory>
   ```

2. **Build and Run Docker Containers**
   ```sh
   docker-compose up --build
   ```

3. **Access the APIs**
   - `bankingaccountapi`: [http://localhost:4040](http://localhost:4040)
   - `transactionapi`: [http://localhost:4041](http://localhost:4041)

4. **Access Swagger UI**
   - `bankingaccountapi`: [http://localhost:4040/swagger](http://localhost:4040/swagger)
   - `transactionapi`: [http://localhost:4041/swagger](http://localhost:4041/swagger)

5. **Access Hangfire Dashboard**
   - `transactionapi`: [http://localhost:4041/hangfire](http://localhost:4041/hangfire)

## Notes
- Ensure that the `Microsoft.EntityFrameworkCore.Sqlite` package is installed in your project.
- The volume in `docker-compose.yml` ensures that the SQLite database file persists across container restarts.
