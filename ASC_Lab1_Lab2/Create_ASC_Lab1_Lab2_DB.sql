IF DB_ID(N'ASC_Lab1_Lab2_DB') IS NULL
BEGIN
    CREATE DATABASE [ASC_Lab1_Lab2_DB];
END
GO

PRINT N'ASC_Lab1_Lab2_DB has been created or already exists.';
PRINT N'Run the ASP.NET Core application to create tables and seed full data automatically.';
GO
