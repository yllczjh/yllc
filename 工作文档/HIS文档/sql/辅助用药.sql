--1.门诊辅助用药
select 
a.门诊病历号,r.姓名,decode(r.性别,'1','男','2','女','未知') as 性别,

nvl((select 类型名称 from 基础项目_挂号类型 where 机构编码=g.机构编码 and 类型编码=g.挂号类型编码),'') as 挂号类型,
nvl((select 人员姓名 from 基础项目_人员资料 where 机构编码=g.机构编码 and 人员编码=g.操作员编码),'') as 挂号操作员,

a.医嘱号,a.操作员姓名 as 开方医生,a.录入时间,a.医嘱状态,a.收费状态,
nvl((select 名称 from 基础项目_字典明细 where 分类编码='GB_009003' and 编码=c.大类编码),'') as 大类名称,
nvl((select 小类名称 from 基础项目_小类字典 where 机构编码=c.机构编码 and 大类编码=c.大类编码 and 小类编码=c.小类编码),'') as 小类名称,

case a.大类编码 when '6' then a.项目编码 else '' end as 套餐编码,
case a.大类编码 when '6' then a.项目名称 else '' end as 套餐名称,

c.项目编码,c.项目名称,c.规格,c.单位名称,c.数量,c.单价,c.总金额,
nvl((select 科室名称 from 基础项目_科室资料 where 机构编码=c.机构编码 and 科室编码=c.执行科室编码),'') as 执行科室,

nvl((select case when x.名称 not in('挂号费','检查费','治疗费','卫生材料费','中成药费','药事服务费','其它费','诊查费','化验费','手术费','西药费','中草药费','一般诊疗费','输血费') then '其它费' else x.名称 end as 名称
from 基础项目_费用归类 g,基础项目_字典明细 x
where g.机构编码=c.机构编码 and g.删除标志='0' and g.类别 = '门诊发票项目归类' and x.分类编码 = 'GB_009001' and g.费用编码=c.归类编码 and g.隶属编码=x.编码),'') as 费用名称,

c.操作员姓名 as 收费员,c.收费时间 as 收费时间,

c.发药标志 as 执行,c.发药人姓名 as 执行人,c.退药标志 as 取消执行,nvl((select 姓名 from 基础项目_人员资料 where 机构编码=c.机构编码 and 人员编码=c.退药人编码),'') as 取消执行人,c.退药时间 as 取消执行时间,

to_char(c.收费时间,'yyyy-mm-dd') as 收费日期,
nvl((select 名称 from 基础项目_字典明细 where 分类编码='GB_009000' and 编码=g.病人类型编码),'') as 病人类型,
nvl((select 科室名称 from 基础项目_科室资料 where 机构编码=c.机构编码 and 科室编码=c.开方科室编码),'') as 开方科室,g.挂号来源,
nvl((select 名称 from 基础项目_字典明细 where 分类编码='GB_009000' and 编码=g.病人类型编码),'') as 结算类型

from 门诊管理_挂号登记 g,门诊管理_门诊医嘱 a,门诊管理_门诊医嘱项目 b,基础项目_病人信息 r,门诊管理_门诊处方 c
where g.机构编码=a.机构编码 and g.挂号序号=a.挂号序号 and a.机构编码=b.机构编码 and a.门诊病历号=b.门诊病历号 and a.医嘱号=b.医嘱号 and a.项目id=b.项目id and a.机构编码=r.机构编码 and a.病人id=r.病人id
and b.计价id=c.计价id and b.机构编码=c.机构编码
and to_char(c.收费时间,'yyyymmdd') between '20190601' and '20191031'
and c.项目编码 in(
'012375','012115','012119','012180','012151',
'012120','012329','012399','012490','012124',
'012460','012159','012110','012111','012253',
'012174')


--2.住院辅助用药
select z.住院病历号,z.病人类型名称,b.姓名,decode(b.性别,'1','男','2','女','未知') as 性别,b.出生日期,
nvl((select 科室名称 from 基础项目_科室资料 where 机构编码=z.机构编码 and 科室编码=z.病人科室编码),'') as 所在科室,z.疾病名称,z.出院诊断名称,z.入科时间,
nvl((select 科室名称 from 基础项目_科室资料 where 机构编码=c.机构编码 and 科室编码=c.开方科室编码),'') as 开方科室,
nvl((select 科室名称 from 基础项目_科室资料 where 机构编码=c.机构编码 and 科室编码=c.执行科室编码),'') as 执行科室,
nvl((select 人员姓名 from 基础项目_人员资料 where 机构编码=c.机构编码 and 人员编码=c.开方医生编码),'') as 开方医生,
nvl((select 人员姓名 from 基础项目_人员资料 where 机构编码=c.机构编码 and 人员编码=c.操作员编码),'') as 收费员,
c.记账时间,
nvl((select 名称 from 基础项目_字典明细 where 分类编码='GB_009003' and 编码=c.大类编码),'') as 大类名称,
nvl((select 小类名称 from 基础项目_小类字典 where 机构编码=c.机构编码 and 大类编码=c.大类编码 and 小类编码=c.小类编码),'') as 小类名称,

c.套餐编码,
nvl((select 套餐名称 from 基础项目_套餐登记 where 机构编码=c.机构编码 and 套餐编码=c.套餐编码),'') as 套餐名称,
c.项目编码,c.项目名称,c.规格,c.批号,c.厂家名称,c.有效期,c.进价,c.批准文号,c.单位名称,c.剂型名称,c.分类名称,
c.单价,c.数量,c.总金额,

nvl((select case when x.名称 not in('挂号费','检查费','治疗费','卫生材料费','中成药费','药事服务费','其它费','诊查费','化验费','手术费','西药费','中草药费','一般诊疗费','输血费') then '其它费' else x.名称 end as 名称
from 基础项目_费用归类 g,基础项目_字典明细 x
where g.机构编码=c.机构编码 and g.删除标志='0' and g.类别 = '门诊发票项目归类' and x.分类编码 = 'GB_009001' and g.费用编码=c.归类编码 and g.隶属编码=x.编码),'') as 费用名称,
c.用法名称,c.频率名称,c.发药标志 as 处理,c.发药人姓名 as 处理人,发药时间 as 处理时间,
nvl((select 名称 from 基础项目_字典明细 t where 分类编码='GB_009050' and t.编码=c.基药分类),'') as 基药分类,医嘱类型,冲账标志,冲账方式,发生时间
from 住院管理_出院病人信息 z,住院管理_出院病人处方 c,基础项目_病人信息 b
where z.机构编码=c.机构编码 and z.住院病历号=c.住院病历号 and z.病人id=b.病人id and z.机构编码=b.机构编码 
and to_char(发生时间,'yyyymmdd') between '20191001' and '20191031'
and c.项目编码 in(
'012375','012115','012119','012180','012151',
'012120','012329','012399','012490','012124',
'012460','012159','012110','012111','012253',
'012174')