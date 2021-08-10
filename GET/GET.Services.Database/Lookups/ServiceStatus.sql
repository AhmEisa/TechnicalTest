IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=1)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (1,N'InProgress',0)

IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=2)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (2,N'Approved',0)

IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=3)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (3,N'Rejected',0)
