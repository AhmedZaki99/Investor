BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Vendors].[VendorId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[UnitConversions].[UnitConversionId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[ScaleUnits].[ScaleUnitId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Products].[ProductId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Invoices].[InvoiceId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[InvoiceItems].[InvoiceItemId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Customers].[CustomerId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Contacts].[ContactId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Categories].[CategoryId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[BusinessTypes].[BusinessTypeId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Businesses].[BusinessId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Bills].[BillId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[BillItems].[BillItemId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Addresses].[AddressId]', N'Id', N'COLUMN';
GO

EXEC sp_rename N'[Accounts].[AccountId]', N'Id', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825191659_RenameModelsIds', N'6.0.8');
GO

COMMIT;
GO

