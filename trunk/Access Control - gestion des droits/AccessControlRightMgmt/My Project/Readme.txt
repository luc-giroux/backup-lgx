-- Morning: removal of Motorola
Door group ndx = 536870934
Childgroupndx = 532613 = DoorID


exec sp_executesql N'DELETE FROM "InetDb".."XRefIndivGroupDoor" WHERE "TenantNdx"=@P1 AND "IndivNdx"=@P2 AND "DoorGroupNdx"=@P3', N'@P1 tinyint,@P2 smallint,@P3 int', 1, 1428, 536870934


exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532613, 0x0000000000010594, 5, 0x0401059400


1 0000 0101 1001 0100 = 0x0000000000010594 = 1428 + 65536
        101 1001 0100 = 1428

Action = 5 ?

0x0401059400 = 17197011968 = idem above << 8 + 0x4 0000 0000

exec InsertEvent 'May 20 2009  9:26:40:000AM', '', 1, 524449, 255, 33, 0, 3, 0, 0, 128, 0.000000000000000e+000, '', '', 'CPAC', '001-01428', 0, 1, 0, 0, 0, 4, @P1 output, @P2 output, @P3 output, @P4 output

exec sp_executesql N'DELETE FROM "InetDb".."DownloadPending" WHERE "PointId"=@P1 AND "BinId"=@P2', N'@P1 int,@P2 varbinary(8)', 532613, 0x0000000000010594



-- PM: Removal of Morotola

exec sp_executesql N'DELETE FROM "InetDb".."XRefIndivGroupDoor" WHERE "TenantNdx"=@P1 AND "IndivNdx"=@P2 AND "DoorGroupNdx"=@P3', N'@P1 tinyint,@P2 smallint,@P3 int', 1, 1428, 536870934

  -- check if already an update
SELECT * FROM DownloadPending WHERE PointId=532613 AND BinId=0x0000000000010594

  -- create the update tx
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532613, 0x0000000000010594, 5, 0x0401059400

  -- Insert event
exec InsertEvent 'May 20 2009  4:06:22:000PM', '', 1, 524449, 255, 33, 0, 3, 0, 0, 128, 0.000000000000000e+000, '', '', 'CPAC', '001-01428', 0, 1, 0, 0, 0, 4, @P1 output, @P2 output, @P3 output, @P4 output

  -- indirectly create ?

  -- check by the controller update process 4112
SELECT TOP 1 PointId,DeviceName,MemFailTime,DpuSpecProc,Host,RetryCount FROM LocalDpuRestore WHERE MemFailTime>'1900-01-09 00:00:00' AND DpuSpecProc>0 AND DpuSpecProc<4 AND Host IN (0,1) ORDER BY PointId
SELECT TOP 10 PointId,DeviceName,MemFailTime,DpuSpecProc,Host,RetryCount FROM LocalDpuRestore WHERE MemFailTime>'1900-01-09 00:00:00' AND DpuSpecProc=4 ORDER BY PointId
SELECT TOP 50 Action,Timestamp,PointId,BinId,Data FROM DownloadPending WHERE Action<8 ORDER BY Action

  -- cleaning up the download
exec sp_executesql N'DELETE FROM "InetDb".."DownloadPending" WHERE "PointId"=@P1 AND "BinId"=@P2', N'@P1 int,@P2 varbinary(8)', 532613, 0x0000000000010594


-- PM: Addition of Morotola

exec sp_executesql N'INSERT INTO "InetDb".."XRefIndivGroupDoor" ("TenantNdx","IndivNdx","DoorGroupNdx","PriorityOrder","DoorSchedule") VALUES (@P1,@P2,@P3,@P4,@P5)', N'@P1 tinyint,@P2 smallint,@P3 int,@P4 tinyint,@P5 tinyint', 1, 1428, 536870934, 1, 0

SELECT ChildGroupNdx,PriorityOrder,DoorSchedule FROM XRefGroupDoor WHERE ParentGroupNdx=536870934 ORDER BY PriorityOrder,ChildGroupNdx

  -- checking no other pending download
SELECT * FROM DownloadPending WHERE PointId=532613 AND BinId=0x0000000000010594

  -- creating download tx
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532613, 0x0000000000010594, 2, 0x0401059401

  -- inserting event
exec InsertEvent 'May 20 2009  4:06:57:000PM', '', 1, 524449, 255, 33, 0, 3, 0, 0, 128, 0.000000000000000e+000, '', '', 'CPAC', '001-01428', 0, 1, 0, 0, 0, 4, @P1 output, @P2 output, @P3 output, @P4 output

  -- other trigger

  -- download process checking if something to do
SELECT TOP 1 PointId,DeviceName,MemFailTime,DpuSpecProc,Host,RetryCount FROM LocalDpuRestore WHERE MemFailTime>'1900-01-09 00:00:00' AND DpuSpecProc>0 AND DpuSpecProc<4 AND Host IN (0,1) ORDER BY PointId
SELECT TOP 10 PointId,DeviceName,MemFailTime,DpuSpecProc,Host,RetryCount FROM LocalDpuRestore WHERE MemFailTime>'1900-01-09 00:00:00' AND DpuSpecProc=4 ORDER BY PointId
SELECT TOP 50 Action,Timestamp,PointId,BinId,Data FROM DownloadPending WHERE Action<8 ORDER BY Action
SELECT * FROM DownloadPending WHERE Timestamp=0x0000000000097E74

  -- cleaning up download
exec sp_executesql N'DELETE FROM "InetDb".."DownloadPending" WHERE "PointId"=@P1 AND "BinId"=@P2', N'@P1 int,@P2 varbinary(8)', 532613, 0x0000000000010594


-- PM: Removal of Main

-- PM: Addition of Main

exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532613, 0x00000000000103FD, 5, 0x040103FD00
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 532885, 0x00000000000103FD, 5, 0x040183FD00
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 533125, 0x00000000000103FD, 2, 0x040103FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 533141, 0x00000000000103FD, 2, 0x040183FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 533637, 0x00000000000103FD, 2, 0x040103FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 533653, 0x00000000000103FD, 2, 0x040183FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 534149, 0x00000000000103FD, 2, 0x040103FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 534165, 0x00000000000103FD, 2, 0x040183FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 538757, 0x00000000000103FD, 2, 0x040103FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 539269, 0x00000000000103FD, 2, 0x040103FD01
exec sp_executesql N'INSERT INTO "InetDb".."DownloadPending" ("PointId","BinId","Action","Data") VALUES (@P1,@P2,@P3,@P4)', N'@P1 int,@P2 varbinary(8),@P3 tinyint,@P4 varbinary(5)', 540293, 0x00000000000103FD, 2, 0x040103FD01

											540293, 0x00000000000103FD,	2, 0x40103FD01
"INSERT INTO [InetDb].[dbo].[DownloadPending] ([PointId],[BinId],[Action],[Data]) VALUES (532613, 0x106D4, 		2, 0x40106D401);"

-- addition is action = 2 + 01
-- removal  is action = 5 + 00

