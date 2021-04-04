CREATE TABLE [dbo].[User] (
    [Id]                   UNIQUEIDENTIFIER   NOT NULL,
    [Email]                NVARCHAR (256)     NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NOT NULL,
    [FirstName]            NVARCHAR (50)     NOT NULL,
    [LastName]             NVARCHAR (50)     NOT NULL,
	[IsDeleted]        BIT              NOT NULL DEFAULT(0),
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);



