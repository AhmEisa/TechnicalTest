CREATE TABLE [dbo].[ServiceStatus] (
    [Id]               INT NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
	[IsDeleted]        BIT              NOT NULL DEFAULT(0),
    CONSTRAINT [PK_ServiceStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);
