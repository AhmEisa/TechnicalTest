/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=1)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (1,N'InProgress',0)

IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=2)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (2,N'Approved',0)

IF NOT EXISTS(SELECT * FROM [dbo].[ServiceStatus] WHERE Id=3)
INSERT INTO [dbo].[ServiceStatus] (Id,[Name],IsDeleted) VALUES (3,N'Rejected',0)


IF NOT EXISTS(SELECT * FROM [dbo].[SERVICE] WHERE Id='80D9F272-83E3-4EC7-B50C-CBEF3D0A96E0')
INSERT INTO [dbo].[Service] (Id,[Name],IsDeleted) VALUES ('80D9F272-83E3-4EC7-B50C-CBEF3D0A96E0',N'A',0)

IF NOT EXISTS(SELECT * FROM [dbo].[SERVICE] WHERE Id='C2EB4AC3-3B11-418C-A143-D3295A97C224')
INSERT INTO [dbo].[Service] (Id,[Name],IsDeleted) VALUES ('C2EB4AC3-3B11-418C-A143-D3295A97C224',N'B',0)

IF NOT EXISTS(SELECT * FROM [dbo].[SERVICE] WHERE Id='3CC2C557-8FE8-47F5-80E7-9F731485CF45')
INSERT INTO [dbo].[Service] (Id,[Name],IsDeleted) VALUES ('3CC2C557-8FE8-47F5-80E7-9F731485CF45',N'C',0)

IF NOT EXISTS(SELECT * FROM [dbo].[SERVICE] WHERE Id='C0658BD6-2CDD-41D5-A6B3-EAA9584BF78C')
INSERT INTO [dbo].[Service] (Id,[Name],IsDeleted) VALUES ('C0658BD6-2CDD-41D5-A6B3-EAA9584BF78C',N'D',0)







IF NOT EXISTS(SELECT * FROM [dbo].[Role] WHERE Id=1)
INSERT INTO dbo.Role
(
    Id,
    Name,
    IsDeleted
)
VALUES (1,N'Admin',0)

IF NOT EXISTS(SELECT * FROM [dbo].[User] WHERE Id='7359A2D7-2E0F-4DFF-A39E-A62B5381F0F4')
INSERT INTO dbo.[User]
(
    Id,
    Email,
    PasswordHash,
    FirstName,
    LastName,
    IsDeleted
)
VALUES
(   '7359A2D7-2E0F-4DFF-A39E-A62B5381F0F4', -- Id 
    N'aeisa@gmail.com',  -- Email 
    N'GPQxRNDwC+5WLoZr/pNKJg== 74BEcbVlsVI+fdIKmGN0fogbhtBN0SECe7JitTLjto0=',  -- Password 12345
    N'Ahmed',  -- FirstName
    N'Eisa',  -- LastName 
    0  -- IsDeleted 
    )

IF NOT EXISTS(SELECT * FROM dbo.UserRole WHERE UserId='7359A2D7-2E0F-4DFF-A39E-A62B5381F0F4' AND RoleId=1)
INSERT INTO dbo.UserRole
(
    Id,
    UserId,
    RoleId,
	IsDeleted
)
VALUES (NEWID(),'7359A2D7-2E0F-4DFF-A39E-A62B5381F0F4' ,1, 0)

ALTER TABLE [dbo].ServiceRequest SET (SYSTEM_VERSIONING = ON);


GO
