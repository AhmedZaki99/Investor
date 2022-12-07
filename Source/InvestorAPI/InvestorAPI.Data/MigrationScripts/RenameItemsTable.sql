BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Items].[IX_Items_InvoiceId]', N'IX_InvoiceItems_InvoiceId', N'INDEX';
GO

EXEC sp_rename N'[Items].[IX_Items_ProductId]', N'IX_InvoiceItems_ProductId', N'INDEX';
GO

EXEC sp_rename N'[Items]', N'InvoiceItems';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221107191046_RenameItemsTable', N'6.0.9');
GO

COMMIT;
GO

