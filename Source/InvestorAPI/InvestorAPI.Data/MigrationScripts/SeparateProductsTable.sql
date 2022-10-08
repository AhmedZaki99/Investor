BEGIN TRANSACTION;
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Accounts_ExpenseAccountId];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Accounts_IncomeAccountId];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Accounts_InventoryAccountId];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_ScaleUnits_ScaleUnitId];
GO

DROP INDEX [IX_Products_ExpenseAccountId] ON [Products];
GO

DROP INDEX [IX_Products_IncomeAccountId] ON [Products];
GO

DROP INDEX [IX_Products_InventoryAccountId] ON [Products];
GO

DROP INDEX [IX_Products_ScaleUnitId] ON [Products];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Cost');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] DROP COLUMN [Cost];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ExpenseAccountId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Products] DROP COLUMN [ExpenseAccountId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'PurchaseDescription');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Products] DROP COLUMN [PurchaseDescription];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Quantity');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Products] DROP COLUMN [Quantity];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ReorderPoint');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Products] DROP COLUMN [ReorderPoint];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'SKU');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Products] DROP COLUMN [SKU];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'SalesDescription');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Products] DROP COLUMN [SalesDescription];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'SalesPrice');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Products] DROP COLUMN [SalesPrice];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ScaleUnitId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Products] DROP COLUMN [ScaleUnitId];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'InventoryAccountId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Products] DROP COLUMN [InventoryAccountId];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'IncomeAccountId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Products] DROP COLUMN [IncomeAccountId];
GO

ALTER TABLE [Products] ADD [SalesInformationId] nvarchar(450) NULL;
GO

ALTER TABLE [Products] ADD [PurchasingInformationId] nvarchar(450) NULL;
GO

ALTER TABLE [Products] ADD [InventoryDetailsId] nvarchar(450) NULL;
GO

CREATE TABLE [InventoryInfos] (
    [Id] nvarchar(450) NOT NULL,
    [SKU] nvarchar(128) NULL,
    [Quantity] float NOT NULL,
    [ReorderPoint] float NULL,
    [ScaleUnitId] nvarchar(450) NULL,
    [InventoryAccountId] nvarchar(450) NULL,
    CONSTRAINT [PK_InventoryInfos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventoryInfos_Accounts_InventoryAccountId] FOREIGN KEY ([InventoryAccountId]) REFERENCES [Accounts] ([Id]),
    CONSTRAINT [FK_InventoryInfos_ScaleUnits_ScaleUnitId] FOREIGN KEY ([ScaleUnitId]) REFERENCES [ScaleUnits] ([Id])
);
GO

CREATE TABLE [TradingInfos] (
    [Id] nvarchar(450) NOT NULL,
    [AccountId] nvarchar(450) NULL,
    [Price] decimal(19,4) NOT NULL,
    [Description] nvarchar(1024) NULL,
    CONSTRAINT [PK_TradingInfos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TradingInfos_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Products_InventoryDetailsId] ON [Products] ([InventoryDetailsId]) WHERE [InventoryDetailsId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Products_PurchasingInformationId] ON [Products] ([PurchasingInformationId]) WHERE [PurchasingInformationId] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Products_SalesInformationId] ON [Products] ([SalesInformationId]) WHERE [SalesInformationId] IS NOT NULL;
GO

CREATE INDEX [IX_InventoryInfos_InventoryAccountId] ON [InventoryInfos] ([InventoryAccountId]);
GO

CREATE INDEX [IX_InventoryInfos_ScaleUnitId] ON [InventoryInfos] ([ScaleUnitId]);
GO

CREATE INDEX [IX_TradingInfos_AccountId] ON [TradingInfos] ([AccountId]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_InventoryInfos_InventoryDetailsId] FOREIGN KEY ([InventoryDetailsId]) REFERENCES [InventoryInfos] ([Id]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_TradingInfos_PurchasingInformationId] FOREIGN KEY ([PurchasingInformationId]) REFERENCES [TradingInfos] ([Id]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_TradingInfos_SalesInformationId] FOREIGN KEY ([SalesInformationId]) REFERENCES [TradingInfos] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003185646_SeparateProductsTable', N'6.0.9');
GO

COMMIT;
GO

