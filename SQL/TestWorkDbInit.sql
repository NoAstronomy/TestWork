CREATE TABLE [Группы] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [CreatedAtUtc] datetime2 NOT NULL,
    CONSTRAINT [PK_Группы] PRIMARY KEY ([Id])
);		

CREATE TABLE [Сотрудники] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(255) NOT NULL,
    [Email] nvarchar(255) NULL,
    [CreatedAtUtc] datetime2 NOT NULL,
    CONSTRAINT [PK_Сотрудники] PRIMARY KEY ([Id])
);

CREATE TABLE [СотрудникиГруппы] (
    [EmployeeId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_СотрудникиГруппы] PRIMARY KEY ([EmployeeId], [GroupId]),
    CONSTRAINT [FK_СотрудникиГруппы_Группы_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Группы] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_СотрудникиГруппы_Сотрудники_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Сотрудники] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Группы_Name] ON [Группы] ([Name]);
CREATE INDEX [IX_СотрудникиГруппы_GroupId] ON [СотрудникиГруппы] ([GroupId]);

DECLARE @administratorsGroupId uniqueidentifier = NEWID();
DECLARE @leadershipGroupId uniqueidentifier = NEWID();

INSERT INTO [Группы] 
VALUES
(@administratorsGroupId, 'Администраторы', GETDATE()),
(@leadershipGroupId, 'Руководство', GETDATE());

DECLARE @user1Id uniqueidentifier = NEWID();
DECLARE @user2Id uniqueidentifier = NEWID();
DECLARE @user3Id uniqueidentifier = NEWID();

INSERT INTO [Сотрудники] 
VALUES
(@user1Id, 'Первый тестовый пользователь', 'rodya_1994@mail.ru', GETDATE()),
(@user2Id, 'Второй тестовый пользователь', 'rodya1994@gmail.com', GETDATE()),
(@user3Id, 'Третий тестовый пользователь', null, GETDATE());


INSERT INTO [СотрудникиГруппы] 
VALUES 
(@user1Id, @administratorsGroupId),
(@user2Id, @leadershipGroupId),
(@user3Id, @administratorsGroupId),
(@user3Id, @leadershipGroupId);