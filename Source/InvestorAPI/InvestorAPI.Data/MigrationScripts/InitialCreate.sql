IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Brands] (
    [BrandId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [ScaleUnit] nvarchar(255) NOT NULL DEFAULT N'Unit',
    [Description] nvarchar(1023) NULL,
    [BuyPrice] decimal(19,4) NULL,
    [SellPrice] decimal(19,4) NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([BrandId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220727033536_InitialCreate', N'6.0.7');
GO

COMMIT;
GO

