BEGIN TRANSACTION;
GO

ALTER TABLE [BillItems] DROP CONSTRAINT [FK_BillItems_Accounts_ExpenseCategoryId];
GO

ALTER TABLE [Vendors] ADD [BusinessId] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [ScaleUnits] ADD [BusinessId] nvarchar(450) NULL;
GO

ALTER TABLE [Products] ADD [BusinessId] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Invoices] ADD [BusinessId] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Customers] ADD [BusinessId] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Categories] ADD [BusinessId] nvarchar(450) NULL;
GO

ALTER TABLE [Bills] ADD [BusinessId] nvarchar(450) NOT NULL DEFAULT N'';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BillItems]') AND [c].[name] = N'ExpenseCategoryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [BillItems] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [BillItems] ALTER COLUMN [ExpenseCategoryId] nvarchar(450) NULL;
GO

ALTER TABLE [Accounts] ADD [BusinessId] nvarchar(450) NULL;
GO

CREATE TABLE [BusinessTypes] (
    [BusinessTypeId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(1023) NULL,
    CONSTRAINT [PK_BusinessTypes] PRIMARY KEY ([BusinessTypeId])
);
GO

CREATE TABLE [Businesses] (
    [BusinessId] nvarchar(450) NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [BusinessTypeId] nvarchar(450) NULL,
    [Country] nvarchar(31) NULL,
    [Currency] nvarchar(31) NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    CONSTRAINT [PK_Businesses] PRIMARY KEY ([BusinessId]),
    CONSTRAINT [FK_Businesses_BusinessTypes_BusinessTypeId] FOREIGN KEY ([BusinessTypeId]) REFERENCES [BusinessTypes] ([BusinessTypeId])
);
GO

CREATE INDEX [IX_Vendors_BusinessId] ON [Vendors] ([BusinessId]);
GO

CREATE INDEX [IX_ScaleUnits_BusinessId] ON [ScaleUnits] ([BusinessId]);
GO

CREATE INDEX [IX_Products_BusinessId] ON [Products] ([BusinessId]);
GO

CREATE INDEX [IX_Invoices_BusinessId] ON [Invoices] ([BusinessId]);
GO

CREATE INDEX [IX_Customers_BusinessId] ON [Customers] ([BusinessId]);
GO

CREATE INDEX [IX_Categories_BusinessId] ON [Categories] ([BusinessId]);
GO

CREATE INDEX [IX_Bills_BusinessId] ON [Bills] ([BusinessId]);
GO

CREATE INDEX [IX_Accounts_BusinessId] ON [Accounts] ([BusinessId]);
GO

CREATE INDEX [IX_Businesses_BusinessTypeId] ON [Businesses] ([BusinessTypeId]);
GO

ALTER TABLE [Accounts] ADD CONSTRAINT [FK_Accounts_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [BillItems] ADD CONSTRAINT [FK_BillItems_Accounts_ExpenseCategoryId] FOREIGN KEY ([ExpenseCategoryId]) REFERENCES [Accounts] ([AccountId]);
GO

ALTER TABLE [Bills] ADD CONSTRAINT [FK_Bills_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [Categories] ADD CONSTRAINT [FK_Categories_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [Invoices] ADD CONSTRAINT [FK_Invoices_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [ScaleUnits] ADD CONSTRAINT [FK_ScaleUnits_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

ALTER TABLE [Vendors] ADD CONSTRAINT [FK_Vendors_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([BusinessId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825184321_AddBusinessWithRelations', N'6.0.8');
GO

COMMIT;
GO

