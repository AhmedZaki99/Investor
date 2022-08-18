BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220727074331_AutoGenerateBrandId', N'6.0.7');
GO

COMMIT;
GO

