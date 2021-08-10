CREATE TABLE [dbo].[ServiceRequest] (
    [Id]                UNIQUEIDENTIFIER NOT NULL ,
    [UserId]            UNIQUEIDENTIFIER NOT NULL ,
    [ServiceId]         UNIQUEIDENTIFIER NOT NULL ,
    [StatusId]          INT              NOT NULL ,
	[CreationDate]      DATETIME         NOT NULL DEFAULT(GETDATE()),
    [CreatedBy]         NVARCHAR (50)    NOT NULL,
    [UpdateDate]        DATETIME         NULL,
    [UpdatedBy]         NVARCHAR (50)    NULL,
    [IsDeleted]         BIT              NOT NULL DEFAULT(0),
	[SysStartTime]      DATETIME2 GENERATED ALWAYS AS ROW START NOT NULL,
	[SysEndTime]        DATETIME2 GENERATED ALWAYS AS ROW END NOT NULL,
	PERIOD FOR SYSTEM_TIME (SysStartTime,SysEndTime),

    CONSTRAINT [PK_ServiceRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceRequest_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
	CONSTRAINT [FK_ServiceRequest_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service] ([Id]),
	CONSTRAINT [FK_ServiceRequest_ServiceStatus] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[ServiceStatus] ([Id])
)
WITH(SYSTEM_VERSIONING=OFF);

