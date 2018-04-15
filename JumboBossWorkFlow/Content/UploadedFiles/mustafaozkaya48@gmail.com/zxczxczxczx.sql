
create table Works
(
Id uniqueidentifier primary key not null,
CreateDateTime datetime not null,
UpdateDateTime datetime not null ,
WorkDescription varchar(300) not null ,
WorkTitle varchar(300) not null, 
EmployeeUser_Id nvarchar(128)  not null,
RequestingUser_Id nvarchar(128) not null 
)

create table WorkAdditions
(
Id uniqueidentifier not null primary key ,
[Filename] varchar(max) ,
FilePath varchar(max),
Work_Id uniqueidentifier 
)


create table WorkCommenteds
(
Id uniqueidentifier primary key not null,
Commented varchar(300) not null,
[User_Id] nvarchar(128) not null,
Workflow_Id  uniqueidentifier not null 
)


create table Workflows
(
Id uniqueidentifier primary key  not null,
RequestingUserId nvarchar(128) not null ,
LikeCount int not null ,
EmployeeUsers_Id nvarchar(128) not null
)


alter table Works add constraint Fk_Works_EmployeeUser_Id foreign key(EmployeeUser_Id) references AspNetUsers(Id)
alter table Works add constraint Fk_Works_RequestingUser_Id foreign key(RequestingUser_Id) references AspNetUsers(Id)

alter table WorkAdditions add constraint Fk_WorkAdditions_Work_Id foreign key(Work_Id) references Works(Id)

alter table WorkCommenteds add constraint Fk_WorkCommenteds_User_Id foreign key([User_Id]) references AspNetUsers(Id)
alter table WorkCommenteds add constraint Fk_WorkCommenteds_Workflow_Id foreign key(Workflow_Id) references Works(Id)


alter table Workflows add constraint Fk_Workflows_EmployeeUser_Id foreign key(EmployeeUsers_Id) references AspNetUsers(Id)
alter table Workflows add constraint Fk_Workflows_RequestingUser_Id foreign key(RequestingUserId) references AspNetUsers(Id)