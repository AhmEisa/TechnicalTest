
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

