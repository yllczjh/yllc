CREATE OR REPLACE Procedure Pr_票据_出院发票_套打_系统默认(Str_传入_机构编码 in varchar2,
                                               Str_传入_票据号   in varchar2,
                                               cur_票据明细      out sys_refcursor,
                                               I_返回值          out integer,
                                               Str_返回信息      out varchar2) AS
  str_是否母婴一体化 varchar2(50);
  str_住院病历号     varchar2(50);
  str_病人姓名       varchar2(50);
  str_科室名称       varchar2(50);
  num_应收总额       number(18, 3);
  num_舍入总额       number(18, 3);
  num_实收总额       number(18, 3);
  num_总补偿总额     number(18, 3);
  num_预交金         number(18, 3);
  num_补收总额       number(18, 3);
  num_退款总额       number(18, 3);
  num_担保总额       number(18, 3);
  str_病人类型名称   varchar2(50);
  dat_结算时间       date;
  str_操作员姓名     varchar2(50);
  dat_入院时间       date;
  dat_出院时间       date;
  str_机构名称       varchar2(200);
  int_是否出院       integer;
  str_个人编号       varchar2(50);
  num_统筹补偿       varchar2(200);
  num_家庭账户补偿   varchar2(200);
  num_民政救助       varchar2(200);
  num_大病商保       varchar2(200);
  num_计生补偿       varchar2(200);
  num_兜底补偿       varchar2(200);
  num_医疗扶助       varchar2(200);
  num_统筹支付金额   number(18, 3); --医保:本次基本医疗报销金额,农合:
  num_个人账户支付   number(18, 3); --医保:个人账户支付部分,农合:家庭账户支付
  --医保:本次大病医疗报销金额,农合:民政救助费用+大病商保补偿+计生救助费用+兜底补偿费用+精准目录补偿+新精准优抚补偿+新精准残联补偿+新精准扶贫补偿+新精准疾控补偿
  num_其他补偿金额   number(18, 3);
  str_医疗证号       varchar2(100);
  str_参保机构编码   varchar2(50);
  str_人群类别       varchar2(50);
  str_医疗统筹登记号 varchar2(50);

  str_性别            varchar2(10);
  str_床位名称        varchar2(10);
  str_函数_住院病历号 varchar2(20);
  int_住院天数        integer;

BEGIN
  --重打印标志,实收总额,大写金额为必须字段

  select 机构名称
    into str_机构名称
    from 基础项目_机构资料
   where 机构编码 = Str_传入_机构编码
     and 删除标志 = '0';

  select 住院病历号,
         科室名称,
         应收金额,
         舍入金额,
         实收金额,
         总补偿金额,
         预交金,
         补收金额,
         退款金额,
         担保金额,
         病人类型名称,
         结算时间,
         操作员姓名,
         入院时间,
         出院时间
    into str_住院病历号,
         str_科室名称,
         num_应收总额,
         num_舍入总额,
         num_实收总额,
         num_总补偿总额,
         num_预交金,
         num_补收总额,
         num_退款总额,
         num_担保总额,
         str_病人类型名称,
         dat_结算时间,
         str_操作员姓名,
         dat_入院时间,
         dat_出院时间
    from 住院管理_出院病人发票登记
   where 机构编码 = Str_传入_机构编码
     and 发票号 = Str_传入_票据号;

  select count(住院病历号)
    into int_是否出院
    from 住院管理_在院病人信息
   where 机构编码 = Str_传入_机构编码
     and 住院病历号 = str_住院病历号;

  if int_是否出院 = 0 then
    select 病人姓名,
           (case 性别
             when '1' then
              '男'
             when '2' then
              '女'
             else
              '未知'
           end),
           (select 床位名称
              from 住院管理_科室床位
             where 机构编码 = a.机构编码
               and 床位编码 = a.床位
               and rownum = 1),
           住院天数
      into str_病人姓名, str_性别, str_床位名称, int_住院天数
      from 住院管理_出院病人信息 a
     where 机构编码 = Str_传入_机构编码
       and 住院病历号 = str_住院病历号;
  else
    select 病人姓名,
           (case 性别
             when '1' then
              '男'
             when '2' then
              '女'
             else
              '未知'
           end),
           (select 床位名称
              from 住院管理_科室床位
             where 机构编码 = a.机构编码
               and 床位编码 = a.床位
               and rownum = 1)
      into str_病人姓名, str_性别, str_床位名称
      from 住院管理_在院病人信息 a
     where 机构编码 = Str_传入_机构编码
       and 住院病历号 = str_住院病历号;
  end if;

  BEGIN
    select 值
      into str_是否母婴一体化
      from 基础项目_机构参数列表
     where 参数编码 = '274'
       and 机构编码 = Str_传入_机构编码;
  EXCEPTION
    WHEN OTHERS THEN
      str_是否母婴一体化 := '是';
  END;

  --接口补偿金额

  if str_病人类型名称 = '农合' then
    BEGIN
      select nvl(C_10, '0') as 统筹补偿,
             nvl(C_11, '0') as 家庭账户,
             nvl(C_13, '0') as 民政救助,
             nvl(C_14, '0') as 大病商保,
             nvl(C_15, '0') as 计生补偿,
             nvl(C_16, '0') as 兜底补偿,
             nvl(C_25, '0') as 医疗扶助,
             C_3 as 医疗证号
        into num_统筹补偿,
             num_家庭账户补偿,
             num_民政救助,
             num_大病商保,
             num_计生补偿,
             num_兜底补偿,
             num_医疗扶助,
             str_医疗证号
        from 接口管理_接口补偿信息
       where 机构编码 = Str_传入_机构编码
         and 接口类型 = '农合'
         and 就诊类型 = '住院'
         and 病历号 = str_住院病历号;
    EXCEPTION
      WHEN OTHERS THEN
        num_统筹补偿     := '0';
        num_家庭账户补偿 := '0';
        num_民政救助     := '0';
        num_大病商保     := '0';
        num_计生补偿     := '0';
        num_兜底补偿     := '0';
        num_医疗扶助     := '0';
    END;
  
    num_统筹支付金额 := to_number(num_统筹补偿);
    num_个人账户支付 := to_number(num_家庭账户补偿);
    num_其他补偿金额 := to_number(num_民政救助) + to_number(num_大病商保) +
                  to_number(num_计生补偿) + to_number(num_兜底补偿);
  end if;

  if str_病人类型名称 = '医保' then
    BEGIN
      ---跟医保沟通 其他补偿包含公务员C_26,列支财政C_27,大额保险C_25(大额商业险保险C_23,大额和大额商业是同一字段)，大病救助C_29
      select to_number(nvl(C_26, '0')) + to_number(nvl(C_27, '0')) +
             to_number(nvl(C_25, '0')) + to_number(nvl(C_29, '0')) as 其他补偿金额,
             to_number(nvl(C_24, '0')) as 统筹补偿,
             nvl(C_16, '0') as 个人账户支付,
             C_5 AS 身份证号,
             C_3 as 参保机构编码,
             C_9 as 人群类别,
             C_30 as 医疗统筹登记号
        into num_其他补偿金额,
             num_统筹支付金额,
             num_个人账户支付,
             str_医疗证号,
             str_参保机构编码,
             str_人群类别,
             str_医疗统筹登记号
        from 接口管理_接口补偿信息
       where 机构编码 = Str_传入_机构编码
         and 接口类型 = '医保'
         and 就诊类型 = '住院'
         and 病历号 = str_住院病历号;
    EXCEPTION
      WHEN OTHERS THEN
        num_统筹支付金额 := 0;
        num_个人账户支付 := 0;
        num_其他补偿金额 := 0;
        num_医疗扶助     := 0;
    END;
  
  end if;

  select fu_转换_住院病历号(Str_传入_机构编码,
                     str_住院病历号,
                     '否',
                     str_是否母婴一体化)
    into str_函数_住院病历号
    from dual;

  OPEN cur_票据明细 FOR
    select Str_传入_票据号 as 票据号,
           str_机构名称 as 机构名称,
           str_个人编号 as 个人编号,
           str_住院病历号 as 住院病历号,
           str_病人姓名 as 病人姓名,
           str_性别 as 性别,
           str_科室名称 as 科室名称,
           '' as 病床,
           str_床位名称 as 床位名称,
           num_统筹补偿 as 统筹补偿,
           num_家庭账户补偿 as 家庭账户补偿,
           num_民政救助 as 民政救助,
           num_大病商保 as 大病商保,
           num_计生补偿 as 计生补偿,
           num_兜底补偿 as 兜底补偿,
           num_统筹支付金额 as 医保统筹,
           num_个人账户支付 as 个人账户支付,
           num_其他补偿金额 as 其他补偿金额,
           num_医疗扶助 as 医疗扶助,
           str_医疗证号 as 医疗证号,
           str_参保机构编码 as 参保机构编码,
           case str_参保机构编码
             when '21000000' then
              '辽宁省异地结算平台'
             when '21080103' then
              '营口市医疗保险事业处'
             when '21080403' then
              '营口市经济开发区医疗保险管理中心'
             when '21081103' then
              '营口市老边医疗保险管理中心'
             when '21088103' then
              '营口市盖州医疗保险管理中心'
             when '21088203' then
              '营口市大石桥医疗保险管理中心'
             else
              str_参保机构编码
           end as 参保机构,
           str_人群类别 as 人群类别编码,
           case str_人群类别
             when 'A' then
              '城镇职工'
             when 'B' then
              '城镇居民'
           end as 人群类别,
           str_医疗统筹登记号 as 医疗统筹登记号,
           num_应收总额 as 应收总额,
           num_舍入总额 as 舍入总额,
           num_实收总额 as 实收总额,
           num_总补偿总额 as 总补偿总额,
           num_预交金 as 预交金,
           num_补收总额 as 补收总额,
           num_退款总额 as 退款总额,
           num_实收总额 - num_总补偿总额 as 自付总额,
           num_担保总额 as 担保总额,
           str_病人类型名称 as 病人类型名称,
           dat_结算时间 as 结算时间,
           str_操作员姓名 as 操作员姓名,
           dat_入院时间 as 入院时间,
           dat_出院时间 as 出院时间,
           nvl(int_住院天数, trunc(dat_出院时间) - trunc(dat_入院时间)) as 住院天数,
           FU_金额_转换成大写(num_实收总额) as 大写金额,
           FU_金额_转换成大写(num_预交金) as 预交金大写,
           FU_金额_转换成大写(num_实收总额 - num_总补偿总额) as 自付总额大写,
           '医疗服务' as 行业分类,
           '住院' as 部门,
           '' as 重打印标志,
           str_病人类型名称 || '补偿' as 补偿方式,
           /*nvl(sum(case 隶属编码
                     when '33' then
                      总金额
                   end),
               0) as 床位费,
           nvl(sum(case 隶属编码
                     when '20' then
                      总金额
                   end),
               0) as 诊查费,
           nvl(sum(case 隶属编码
                     when '10' then
                      总金额
                   end),
               0) as 检查费,
           nvl(sum(case 隶属编码
                     when '19' then
                      总金额
                   end),
               0) as 治疗费,
           nvl(sum(case 隶属编码
                     when '16' then
                      总金额
                   end),
               0) as 护理费,
           nvl(sum(case 隶属编码
                     when '5' then
                      总金额
                   end),
               0) as 手术费,
           nvl(sum(case 隶属编码
                     when '7' then
                      总金额
                   end),
               0) as 化验费,
           nvl(sum(case 隶属编码
                     when '1' then
                      总金额
                   end),
               0) as 西药费,
           nvl(sum(case 隶属编码
                     when '2' then
                      总金额
                   end),
               0) as 中成药,
           nvl(sum(case 隶属编码
                     when '3' then
                      总金额
                   end),
               0) as 中草药,
           nvl(sum(case 隶属编码
                     when '18' then
                      总金额
                   end),
               0) as 卫材费,
           nvl(sum(case 隶属编码
                     when '9' then
                      总金额
                   end),
               0) as 放射费,
           nvl(sum(case 隶属编码
                     when '30' then
                      总金额
                   end),
               0) as 输血费,
           
           nvl(sum(case 隶属编码
                     when '33' then
                      0
                     when '20' then
                      0
                     when '10' then
                      0
                     when '19' then
                      0
                     when '16' then
                      0
                     when '5' then
                      0
                     when '7' then
                      0
                     when '1' then
                      0
                     when '2' then
                      0
                     when '3' then
                      0
                     when '18' then
                      0
                     when '9' then
                      0
                     else
                      总金额
                   end),
               0) as 其它费*/
           费用名称,
           sum(总金额) as 费用金额
      from (select nvl((select b.隶属编码
                         from 基础项目_费用归类 b
                        where b.机构编码 = a.机构编码
                          and b.费用编码 = a.归类编码
                          and b.删除标志 = '0'
                          and b.类别 = '住院发票项目归类'),
                       a.归类编码) as 隶属编码,
                   nvl((select x.名称
                         from 基础项目_费用归类 g, 基础项目_字典明细 x
                        where g.机构编码 = a.机构编码
                          and g.删除标志 = '0'
                          and g.类别 = '住院发票项目归类'
                          and x.分类编码 = 'GB_009001'
                          and g.费用编码 = a.归类编码                             
                          and g.隶属编码 = x.编码),
                       '') as 费用名称,
                   (case
                     when num_应收总额 < 0 then
                      -a.总金额
                     else
                      a.总金额
                   end) as 总金额
              from 住院管理_出院病人处方 a, 临时表_病历号 c
             where a.记账时间 >= dat_入院时间
               and a.记账时间 <= dat_结算时间
               and a.机构编码 = Str_传入_机构编码
               and a.住院病历号 = c.病历号
            
            union all
            select nvl((select b.隶属编码
                         from 基础项目_费用归类 b
                        where b.机构编码 = a.机构编码
                          and b.费用编码 = a.归类编码
                          and b.删除标志 = '0'
                          and b.类别 = '住院发票项目归类'),
                       a.归类编码) as 隶属编码,
                   nvl((select x.名称
                         from 基础项目_费用归类 g, 基础项目_字典明细 x
                        where g.机构编码 = a.机构编码
                          and g.删除标志 = '0'
                          and g.类别 = '住院发票项目归类'
                          and x.分类编码 = 'GB_009001'
                          and g.费用编码 = a.归类编码                         
                          and g.隶属编码 = x.编码),
                       '') as 费用名称,
                   (case
                     when num_应收总额 < 0 then
                      -a.总金额
                     else
                      a.总金额
                   end) as 总金额
              from 住院管理_在院病人处方 a, 临时表_病历号 c
             where a.记账时间 >= dat_入院时间
               and a.记账时间 <= dat_结算时间
               and a.机构编码 = Str_传入_机构编码
               and a.住院病历号 = c.病历号) g group by g.隶属编码,g.费用名称;

  if sqlcode <> 0 then
    I_返回值     := 0;
    Str_返回信息 := sqlerrm;
    return;
  end if;

  I_返回值     := 1;
  Str_返回信息 := 'OK';
  commit;
END;
/
