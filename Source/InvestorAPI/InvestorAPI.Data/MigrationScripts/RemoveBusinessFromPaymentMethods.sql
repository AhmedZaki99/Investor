BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] DROP CONSTRAINT [FK_Customers_Addresses_BillingAddressId];
GO

ALTER TABLE [Customers] DROP CONSTRAINT [FK_Customers_Contacts_PrimaryContactId];
GO

ALTER TABLE [PaymentMethods] DROP CONSTRAINT [FK_PaymentMethods_Businesses_BusinessId];
GO

DROP INDEX [IX_PaymentMethods_BusinessId] ON [PaymentMethods];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PaymentMethods]') AND [c].[name] = N'BusinessId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PaymentMethods] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [PaymentMethods] DROP COLUMN [BusinessId];
GO

EXEC sp_rename N'[Customers].[PrimaryContactId]', N'ContactId', N'COLUMN';
GO

EXEC sp_rename N'[Customers].[BillingAddressId]', N'AddressId', N'COLUMN';
GO

EXEC sp_rename N'[Customers].[IX_Customers_PrimaryContactId]', N'IX_Customers_ContactId', N'INDEX';
GO

EXEC sp_rename N'[Customers].[IX_Customers_BillingAddressId]', N'IX_Customers_AddressId', N'INDEX';
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]);
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003150144_RemoveBusinessFromPaymentMethods', N'6.0.9');
GO

COMMIT;
GO

