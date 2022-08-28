BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'IsDue');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Invoices] DROP COLUMN [IsDue];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bills]') AND [c].[name] = N'IsDue');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Bills] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Bills] DROP COLUMN [IsDue];
GO

ALTER TABLE [Invoices] ADD [AmountDue] decimal(19,4) NULL;
GO

ALTER TABLE [Invoices] ADD [TotalAmount] decimal(19,4) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Bills] ADD [AmountDue] decimal(19,4) NULL;
GO

ALTER TABLE [Bills] ADD [TotalAmount] decimal(19,4) NOT NULL DEFAULT 0.0;
GO

CREATE TABLE [PaymentMethods] (
    [Id] nvarchar(450) NOT NULL,
    [BusinessId] nvarchar(450) NULL,
    [Name] nvarchar(256) NOT NULL,
    [Description] nvarchar(1024) NULL,
    CONSTRAINT [PK_PaymentMethods] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentMethods_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id])
);
GO

CREATE TABLE [CreditPayments] (
    [Id] nvarchar(450) NOT NULL,
    [BusinessId] nvarchar(450) NOT NULL,
    [VendorId] nvarchar(450) NULL,
    [PaymentMethodId] nvarchar(450) NOT NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Number] int NULL,
    [Amount] decimal(19,4) NOT NULL,
    [PaymentDate] datetime2(0) NOT NULL,
    [Notes] nvarchar(1024) NULL,
    CONSTRAINT [PK_CreditPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CreditPayments_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]),
    CONSTRAINT [FK_CreditPayments_PaymentMethods_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [PaymentMethods] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CreditPayments_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([Id])
);
GO

CREATE TABLE [CustomerPayments] (
    [Id] nvarchar(450) NOT NULL,
    [BusinessId] nvarchar(450) NOT NULL,
    [CustomerId] nvarchar(450) NULL,
    [PaymentMethodId] nvarchar(450) NOT NULL,
    [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE()),
    [DateModified] datetime2(3) NULL,
    [Number] int NULL,
    [Amount] decimal(19,4) NOT NULL,
    [PaymentDate] datetime2(0) NOT NULL,
    [Notes] nvarchar(1024) NULL,
    CONSTRAINT [PK_CustomerPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerPayments_Businesses_BusinessId] FOREIGN KEY ([BusinessId]) REFERENCES [Businesses] ([Id]),
    CONSTRAINT [FK_CustomerPayments_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]),
    CONSTRAINT [FK_CustomerPayments_PaymentMethods_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [PaymentMethods] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CreditPayments_BusinessId] ON [CreditPayments] ([BusinessId]);
GO

CREATE INDEX [IX_CreditPayments_PaymentMethodId] ON [CreditPayments] ([PaymentMethodId]);
GO

CREATE INDEX [IX_CreditPayments_VendorId] ON [CreditPayments] ([VendorId]);
GO

CREATE INDEX [IX_CustomerPayments_BusinessId] ON [CustomerPayments] ([BusinessId]);
GO

CREATE INDEX [IX_CustomerPayments_CustomerId] ON [CustomerPayments] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerPayments_PaymentMethodId] ON [CustomerPayments] ([PaymentMethodId]);
GO

CREATE INDEX [IX_PaymentMethods_BusinessId] ON [PaymentMethods] ([BusinessId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220828142856_AddPayments', N'6.0.8');
GO

COMMIT;
GO

