BEGIN TRANSACTION;
GO

ALTER TABLE [Brands] ADD [DateCreated] datetime2(3) NOT NULL DEFAULT (GETUTCDATE());
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220727072527_UpdateBrand', N'6.0.7');
GO

COMMIT;
GO

