BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'Amount');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Items] DROP COLUMN [Amount];
ALTER TABLE [Items] ADD [Amount] decimal(19,4) NOT NULL;
GO

DROP INDEX [IX_Invoices_InvoiceType_IsReturn_TraderId] ON [Invoices];
GO
DROP INDEX [IX_Invoices_InvoiceType_TraderId] ON [Invoices];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'IsReturn');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Invoices] DROP COLUMN [IsReturn];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'Quantity');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Items] ALTER COLUMN [Quantity] float NOT NULL;
GO

CREATE INDEX [IX_Invoices_InvoiceType_TraderId] ON [Invoices] ([InvoiceType], [TraderId]);
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'Amount');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Items] DROP COLUMN [Amount];
ALTER TABLE [Items] ADD [Amount] AS [Quantity] * [Price];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003173855_ConcatInvoiceTypeAndReturn', N'6.0.9');
GO

COMMIT;
GO

