--管理员初始化脚本
DECLARE @AdminID INT; 
INSERT INTO M_Admin(FEmail,FName,FPwdSalt,FPwd,FState,FIsDeleted,FCreateTime,FCreateUserID) VALUES('425527169@qq.com','yjq','1234567890','42c224b3c8899047460f5a6d1c041411',1,0,GETDATE(),1);
SET @AdminID=(SELECT @@IDENTITY);
INSERT INTO M_AdminDetail(FAdminID,FIsDeleted,FCreateTime,FCreateUserID) VALUES(@AdminID,0,GETDATE(),@AdminID);