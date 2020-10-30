alter table 门诊管理_当天排班记录 add 出诊状态 varchar2(50);
COMMENT ON COLUMN 门诊管理_当天排班记录.出诊状态 IS '0停诊 1出诊 2暂未开放';


alter table 基础项目_科室资料 add 是否排班 varchar2(50);
update 基础项目_科室资料 set 是否排班='否';


alter table 门诊管理_预约挂号 add 预约时段编码 varchar2(50);
alter table 门诊管理_预约挂号 add 预约时段开始 date;
alter table 门诊管理_预约挂号 add 预约时段结束 date;
alter table 门诊管理_预约挂号 add 支付标志 varchar2(50);
alter table 门诊管理_预约挂号 add 超时时间 date;
alter table 门诊管理_预约挂号 add 取号时间 date;
alter table 门诊管理_预约挂号 add 挂号费 NUMBER(18,3);
alter table 门诊管理_预约挂号 add 诊查费 NUMBER(18,3);
alter table 门诊管理_预约挂号 add 归类编码 varchar2(50);
alter table 门诊管理_预约挂号 add 挂号类型名称 varchar2(50);
alter table 门诊管理_预约挂号 add 挂号医生姓名 varchar2(50);
alter table 门诊管理_预约挂号 add 挂号科室名称 varchar2(50);
alter table 门诊管理_预约挂号 add 日班次标识 varchar2(50);

alter table 门诊管理_挂号登记 add 就诊时间 date;
alter table 门诊管理_挂号登记 add 预约开始时间 date;
alter table 门诊管理_挂号登记 add 预约结束时间 date;
alter table 门诊管理_挂号登记 add 日班次标识 varchar2(50);


create sequence SEQ_门诊管理_日排班_排班标识
minvalue 1
maxvalue 9999999999
start with 1
increment by 1
cache 10;



-- Create table
create table 门诊管理_日排班时段表
(
  日班次标识  VARCHAR2(50) not null,
  机构编码   VARCHAR2(50),
  排班序号   VARCHAR2(50),
  记录id   VARCHAR2(50),
  限号类型编码 VARCHAR2(50),
  时段编码   VARCHAR2(50),
  开始时间   VARCHAR2(50),
  结束时间   VARCHAR2(50),
  限号数    INTEGER,
  顺序号    INTEGER default 0 not null,
  有效状态   VARCHAR2(50),
  已挂号数   INTEGER,
  支持共享   VARCHAR2(50),
  时段分组编码 VARCHAR2(50),
  CONSTRAINT KP_门诊管理_日排班时段表 PRIMARY KEY (日班次标识)
);

-- Add comments to the table 
comment on table 门诊管理_日排班时段表
  is '门诊管理_日排班时段表';
-- Add comments to the columns 
comment on column 门诊管理_日排班时段表.日班次标识
  is '日班次标识';
comment on column 门诊管理_日排班时段表.机构编码
  is '机构编码';
comment on column 门诊管理_日排班时段表.排班序号
  is '排班序号';
comment on column 门诊管理_日排班时段表.记录id
  is '记录ID';
comment on column 门诊管理_日排班时段表.限号类型编码
  is '限号类型编码';
comment on column 门诊管理_日排班时段表.时段编码
  is '时段编码';
comment on column 门诊管理_日排班时段表.开始时间
  is '开始时间';
comment on column 门诊管理_日排班时段表.结束时间
  is '结束时间';
comment on column 门诊管理_日排班时段表.限号数
  is '限号数';
comment on column 门诊管理_日排班时段表.顺序号
  is '顺序号';
comment on column 门诊管理_日排班时段表.有效状态
  is '有效；无效';
comment on column 门诊管理_日排班时段表.已挂号数
  is '已挂号数';
comment on column 门诊管理_日排班时段表.支持共享
  is '支持共享';
comment on column 门诊管理_日排班时段表.时段分组编码
  is '时段分组编码';


  
  
  
  
  
  
  
  -- Create table
create table 门诊管理_周排班时段表
(
  机构编码   VARCHAR2(50) not null,
  排班序号   VARCHAR2(50) not null,
  时段编码   VARCHAR2(50) not null,
  限号类型编码 VARCHAR2(50) not null,
  开始时间   VARCHAR2(50),
  结束时间   VARCHAR2(50),
  限号数    INTEGER,
  顺序号    INTEGER default 0 not null,
  有效状态   VARCHAR2(50),
  支持共享   VARCHAR2(50),
  时段分组编码 VARCHAR2(50),
  CONSTRAINT KP_门诊管理_周排班时段表 PRIMARY KEY (机构编码, 排班序号, 时段编码, 限号类型编码)
);

-- Add comments to the table 
comment on table 门诊管理_周排班时段表
  is '门诊管理_周排班时段表';
-- Add comments to the columns 
comment on column 门诊管理_周排班时段表.机构编码
  is '机构编码';
comment on column 门诊管理_周排班时段表.排班序号
  is '排班序号';
comment on column 门诊管理_周排班时段表.时段编码
  is '时段编码';
comment on column 门诊管理_周排班时段表.限号类型编码
  is '限号类型编码';
comment on column 门诊管理_周排班时段表.开始时间
  is '开始时间';
comment on column 门诊管理_周排班时段表.结束时间
  is '结束时间';
comment on column 门诊管理_周排班时段表.限号数
  is '限号数';
comment on column 门诊管理_周排班时段表.顺序号
  is '顺序号';
comment on column 门诊管理_周排班时段表.有效状态
  is '有效；无效';
comment on column 门诊管理_周排班时段表.支持共享
  is '支持共享';
comment on column 门诊管理_周排班时段表.时段分组编码
  is '时段分组编码';

  
  
  
  
  
  
  
  -- Create table
create table 门诊管理_预约时段字典
(
  机构编码   VARCHAR2(50) not null,
  时段编码   VARCHAR2(50) not null,
  开始时间   VARCHAR2(50),
  结束时间   VARCHAR2(50),
  顺序号    INTEGER default 0 not null,
  有效状态   VARCHAR2(50) default '有效' not null,
  时段分组编码 VARCHAR2(50),
  CONSTRAINT KP_门诊管理_预约时段字典 PRIMARY KEY (机构编码, 时段编码)
);

-- Add comments to the columns 
comment on column 门诊管理_预约时段字典.机构编码
  is '机构编码';
comment on column 门诊管理_预约时段字典.时段编码
  is '时段编码';
comment on column 门诊管理_预约时段字典.开始时间
  is '开始时间';
comment on column 门诊管理_预约时段字典.结束时间
  is '结束时间';
comment on column 门诊管理_预约时段字典.顺序号
  is '顺序号';
comment on column 门诊管理_预约时段字典.有效状态
  is '有效；无效';
comment on column 门诊管理_预约时段字典.时段分组编码
  is '时段分组编码';

  
  
  
  
  -- Create table
create table 门诊管理_限号类型字典
(
  限号类型编码 VARCHAR2(50) not null,
  限号类型名称 VARCHAR2(50),
  支持共享   VARCHAR2(50),
  顺序号    INTEGER default 0 not null,
  有效状态   VARCHAR2(50) not null,
  CONSTRAINT KP_门诊管理_限号类型字典 PRIMARY KEY (限号类型编码)
);

-- Add comments to the table 
comment on table 门诊管理_限号类型字典
  is '门诊管理_限号类型字典';
-- Add comments to the columns 
comment on column 门诊管理_限号类型字典.限号类型编码
  is '限号类型编码';
comment on column 门诊管理_限号类型字典.限号类型名称
  is '限号类型名称';
comment on column 门诊管理_限号类型字典.支持共享
  is '支持共享';
comment on column 门诊管理_限号类型字典.顺序号
  is '顺序号';
comment on column 门诊管理_限号类型字典.有效状态
  is '有效；无效';

  
insert into 门诊管理_限号类型字典 (限号类型编码, 限号类型名称, 支持共享, 顺序号, 有效状态)
values ('-1', '不限类型', '是', 1, '有效');

insert into 门诊管理_限号类型字典 (限号类型编码, 限号类型名称, 支持共享, 顺序号, 有效状态)
values ('1', '窗口挂号', '是', 2, '有效');

insert into 门诊管理_限号类型字典 (限号类型编码, 限号类型名称, 支持共享, 顺序号, 有效状态)
values ('2', '网络预约', '是', 3, '有效');

  
  
  
  
insert into 基础项目_字典分类 (分类编码, 分类名称, 拼音码, 五笔码, 序号, 上级编码, 所属标准, 公有标志, 有效状态, 删除标志, 更新时间)
values ('预约时段分组', '预约时段分组', null, null, null, 'GB_02', null, '0', '1', '0', to_date('27-10-2011', 'dd-mm-yyyy'));

insert into 基础项目_字典明细 (编码, 名称, 拼音码, 五笔码, 自编码, 分类编码, 有效状态, 上级编码, 是否默认, 删除标志, 更新时间, 国标码)
values ('1', '全天', null, null, null, '预约时段分组', '有效', 'GB_02', null, '0', to_date('22-04-2012 16:20:10', 'dd-mm-yyyy hh24:mi:ss'), null);

insert into 基础项目_字典明细 (编码, 名称, 拼音码, 五笔码, 自编码, 分类编码, 有效状态, 上级编码, 是否默认, 删除标志, 更新时间, 国标码)
values ('2', '分时', null, null, null, '预约时段分组', '有效', 'GB_02', null, '0', to_date('22-04-2012 16:20:10', 'dd-mm-yyyy hh24:mi:ss'), null);
