IF DB_ID(N'ASC_Lab1_Lab2_DB') IS NOT NULL
BEGIN
    ALTER DATABASE [ASC_Lab1_Lab2_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [ASC_Lab1_Lab2_DB];
END
GO

PRINT N'ASC_Lab1_Lab2_DB has been dropped if it existed.';
PRINT N'Run the ASP.NET Core application again to recreate schema and seed data.';
GO
