BEGIN TRANSACTION;
GO

ALTER TABLE [Accounts] DROP CONSTRAINT [FK_Accounts_Accounts_ParentAccountId];
GO

DROP TABLE [Brands];
GO

DROP INDEX [IX_Accounts_ParentAccountId] ON [Accounts];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounts]') AND [c].[name] = N'ParentAccountId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Accounts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Accounts] DROP COLUMN [ParentAccountId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Accounts]') AND [c].[name] = N'AccountType');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Accounts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Accounts] ALTER COLUMN [AccountType] int NOT NULL;
ALTER TABLE [Accounts] ADD DEFAULT 0 FOR [AccountType];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220929180209_RemoveAccountParentRelation', N'6.0.9');
GO

COMMIT;
GO

