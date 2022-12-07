BEGIN TRANSACTION;
GO

ALTER TABLE [InvoiceItems] DROP CONSTRAINT [PK_InvoiceItems];
GO

DROP INDEX [IX_InvoiceItems_InvoiceId] ON [InvoiceItems];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InvoiceItems]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [InvoiceItems] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [InvoiceItems] DROP COLUMN [Id];
GO

ALTER TABLE [InvoiceItems] ADD [Id] int NOT NULL IDENTITY;
GO

ALTER TABLE [InvoiceItems] ADD CONSTRAINT [PK_InvoiceItems] PRIMARY KEY ([InvoiceId], [Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221112225359_ConvertInvoiceItemsToOwned', N'7.0.0');
GO

COMMIT;
GO

