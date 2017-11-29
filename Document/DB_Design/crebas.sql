/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2017/11/29 21:35:30                          */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('M_Admin')
            and   type = 'U')
   drop table M_Admin
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_AdminDetail')
            and   type = 'U')
   drop table M_AdminDetail
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_EmailSendedRecord')
            and   type = 'U')
   drop table M_EmailSendedRecord
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_OperateLog')
            and   type = 'U')
   drop table M_OperateLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_Project')
            and   type = 'U')
   drop table M_Project
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_RuntimeLog')
            and   type = 'U')
   drop table M_RuntimeLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_RuntimeSql')
            and   type = 'U')
   drop table M_RuntimeSql
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_ServiceGroup')
            and   type = 'U')
   drop table M_ServiceGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_ServiceGroupMap')
            and   type = 'U')
   drop table M_ServiceGroupMap
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_Servicer')
            and   type = 'U')
   drop table M_Servicer
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_SqlParameter')
            and   type = 'U')
   drop table M_SqlParameter
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_SysConfig')
            and   type = 'U')
   drop table M_SysConfig
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_WarningInfoOperateLog')
            and   type = 'U')
   drop table M_WarningInfoOperateLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_WarningLog')
            and   type = 'U')
   drop table M_WarningLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('M_WarningSql')
            and   type = 'U')
   drop table M_WarningSql
go

/*==============================================================*/
/* Table: M_Admin                                               */
/*==============================================================*/
create table M_Admin (
   FID                  int                  identity(1,1),
   FEmail               varchar(100)         null,
   FName                nvarchar(50)         null,
   FPwdSalt             varchar(10)          null,
   FPwd                 varchar(32)          null,
   FState               int                  not null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_ADMIN primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_Admin') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_Admin' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '管理员', 
   'user', @CurrentUser, 'table', 'M_Admin'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '管理员ID',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FEmail')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FEmail'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FEmail'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名字',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FPwdSalt')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FPwdSalt'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码盐值',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FPwdSalt'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FPwd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FPwd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FPwd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '管理员状态(1:正常,10:禁用,20:注销)',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Admin')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_Admin', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_AdminDetail                                         */
/*==============================================================*/
create table M_AdminDetail (
   FID                  int                  identity(1,1),
   FAdminID             int                  not null,
   FlastLoginTime       datetime             null,
   FLastLoginIP         varchar(15)          null,
   FLastChangePwdTime   datetime             null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_ADMINDETAIL primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_AdminDetail') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_AdminDetail' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '管理员详情信息', 
   'user', @CurrentUser, 'table', 'M_AdminDetail'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键、自增',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FAdminID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FAdminID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '管理员ID',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FAdminID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FlastLoginTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FlastLoginTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后一次登录时间',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FlastLoginTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastLoginIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastLoginIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后一次登录IP',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastLoginIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastChangePwdTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastChangePwdTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后一次修改密码时间',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastChangePwdTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_AdminDetail')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_AdminDetail', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_EmailSendedRecord                                   */
/*==============================================================*/
create table M_EmailSendedRecord (
   FID                  int                  identity(1,1),
   FReceiveEmail        varchar(128)         null,
   FTheme               nvarchar(256)        null,
   FContent             nvarchar(max)        null,
   FSendEmail           varchar(128)         null,
   FSendState           int                  not null,
   FStateRemark         varchar(256)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_EMAILSENDEDRECORD primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_EmailSendedRecord') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '邮件发送记录', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键、自增',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FReceiveEmail')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FReceiveEmail'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '接收账号',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FReceiveEmail'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FTheme')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FTheme'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主题',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FTheme'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '内容',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSendEmail')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FSendEmail'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送账号',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FSendEmail'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSendState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FSendState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送状态【1:待发送，2:已发送,3:发送失败,4:不发送】',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FSendState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FStateRemark')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FStateRemark'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送状态备注',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FStateRemark'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_EmailSendedRecord')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_EmailSendedRecord', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_OperateLog                                          */
/*==============================================================*/
create table M_OperateLog (
   FID                  int                  identity(1,1),
   FAdminID             int                  not null,
   FModuleType          int                  not null,
   FModuleName          varchar(64)          null,
   FModuleNodeType      int                  not null,
   FModuleNodeName      varchar(64)          null,
   FOperateIP           varchar(15)          null,
   FOperateIPAddress    varchar(64)          null,
   FOperateUrl          varchar(150)         null,
   FOperateContent      nvarchar(256)        null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_OPERATELOG primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_OperateLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_OperateLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '管理员操作记录', 
   'user', @CurrentUser, 'table', 'M_OperateLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键、自增',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FAdminID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FAdminID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '管理员ID',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FAdminID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FModuleType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作模块类型(枚举)',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FModuleName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作模块节点名称',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FModuleNodeType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleNodeType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作模块节点类型',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleNodeType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FModuleNodeName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleNodeName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作模块节点类型名称',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FModuleNodeName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作IP',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateIPAddress')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateIPAddress'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作IP地址',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateIPAddress'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作地址',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作内容',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FOperateContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_OperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_OperateLog', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_Project                                             */
/*==============================================================*/
create table M_Project (
   FID                  int                  identity(1,1),
   FName                varchar(100)         null,
   FComment             nvarchar(200)        null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_PROJECT primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_Project') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_Project' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '项目信息', 
   'user', @CurrentUser, 'table', 'M_Project'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目ID(主键、自增)',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目名字(唯一、不重复)',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目说明',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Project')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_Project', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_RuntimeLog                                          */
/*==============================================================*/
create table M_RuntimeLog (
   FID                  int                  identity(1,1),
   FLogLevel            int                  not null,
   FProjectID           int                  not null,
   FProjectName         varchar(128)         null,
   FServicerID          int                  not null,
   FServicerMac         varchar(128)         null,
   FCallMemberName      varchar(256)         null,
   FContent             varchar(max)         null,
   FSource              int                  not null,
   FExecuteTime         datetime             not null,
   FRequestGuid         varchar(64)          null,
   FCreateTime          datetime             not null,
   FIsDeleted           bit                  not null,
   constraint PK_M_RUNTIMELOG primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_RuntimeLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_RuntimeLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '运行日志信息', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键、自增',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLogLevel')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FLogLevel'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志级别',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FLogLevel'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FProjectID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FProjectID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属项目',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FProjectID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FProjectName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FProjectName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目名字',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FProjectName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FServicerID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FServicerID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部署服务器ID',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FServicerID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FServicerMac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FServicerMac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器Mac地址',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FServicerMac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCallMemberName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FCallMemberName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '调用方法名字',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FCallMemberName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志内容',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSource')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FSource'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志来源【1:前端,2:后台,3:IOS,4:Android,5:API,6:其它】',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FSource'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FExecuteTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FExecuteTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志生成时间',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FExecuteTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FRequestGuid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FRequestGuid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求标识(同一次请求中值相同)',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FRequestGuid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_RuntimeLog', 'column', 'FIsDeleted'
go

/*==============================================================*/
/* Table: M_RuntimeSql                                          */
/*==============================================================*/
create table M_RuntimeSql (
   FID                  int                  identity(1,1),
   FProjectID           int                  not null,
   FProjectName         varchar(128)         null,
   FServicerID          int                  not null,
   FServicerMac         varchar(128)         null,
   FSqlDbType           varchar(64)          null,
   FSqlText             varchar(4000)        null,
   FRequestGuid         varchar(64)          null,
   FTimeElapsed         decimal(18,2)        not null,
   FIsSuccess           bit                  not null,
   FExecutedTime        datetime             not null,
   FCreateTime          datetime             not null,
   FIsDeleted           bit                  not null,
   constraint PK_M_RUNTIMESQL primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_RuntimeSql') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_RuntimeSql' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '执行SQL信息', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键ID',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FProjectID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FProjectID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所属项目',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FProjectID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FProjectName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FProjectName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目名字',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FProjectName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FServicerID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FServicerID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部署服务器ID',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FServicerID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FServicerMac')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FServicerMac'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器Mac地址',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FServicerMac'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSqlDbType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FSqlDbType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SQL数据库类型',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FSqlDbType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSqlText')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FSqlText'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SQL文本',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FSqlText'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FRequestGuid')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FRequestGuid'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '请求标识(同一次请求中值相同)',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FRequestGuid'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FTimeElapsed')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FTimeElapsed'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '执行消耗时间',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FTimeElapsed'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsSuccess')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FIsSuccess'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否成功',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FIsSuccess'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FExecutedTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FExecutedTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '执行时间',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FExecutedTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_RuntimeSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_RuntimeSql', 'column', 'FIsDeleted'
go

/*==============================================================*/
/* Table: M_ServiceGroup                                        */
/*==============================================================*/
create table M_ServiceGroup (
   FID                  int                  identity(1,1),
   FName                varchar(128)         null,
   FComment             varchar(256)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_SERVICEGROUP primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_ServiceGroup') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_ServiceGroup' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '服务器组信息', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组名字',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroup')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroup', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_ServiceGroupMap                                     */
/*==============================================================*/
create table M_ServiceGroupMap (
   FID                  int                  identity(1,1),
   FGroupID             int                  not null,
   FservicerID          int                  not null,
   FComment             varchar(256)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_SERVICEGROUPMAP primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_ServiceGroupMap') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '服务器所属组关系', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FGroupID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FGroupID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FGroupID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FservicerID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FservicerID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FservicerID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_ServiceGroupMap')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_ServiceGroupMap', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_Servicer                                            */
/*==============================================================*/
create table M_Servicer (
   FID                  int                  identity(1,1),
   FMacAddress          varchar(128)         null,
   FIP                  varchar(64)          null,
   FName                varchar(128)         null,
   FComment             nvarchar(200)        null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_SERVICER primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_Servicer') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_Servicer' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '服务器信息', 
   'user', @CurrentUser, 'table', 'M_Servicer'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器ID(主键、自增)',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FMacAddress')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FMacAddress'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'MAC地址',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FMacAddress'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器IP(多个用逗号隔开)',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器名字',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '服务器说明',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_Servicer')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_Servicer', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_SqlParameter                                        */
/*==============================================================*/
create table M_SqlParameter (
   FID                  bigint               identity(1,1),
   FRuntimeSqlID        int                  not null,
   FName                varchar(128)         null,
   FValue               varchar(max)         null,
   FCreateTime          datetime             not null,
   FIsdeleted           bit                  not null,
   constraint PK_M_SQLPARAMETER primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_SqlParameter') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_SqlParameter' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'SQL参数信息', 
   'user', @CurrentUser, 'table', 'M_SqlParameter'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FRuntimeSqlID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FRuntimeSqlID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SQL记录ID',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FRuntimeSqlID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FName')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FName'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '参数名',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FName'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '参数值',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '添加时间',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SqlParameter')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsdeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FIsdeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_SqlParameter', 'column', 'FIsdeleted'
go

/*==============================================================*/
/* Table: M_SysConfig                                           */
/*==============================================================*/
create table M_SysConfig (
   FID                  int                  identity(1,1),
   FKey                 varchar(128)         null,
   FValue               varchar(512)         null,
   FComment             varchar(512)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_SYSCONFIG primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_SysConfig') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_SysConfig' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '系统配置信息', 
   'user', @CurrentUser, 'table', 'M_SysConfig'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键、自增',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FKey')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FKey'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '键名(唯一不重复)',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FKey'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '值',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FComment')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FComment'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FComment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_SysConfig')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_SysConfig', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_WarningInfoOperateLog                               */
/*==============================================================*/
create table M_WarningInfoOperateLog (
   FID                  int                  identity(1,1),
   FType                int                  not null,
   FWarningInfoID       int                  not null,
   FOperateIP           varchar(15)          null,
   FOperateUrl          varchar(150)         null,
   FOperateContent      nvarchar(256)        null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_WARNINGINFOOPERATELOG primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_WarningInfoOperateLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '预警信息操作记录', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键自增',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FType')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FType'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预警信息类型(1:日志)',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FType'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FWarningInfoID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FWarningInfoID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预警信息ID',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FWarningInfoID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateIP')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateIP'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作IP',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateIP'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作地址',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateContent')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateContent'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作内容',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FOperateContent'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningInfoOperateLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_WarningInfoOperateLog', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_WarningLog                                          */
/*==============================================================*/
create table M_WarningLog (
   FID                  int                  identity(1,1),
   FRunTimeLogID        int                  not null,
   FLogSign             int                  not null,
   FOperateAdvice       varchar(256)         null,
   FNoticeState         int                  not null,
   FDealState           int                  not null,
   FTreatmentScheme     varchar(256)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_WARNINGLOG primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_WarningLog') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_WarningLog' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '日志预警信息', 
   'user', @CurrentUser, 'table', 'M_WarningLog'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键ID',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FRunTimeLogID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FRunTimeLogID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志记录ID',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FRunTimeLogID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLogSign')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLogSign'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志标识',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLogSign'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateAdvice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FOperateAdvice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理建议',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FOperateAdvice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FNoticeState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FNoticeState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通知状态(1:未通知 2:已通知 3:通知失败)',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FNoticeState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FDealState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FDealState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理状态(1:待处理,2:已处理,3:不处理)',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FDealState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FTreatmentScheme')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FTreatmentScheme'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理方案',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FTreatmentScheme'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningLog')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_WarningLog', 'column', 'FLastModifyUserID'
go

/*==============================================================*/
/* Table: M_WarningSql                                          */
/*==============================================================*/
create table M_WarningSql (
   FID                  int                  identity(1,1),
   FRuntimeSqlID        int                  not null,
   FSqlSign             int                  not null,
   FOperateAdvice       varchar(256)         null,
   FNoticeState         int                  not null,
   FDealState           int                  not null,
   FTreatmentScheme     varchar(256)         null,
   FIsDeleted           bit                  not null,
   FCreateTime          datetime             not null,
   FCreateUserID        int                  not null,
   FLastModifyTime      datetime             null,
   FLastModifyUserID    int                  null,
   constraint PK_M_WARNINGSQL primary key (FID)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('M_WarningSql') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'M_WarningSql' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'SQL预警信息', 
   'user', @CurrentUser, 'table', 'M_WarningSql'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主键',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FRuntimeSqlID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FRuntimeSqlID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SQL记录ID',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FRuntimeSqlID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FSqlSign')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FSqlSign'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SQL标识',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FSqlSign'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FOperateAdvice')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FOperateAdvice'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理建议',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FOperateAdvice'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FNoticeState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FNoticeState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '通知状态(1:未通知 2:已通知 3:通知失败)',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FNoticeState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FDealState')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FDealState'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理状态(1:待处理,2:已处理,3:不处理)',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FDealState'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FTreatmentScheme')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FTreatmentScheme'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '处理方案',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FTreatmentScheme'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FIsDeleted')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FIsDeleted'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FIsDeleted'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FCreateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FCreateTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FCreateUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FCreateUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建人ID',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FCreateUserID'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FLastModifyTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FLastModifyTime'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('M_WarningSql')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'FLastModifyUserID')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FLastModifyUserID'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改人ID',
   'user', @CurrentUser, 'table', 'M_WarningSql', 'column', 'FLastModifyUserID'
go

