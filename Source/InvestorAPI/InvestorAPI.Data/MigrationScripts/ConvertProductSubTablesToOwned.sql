BEGIN TRANSACTION;
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_InventoryInfos_InventoryDetailsId];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_TradingInfos_PurchasingInformationId];
GO

ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_TradingInfos_SalesInformationId];
GO

DROP TABLE [TradingInfos];
GO

DROP INDEX [IX_Products_InventoryDetailsId] ON [Products];
GO

DROP INDEX [IX_Products_PurchasingInformationId] ON [Products];
GO

DROP INDEX [IX_Products_SalesInformationId] ON [Products];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'InventoryDetailsId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] DROP COLUMN [InventoryDetailsId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'PurchasingInformationId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Products] DROP COLUMN [PurchasingInformationId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'SalesInformationId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Products] DROP COLUMN [SalesInformationId];
GO

EXEC sp_rename N'[InventoryInfos].[Id]', N'ProductId', N'COLUMN';
GO

CREATE TABLE [PurchasingInfos] (
    [ProductId] nvarchar(450) NOT NULL,
    [AccountId] nvarchar(450) NULL,
    [Price] decimal(19,4) NOT NULL,
    [Description] nvarchar(1024) NULL,
    CONSTRAINT [PK_PurchasingInfos] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_PurchasingInfos_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]),
    CONSTRAINT [FK_PurchasingInfos_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SalesInfos] (
    [ProductId] nvarchar(450) NOT NULL,
    [AccountId] nvarchar(450) NULL,
    [Price] decimal(19,4) NOT NULL,
    [Description] nvarchar(1024) NULL,
    CONSTRAINT [PK_SalesInfos] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_SalesInfos_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]),
    CONSTRAINT [FK_SalesInfos_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_PurchasingInfos_AccountId] ON [PurchasingInfos] ([AccountId]);
GO

CREATE INDEX [IX_SalesInfos_AccountId] ON [SalesInfos] ([AccountId]);
GO

ALTER TABLE [InventoryInfos] ADD CONSTRAINT [FK_InventoryInfos_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221112234252_ConvertProductSubTablesToOwned', N'7.0.0');
GO

COMMIT;
GO

