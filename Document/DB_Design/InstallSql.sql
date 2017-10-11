--管理员初始化脚本
DECLARE @AdminID INT; 
INSERT INTO M_Admin(FEmail,FName,FPwdSalt,FPwd,FState,FIsDeleted,FCreateTime,FCreateUserID) VALUES('425527169@qq.com','yjq','1234567890','42c224b3c8899047460f5a6d1c041411',1,0,GETDATE(),1);
SET @AdminID=(SELECT @@IDENTITY);
INSERT INTO M_AdminDetail(FAdminID,FIsDeleted,FCreateTime,FCreateUserID) VALUES(@AdminID,0,GETDATE(),@AdminID);

--设置发送邮箱的账号密码
INSERT INTO M_SysConfig(FKey,FValue,FCreateUserID,FCreateTime,FIsDeleted)VALUES('SendEmailAccount','18268932376@163.com',1,GETDATE(),0);
INSERT INTO M_SysConfig(FKey,FValue,FCreateUserID,FCreateTime,FIsDeleted)VALUES('SendEmailAccountPwd','qwggmm1314',1,GETDATE(),0);