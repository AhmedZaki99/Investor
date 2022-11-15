BEGIN TRANSACTION;
GO

ALTER TABLE [Traders] DROP CONSTRAINT [FK_Traders_Addresses_AddressId];
GO

ALTER TABLE [Traders] DROP CONSTRAINT [FK_Traders_Contacts_ContactId];
GO

DROP INDEX [IX_Traders_AddressId] ON [Traders];
GO

DROP INDEX [IX_Traders_ContactId] ON [Traders];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Traders]') AND [c].[name] = N'AddressId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Traders] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Traders] DROP COLUMN [AddressId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Traders]') AND [c].[name] = N'ContactId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Traders] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Traders] DROP COLUMN [ContactId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contacts]') AND [c].[name] = N'DateCreated');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Contacts] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Contacts] DROP COLUMN [DateCreated];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contacts]') AND [c].[name] = N'DateModified');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Contacts] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Contacts] DROP COLUMN [DateModified];
GO

EXEC sp_rename N'[Contacts].[Id]', N'TraderId', N'COLUMN';
GO

EXEC sp_rename N'[Addresses].[Id]', N'TraderId', N'COLUMN';
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounts]') AND [c].[name] = N'AccountScope');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Accounts] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Accounts] DROP COLUMN [AccountScope];
ALTER TABLE [Accounts] ADD [AccountScope] AS CASE
    WHEN [BusinessId] IS NULL
    THEN CASE
        WHEN [BusinessTypeId] IS NULL
        THEN CAST(2 AS INT)
        ELSE CAST(3 AS INT)
    END
    ELSE CAST(1 AS INT)
END;
GO

ALTER TABLE [Addresses] ADD CONSTRAINT [FK_Addresses_Traders_TraderId] FOREIGN KEY ([TraderId]) REFERENCES [Traders] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Contacts] ADD CONSTRAINT [FK_Contacts_Traders_TraderId] FOREIGN KEY ([TraderId]) REFERENCES [Traders] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221112224205_ConvertTraderSubTablesToOwned', N'7.0.0');
GO

COMMIT;
GO

