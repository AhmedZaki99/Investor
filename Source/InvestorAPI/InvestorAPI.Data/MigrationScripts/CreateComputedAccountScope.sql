BEGIN TRANSACTION;
GO

ALTER TABLE [Accounts] ADD [AccountScope] AS 
                        CASE
                            WHEN [BusinessId] IS NULL
                            THEN CASE
		                        WHEN [BusinessTypeId] IS NULL
		                        THEN CAST(2 AS INT)
		                        ELSE CAST(3 AS INT)
	                        END
	                        ELSE CAST(1 AS INT)
                        END
                    ;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003183030_CreateComputedAccountScope', N'6.0.9');
GO

COMMIT;
GO

